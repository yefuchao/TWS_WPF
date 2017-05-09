using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWS_WPFVersion
{
    public class AccountsSummaryEndMessage : IBMessage
    {
        private int requestId;

        public int RequestId
        { get { return requestId; } set { requestId = value; } }

        public AccountsSummaryEndMessage(int requestId)
        {
            RequestId = requestId;
            Type = MessageType.AccountSummaryEnd;
        }

    }
}
