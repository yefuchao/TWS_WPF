using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWS_WPFVersion.Manager
{
    public class OptionsManager
    {
        public const int OPTIONS_ID_BASE = 70000000;

        private const int OPTIONS_DATA_CALL_BASE = OPTIONS_ID_BASE + 100000;
        private const int OPTIONS_DATA_PUT_BASE = OPTIONS_ID_BASE + 200000;

        private const int OPTIONS_EXERCISING_BASE = OPTIONS_ID_BASE + 1000000;

        private IBClient ibClient;

    }
}
