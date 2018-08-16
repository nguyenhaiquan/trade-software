using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacTec.TA.Library;
using commonClass;
using application.Indicators;

namespace Indicators
{

    /// <summary>
    /// Create new Indicator
    /// 1: Having IndicatorHelper
    /// 2:
    /// </summary>
    public class SharpeRatioIndicatorHelper : Helpers
    {
        public SharpeRatioIndicatorHelper()
        {
            //Init(typeof(MACD), typeof(forms.MACD));
            Init(typeof(SharpeRatioIndicator), typeof(application.forms.commonIndicatorForm),typeof(DataSeries));
        }
    }

    public class SharpeRatioIndicator : DataSeries 
    {
        public SharpeRatioIndicator(DataSeries ds,string name): base(ds, name)
        {
            int period = 1;
            double optInNbDev = 0.2;

            int begin = 0, length = 0;
            double delta = 0, lastDelta = 0;

            double[] output = new double[ds.Count];

            ROCP rocp = Indicators.ROCP.Series(ds, period, "rocp" + name);
            STDDEV std = Indicators.STDDEV.Series(ds, period, optInNbDev, "std" + name);

            DataSeries sharpe = (DataSeries)rocp / (DataSeries)std;
            Values = sharpe.Values;

            //SMA sma50Indicator = new SMA(ds, 50, "sma50"+name);
            
            
            ////Assign first bar that contains indicator data
            //if (length <= 0)
            //    FirstValidValue = begin + output.Length + 1;
            //else
            //    FirstValidValue = begin;
            //this.Name = name;
            //for (int i = begin, j = 0; j < length; i++, j++) this[i] = output[j];
        }

        public static SharpeRatioIndicator Series(DataSeries ds, string name)
        {
            //Build description
            string description = "(" + name +  ")";
            //See if it exists in the cache
            object obj = ds.Cache.Find(description);
            if (obj != null) return (SharpeRatioIndicator)obj;

            //Create SharpeRatio, cache it, return it
            SharpeRatioIndicator qi = new SharpeRatioIndicator(ds, description);
            ds.Cache.Add(description, qi);
            return qi;
        }
    }
}
