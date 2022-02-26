using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Timers;
using System.IO.Ports;
using System.Configuration;

namespace Library.GMS
{
    public class GSMManager
    {
        public List<SMSMessage> RecivedMessages = new List<SMSMessage>();
        public List<SMSMessage> SendingMessages = new List<SMSMessage>();
        Timer timer = new Timer();
        public GSMManager()
        {
            timer.Interval = 1000;

        }
        public System.IO.Ports.SerialPort comPort = new System.IO.Ports.SerialPort();
        public bool SMSEnable = true;
        public void SMSConfig(string Port = "Com6", int BaudRate = 115200)
        {
            if (comPort.IsOpen == false)
            {
                comPort.PortName = Port;
                comPort.BaudRate = 115200;
                comPort.Parity = Parity.None;
                comPort.StopBits = StopBits.One;
                comPort.DataBits = 8;
                comPort.ReadBufferSize = 10000;
                comPort.ReadTimeout = 1000;
                comPort.WriteBufferSize = 10000;
                comPort.WriteTimeout = 10000;
                comPort.RtsEnable = true;
            }
            if (comPort.IsOpen == false)
            {
                comPort.DataReceived -= new SerialDataReceivedEventHandler(comPort_ReadIncommingSMS);
                comPort.RtsEnable = false;
                comPort.Close();
                try { comPort.Open(); }
                catch
                {
                    SMSEnable = false;
                    return;
                }
                comPort.RtsEnable = true;
                comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_ReadIncommingSMS);
            }
            comPort.DiscardInBuffer();
            comPort.DiscardOutBuffer();
            comPort.Write("AT+CMGF=1" + (char)13);
            SMSEnable = true;
            return;
        }
        public SMSMessage ReadExistingSMS(int MessageIndex)
        {
            DateTime t1 = DateTime.Now;
            Readed = "";
            comPort.Write("AT+CMGR=" + MessageIndex.ToString() + (char)13);
            SMSMessage mess = new SMSMessage();
            return null;

        }
        public enum ReadIncommingSMSType
        {
            REC_UNREAD = 0,
            REC_READ = 1,
            STO_UNSENT = 2,
            STO_SENT = 3,
            ALL = 4
        }
        public void DeleteExistingSMS(int index)
        {
            comPort.Write("AT+CMGD=" + index.ToString() + (char)13);
        }
        public void SendSMS(SMSMessage sms)
        {
            SendSMS(sms.PhoneNo, sms.Message);
        }
        public string callservice;
        public void SendSMS(string PhoneNo, string Message)
        {
            comPort.Write("AT+CSCA=\"" + callservice + "\"" + (char)13);
            comPort.Write("AT+CMGS=\"" + PhoneNo + "\"" + (char)13);
            comPort.Write(Message + (char)26);
            //comPort.Close();
        }

        public List<SMSMessage> ReadIncommingSMS(ReadIncommingSMSType type)
        {
            DateTime t1 = DateTime.Now;
            Readed = "";
            if (SMSEnable == false) return null;

            switch (type)
            {

                case ReadIncommingSMSType.STO_SENT:
                    comPort.Write("AT+CMGL=\"STO SENT\"" + (char)13);
                    break;
                case ReadIncommingSMSType.REC_UNREAD:
                    comPort.Write("AT+CMGL=\"REC UNREAD\"" + (char)13);
                    break;
                case ReadIncommingSMSType.ALL:
                    comPort.Write("AT+CMGL=\"ALL\"" + (char)13);
                    break;
                case ReadIncommingSMSType.REC_READ:
                    comPort.Write("AT+CMGL=\"REC READ\"" + (char)13);
                    break;
                case ReadIncommingSMSType.STO_UNSENT:
                    comPort.Write("AT+CMGL=\"STO UNSENT\"" + (char)13);
                    break;
            }
            List<SMSMessage> messList = new List<SMSMessage>();
            string[] re = Readed.Split(new string[] { "+CMGL:" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < re.Length; i++)
            {
                int k = 0;
                bool messErr = false;
                int l = 0;
                string Str = re[i];
                SMSMessage mess = new SMSMessage();
                string ind = Str.Substring(0, Str.IndexOf(",")).Trim();
                mess.index = int.Parse(ind);
                for (int j = 0; j < 3; j++)
                {
                    if (k == -1 || l == -1) { messErr = true; continue; }
                    k = Str.IndexOf("\"", l);
                    if (k == -1 || l == -1) { messErr = true; continue; }
                    l = Str.IndexOf("\"", k + 1);
                    if (k == -1 || l == -1) { messErr = true; continue; }
                    string inc = Str.Substring(k + 1, l - k - 1);
                    if (j == 1 && messErr == false)
                        mess.PhoneNo = inc;
                    if (j == 2 && messErr == false)
                        mess.Time = DateTime.Parse(inc);
                    l++;
                }
                if (messErr == false)
                {
                    mess.Message = Str.Substring(l + 2, Str.Length - l + 2 - 8 + 2);
                    mess.Message = mess.Message.Replace("\r\nOK", "");
                    mess.Status = SMSMessage.SMSMessageStatus.Recived;
                    messList.Add(mess);
                }
            }
            return messList;
        }
        string Readed = "";
        void comPort_ReadIncommingSMS(object sender, SerialDataReceivedEventArgs e)
        {
            System.IO.Ports.SerialPort SP = (System.IO.Ports.SerialPort)sender;
            string s = SP.ReadExisting();
            Readed += s;
        }
    }
}


        //GSMManager GSM_Manager1 = new GSMManager();

        //public void Send(string noidung, string phone)
        //{
        //    foreach (string p in phone.Split(','))
        //    {
        //        try
        //        {
        //            GSM_Manager1.callservice = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["Sim"]];//"+84900000012";
        //            GSM_Manager1.SMSConfig();
        //            GSM_Manager1.RecivedMessages.Clear();
        //            SMSMessage m = new SMSMessage(p, noidung);
        //            if (GSM_Manager1.SMSEnable == true) GSM_Manager1.SendSMS(m);
        //            System.Threading.Thread.Sleep(5000);
        //        }
        //        catch { }
        //    }
        //    GSM_Manager1.comPort.Close();
        //}