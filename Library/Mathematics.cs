using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class Mathematics
    {
        public string ConverterDecToBin(int myValue)
        {
            return string.Concat(ConverterDecToBin(myValue).Reverse());
        }
        private int ConverterBinToDec(string myValue)
        {
            return Converter(myValue);
        }

        private IEnumerable<int> Converter(int myValue)
        {
            while (myValue != 0)
            {
                if (myValue % 2 == 0)
                    yield return 0;
                else
                    yield return 1;
                myValue /= 2;
            }
        }
        private int Converter(string myValue)
        {
            double result = 0;
            int length = myValue.Length - 1;

            foreach (char item in myValue.ToCharArray())
            {
                result += int.Parse(item.ToString()) * System.Math.Pow(2, length);
                if (length == 0)
                    break;
                length--;
            }
            return (int)result;

        }
    }
}
