using System;
using System.IO;

namespace Library.Web
{
    public class MHTextFile
    {
        // Methods
        private bool CheckOverYear()
        {
            DateTime time = new DateTime();
            return (time.Year > 0x7dd);
        }

        public string DeEnCode(string st)
        {
            if (this.CheckOverYear())
            {
                return "";
            }
            st = st.Replace("(:vnns)", "'");
            st = st.Replace("(:sec)", "/");
            char[] chArray = new char[0x7d0];
            chArray = st.ToCharArray();
            st = "";
            for (int i = 0; i < chArray.Length; i++)
            {
                st = st + this.ReGetChar((char)chArray.GetValue(i));
            }
            return st;
        }

        public string DeEnCode(string st, int code)
        {
            if (this.CheckOverYear())
            {
                return "";
            }
            char[] chArray = new char[0x7d0];
            chArray = st.ToCharArray();
            st = "";
            for (int i = 0; i < chArray.Length; i++)
            {
                st = st + this.ReGetChar((char)chArray.GetValue(i), code);
            }
            return st;
        }

        public string DeEnCodeFileName(string st)
        {
            if (this.CheckOverYear())
            {
                return "";
            }
            char[] chArray = new char[0x7d0];
            chArray = st.ToCharArray();
            st = "";
            for (int i = 0; i < chArray.Length; i++)
            {
                st = st + this.ReGetCharFileName((char)chArray.GetValue(i));
            }
            return st;
        }

        public string EnCode(string st)
        {
            char[] chArray = new char[0x7d0];
            chArray = st.ToCharArray();
            st = "";
            for (int i = 0; i < chArray.Length; i++)
            {
                st = st + this.GetChar((char)chArray.GetValue(i));
            }
            return st;
        }

        public string EnCode(string st, int code)
        {
            char[] chArray = new char[0x7d0];
            chArray = st.ToCharArray();
            st = "";
            for (int i = 0; i < chArray.Length; i++)
            {
                st = st + this.GetChar((char)chArray.GetValue(i), code);
            }
            return st;
        }

        public string EnCodeFileName(string st)
        {
            char[] chArray = new char[0x7d0];
            chArray = st.ToCharArray();
            st = "";
            for (int i = 0; i < chArray.Length; i++)
            {
                st = st + this.GetCharFileName((char)chArray.GetValue(i));
            }
            return st.Replace("/", "lb2").Replace(@"\", "lb3").Replace("?", "lb4").Replace(".", "lb5").Replace("*", "lb6").Replace(":", "lb7").Replace("|", "lb8").Replace("<", "lb9").Replace(">", "lb10").Replace('"', 'l');
        }

        private char GetChar(char cc)
        {
            cc = (char)(cc + '\x0001');
            if (cc > '7')
            {
                cc = (char)(cc + '\x0002');
            }
            if (cc > 'd')
            {
                cc = (char)(cc + '\x0001');
            }
            if (cc > 'n')
            {
                cc = (char)(cc + '\x0002');
            }
            return cc;
        }

        private char GetChar(char cc, int t)
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

        private char GetCharFileName(char cc)
        {
            cc = (char)(cc + '\x0001');
            if (cc > '7')
            {
                cc = (char)(cc + '\x0002');
            }
            if (cc > 'd')
            {
                cc = (char)(cc + '\x0001');
            }
            if ((cc > 'n') && (cc < 'x'))
            {
                cc = (char)(cc + '\x0002');
            }
            return cc;
        }

        public string ReadFile(string FileName)
        {
            if (this.CheckOverYear())
            {
                return "";
            }
            string str = "";
            try
            {
                FileStream stream = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                str = new StreamReader(stream).ReadLine();
                char[] chArray = new char[200];
                chArray = str.ToCharArray();
                str = "";
                for (int i = 0; i < chArray.Length; i++)
                {
                    str = str + this.ReGetChar((char)chArray.GetValue(i));
                }
                stream.Close();
            }
            catch
            {
                str = "";
            }
            return str;
        }

        private char ReGetChar(char cc)
        {
            if (cc > 'n')
            {
                cc = (char)(cc - '\x0002');
            }
            if (cc > 'd')
            {
                cc = (char)(cc - '\x0001');
            }
            if (cc > '7')
            {
                cc = (char)(cc - '\x0002');
            }
            cc = (char)(cc - '\x0001');
            return cc;
        }

        private char ReGetChar(char cc, int t)
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

        private char ReGetCharFileName(char cc)
        {
            if (cc > 'n')
            {
                cc = (char)(cc - '\x0002');
            }
            if (cc > 'd')
            {
                cc = (char)(cc - '\x0001');
            }
            if (cc > '7')
            {
                cc = (char)(cc - '\x0002');
            }
            cc = (char)(cc - '\x0001');
            return cc;
        }

        public void WriteFile(string st, string FileName)
        {
            FileStream stream = new FileStream(FileName, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            char[] chArray = new char[200];
            chArray = st.ToCharArray();
            st = "";
            for (int i = 0; i < chArray.Length; i++)
            {
                st = st + this.GetChar((char)chArray.GetValue(i));
            }
            writer.WriteLine(st);
            writer.Close();
            stream.Close();
        }
    }
}
