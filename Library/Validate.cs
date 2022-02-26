using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Library
{
    public enum Regular
    {
        required = 1,
        phone = 2,
        number = 3,
        alphanum = 4,
        alpha = 5,
        email = 6,
        url = 7,
        isDateDDMMYYYY = 8,
        isDateDDMMMYY = 9,
        isDateDD_MM_YYYY = 10
    }

    public class Validate 
    {
        public bool checkValue(string value, string methodName)
        {
            Type type = this.GetType();
            MethodInfo method = type.GetMethod(methodName);
            if (method != null)
            {
                //object[] args = new object[] { value };

                bool isTrue = (bool)method.Invoke(this, new object[] { value });
                return isTrue;
            } 
            return false;
        }

        public static bool isValid(string value, string regular)
        {
            return Regex.IsMatch(value, regular);
        }

        public static bool required(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            else return true;
        }

        public static bool phone(string value)
        {
            string regular = @"^\+{0,1}[0-9 \(\)\.\-]+$";
            return isValid(value, regular);
        }
        public static bool number(string value)
        {
            string regular = @"^(\+|-)?[0-9][0-9]*(\.[0-9]*)?$";
            return isValid(value, regular);
        }
        public static bool alphanum(string value)
        {
            string regular = @"[^a-zA-Z0-9]";
            return isValid(value, regular);
        }
        public static bool alpha(string value)
        {
            string regular = @"[^a-zA-Z]";
            return isValid(value, regular);
        }
        public static bool email(string value)
        {
            string regular = @"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})";
            return isValid(value, regular);
        }
        public static bool url(string value)
        {
            string regular = @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?";
            return isValid(value, regular);
        }
        public static bool isDateDDMMYYYY(string value)
        {
            string regular = @"((0[1-9]|[12][0-9]|3[01]))[/|-](0[1-9]|1[0-2])[/|-]((?:\d{4}|\d{2}))";
            return isValid(value, regular);
        }
        public static bool isDateDDMMMYY(string value)
        {
            string regular = @"((0[1-9]|[12][0-9]|3[01]))[/|-](0[1-9]|1[0-2])[/|-]((?:\d{2}))";
            return isValid(value, regular);
        }
        public static bool isDateDD_MM_YYYY(string value)
        {
            string regular = @"^((31(?!\ (Feb(ruary)?|Apr(il)?|June?|(Sep(?=\b|t)t?|Nov)(ember)?)))|((30|29)(?!\ Feb(ruary)?))|(29(?=\Feb(ruary)?\ (((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))|(0?[1-9])|1\d|2[0-8])\(Jan(uary)?|Feb(ruary)?|Ma(r(ch)?|y)|Apr(il)?|Ju((ly?)|(ne?))|Aug(ust)?|Oct(ober)?|(Sep(?=\b|t)t?|Nov|Dec)(ember)?)\((1[6-9]|[2-9]\d)\d{2})";
            return isValid(value, regular);
        }
    }
}
