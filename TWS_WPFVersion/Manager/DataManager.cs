using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TWS_WPFVersion.Message
{
    public abstract class DataManager
    {
        protected Control uiControl;
        protected IBClient ibClient;
        protected int currentTicker = 1;

        protected delegate void UpdateUICallback(IBMessage message);

        public DataManager(IBClient ibClient,Control dataGrid)
        {
            this.ibClient = ibClient;
            uiControl = dataGrid;
        }

        public abstract void NotifyError(int requestId);

        public abstract void Clear();

        public abstract void UpdateUI(IBMessage message);
    }
}
