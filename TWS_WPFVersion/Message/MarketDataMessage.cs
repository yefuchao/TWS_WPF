using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWS_WPFVersion.Message
{
    public class MarketDataMessage :IBMessage
    {
        protected int requestId;
        protected int field;

        public MarketDataMessage(MessageType type ,int reqId ,int field)
        {
            Type = type;
            Field = field;
            RequestId = reqId;
            
        }

        public int RequestId
        { get { return requestId; } set { requestId = value; } }

        public int Field
        { get { return field; } set { field = value; } }
    }
}
