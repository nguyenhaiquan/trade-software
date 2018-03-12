using System;
using System.Collections.Generic;
using System.Text;
using application.Strategy;
using commonClass;

namespace Strategy
{
    public class BasicVolumeSCR_Helper : baseHelper
    {
        public BasicVolumeSCR_Helper() : base(typeof(BasicVolumeSCR)) { }
    }

    public class VolumeSCR_Helper : baseHelper
    {
        public VolumeSCR_Helper() : base(typeof(VolumeSCR)) { }
    }

    public class BasicVolumeSCR : GenericStrategy
    {
        override protected void StrategyExecute()
        {
            int Bar = data.Close.Count - 1;
            if (Bar <= 1) return;
            BusinessInfo info = new BusinessInfo();
            info.Weight = data.Volume[Bar] * data.Close[Bar]/1000000;
            SelectStock(Bar, info);
        }
    }

    public class VolumeSCR : GenericStrategy
    {
        override protected void StrategyExecute()
        {
            int Bar = data.Close.Count - 1;
            if (Bar <= 1) return;

            //Tim cac stocks co volume hom nay lon hon hom truoc
            //if (data.Volume[Bar] > data.Volume[Bar - 1])
            {
                BusinessInfo info = new BusinessInfo();
                info.Weight = data.Volume[Bar];
                SelectStock(Bar, info);
            }
        }
    }
}
