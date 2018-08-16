using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using application.Strategy;
using commonClass;
using Indicators;

namespace Strategy
{
    public class RSISCR_Helper : baseHelper
    {
        public RSISCR_Helper() : base(typeof(RSISCR)) { }
    }

    class RSISCR : GenericStrategy
    {
        override protected void StrategyExecute()
        {
            int weight = 0; //weight the hien weight cua 

            int period = (int)parameters[0];
            //double distance = (double)parameters[1];

            int Bar = data.Close.Count - 1;
            if (Bar <= 1) return;

            Indicators.RSI indicatorRSI = new Indicators.RSI(data.Close,period, "RSI14");
            BusinessInfo info = new BusinessInfo();
            if (indicatorRSI.Count > 1) info.Weight = indicatorRSI[indicatorRSI.Count - 1];
            SelectStock(Bar, info);
        }
    }

}
