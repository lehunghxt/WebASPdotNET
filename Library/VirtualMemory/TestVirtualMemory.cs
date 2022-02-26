// -----------------------------------------------------------------------
// <copyright file="TestVirtualMemory.cs" company="Test">
// Co., Ltd
// </copyright>
// -----------------------------------------------------------------------

namespace Library.VirtualMemory
{
    using System;

    /// <summary>
    /// The program.
    /// </summary>
    public class TestVirtualMemory
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            var lst = new ListVirtualMemory<MyClass>("C:\\test.txt");
            for (var i = 0; i < 100000; i++)
            {
                var myclass = new MyClass();
                lst.Add(myclass);
                myclass.Name = "test length" + i.ToString();
            }
        }
    }

    /// <summary>
    /// The my class.
    /// </summary>
    [Serializable]
    public class MyClass : IObjectVirtualMemory
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

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
