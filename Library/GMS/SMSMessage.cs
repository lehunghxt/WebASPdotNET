using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.GMS
{
    public class SMSMessage
    {
        public string Message;
        public string PhoneNo;
        public int index;
        public DateTime Time;
        public SMSMessageStatus Status;
        public SMSMessage() { }
        public SMSMessage(string phoneNo, string message)
        {
            this.Message = message;
            this.PhoneNo = phoneNo;
        }
        public override string ToString()
        {
            if (this.Status == SMSMessageStatus.Send)
                return "TO : " + PhoneNo + " Time to Send : " + Time.ToString("g") + " Message : " + Message;
            else
                return "From : " + PhoneNo + " Time to Send : " + Time.ToString("g") + " Message : " + Message;

        }

        public enum SimCardProvider
        {
            IR_TCI = 1,
            IranCell = 2,
            MCI = 3
        }
        public enum SMSMessageStatus
        {
            Recived,
            Send
        }
    }
}