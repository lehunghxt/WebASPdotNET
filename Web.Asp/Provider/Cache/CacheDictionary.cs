// -----------------------------------------------------------------------
// <copyright file="CacheDictionary.cs" company="Zicki.vn">
// Zicki.vn
// </copyright>
// -----------------------------------------------------------------------

namespace Web.Asp.Provider.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Library.VirtualMemory;

    /// <summary>
    /// The cache dictionary.
    /// </summary>
    /// <typeparam name="TKey">
    /// </typeparam>
    /// <typeparam name="TValue">
    /// </typeparam>
    [Serializable]
    public class CacheDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IObjectVirtualMemory
    {
        public CacheDictionary()
        {
        }

        protected CacheDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Gets or sets the object manager.
        /// </summary>
        public object ObjectManager { get; set; }

        /// <summary>
        /// The assign object manager.
        /// </summary>
        /// <param name="objectManager">
        /// The object manager.
        /// </param>
        public void AssignObjectManager(object objectManager)
        {
            this.ObjectManager = objectManager;
        }
    }
}
