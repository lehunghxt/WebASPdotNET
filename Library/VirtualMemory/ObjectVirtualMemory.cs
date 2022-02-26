// -----------------------------------------------------------------------
// <copyright file="ObjectVirtualMemory.cs" company="Home">
// Co., Ltd
// </copyright>
// -----------------------------------------------------------------------

namespace Library.VirtualMemory
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// The object virtual memory.
    /// </summary>
    [Serializable]
    internal class ObjectVirtualMemory
    {
        /// <summary>
        /// The delegate on finalizes.
        /// </summary>
        [NonSerialized]
        private Action<ObjectVirtualMemory, long> delegateOnFinalizes;

        /// <summary>
        /// The data object.
        /// </summary>
        private object dataObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectVirtualMemory"/> class.
        /// </summary>
        public ObjectVirtualMemory()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectVirtualMemory"/> class.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public ObjectVirtualMemory(object value)
            : this()
        {
            this.DataObject = value;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ObjectVirtualMemory"/> class. 
        /// </summary>
        ~ObjectVirtualMemory()
        {
            if (this.DelegateOnFinalizes != null)
            {
                this.DelegateOnFinalizes(this, this.Handle);
            }
        }

        /// <summary>
        /// Gets or sets the handle.
        /// </summary>
        public long Handle { get; set; }

        /// <summary>
        /// Gets or sets the delegate on finalizes.
        /// </summary>
        public Action<ObjectVirtualMemory, long> DelegateOnFinalizes
        {
            get { return this.delegateOnFinalizes; }
            set { this.delegateOnFinalizes = value; }
        }

        /// <summary>
        /// Gets or sets the data object.
        /// </summary>
        public object DataObject
        {
            get { return this.dataObject; }
            set { this.dataObject = value; }
        }

        /// <summary>
        /// The serialize object.
        /// </summary>
        /// <returns>
        /// The byte[].
        /// </returns>
        public byte[] SerializeObject()
        {
            if (this.DataObject == null)
            {
                return new byte[0];
            }

            using (var ms = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(ms, this.DataObject);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// The deserialize object.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public void DeserializeObject(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                return;
            }

            using (var ms = new MemoryStream(data))
            {
                var binaryFormatter = new BinaryFormatter();
                this.DataObject = binaryFormatter.Deserialize(ms);
            }
        }
    }
}
