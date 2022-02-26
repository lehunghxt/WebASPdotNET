// -----------------------------------------------------------------------
// <copyright file="ListVirtualMemory.cs" company="Home">
// Co., Ltd
// </copyright>
// -----------------------------------------------------------------------

namespace Library.VirtualMemory
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.MemoryMappedFiles;
    using System.Threading;

    /// <summary>
    /// The list virtual memory.
    /// </summary>
    /// <typeparam name="T">
    /// T is inherit from <see cref="IObjectVirtualMemory"/>
    /// </typeparam>
    public class ListVirtualMemory<T> : IList<T> where T : IObjectVirtualMemory
    {
        /// <summary>
        /// The file stream.
        /// </summary>
        private readonly FileStream fileStream;

        /// <summary>
        /// The list element position.
        /// </summary>
        private readonly List<long> listElementPosition;

        /// <summary>
        /// The queue element position.
        /// </summary>
        private readonly Queue<long> queueElementPosition;

        /// <summary>
        /// The cache.
        /// </summary>
        private readonly Dictionary<long, WeakReference> cache;

        /// <summary>
        /// The memory mapped file.
        /// </summary>
        private MemoryMappedFile memoryMappedFile;

        /// <summary>
        /// The accessor.
        /// </summary>
        private UnmanagedMemoryAccessor accessor;

        /// <summary>
        /// The position end of file.
        /// </summary>
        private long fileSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListVirtualMemory{T}"/> class.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        public ListVirtualMemory(string fileName)
        {
            this.fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            this.FileSize = 1024 * 1024;
            this.memoryMappedFile = MemoryMappedFile.CreateFromFile(
                this.fileStream,
                "test" + Guid.NewGuid().ToString(),
                0,
                MemoryMappedFileAccess.ReadWrite,
                null,
                HandleInheritability.Inheritable,
                true);
            this.accessor = this.memoryMappedFile.CreateViewAccessor(0, 0);
            this.SyncRoot = new object();
            this.listElementPosition = new List<long>();
            this.queueElementPosition = new Queue<long>();
            this.cache = new Dictionary<long, WeakReference>();
            this.SetPointerWrite(0);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ListVirtualMemory{T}"/> class. 
        /// </summary>
        ~ListVirtualMemory()
        {
            try
            {
                this.accessor.Dispose();
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
            // ReSharper restore EmptyGeneralCatchClause
            {
            }

            try
            {
                this.memoryMappedFile.Dispose();
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
            // ReSharper restore EmptyGeneralCatchClause
            {
            }

            try
            {
                this.fileStream.Dispose();
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
            // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get
            {
                lock (this.SyncRoot)
                {
                    return this.listElementPosition.Count;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether is synchronized.
        /// </summary>
        public bool IsSynchronized
        {
            get
            {
                var isNotLocked = false;
                try
                {
                    isNotLocked = Monitor.TryEnter(this.SyncRoot);
                }
                finally
                {
                    if (isNotLocked)
                    {
                        Monitor.Exit(this.SyncRoot);
                    }
                }

                return isNotLocked;
            }
        }

        /// <summary>
        /// Gets the sync root.
        /// </summary>
        public object SyncRoot { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ListVirtualMemory{T}"/> has a fixed size
        /// </summary>
        public bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ListVirtualMemory{T}"/> is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets or sets the file size.
        /// </summary>
        private long FileSize
        {
            get
            {
                return this.fileSize;
            }

            set
            {
                this.fileSize = value;
                this.fileStream.SetLength(value);
            }
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">
        /// The zero-based index of the element to get or set.
        /// </param>
        /// <returns>
        /// The element at the specified index.
        /// </returns>
        public T this[int index]
        {
            get
            {
                lock (this.SyncRoot)
                {
                    ObjectVirtualMemory objectManager;
                    var pointerWrite = this.listElementPosition[index];
                    if (this.cache.ContainsKey(pointerWrite))
                    {
                        objectManager = (ObjectVirtualMemory)this.cache[pointerWrite].Target;
                        return (T)objectManager.DataObject;
                    }

                    objectManager = new ObjectVirtualMemory();
                    var data = this.ReadFromPosition(pointerWrite);
                    objectManager.DeserializeObject(data);
                    ((T)objectManager.DataObject).AssignObjectManager(objectManager);
                    if (!this.cache.ContainsKey(pointerWrite))
                    {
                        this.cache.Add(pointerWrite, new WeakReference(objectManager, true));
                    }
                    else
                    {
                        this.cache[pointerWrite] = new WeakReference(objectManager, true);
                    }

                    objectManager.Handle = pointerWrite;
                    objectManager.DelegateOnFinalizes = this.OnFinalizesObject;
                    return (T)objectManager.DataObject;
                }
            }

            set
            {
                lock (this.SyncRoot)
                {
                    ObjectVirtualMemory objectManager;
                    var pointerWrite = this.listElementPosition[index];
                    if (!this.cache.ContainsKey(pointerWrite))
                    {
                        objectManager = new ObjectVirtualMemory(value);
                        value.AssignObjectManager(objectManager);
                        var data = objectManager.SerializeObject();
                        this.WriteFromPosition(pointerWrite, data, 0);
                        this.cache.Add(pointerWrite, new WeakReference(objectManager, true));
                    }
                    else
                    {
                        objectManager = (ObjectVirtualMemory)this.cache[pointerWrite].Target;
                        if (objectManager.DataObject != (object)value)
                        {
                            objectManager.DataObject = value;
                            value.AssignObjectManager(objectManager);
                            var data = objectManager.SerializeObject();
                            this.WriteFromPosition(pointerWrite, data, 0);
                        }
                    }

                    objectManager.Handle = pointerWrite;
                    objectManager.DelegateOnFinalizes = this.OnFinalizesObject;
                }
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An System.Collections.IEnumerator object that can be used to iterate through
        /// the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new ListVirtualMemoryEnumerator(this);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An System.Collections.IEnumerator object that can be used to iterate through
        /// the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Adds an item to the <see cref="ListVirtualMemory{T}"/>
        /// </summary>
        /// <param name="value">
        /// The object to add to the <see cref="ListVirtualMemory{T}"/>
        /// </param>
        public void Add(T value)
        {
            this.InsertCore(-1, value, true);
        }

        /// <summary>
        /// Removes all items from the <see cref="ListVirtualMemory{T}"/>.
        /// </summary>
        public void Clear()
        {
            lock (this.SyncRoot)
            {
                var oldPointer = this.GetPointerWrite();
                while (this.listElementPosition.Count > 0)
                {
                    this.SetPointerWrite(this.listElementPosition[0]);
                    this.listElementPosition.RemoveAt(0);
                }
                
                this.SetPointerWrite(oldPointer);
            }
        }

        /// <summary>
        /// Determines whether the <see cref="ListVirtualMemory{T}"/> contains a specific value.
        /// </summary>
        /// <param name="value">
        /// The object to locate in the <see cref="ListVirtualMemory{T}"/>.
        /// </param>
        /// <returns>
        /// true if the System.Object is found in the <see cref="ListVirtualMemory{T}"/>; otherwise, false.
        /// </returns>
        public bool Contains(T value)
        {
            lock (this.SyncRoot)
            {
                var index = this.IndexOf(value);
                return index != -1;
            }
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="ListVirtualMemory{T}"/>.
        /// </summary>
        /// <param name="value">
        /// The object to locate in the <see cref="ListVirtualMemory{T}"/>.
        /// </param>
        /// <returns>
        /// The index of value if found in the list; otherwise, -1.
        /// </returns>
        public int IndexOf(T value)
        {
            lock (this.SyncRoot)
            {
                for (var i = 0; i < this.Count; i++)
                {
                    if ((object)this[i] == (object)value)
                    {
                        return i;
                    }
                }

                return -1;
            }
        }

        /// <summary>
        /// Inserts an item to the <see cref="ListVirtualMemory{T}"/> at the specified index.
        /// </summary>
        /// <param name="index">
        /// The zero-based index at which value should be inserted.
        /// </param>
        /// <param name="value">
        /// The object to insert into the <see cref="ListVirtualMemory{T}"/>.
        /// </param>
        public void Insert(int index, T value)
        {
            this.InsertCore(index, value, false);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="ListVirtualMemory{T}"/>.
        /// </summary>
        /// <param name="value">
        /// The object to remove from the <see cref="ListVirtualMemory{T}"/>.
        /// </param>
        /// <returns>
        /// true if item was successfully removed from the <see cref="ListVirtualMemory{T}"/>;
        /// otherwise, false. This method also returns false if item is not found in
        /// the original <see cref="ListVirtualMemory{T}"/>.
        /// </returns>
        public bool Remove(T value)
        {
            lock (this.SyncRoot)
            {
                var index = this.IndexOf(value);
                if (index >= 0)
                {
                    this.RemoveAt(index);
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Removes the <see cref="ListVirtualMemory{T}"/> item at the specified index.
        /// </summary>
        /// <param name="index">
        /// The zero-based index of the item to remove.
        /// </param>
        public void RemoveAt(int index)
        {
            lock (this.SyncRoot)
            {
                var pointerWrite = this.listElementPosition[index];
                this.SetPointerWrite(pointerWrite);
                this.listElementPosition.RemoveAt(index);
                if (this.cache.ContainsKey(pointerWrite))
                {
                    this.cache.Remove(pointerWrite);
                }
            }
        }

        /// <summary>
        /// Copies the elements of the System.Collections.ICollection to an System.Array,
        /// starting at a particular System.Array index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional System.Array that is the destination of the elements
        /// copied from System.Collections.ICollection. The System.Array must have zero-based
        /// indexing.
        /// </param>
        /// <param name="index">
        /// The zero-based index in array at which copying begins.
        /// </param>
        /// <exception cref="ArgumentException">
        /// array is multidimensional.-or- The number of elements in the source System.Collections.ICollection
        /// is greater than the available space from index to the end of the destination
        /// array.-or-The type of the source System.Collections.ICollection cannot be
        /// cast automatically to the type of the destination array.
        /// </exception>
        public void CopyTo(T[] array, int index)
        {
            lock (this.SyncRoot)
            {
                if (this.Count > array.Length - index)
                {
                    throw new ArgumentException();
                }

                for (var i = 0; i < this.Count; i++)
                {
                    array.SetValue(this[i], i + index);
                }
            }
        }

        /// <summary>
        /// The insert core.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="isAppend">
        /// The is Append.
        /// </param>
        private void InsertCore(int index, T value, bool isAppend)
        {
            lock (this.SyncRoot)
            {
                var objectManager = new ObjectVirtualMemory(value);
                value.AssignObjectManager(objectManager);
                var pointerWrite = this.GetPointerWrite();
                var ind = isAppend ? this.listElementPosition.Count : index;
                this.listElementPosition.Insert(ind, pointerWrite);
                var data = objectManager.SerializeObject();
                this.WriteFromPosition(pointerWrite, data, 0);
                if (!this.cache.ContainsKey(pointerWrite))
                {
                    this.cache.Add(pointerWrite, new WeakReference(objectManager, true));
                }
                else
                {
                    this.cache[pointerWrite] = new WeakReference(objectManager, true);
                }

                objectManager.Handle = pointerWrite;
                objectManager.DelegateOnFinalizes = this.OnFinalizesObject;
            }
        }

        /// <summary>
        /// The get pointer write.
        /// </summary>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        private long GetPointerWrite()
        {
            return this.queueElementPosition.Dequeue();
        }

        /// <summary>
        /// The set pointer write.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        private void SetPointerWrite(long value)
        {
            this.queueElementPosition.Enqueue(value);
        }

        /// <summary>
        /// The on finalizes object.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="handle">
        /// The handle.
        /// </param>
        private void OnFinalizesObject(ObjectVirtualMemory sender, long handle)
        {
            var data = sender.SerializeObject();
            lock (this.SyncRoot)
            {
                this.WriteFromPosition(handle, data, 0);
                if (this.cache.ContainsKey(handle))
                {
                    this.cache.Remove(handle);
                }
            }
        }

        /// <summary>
        /// The read from position.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        /// <returns>
        /// The byte[].
        /// </returns>
        private byte[] ReadFromPosition(long position)
        {
            if (position + 20 > this.FileSize)
            {
                throw new Exception("Position invalid with File size!");
            }

            var lengthOffset = this.accessor.ReadInt32(position) - 16;
            var buffer = new byte[lengthOffset];
            var dataRead = this.accessor.ReadArray(position + 4, buffer, 0, lengthOffset);
            var endPosition = this.accessor.ReadInt64(position + 4 + lengthOffset);
            var nextPosition = this.accessor.ReadInt64(position + 12 + lengthOffset);
            if (endPosition > position + 4)
            {
                var dataLength = endPosition - position - 4;
                var resultDataRead = new byte[dataLength];
                Array.Copy(buffer, 0, resultDataRead, 0, dataLength);
                return resultDataRead;
            }
            
            var buffer2 = this.ReadFromPosition(nextPosition);
            var result = new byte[dataRead + buffer2.Length];
            Array.Copy(buffer, 0, result, 0, dataRead);
            Array.Copy(buffer2, 0, result, dataRead, buffer2.Length);
            return result;
        }

        /// <summary>
        /// The write from position.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <param name="offset">
        /// The offset.
        /// </param>
        private void WriteFromPosition(long position, byte[] data, int offset)
        {
            this.SyncFileSize(position + 4);

            var lengthOffset = this.accessor.ReadInt32(position) - 16;
            if (lengthOffset > 0)
            {
                var count = Math.Min(data.Length - offset, lengthOffset);
                this.SyncFileSize(position + 4 + count);
                this.accessor.WriteArray(position + 4, data, offset, count); // write data
                this.SyncFileSize(position + 12 + count);
                if (data.Length - offset <= lengthOffset)
                {
                    this.accessor.Write(position + 4 + lengthOffset, position + 4 + count); // write endPosition
                }
                else
                {
                    this.accessor.Write(position + 4 + count, (long)-1); // write endPosition
                    this.SyncFileSize(position + 20 + count);
                    var nextPosition = this.accessor.ReadInt64(position + 12 + lengthOffset);
                    if (nextPosition > 0)
                    {
                        this.WriteFromPosition(nextPosition, data, count);
                    }
                    else
                    {
                        var pointerWrite = this.GetPointerWrite();
                        this.accessor.Write(position + 12 + lengthOffset, pointerWrite); // write nextPosition
                        var dataRemainLength = data.Length - count;
                        if (dataRemainLength < count)
                        {
                            this.SyncFileSize(pointerWrite + 20 + count);
                            this.accessor.Write(pointerWrite, count + 16); // write lengthOffset to memory
                            this.accessor.Write(pointerWrite + 12 + count, (long)-1); // write nextPosition
                            this.SetPointerWrite(pointerWrite + 20 + count);
                        }

                        this.WriteFromPosition(pointerWrite, data, count);
                    }
                }
            }
            else
            {
                var count = data.Length - offset;
                this.SyncFileSize(position + 20 + count);
                this.accessor.Write(position, count + 16); // write lengthOffset to memory
                this.accessor.WriteArray(position + 4, data, offset, count); // write data
                this.accessor.Write(position + 4 + count, position + 4 + count); // write endPosition
                this.accessor.Write(position + 12 + count, (long)-1); // write nextPosition
                this.SetPointerWrite(position + 20 + count);
            }
        }

        /// <summary>
        /// The sync file size.
        /// </summary>
        /// <param name="size">
        /// The size.
        /// </param>
        private void SyncFileSize(long size)
        {
            while (size > this.FileSize)
            {
                this.FileSize = this.FileSize * 2;
            }

            if (this.accessor.Capacity < this.FileSize)
            {
                this.accessor.Dispose();
                this.memoryMappedFile.Dispose();
                this.memoryMappedFile = MemoryMappedFile.CreateFromFile(
                this.fileStream,
                "test",
                0,
                MemoryMappedFileAccess.ReadWrite,
                null,
                HandleInheritability.Inheritable,
                true);
                this.accessor = this.memoryMappedFile.CreateViewAccessor(0, 0);
            }
        }

        /// <summary>
        /// The list virtual memory enumerator.
        /// </summary>
        private class ListVirtualMemoryEnumerator : IEnumerator<T>
        {
            /// <summary>
            /// The owner.
            /// </summary>
            private readonly ListVirtualMemory<T> owner;

            /// <summary>
            /// The position.
            /// </summary>
            private int position;

            /// <summary>
            /// Initializes a new instance of the <see cref="ListVirtualMemoryEnumerator"/> class.
            /// </summary>
            /// <param name="owner">
            /// The owner.
            /// </param>
            public ListVirtualMemoryEnumerator(ListVirtualMemory<T> owner)
            {
                this.owner = owner;
                this.position = -1;
            }

            /// <summary>
            /// Gets the current element in the collection.
            /// </summary>
            /// <exception cref="Exception">
            /// Cursor point to null!
            /// </exception>
            public T Current
            {
                get
                {
                    if (this.position < 0)
                    {
                        throw new Exception("Cursor point to null!");
                    }

                    return this.owner[this.position];
                }
            }

            /// <summary>
            /// Gets the current element in the collection.
            /// </summary>
            /// <exception cref="Exception">
            /// Cursor point to null!
            /// </exception>
            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or
            /// resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            /// true if the enumerator was successfully advanced to the next element; false
            /// if the enumerator has passed the end of the collection.
            /// </returns>
            public bool MoveNext()
            {
                if (this.position < this.owner.Count)
                {
                    this.position++;
                }

                return this.position < this.owner.Count;
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element
            /// in the collection.
            /// </summary>
            public void Reset()
            {
                this.position = -1;
            }
        }
    }
}
