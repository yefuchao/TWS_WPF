using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWS_WPFVersion.Message
{
    public class ConnectionStatusMessage :IBMessage
    {
        private bool isConnected;

        public bool IsConnected
        { get { return IsConnected; } }

        public ConnectionStatusMessage(bool isConnected)
        {
            this.isConnected = isConnected;
            Type = MessageType.ConnectionStatus;
        }


    }
}
