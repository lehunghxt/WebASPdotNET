// -----------------------------------------------------------------------
// <copyright file="IObjectVirtualMemory.cs" company="Home">
// Co., Ltd
// </copyright>
// -----------------------------------------------------------------------

namespace Library.VirtualMemory
{
    /// <summary>
    /// The ObjectVirtualMemory interface.
    /// </summary>
    public interface IObjectVirtualMemory
    {
        /// <summary>
        /// Gets or sets the object manager.
        /// </summary>
        object ObjectManager { get; set; }

        /// <summary>
        /// The assign object manager.
        /// </summary>
        /// <param name="objectManager">
        /// The object manager.
        /// </param>
        void AssignObjectManager(object objectManager);
    }
}
