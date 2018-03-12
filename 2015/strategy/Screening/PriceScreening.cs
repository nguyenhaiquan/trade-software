using System;
using System.Collections.Generic;
using System.Text;
using application.Strategy;
using commonClass;

namespace Strategy
{
    public class PriceChangeSCR_Helper : baseHelper
    {
        public PriceChangeSCR_Helper() : base(typeof(PriceChangeSCR)) { }
    }

    public class PriceSCR_Helper : baseHelper
    {
        public PriceSCR_Helper() : base(typeof(PriceSCR)) { }
    }

    public class BasicPriceSCR_Helper : baseHelper
    {
        public BasicPriceSCR_Helper() : base(typeof(BasicPriceSCR)) { }
    }

<<<<<<< HEAD
    


=======
>>>>>>> origin/Change-download-page
    public class BasicPriceSCR : GenericStrategy
    {
        override protected void StrategyExecute()
        {
            int Bar = data.Close.Count - 1;
            if (Bar <= 1) return;
            BusinessInfo info = new BusinessInfo();
            info.Weight = data.Close[Bar];
            SelectStock(Bar, info);
        }
    }

    public class PriceChangeSCR : GenericStrategy
    {
        override protected void StrategyExecute()
        {
            int Bar = data.Close.Count - 1;
            int period = (int)parameters[0];//period la gia cua Bar phia truoc, default=14
            if (Bar-period < 0) return;

            //Tim cac stocks co Price hom nay lon hon SMA
            BusinessInfo info = new BusinessInfo();
            if (data.Close[Bar - period] != 0)
            {
                info.Weight = (data.Close[Bar] - data.Close[Bar - period]) / data.Close[Bar - period];
                info.WeightType = "percent";
            }
            else
                info.Weight = double.NegativeInfinity;
            SelectStock(Bar, info);
        }
    }

    public class PriceSCR : GenericStrategy 
    {
        override protected void StrategyExecute()
        {
            int Bar = data.Close.Count-1;
            if (Bar <= 1) return;

            int period = 65;
            DataSeries ema65 = Indicators.EMA.Series(data.Close,period,"");

            //Tim khoang cach
            if (data.Close[Bar] > ema65[Bar])
            {
                BusinessInfo info = new BusinessInfo();
                info.Weight = (data.Close[Bar]-ema65[Bar])/ema65[Bar]*100;
                SelectStock(Bar, info);
            }
        }
    }

    
}
