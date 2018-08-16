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
    public class QuantumIndicatorHelper : Helpers
    {
        public QuantumIndicatorHelper()
        {
            //Init(typeof(MACD), typeof(forms.MACD));
            Init(typeof(QuantumIndicator), typeof(application.forms.commonIndicatorForm),typeof(DataBars));
        }
    }
    public class QuantumIndicator : DataSeries 
    {
        public QuantumIndicator(DataBars db,string name): base(db, name)
        {
            int begin = 0, length = 0;
            DataSeries ds = db.Close;
            double delta = 0, lastDelta = 0;

            double[] output = new double[ds.Count];

            SMA sma50Indicator = new SMA(ds, 50, "sma50"+name);
            SMA sma20Indicator = new SMA(ds, 20, "sma20"+name);
            SMA sma10Indicator = new SMA(ds, 10, "sma10"+name);

            //MACD
            MACD macd = Indicators.MACD.Series(ds, 12, 26, 9, "macd"+name);
            DataSeries ema = macd.SignalSeries;
            DataSeries hist = macd.HistSeries;

            //SAR
            SAR sar = new Indicators.SAR(db, 0.02, 0.2, "sar"+name);

            //ATR
            ATR atr = Indicators.ATR.Series(db, 14, name);

            //Stochastic
            Stoch stoch = Indicators.Stoch.Series(db, 14, 3, 3, "stoch"+name);
            DataSeries line1 = stoch.SlowKSeries;
            DataSeries line2 = stoch.SlowDSeries;

            baseSMA rsi = Indicators.RSI.Series(ds, 14, "rsi" + name);

            //Relative Strength vs VN-Index
            //application.AnalysisData vnidxData = new application.AnalysisData();
            //vnidxData.New("VN-IDX");
            //vnidxData.LoadData();

            //Indicators.ROCR100 roc = Indicators.ROCR100.Series(db.Close, 25, "roc"+name);
            //Indicators.ROCR100 roc_index = Indicators.ROCR100.Series(vnidxData.Close, 25, "roc_index"+name);

            length = ds.Count;
            for (int i = 0; i < length; i++)
            {
                output[i] = 0;
                if (ds[i]>=sma10Indicator[i]) output[i]++;
                else output[i]--;

                if (ds[i] >= sma20Indicator[i]) output[i]++;
                else output[i]--;

                if (ds[i] >= sma50Indicator[i]) output[i]++;
                else output[i]--;

                //Neu sma tang gia : so 10 va sma20
                if (sma10Indicator[i] >= sma20Indicator[i]) output[i]++;
                else output[i]--;

                if (sma10Indicator[i] >= sma50Indicator[i]) output[i]++;
                else output[i]--;

                if (sma20Indicator[i] >= sma50Indicator[i]) output[i]++;
                else output[i] = output[i]--;

                //Xu huong Tang MACD
                if (macd[i] >= 0) output[i]++;
                else output[i]--;

                if (macd[i] >= ema[i]) output[i]++;
                else output[i]--;

                //MACD Histogram
                if ((i >= 2) && (hist != null))
                {
                    lastDelta = (hist[i - 1] - hist[i - 2]);
                    delta = (hist[i] - hist[i - 1]);

                    if (delta >= 0) output[i]++;
                    else output[i]--;
                }
                
                //SAR
                if (ds[i] >= sar[i]) output[i]++;
                else output[i]--;

                //ATR
                if ((i>=1)&&(ds[i] >= atr[i] + ds[i - 1])) output[i]++;
                else output[i]--;

                //Stochastic
                if (line1[i] >= line2[i]) output[i]++;
                else output[i]--;

                //RSI
                if (rsi[i] >= 70)
                    output[i] = output[i] -2;
                else 
                    if (rsi[i]<=30) output[i] = output[i] + 2;

                //Rate of change - Multiple if have lots of change compared to vnindex
                //double rs=0;
                //if (roc_index[i]!=0) rs=roc[i] / roc_index[i];
                //output[i] = output[i] * (1 + rs);
            }

            //Assign first bar that contains indicator data
            if (length <= 0)
                FirstValidValue = begin + output.Length + 1;
            else
                FirstValidValue = begin;
            this.Name = name;
            for (int i = begin, j = 0; j < length; i++, j++) this[i] = output[j];
        }

        public static QuantumIndicator Series(DataBars ds, string name)
        {
            //Build description
            string description = "(" + name +  ")";
            //See if it exists in the cache
            object obj = ds.Cache.Find(description);
            if (obj != null) return (QuantumIndicator)obj;

            //Create MACD, cache it, return it
            QuantumIndicator qi = new QuantumIndicator(ds, description);
            ds.Cache.Add(description, qi);
            return qi;
        }
    }
}
