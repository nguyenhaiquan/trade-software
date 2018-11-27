using application.Strategy;
using commonClass;
using commonTypes;
using System;

namespace Strategy
{
    public class PriceTwoSMA_Helper : baseHelper
    {
        public PriceTwoSMA_Helper() : base(typeof(PriceTwoSMA)) { }
    }

    public class PriceTwoSMA : GenericStrategy
    {
        override protected void StrategyExecute()
        {
            PriceTwoSMARule smarule = new PriceTwoSMARule(data.Close, parameters[0], parameters[1]);

            double distance_to_long_indicator = 0.5;
            double cutlosslevel = 3; //point (not %)
            double takeprofitlevel = 3;
            Indicators.MIN min = Indicators.MIN.Series(data.Close, parameters[0], "min");
            Indicators.MAX max = Indicators.MAX.Series(data.Close, parameters[1], "max");
            Indicators.Fibonnanci fibo = Indicators.Fibonnanci.Series(data.Bars, parameters[1], "fibo");


            for (int idx = smarule.long_indicator.FirstValidValue; idx < smarule.long_indicator.Count; idx++)
            {
                if (smarule.isValid_forBuy(idx))
                {
                    BusinessInfo info = new BusinessInfo();
                    info.SetTrend(AppTypes.MarketTrend.Upward, AppTypes.MarketTrend.Unspecified, AppTypes.MarketTrend.Unspecified);


                    info.Short_Resistance = max[idx];
                    info.Short_Support = min[idx];

                    info.Short_Target = fibo.Fibo23pc[idx];

                    if (info.Short_Target < smarule.short_indicator[idx] + takeprofitlevel)
                        info.Short_Target = smarule.short_indicator[idx] + takeprofitlevel;

                    info.Stop_Loss = Math.Min(data.Close[idx] - cutlosslevel,smarule.long_indicator[idx]-distance_to_long_indicator);

                    BuyAtClose(idx, info);
                }
                else
                    if (smarule.isValid_forSell(idx))
                {
                    BusinessInfo info = new BusinessInfo();
                    info.SetTrend(AppTypes.MarketTrend.Upward, AppTypes.MarketTrend.Unspecified, AppTypes.MarketTrend.Unspecified);
                    info.Short_Resistance = max[idx];
                    info.Short_Support = min[idx];
                    info.Short_Target = fibo.Fibo23pc[idx];

                    info.Stop_Loss = data.Close[idx]+cutlosslevel;

                    SellAtClose(idx, info);
                }

                //Update Trailing Stop

                if (adviceInfo.Count > 0){ //
                    //Lay thong tin cua Last Advise
                    TradePointInfo info =( (TradePointInfo)adviceInfo[adviceInfo.Count-1]);
                    if (info.TradeAction == AppTypes.TradeActions.Buy)
                    {
                        info.BusinessInfo.Stop_Loss = Math.Min(data.Close[idx] - cutlosslevel, smarule.long_indicator[idx] - distance_to_long_indicator);
                    }

                    if (info.TradeAction == AppTypes.TradeActions.Sell)
                    {
                        info.BusinessInfo.Stop_Loss = Math.Max(data.Close[idx] + cutlosslevel, smarule.long_indicator[idx] + distance_to_long_indicator);
                    }
                }
                
                //If CutLoss then Action=CutLoss
                //If TakeProfit the TakeProfit action o take profit

                //if (data.Close[idx]<((TradePointInfo)adviceInfo[adviceInfo.Count-1]).BusinessInfo.Stop_Loss)

            }
        }
    }

    
}
