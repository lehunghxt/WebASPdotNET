using System;
using System.Text;
using System.Net.NetworkInformation;

namespace Library
{
    public partial class Address
    {
        public static string MAC
        {
            get { return getMAC(); }
        }

        private static string getMAC()
        {
            StringBuilder macAdd = new StringBuilder();
            int j = 0;
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            PhysicalAddress address = nics[j].GetPhysicalAddress();
            byte[] bytes = address.GetAddressBytes();

            for (int i = 0; i < bytes.Length; i++)
            {
                macAdd.AppendFormat("{0}", bytes[i].ToString("X2"));

                if (i != bytes.Length - 1)
                {
                    macAdd.Append("-");
                }
            }

            Console.ReadLine();
            return macAdd.ToString();
        }
    }
}
