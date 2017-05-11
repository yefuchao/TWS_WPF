using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWS_WPFVersion.ViewModel
{
    public class MarketData
    {
        private string description;

        private int bidSize;

        private double bid;

        private double ask;

        private int askSize;

        private int lastSize;

        private double close;

        public string Description { get { return description; } set { description = value; } }

        public int BidSize { get { return bidSize; } set { bidSize = value; } }

        public double Bid { get { return bid; } set { bid = value; } }

        public double Ask { get { return ask; } set { ask = value; } }

        public int AskSize { get { return askSize; } set { askSize = value; } }

        public int LastSize { get { return lastSize; } set { lastSize = value; } }

        public double Close { get { return close; } set { close = value; } }

        public MarketData(string desc, int bidSize = 0, double bid = 0.00, double ask = 0.00, int askSize = 0, int lastSize = 0, double close = 0.00)
        {
            Description = desc;
            BidSize = bidSize;
            Bid = bid;
            Ask = ask;
            AskSize = askSize;
            LastSize = lastSize;
            Close = Close;
        }


    }
}
