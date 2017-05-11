using IBApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TWS_WPFVersion.util;
using TWS_WPFVersion.ViewModel;

namespace TWS_WPFVersion.Message
{
    public class MarketDataManager : DataManager
    {
        public const int TICK_ID_BASE = 10000000;

        private const int DESCRIPTION_INDEX = 0;

        private const int BID_PRICE_INDEX = 2;
        private const int ASK_PRICE_INDEX = 3;
        private const int CLOSE_PRICE_INDEX = 7;

        private const int BID_SIZE_INDEX = 1;
        private const int ASK_SIZE_INDEX = 4;
        private const int LAST_SIZE_INDEX = 5;
        private const int VOLUME_SIZE_INDEX = 6;

        private List<Contract> activeRequests = new List<Contract>();

        public ObservableCollection<MarketData> marketDataList = new ObservableCollection<MarketData>();

        public MarketDataManager(IBClient ibClient, Control dataGrid) : base(ibClient, dataGrid)
        {
        }

        public void AddRequest(Contract contract ,string genericTickList)
        {
            contract.SecType = "CASH";
            activeRequests.Add(contract);
            int nextReqId = TICK_ID_BASE + (currentTicker++);
            checkToAddRow(nextReqId);
            ibClient.ClientSocket.reqMktData(nextReqId, contract, genericTickList, false, new List<TagValue>());

        }

        public void checkToAddRow(int reqId)
        {
            ListView listView = (ListView)uiControl;

            marketDataList.Add(new MarketData(Utils.ContractToString(activeRequests[GetIndex(reqId)])));

            listView.ItemsSource = marketDataList;
           
        }

        private int GetIndex(int reqId)
        {
            return reqId - TICK_ID_BASE - 1;
        }

        public override void Clear()
        {
            throw new NotImplementedException();
        }

        public override void NotifyError(int requestId)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUI(IBMessage message)
        {
            MarketDataMessage dataMessage = (MarketDataMessage)message;

            ListView grid = (ListView)uiControl;

            if (message is TickPriceMessage)
            {
                TickPriceMessage priceMessage = (TickPriceMessage)message;
                switch (dataMessage.Field)
                {
                    case 1:
                        marketDataList[GetIndex(dataMessage.RequestId)].Bid = priceMessage.Price;
                        break;
                    case 2:
                        marketDataList[GetIndex(dataMessage.RequestId)].Ask = priceMessage.Price;
                        break;
                    case 9:
                        marketDataList[GetIndex(dataMessage.RequestId)].Close = priceMessage.Price;
                        break;
                    default:
                        break;
                }
                grid.Items.Refresh();
            }

        }
    }
}
