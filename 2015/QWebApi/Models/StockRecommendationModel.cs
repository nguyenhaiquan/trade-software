using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QWebApi.Models
{
    public class StockRecommendationModel
    {
        public string stockcode{get;set;}
        public double actual_price{get;set;}
        public double percent_change{get;set;}
        public string recommendation{get;set;}    
        public double last_price{get;set;}
        public DateTime date{get;set;}

        public StockRecommendationModel()
        {
        }

        //public TradePointInfo[] GetTradePointWithEstimationDetail(DataParams dataParam, string stockCode, string strategyCode, EstimateOptions options,
        //                                                          out databases.tmpDS.tradeEstimateDataTable toTbl, out application.StrategyStatistics statistics)
        //{
        //    toTbl = null;
        //    statistics = null;
        //    try
        //    {
        //        string dataKey = LoadAnalysisData(stockCode, dataParam, false);
        //        TradePointInfo[] tradePoints = Analysis(dataKey, strategyCode);
        //        statistics = new application.StrategyStatistics();
        //        toTbl = application.Strategy.StrategyLibs.EstimateTrading_Details(sysDataCache.Find(dataKey) as application.AnalysisData, tradePoints, options, out statistics);
        //        return tradePoints;
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteSysLogLocal("WS083", ex);
        //    }
        //    return null;
        //}
    }
}