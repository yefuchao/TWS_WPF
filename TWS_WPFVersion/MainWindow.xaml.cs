using IBApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TWS_WPFVersion.Message;

namespace TWS_WPFVersion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool IsConnected = false;

        private IBClient IBClient;

        private AccountManager accountManager;

        delegate void MessageHandlerDelegate(IBMessage message);

        private EReaderMonitorSignal signal = new EReaderMonitorSignal();

        public MainWindow()
        {
            IBClient = new IBClient(signal);

            InitializeComponent();

            Data.Source = new System.Uri("Main.xaml", UriKind.Relative);

            accountManager = new AccountManager(IBClient, accountList, accountSum, accountUpd);

            IBClient.Error += IbClient_Error;

            IBClient.ManagedAccounts += accountsList => HandleMessage(new ManageAccountsMessage(accountsList));

            IBClient.AccountSummary += (reqId, account, tag, value, currency) => HandleMessage(new AccountSummaryMessage(reqId, account, tag, value, currency));

            IBClient.AccountSummaryEnd += requestId => HandleMessage(new AccountsSummaryEndMessage(requestId));

            accountList.SelectionChanged += AccountList_SelectionChanged;

            if (!IsConnected)
                status.Content = "disconnet";
        }

        private void IbClient_Error(int id, int errorCode, string str, Exception ex)
        {
            if (ex != null)
            {
                addMessageToBox("Error :" + ex.Message);
                return;
            }

            if (id == 0 || errorCode == 0)
            {
                addMessageToBox("Error :" + str);
                return;
            }

            HandleMessage(new ErrorMessage(str, errorCode, id));

        }

        private void addMessageToBox(string str)
        {
            HandleMessage(new ErrorMessage(str, -1, -1));
        }

        private void AccountList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            accountManager.RequestAccountSummary();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnected)
            {
                try
                {
                    //int port = Int32.Parse(Port.Text);
                    string host = string.IsNullOrEmpty(Ip.Text) ? "127.0.0.1" : Ip.Text;

                    IBClient.ClientSocket.eConnect(host, 7496, 0);

                    var reader = new EReader(IBClient.ClientSocket, signal);

                    reader.Start();

                    new Thread(() =>
                    {
                        while (IBClient.ClientSocket.IsConnected())
                        {
                            signal.waitForSignal();
                            reader.processMsgs();
                        }
                    })
                    { IsBackground = true }.Start();

                    if (IBClient.ClientSocket.IsConnected())
                    {

                        Connect.Content = "DisConnect";
                        status.Content = "Connected";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("请检查ip和port是否正确！");
                }
            }
            else
            {
                IBClient.ClientSocket.eDisconnect();
                Connect.Content = "Connected";
                status.Content = "DisConnect";
            }
            IsConnected = !IsConnected;
        }

        public void HandleMessage(IBMessage message)
        {
            if (!this.Dispatcher.CheckAccess())
            {
                MessageHandlerDelegate callback = new MessageHandlerDelegate(HandleMessage);
                this.Dispatcher.Invoke(callback, new object[] { message });
            }
            else
            {
                UpdateUI(message);
            }
        }

        public void UpdateUI(IBMessage message)
        {
            switch (message.Type)
            {
                case MessageType.ManagedAccounts:
                    accountManager.ManageAccounts = ((ManageAccountsMessage)message).ManageAccounts;
                    accountList.ItemsSource = ((ManageAccountsMessage)message).ManageAccounts.ToArray();
                    break;
                case MessageType.AccountSummary:
                    accountManager.UpdateUI(message);
                    break;
                case MessageType.AccountSummaryEnd:
                    accountManager.UpdateUI(message);
                    break;
                case MessageType.Error:
                    ErrorMessage error = (ErrorMessage)message;
                    ShowMessageOnPanel(error.ToString());
                    break;
                case MessageType.ConnectionStatus:
                    ConnectionStatusMessage statusMessage = (ConnectionStatusMessage)message;
                    if (statusMessage.IsConnected)
                    {

                    }
                    break;
                default:
                    break;
            }
        }

        private void ShowMessageOnPanel(string str)
        {

        }

        private void Request_Click(object sender, RoutedEventArgs e)
        {
            accountManager.RequestAccountSummary();
        }
    }
}


