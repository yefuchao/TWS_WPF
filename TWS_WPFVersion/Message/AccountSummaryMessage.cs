﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWS_WPFVersion
{
    public class AccountSummaryMessage : IBMessage
    {
        private int requestId;
        private string account;
        private string tag;
        private string value;
        private string currency;

        public int ReqestId
        {
            get { return requestId; }
            set { requestId = value; }
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

        public AccountSummaryMessage(int requestId,string account,string tag,string value,string currency)
        {
            Type = MessageType.AccountSummary;
            ReqestId = requestId;
            Account = account;
            Tag = tag;
            Value = value;
            Currency = currency;
        }
    }
}
