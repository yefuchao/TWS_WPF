using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TWS_WPFVersion.ViewModel;

namespace TWS_WPFVersion
{
    public class AccountManager
    {
        private const int ACCOUNT_ID_BASE = 500000000;

        private const int ACCOUNT_SUMMARY_ID = ACCOUNT_ID_BASE + 1;

        private const string ACCOUNT_SUMMARY_TAGS = "AccountType,NetLiquidation,TotalCashValue,SettledCash,AccruedCash,BuyingPower,EquityWithLoanValue,PreviousEquityWithLoanValue,"
             + "GrossPositionValue,ReqTEquity,ReqTMargin,SMA,InitMarginReq,MaintMarginReq,AvailableFunds,ExcessLiquidity,Cushion,FullInitMarginReq,FullMaintMarginReq,FullAvailableFunds,"
             + "FullExcessLiquidity,LookAheadNextChange,LookAheadInitMarginReq ,LookAheadMaintMarginReq,LookAheadAvailableFunds,LookAheadExcessLiquidity,HighestSeverity,DayTradesRemaining,Leverage";

        private IBClient ibClient;

        private List<string> manageAccounts;

        private ComboBox accountSelector;

        private DataGrid accountSumGrid;

        private DataGrid accountUpdGrid;

        private bool accountSummaryRequestActive = false;

        public ObservableCollection<AccountInfo> accountSumList = new ObservableCollection<AccountInfo>();

        public AccountManager(IBClient ibClient, ComboBox accountSelector, DataGrid accountSumGrid, DataGrid accountUpdGrid)
        {
            IbClient = ibClient;
            AccountSelector = accountSelector;
            AccountSumGrid = accountSumGrid;
            AccountUpdGrid = accountSumGrid;
        }

        public IBClient IbClient
        {
            get { return ibClient; }
            set { ibClient = value; }
        }

        public DataGrid AccountSumGrid
        {
            get { return accountSumGrid; }
            set
            {
                accountSumGrid = value;
            }
        }

        public DataGrid AccountUpdGrid
        {
            get { return accountUpdGrid; }
            set { accountUpdGrid = value; }
        }

        public List<string> ManageAccounts
        {
            get { return manageAccounts; }
            set
            {
                manageAccounts = value;
                SetManageAccounts(value);
            }
        }

        public ComboBox AccountSelector
        {
            get { return accountSelector; }
            set { accountSelector = value; }
        }

        public void SetManageAccounts(List<string> manageAccounts)
        {
            //AccountSelector.Items.Add(manageAccounts.ToArray());
            accountSelector.ItemsSource = manageAccounts.ToArray();
            AccountSelector.SelectedIndex = 0;
        }

        public void UpdateUI(IBMessage message)
        {
            switch (message.Type)
            {
                case MessageType.AccountSummary:
                    HandleAccountSummary((AccountSummaryMessage)message);
                    break;
                case MessageType.AccountSummaryEnd:
                    HandleAccountSummaryEnd();
                    break;
                default:
                    break;
            }
        }

        private void HandleAccountSummaryEnd()
        {
            accountSummaryRequestActive = false;
        }

        private void HandleAccountSummary(AccountSummaryMessage message)
        {
            AccountInfo accountInfo = new AccountInfo(message.Account, message.Tag, message.Value, message.Currency);
            accountSumList.Add(accountInfo);
            accountSumGrid.ItemsSource = accountSumList;
        }

        public void RequestAccountSummary()
        {
            //1 bug
            //if (!accountSummaryRequestActive)
            //{
            //    accountSummaryRequestActive = true;
            //    accountSumList.Clear();
            //    ibClient.ClientSocket.reqAccountSummary(ACCOUNT_SUMMARY_ID, "All", ACCOUNT_SUMMARY_TAGS);
            //}
            //else
            //{
            //    ibClient.ClientSocket.cancelAccountSummary(ACCOUNT_SUMMARY_ID);
            //}

            accountSumList.Clear();
            ibClient.ClientSocket.reqAccountSummary(ACCOUNT_SUMMARY_ID, "All", ACCOUNT_SUMMARY_TAGS);
        }
    }
}
