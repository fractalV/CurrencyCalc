using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyCalc2
{
    internal class CurrencyHistoryClass 
    {
        public CurrencyHistoryClass(DateTime dateTime, Currency currency)
        {
            DateTime = dateTime;
            Currency = currency;
        }

        public DateTime DateTime { get; private set; }
        public Currency Currency { get; }
    }
}
