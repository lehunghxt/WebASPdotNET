using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public class GenerateRandomCode
    {
        public static string RandomString(int size, bool lowerCase = false)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public static string RandomNumber(int size)
        {
            Random random = new Random();
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < size; i++)
                s.Append(random.Next(10).ToString());
            return s.ToString();
        }

        public static string RandomCode(int size)
        {
            string[] chars = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "P", "Q", "R", "S",
                        "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            Random rnd = new Random();
            StringBuilder random = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                random.Append(chars[rnd.Next(0, 33)]);
            }
            return random.ToString();
        }
    }
}
