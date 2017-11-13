using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using commonTypes;
using commonClass;

namespace Strategy
{

    public class QuantumIndicatorRule : Rule
    {
        public Indicators.QuantumIndicator qI;
        //public DataSeries ema;
        public QuantumIndicatorRule(DataBars db)
        {
            //macd = Indicators.MACD.Series(ds, fast, slow, signal, "macd");
            //ema = macd.SignalSeries;
            qI = Indicators.QuantumIndicator.Series(db, "quantumIndicator");

        }

        /// <summary>
        /// Condition for Screening
        /// </summary>
        /// <returns></returns>
        public override bool isValid()
        {
            return isValid_forBuy(qI.Count - 1);
        }

        /// <summary>
        /// If last Macd condition is downward and current Macd Condition upward then BUY
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public override bool isValid_forBuy(int idx)
        {
            if (DownTrend(idx - 1) && UpTrend(idx)) return true;
            return false;
        }

        public override bool isValid_forSell(int idx)
        {
            if (DownTrend(idx) && UpTrend(idx - 1)) return true;
            return false;
        }

        public override bool isValid(int index)
        {
            return base.isValid_forBuy(index);
        }

        public override bool DownTrend(int index)
        {
            if (index < qI.FirstValidValue) return false;

            if (qI[index] < 0)
                return true;
            return false;
        }

        public override bool UpTrend(int index)
        {
            if (index < qI.FirstValidValue) return false;

            if (qI[index] >= 0 )
                return true;
            return false;
        }
    }
}
