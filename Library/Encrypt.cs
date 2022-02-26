using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Library
{
    public class Encrypt
    {
        // Mã hóa Base64 với chuỗi Unicode.
        public static string StringToBase64(string src)
        {
            // Chuyển chuỗi thành mảng kiểu byte.
            byte[] b = Encoding.Unicode.GetBytes(src);
            // Trả về chuỗi được mã hóa theo Base64.
            return Convert.ToBase64String(b);
        }

        // Giải mã một chuỗi Unicode được mã hóa theo Base64.
        public static string Base64ToString(string src)
        {
            // Giải mã vào mảng kiểu byte.
            byte[] b = Convert.FromBase64String(src);
            // Trả về chuỗi Unicode.
            return Encoding.Unicode.GetString(b);
        }

        #region Mã hóa một chiều
        public static string EnCodeMD5(string text)
        {
            //Encoder enc = Encoding.Unicode.GetEncoder();
            //Byte[] uniText = new Byte[text.Length * 2];
            //enc.GetBytes(text.ToCharArray(), 0, text.Length, uniText, 0, true);

            //MD5 md5 = new MD5CryptoServiceProvider();
            //Byte[] kq = md5.ComputeHash(uniText);

            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < kq.Length; i++)
            //    sb.Append(kq[i].ToString());
        
            //return sb.ToString();
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            //originalBytes = ASCIIEncoding.Default.GetBytes(text);
            originalBytes = new UTF8Encoding().GetBytes(text);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }

        public static string SHA(string str)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            Byte[] hashBytes = UE.GetBytes(str);
            SHA1CryptoServiceProvider sha1c = new SHA1CryptoServiceProvider();
            Byte[] crypt = sha1c.ComputeHash(hashBytes);
            return BitConverter.ToString(crypt);
        }

        public static string SHA512(string str)
        {
            SHA512 sha512 = new System.Security.Cryptography.SHA512Managed();
            byte[] sha512Bytes = System.Text.Encoding.Default.GetBytes(str);
            byte[] cryString = sha512.ComputeHash(sha512Bytes);

            StringBuilder s = new StringBuilder();
            foreach (byte b in cryString)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }

        public static string SHA256(string text)
        {
            SHA256Managed crypt = new SHA256Managed();
            StringBuilder hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(text));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        public static string HMACSHA256(string text, string key)
        {
            StringBuilder hash = new StringBuilder();
            byte[] keyByte = Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = Encoding.UTF8.GetBytes(text);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                for (int i = 0; i < hashmessage.Length; i++)
                {
                    byte bit = hashmessage[i];
                    hash.Append(bit.ToString("x2"));
                }
            }
            return hash.ToString();
        }

        public static string CreateLicense(string domain, DateTime experDate, string token, int key)
        {
            var str = domain + experDate.ToString("dd/MM/yyyy");
            var tk = str.EnCode(key);
            if (tk == token)
            {
                return EnCode(str).EnCodeMD5();
            }
            return "TO-CH-AC-HU-NG-MA-Y";
        }
        #endregion

        private static string GetMatrix(string code)
        {
            if (code == null) return null;

            char[] chArray = code.ToCharArray();
            int n, l = chArray.Length;

            for (n = 0; n < l; n++)
                if (n * n > l) break;

            StringBuilder En = new StringBuilder();

            char[,] matrix = new char[n, n];
            int k = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    try { matrix[i, j] = chArray[k++]; }
                    catch { matrix[i, j] = ' '; }
                }

            for (int j = 0; j < n; j++)
                for (int i = 0; i < n; i++)
                {
                    En.Append(matrix[i, j]);
                }
            return En.ToString().Trim();
        }

        #region Private-key encryption
        public static string EnCode(string st, int key = 0)
        {
            if (st == null) return null;

            st = GetMatrix(st);

            char[] chArray = new char[0x7d0];
            chArray = st.ToCharArray();
            StringBuilder En = new StringBuilder();

            for (int i = 0; i < chArray.Length; i++)
            {
                En.Append(GetChar((char)chArray.GetValue(i), key));
            }
            st = En.ToString();

            return st;
        }
        public static string DeEnCode(string st, int key = 0)
        {
            if (st == null) return null;

            char[] chArray = new char[0x7d0];
            chArray = st.ToCharArray();

            StringBuilder De = new StringBuilder();
            for (int i = 0; i < chArray.Length; i++)
            {
                De.Append(ReGetChar((char)chArray.GetValue(i), key));
            }
            st = De.ToString();
            st = GetMatrix(st);

            return st;
        }

        //giải thuật mã hóa đối xứng Rijndael
        //Rijndael sử dụng kĩ thuật chaining để bảo vệ khóa mã. Kĩ thuật này yêu cầu cung cấp một vector khởi tạo. Vector đó được thể hiện bởi biến rgbIV. Key là khóa để mã hóa dữ liệu. Dữ liệu sau khi mã hóa lại được trả về ở dạng chuỗi (đương nhiên là không thể đọc được nữa!)
        public static string EncodeRijndael(string source)
        {
            byte[] rgbIV = Encoding.ASCII.GetBytes("initialization vector");
            byte[] key = Encoding.ASCII.GetBytes("key to encrypt/decrypt data");
            return EncodeRijndael(source, key, rgbIV);
        }
        public static string EncodeRijndael(string source, byte[] key, byte[] rgbIV)
        {
            byte[] binarySource = Encoding.UTF8.GetBytes(source);
            System.Security.Cryptography.SymmetricAlgorithm rijn = System.Security.Cryptography.SymmetricAlgorithm.Create();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, rijn.CreateEncryptor(key, rgbIV), CryptoStreamMode.Write);
            cs.Write(binarySource, 0, binarySource.Length);
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }

        public static string DecodeRijndael(string source)
        {
            byte[] rgbIV = Encoding.ASCII.GetBytes("initialization vector");
            byte[] key = Encoding.ASCII.GetBytes("key to encrypt/decrypt data");
            return DecodeRijndael(source, key, rgbIV);
        }
        public static string DecodeRijndael(string source, byte[] key, byte[] rgbIV)
        {
            byte[] binarySource = Convert.FromBase64String(source);
            MemoryStream ms = new MemoryStream();
            System.Security.Cryptography.SymmetricAlgorithm rijn = System.Security.Cryptography.SymmetricAlgorithm.Create();
            
            CryptoStream cs = new CryptoStream(ms, rijn.CreateDecryptor(key, rgbIV),
            CryptoStreamMode.Write);
            cs.Write(binarySource, 0, binarySource.Length);
            cs.Close();
            return Encoding.UTF8.GetString(ms.ToArray());
        }
        #endregion

        private static char GetChar(char cc, int t)
        {
            cc = (char)(cc + '\x0001');
            int num = t + 20;
            if (cc > num)
            {
                cc = (char)(cc + '\x0002');
            }
            num = t + 50;
            if (cc > num)
            {
                cc = (char)(cc + '\x0001');
            }
            num = t + 80;
            if (cc > num)
            {
                cc = (char)(cc + '\x0002');
            }
            num = t + 110;
            if (cc > num)
            {
                cc = (char)(cc + '\x0002');
            }
            return cc;
        }
        private static char ReGetChar(char cc, int t)
        {
            int num = t + 110;
            if (cc > num)
            {
                cc = (char)(cc - '\x0002');
            }
            num = t + 80;
            if (cc > num)
            {
                cc = (char)(cc - '\x0002');
            }
            num = t + 50;
            if (cc > num)
            {
                cc = (char)(cc - '\x0001');
            }
            num = t + 20;
            if (cc > num)
            {
                cc = (char)(cc - '\x0002');
            }
            cc = (char)(cc - '\x0001');
            return cc;
        }        
    }
}
