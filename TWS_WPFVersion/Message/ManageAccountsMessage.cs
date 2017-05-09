using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWS_WPFVersion
{
    public class ManageAccountsMessage : IBMessage
    {
        List<string> manageAccounts;

        public List<string> ManageAccounts
        {
            get { return manageAccounts; }
            set { manageAccounts = value; }
        }

        public ManageAccountsMessage(string manageAccounts)
        {
            this.manageAccounts = new List<string>(manageAccounts.Split(','));
            Type = MessageType.ManagedAccounts;
        }
        
    }
}
