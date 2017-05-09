using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWS_WPFVersion.ViewModel
{
    public class AccountInfo
    {
        private string account;
        private string tag;
        private string value;
        private string currency;

        public AccountInfo(string account,string tag,string value,string currency)
        {
            Account = account;
            Tag = tag;
            Value = value;
            Currency = currency;
        }

        public string Account
        {
            get { return account; }
            set { account = value; }
        }

        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }
    }
}
