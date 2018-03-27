using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using application.Strategy;
using commonClass;
using Indicators;

namespace Strategy
{
    public class EMA5SCR_Helper : baseHelper
    {
        public EMA5SCR_Helper() : base(typeof(EMA5_SCR)) { }
    }

    class EMA5_SCR : GenericStrategy
    {
        override protected void StrategyExecute()
        {
            int weight = 0; //weight the hien weight cua 

            int period = 5;
            //double distance = (double)parameters[1];

            int Bar = data.Close.Count - 1;
            if (Bar <= 1) return;

            Indicators.EMA indicator = new Indicators.EMA(data.Close, period, "EMA05");
            BusinessInfo info = new BusinessInfo();
            //Show the distance between
            if (indicator.Count > 1)
                if (indicator[indicator.Count - 1] > 0)
                {
                    info.Weight = (data.Close[Bar] - indicator[indicator.Count - 1]) / indicator[indicator.Count - 1];
                    info.WeightType = "percent";
                }
            SelectStock(Bar, info);
        }
    }
    public class EMA9SCR_Helper : baseHelper
    {
        public EMA9SCR_Helper() : base(typeof(EMA9_SCR)) { }
    }

    class EMA9_SCR : GenericStrategy
    {
        override protected void StrategyExecute()
        {
            int weight = 0; //weight the hien weight cua 

            int period = 9;
            //double distance = (double)parameters[1];

            int Bar = data.Close.Count - 1;
            if (Bar <= 1) return;

            Indicators.EMA indicator = new Indicators.EMA(data.Close, period, "EMA09");
            BusinessInfo info = new BusinessInfo();
            //Show the distance between
            if (indicator.Count > 1)
                if (indicator[indicator.Count - 1] > 0)
                {
                    info.Weight = (data.Close[Bar] - indicator[indicator.Count - 1]) / indicator[indicator.Count - 1];
                    info.WeightType = "percent";
                }
            SelectStock(Bar, info);
        }
    }

    public class EMA15SCR_Helper : baseHelper
    {
        public EMA15SCR_Helper() : base(typeof(EMA15_SCR)) { }
    }

    class EMA15_SCR : GenericStrategy
    {
        override protected void StrategyExecute()
        {
            int weight = 0; //weight the hien weight cua 

            int period = 15;
            //double distance = (double)parameters[1];

            int Bar = data.Close.Count - 1;
            if (Bar <= 1) return;

            Indicators.EMA indicator = new Indicators.EMA(data.Close, period, "EMA15");
            BusinessInfo info = new BusinessInfo();
            //Show the distance between
            if (indicator.Count > 1)
                if (indicator[indicator.Count - 1] > 0)
                {
                    info.Weight = (data.Close[Bar] - indicator[indicator.Count - 1]) / indicator[indicator.Count - 1];
                    info.WeightType = "percent";
                }
            SelectStock(Bar, info);
        }
    }

    public class EMA65SCR_Helper : baseHelper
    {
        public EMA65SCR_Helper() : base(typeof(EMA65_SCR)) { }
    }

    class EMA65_SCR : GenericStrategy
    {
        override protected void StrategyExecute()
        {
            int weight = 0; //weight the hien weight cua 

            int period = 65;
            //double distance = (double)parameters[1];

            int Bar = data.Close.Count - 1;
            if (Bar <= 1) return;

            Indicators.EMA indicator = new Indicators.EMA(data.Close, period, "EMA65");
            BusinessInfo info = new BusinessInfo();
            //Show the distance between
            if (indicator.Count > 1)
                if (indicator[indicator.Count - 1] > 0)
                {
                    info.Weight = (data.Close[Bar] - indicator[indicator.Count - 1]) / indicator[indicator.Count - 1] ;
                    info.WeightType = "percent";
                }
            SelectStock(Bar, info);
        }
    }

    public class EMA200SCR_Helper : baseHelper
    {
        public EMA200SCR_Helper() : base(typeof(EMA200_SCR)) { }
    }

    class EMA200_SCR : GenericStrategy
    {
        override protected void StrategyExecute()
        {
            int weight = 0; //weight the hien weight cua 

            int period = 200;
            //double distance = (double)parameters[1];

            int Bar = data.Close.Count - 1;
            if (Bar <= 1) return;

            Indicators.EMA indicator = new Indicators.EMA(data.Close, period, "EMA200");
            BusinessInfo info = new BusinessInfo();
            //Show the distance between
            if (indicator.Count > 1)
                if (indicator[indicator.Count - 1] > 0)
                {
                    info.Weight = (data.Close[Bar] - indicator[indicator.Count - 1]) / indicator[indicator.Count - 1] ;
                    info.WeightType = "percent";
                }
            SelectStock(Bar, info);
        }
    }
}
