using IBApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TWS_WPFVersion
{
    public class IBClient : EWrapper
    {
        private EClientSocket clientSocket;

        private int clientId;

        private int nextOrderId;

        public int ClientId
        {
            get { return clientId; }
            set { clientId = value; }
        }

        public int NextOrderId
        {
            get { return nextOrderId; }
            set { nextOrderId = value; }
        }

        public IBClient(EReaderSignal signal)
        {
            ClientSocket = new EClientSocket(this, signal);
        }

        public EClientSocket ClientSocket
        {
            get { return clientSocket; }
            set { clientSocket = value; }
        }

        public event Action<int, int, string, Exception> Error;

        void EWrapper.error(Exception e)
        {
            var tmp = Error;

            if ( tmp!= null)
            {
                tmp(0, 0, null, e);
            }
        }

        void EWrapper.error(string str)
        {
            var tmp = Error;
            if (tmp != null)
                tmp(0, 0, str, null);
        }

        void EWrapper.error(int id,int errorCode,string errorMsg)
        {
            var tmp = Error;

            if (tmp != null)
            {
                tmp(id, errorCode, errorMsg, null);
            }
        }

        //定义事件成员
        public event Action<long> CurrentTime;

        //定义负责引发事件的方法来通知事件的登记对象
        void EWrapper.currentTime(long time)
        {
            //出于线程安全的考虑，现在将委托字段的引用复制到一个临时变量
            var tmp = Volatile.Read(ref CurrentTime);
            //任何方法登记了对事件的关注，就通知他们
            if (tmp != null)
            {
                tmp(time);
            }
        }

        public event Action<int, int, double, int> TickPrice;

        void EWrapper.tickPrice(int tickerId, int field, double price, int canAutoExecute)
        {
            var tmp = TickPrice;

            if (tmp != null)
            {
                tmp(tickerId, field, price, canAutoExecute);
            }
        }

        public event Action<int, int, int> TickSize;

        void EWrapper.tickSize(int tickerId, int field, int size)
        {
            var tmp = TickSize;
            if (tmp != null)
            {
                tmp(tickerId, field, size);
            }
        }

        public event Action<int, int, string> TickString;

        void EWrapper.tickString(int tickerId, int field, string value)
        {
            var tmp = TickString;
            if (tmp != null)
            {
                tmp(tickerId, field, value);
            }
        }

        public event Action ConnectionClosed;

        void EWrapper.connectionClosed()
        {
            var tmp = ConnectionClosed;

            if (tmp != null)
                tmp();
        }

        public event Action<int, int, double> TickGeneric;

        void EWrapper.tickGeneric(int tickerId, int field, double value)
        {
            var tmp = TickGeneric;

            if (tmp != null)
                tmp(tickerId, field, value);
        }

        public event Action<int, int, double, string, double, int, string, double, double> TickEFP;

        void EWrapper.tickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints, double impliedFuture, int holdDays, string futureLastTradeDate, double dividendImpact, double dividendsToLastTradeDate)
        {
            var tmp = TickEFP;

            if (tmp != null)
                tmp(tickerId, tickType, basisPoints, formattedBasisPoints, impliedFuture, holdDays, futureLastTradeDate, dividendImpact, dividendsToLastTradeDate);
        }

        public event Action<int> TickSnapshotEnd;

        void EWrapper.tickSnapshotEnd(int tickerId)
        {
            var tmp = TickSnapshotEnd;

            if (tmp != null)
                tmp(tickerId);
        }

        public event Action<int> NextValidId;

        void EWrapper.nextValidId(int orderId)
        {
            var tmp = NextValidId;

            if (tmp != null)
                tmp(orderId);

            NextOrderId = orderId;
        }

        public event Action<int, UnderComp> DeltaNeutralValidation;

        void EWrapper.deltaNeutralValidation(int reqId, UnderComp underComp)
        {
            var tmp = DeltaNeutralValidation;

            if (tmp != null)
                tmp(reqId, underComp);
        }

        //定义事件成员
        public static event Action<string> ManagedAccounts;

        //public event EventHandler<AccountEventArgs> ManagedAccounts;

        void EWrapper.managedAccounts(string accountsList)
        {
            var tmp = ManagedAccounts;

            if (tmp != null)
                tmp(accountsList);
                //tmp(this, new AccountEventArgs(accountsList));
        }

        //public virtual void managedAccounts(string accountsKist)
        //{
        //    Console.WriteLine("accountList" + accountsKist);
        //}

        public event Action<int, int, double, double, double, double, double, double, double, double> TickOptionCommunication;

        void EWrapper.tickOptionComputation(int tickerId, int field, double impliedVolatility, double delta, double optPrice, double pvDividend, double gamma, double vega, double theta, double undPrice)
        {
            var tmp = TickOptionCommunication;

            if (tmp != null)
                tmp(tickerId, field, impliedVolatility, delta, optPrice, pvDividend, gamma, vega, theta, undPrice);
        }

        public static event Action<int, string, string, string, string> AccountSummary;

        void EWrapper.accountSummary(int reqId, string account, string tag, string value, string currency)
        {
            var tmp = AccountSummary;

            if (tmp != null)
                tmp(reqId, account, tag, value, currency);
        }

        public event Action<int> AccountSummaryEnd;

        void EWrapper.accountSummaryEnd(int reqId)
        {
            var tmp = AccountSummaryEnd;

            if (tmp != null)
                tmp(reqId);
        }

        public event Action<string, string, string, string> UpdateAccountValue;

        void EWrapper.updateAccountValue(string key, string value, string currency, string accountName)
        {
            var tmp = UpdateAccountValue;

            if (tmp != null)
                tmp(key, value, currency, accountName);
        }

        public event Action<Contract, double, double, double, double, double, double, string> UpdatePortfolio;

        void EWrapper.updatePortfolio(Contract contract, double position, double marketPrice, double marketValue, double averageCost, double unrealisedPNL, double realisedPNL, string accountName)
        {
            var tmp = UpdatePortfolio;

            if (tmp != null)
                tmp(contract, position, marketPrice, marketValue, averageCost, unrealisedPNL, realisedPNL, accountName);
        }

        public event Action<string> UpdateAccountTime;

        void EWrapper.updateAccountTime(string timestamp)
        {
            var tmp = UpdateAccountTime;

            if (tmp != null)
                tmp(timestamp);
        }

        public event Action<string> AccountDownloadEnd;

        void EWrapper.accountDownloadEnd(string account)
        {
            var tmp = AccountDownloadEnd;

            if (tmp != null)
                tmp(account);
        }

        public event Action<int, string, double, double, double, int, int, double, int, string> OrderStatus;

        void EWrapper.orderStatus(int orderId, string status, double filled, double remaining, double avgFillPrice, int permId, int parentId, double lastFillPrice, int clientId, string whyHeld)
        {
            var tmp = OrderStatus;

            if (tmp != null)
                tmp(orderId, status, filled, remaining, avgFillPrice, permId, parentId, lastFillPrice, clientId, whyHeld);
        }

        public event Action<int, Contract, Order, OrderState> OpenOrder;

        void EWrapper.openOrder(int orderId, Contract contract, Order order, OrderState orderState)
        {
            var tmp = OpenOrder;

            if (tmp != null)
                tmp(orderId, contract, order, orderState);
        }

        public event Action OpenOrderEnd;

        void EWrapper.openOrderEnd()
        {
            var tmp = OpenOrderEnd;

            if (tmp != null)
                tmp();
        }

        public event Action<int, ContractDetails> ContractDetails;

        void EWrapper.contractDetails(int reqId, ContractDetails contractDetails)
        {
            var tmp = ContractDetails;

            if (tmp != null)
                tmp(reqId, contractDetails);
        }

        public event Action<int> ContractDetailsEnd;

        void EWrapper.contractDetailsEnd(int reqId)
        {
            var tmp = ContractDetailsEnd;

            if (tmp != null)
                tmp(reqId);
        }

        public event Action<int, Contract, Execution> ExecDetails;

        void EWrapper.execDetails(int reqId, Contract contract, Execution execution)
        {
            var tmp = ExecDetails;

            if (tmp != null)
                tmp(reqId, contract, execution);
        }

        public event Action<int> ExecDetailsEnd;

        void EWrapper.execDetailsEnd(int reqId)
        {
            var tmp = ExecDetailsEnd;

            if (tmp != null)
                tmp(reqId);
        }

        public event Action<CommissionReport> CommissionReport;

        void EWrapper.commissionReport(CommissionReport commissionReport)
        {
            var tmp = CommissionReport;

            if (tmp != null)
                tmp(commissionReport);
        }

        public event Action<int, string> FundamentalData;

        void EWrapper.fundamentalData(int reqId, string data)
        {
            var tmp = FundamentalData;

            if (tmp != null)
                tmp(reqId, data);
        }

        public event Action<int, string, double, double, double, double, int, int, double, bool> HistoricalData;

        void EWrapper.historicalData(int reqId, string date, double open, double high, double low, double close, int volume, int count, double WAP, bool hasGaps)
        {
            var tmp = HistoricalData;

            if (tmp != null)
                tmp(reqId, date, open, high, low, close, volume, count, WAP, hasGaps);
        }

        public event Action<int, string, string> HistoricalDataEnd;

        void EWrapper.historicalDataEnd(int reqId, string startDate, string endDate)
        {
            var tmp = HistoricalDataEnd;

            if (tmp != null)
                tmp(reqId, startDate, endDate);
        }

        public event Action<int, int> MarketDataType;

        void EWrapper.marketDataType(int reqId, int marketDataType)
        {
            var tmp = MarketDataType;

            if (tmp != null)
                tmp(reqId, marketDataType);
        }

        public event Action<int, int, int, int, double, int> UpdateMktDepth;

        void EWrapper.updateMktDepth(int tickerId, int position, int operation, int side, double price, int size)
        {
            var tmp = UpdateMktDepth;

            if (tmp != null)
                tmp(tickerId, position, operation, side, price, size);
        }

        public event Action<int, int, string, int, int, double, int> UpdateMktDepthL2;

        void EWrapper.updateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side, double price, int size)
        {
            var tmp = UpdateMktDepthL2;

            if (tmp != null)
                tmp(tickerId, position, marketMaker, operation, side, price, size);
        }

        public event Action<int, int, String, String> UpdateNewsBulletin;

        void EWrapper.updateNewsBulletin(int msgId, int msgType, String message, String origExchange)
        {
            var tmp = UpdateNewsBulletin;

            if (tmp != null)
                tmp(msgId, msgType, message, origExchange);
        }

        public event Action<string, Contract, double, double> Position;

        void EWrapper.position(string account, Contract contract, double pos, double avgCost)
        {
            var tmp = Position;

            if (tmp != null)
                tmp(account, contract, pos, avgCost);
        }

        public event Action PositionEnd;

        void EWrapper.positionEnd()
        {
            var tmp = PositionEnd;

            if (tmp != null)
                tmp();
        }

        public event Action<int, long, double, double, double, double, long, double, int> RealtimeBar;

        void EWrapper.realtimeBar(int reqId, long time, double open, double high, double low, double close, long volume, double WAP, int count)
        {
            var tmp = RealtimeBar;

            if (tmp != null)
                tmp(reqId, time, open, high, low, close, volume, WAP, count);
        }

        public event Action<string> ScannerParameters;

        void EWrapper.scannerParameters(string xml)
        {
            var tmp = ScannerParameters;

            if (tmp != null)
                tmp(xml);
        }

        public event Action<int, int, ContractDetails, string, string, string, string> ScannerData;

        void EWrapper.scannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark, string projection, string legsStr)
        {
            var tmp = ScannerData;

            if (tmp != null)
                tmp(reqId, rank, contractDetails, distance, benchmark, projection, legsStr);
        }

        public event Action<int> ScannerDataEnd;

        void EWrapper.scannerDataEnd(int reqId)
        {
            var tmp = ScannerDataEnd;

            if (tmp != null)
                tmp(reqId);
        }

        public event Action<int, string> ReceiveFA;

        void EWrapper.receiveFA(int faDataType, string faXmlData)
        {
            var tmp = ReceiveFA;

            if (tmp != null)
                tmp(faDataType, faXmlData);
        }

        public event Action<int, ContractDetails> BondContractDetails;

        void EWrapper.bondContractDetails(int requestId, ContractDetails contractDetails)
        {
            var tmp = BondContractDetails;

            if (tmp != null)
                tmp(requestId, contractDetails);
        }

        public event Action<string> VerifyMessageAPI;

        void EWrapper.verifyMessageAPI(string apiData)
        {
            var tmp = VerifyMessageAPI;

            if (tmp != null)
                tmp(apiData);
        }
        public event Action<bool, string> VerifyCompleted;

        void EWrapper.verifyCompleted(bool isSuccessful, string errorText)
        {
            var tmp = VerifyCompleted;

            if (tmp != null)
                tmp(isSuccessful, errorText);
        }

        public event Action<string, string> VerifyAndAuthMessageAPI;

        void EWrapper.verifyAndAuthMessageAPI(string apiData, string xyzChallenge)
        {
            var tmp = VerifyAndAuthMessageAPI;

            if (tmp != null)
                tmp(apiData, xyzChallenge);
        }

        public event Action<bool, string> VerifyAndAuthCompleted;

        void EWrapper.verifyAndAuthCompleted(bool isSuccessful, string errorText)
        {
            var tmp = VerifyAndAuthCompleted;

            if (tmp != null)
                tmp(isSuccessful, errorText);
        }

        public event Action<int, string> DisplayGroupList;

        void EWrapper.displayGroupList(int reqId, string groups)
        {
            var tmp = DisplayGroupList;

            if (tmp != null)
                tmp(reqId, groups);
        }

        public event Action<int, string> DisplayGroupUpdated;

        void EWrapper.displayGroupUpdated(int reqId, string contractInfo)
        {
            var tmp = DisplayGroupUpdated;

            if (tmp != null)
                tmp(reqId, contractInfo);
        }

        void EWrapper.connectAck()
        {
            if (ClientSocket.AsyncEConnect)
                ClientSocket.startApi();
        }

        public event Action<int, string, string, Contract, double, double> PositionMulti;

        void EWrapper.positionMulti(int reqId, string account, string modelCode, Contract contract, double pos, double avgCost)
        {
            var tmp = PositionMulti;

            if (tmp != null)
                tmp(reqId, account, modelCode, contract, pos, avgCost);
        }

        public event Action<int> PositionMultiEnd;

        void EWrapper.positionMultiEnd(int reqId)
        {
            var tmp = PositionMultiEnd;

            if (tmp != null)
                tmp(reqId);
        }

        public event Action<int, string, string, string, string, string> AccountUpdateMulti;

        void EWrapper.accountUpdateMulti(int reqId, string account, string modelCode, string key, string value, string currency)
        {
            var tmp = AccountUpdateMulti;

            if (tmp != null)
                tmp(reqId, account, modelCode, key, value, currency);
        }

        public event Action<int> AccountUpdateMultiEnd;

        void EWrapper.accountUpdateMultiEnd(int reqId)
        {
            var tmp = AccountUpdateMultiEnd;

            if (tmp != null)
                tmp(reqId);
        }

        public event Action<int, string, int, string, string, HashSet<string>, HashSet<double>> SecurityDefinitionOptionParameter;

        void EWrapper.securityDefinitionOptionParameter(int reqId, string exchange, int underlyingConId, string tradingClass, string multiplier, HashSet<string> expirations, HashSet<double> strikes)
        {
            var tmp = SecurityDefinitionOptionParameter;

            if (tmp != null)
                tmp(reqId, exchange, underlyingConId, tradingClass, multiplier, expirations, strikes);
        }

        public event Action<int> SecurityDefinitionOptionParameterEnd;

        void EWrapper.securityDefinitionOptionParameterEnd(int reqId)
        {
            var tmp = SecurityDefinitionOptionParameterEnd;

            if (tmp != null)
                tmp(reqId);
        }

        public event Action<int, SoftDollarTier[]> SoftDollarTiers;

        void EWrapper.softDollarTiers(int reqId, SoftDollarTier[] tiers)
        {
            var tmp = SoftDollarTiers;

            if (tmp != null)
                tmp(reqId, tiers);
        }

    }

}
