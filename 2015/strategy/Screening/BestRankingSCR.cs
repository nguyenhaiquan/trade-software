using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using application.Strategy;
using commonClass;

namespace Strategy
{
    public class BestRankingSCR_Helper : baseHelper
    {
        public BestRankingSCR_Helper() : base(typeof(BestRankingSCR)) { }
    }

    class BestRankingSCR : GenericStrategy
    {
        override protected void StrategyExecute()
        {
            int weight = 0; //weight the hien weight cua 

            //int period = (int)parameters[0];
            //double distance = (double)parameters[1];

            int Bar = data.Close.Count - 1;
            if (Bar <= 1) return;
            //double support = strategyLib.findSupport(data.Close, Bar, period);
            //if (support == -1) return;
            //if ((data.Close[Bar] - support) / support * 100 < distance)
            //{
            //    BusinessInfo info = new BusinessInfo();
            //    info.Weight = support;
            //    SelectStock(Bar, info);
            //}

            //1. Tinh diem cong cho tung chien luoc
            //Indicators.SMA smaVolume = new Indicators.SMA(data.Volume, parameters[0], "sma");
            //if (data.Volume[Bar] > 1.2 * smaVolume[Bar])
            //    weight++;

            //if (data.Volume[Bar] < 1.2 * smaVolume[Bar])
            //    weight--;

            ////2. 
            //BasicMACDRule rule = new BasicMACDRule(data.Close, 29, 9, 9);

            Indicators.QuantumIndicator qI = new Indicators.QuantumIndicator(data.Bars, "quantum");
            BusinessInfo info = new BusinessInfo();
            if (qI.Count>1) info.Weight = qI[qI.Count-1];
            SelectStock(Bar, info);
        }
    }

}
