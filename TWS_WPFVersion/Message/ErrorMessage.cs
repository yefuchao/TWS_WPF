using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWS_WPFVersion.Message
{
    public class ErrorMessage :IBMessage
    {
        private string message;
        private int errorCode;
        private int requestId;

        public ErrorMessage(string message,int errorCode,int requestId)
        {
            Type = MessageType.Error;
            Message = message;
            ErrorCode = errorCode;
            RequestId = requestId;
        }

        public string Message
        { get { return message; }set { message = value; } }

        public int ErrorCode
        { get { return errorCode; } set { errorCode = value; } }

        public int RequestId
        { get { return requestId; } set { requestId = value; } }

        public override string ToString()
        {
            return "Error. Request:" + RequestId + "，Code：" + errorCode + "-" + Message;
        }
    }
}
