using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWS_WPFVersion.Message
{
    public class TickPriceMessage : MarketDataMessage
    {

        private double price;
        private int canAutoExecute;

        public TickPriceMessage(int reqId, int field,double price,int canAutoExecute) : base(MessageType.TickPrice, reqId, field)
        {
            this.price = price;
            this.canAutoExecute = canAutoExecute;
        }

        public double Price { get { return price; } set { price = value; } }

        public int CanAutoExecute { get { return canAutoExecute; } set { canAutoExecute = value; } }
    }
}
