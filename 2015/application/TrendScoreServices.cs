using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Specialized;

namespace application
{
    public class TrendScoreServices
    {
        /// <summary>
        /// 2017-11-01 by QN
        /// Tinh toan chien luoc tot nhat cho tat ca cac co phieu.
        /// Nen chay trong mot background process
        /// </summary>
        //public void createTrendScores()
        //{

        //    //1. Lay tat ca data
        //    //2. Tinh toan Trend Score cua mot Stock nao do

        //    //3. Ghi vao Databases


        //    //1.Lay tat ca stock trong database
        //    application.StrategyStatistics statistics;
        //    StringCollection stockCodeList = new StringCollection();
        //    //databases.baseDS.stockCodeDataTable stockCodeTable = new databases.baseDS.stockCodeDataTable();
        //    //databases.DbAccess.LoadData(stockCodeTable);
        //    //for (int i = 0; i < stockCodeTable.Count; i++)
        //    //    stockCodeList.Add(stockCodeTable[i].code);
        //    stockCodeList.Add("SSI");
        //    //stockCodeList.Add("DXG");

        //    //2. Lay tat ca cac Strategy
        //    databases.tmpDS.codeListDataTable strategyTable = new databases.tmpDS.codeListDataTable();
        //    LoadStrategy(strategyTable, AppTypes.StrategyTypes.Strategy);
        //    StringCollection strategyList = new StringCollection();
        //    for (int i = 0; i < strategyTable.Count; i++)
        //        strategyList.Add(strategyTable[i].code);
        //    //4. Lay Time Scale la D1
        //    AppTypes.TimeScale timeScale = AppTypes.TimeScaleFromCode("D1");

        //    //3. Lay tat ca cac Time Ranges (1 month, 3 month, 6 months...)
        //    //AppTypes.TimeRanges timeRange=AppTypes.m;
        //    StringCollection timeRangeList = new StringCollection();
        //    timeRangeList.Add("M1");
        //    timeRangeList.Add("M3");
        //    timeRangeList.Add("M6");
        //    timeRangeList.Add("Y1");
        //    timeRangeList.Add("Y2");
        //    timeRangeList.Add("Y3");

        //    decimal[] tile = { 0.1m, 0.1m, 0.2m, 0.3m, 0.2m, 0.1m };
        //    //None,W1,W2,W3,M1,M2,M3,M4,M5,M6,YTD,Y1,Y2,Y3,Y4,Y5,All

        //    EstimateOptions estimateOption = new EstimateOptions();

        //    decimal[,] matrixResult = new decimal[strategyList.Count, timeRangeList.Count + 1];

        //    StrategyDetail[] bestStrategy = new StrategyDetail[strategyList.Count];
        //    databases.baseDS.bestStrategyDataTable bestStrategyDataTable = new databases.baseDS.bestStrategyDataTable();
        //    databases.DbAccess.LoadData(bestStrategyDataTable);
        //    try
        //    {
        //        for (int rowId = 0; rowId < stockCodeList.Count; rowId++)
        //        {
        //            ((BackgroundWorker)sender).ReportProgress(rowId);
        //            for (int iStrategy = 0; iStrategy < strategyList.Count; iStrategy++)
        //            {
        //                bestStrategy[iStrategy] = new StrategyDetail(strategyList[iStrategy].ToString(), 0);
        //                for (int colId = 0; colId < timeRangeList.Count; colId++)
        //                {
        //                    AppTypes.TimeRanges timeRange = AppTypes.TimeRangeFromCode(timeRangeList[colId]);
        //                    commonClass.DataParams dataParm = new DataParams(timeScale.Code, timeRange, 0);

        //                    StrategyData.ClearCache();
        //                    AnalysisData analysisData = new AnalysisData(stockCodeList[rowId], dataParm);

        //                    TradePoints advices = AnalysisStrategy(analysisData, strategyList[iStrategy]);

        //                    if (advices != null)
        //                        matrixResult[iStrategy, colId] = EstimateTrading_Profit(analysisData, ToTradePointInfo(advices), estimateOption, out statistics);
        //                    else matrixResult[iStrategy, colId] = 0;
        //                }
        //                for (int colId = 0; colId < timeRangeList.Count; colId++)
        //                    bestStrategy[iStrategy].profit = matrixResult[iStrategy, colId] * tile[colId];
        //            }
        //            Array.Sort(bestStrategy, delegate (StrategyDetail s1, StrategyDetail s2)
        //            {
        //                return s1.profit.CompareTo(s2.profit);
        //            }
        //            );
        //            databases.baseDS.bestStrategyRow bestStrategyRow = bestStrategyDataTable.NewbestStrategyRow();
        //            bestStrategyRow.stockCode = stockCodeList[rowId];
        //            bestStrategyRow.timeFrame = timeScale.Code;
        //            bestStrategyRow.strategyCode1 = bestStrategy[strategyList.Count - 1].strategyName;
        //            bestStrategyRow.strategyCode2 = bestStrategy[strategyList.Count - 2].strategyName;
        //            bestStrategyRow.strategyCode3 = bestStrategy[strategyList.Count - 3].strategyName;

        //            //Console.WriteLine(stockCodeList[rowId] + " s1 name=" + bestStrategy[strategyList.Count - 1].strategyName + "profit=" + bestStrategy[strategyList.Count - 1].profit.ToString());

        //            //Write to database the result    
        //            //databases.baseDS.bestStrategyRow existingRow = databases.DbAccess.databases.baseDS.bestStrategyDataTable.FindBystockCodetimeFrame(bestStrategyRow.stockCode, bestStrategyRow.timeFrame);
        //            databases.baseDS.bestStrategyRow existingRow = databases.DbAccess.GetBestStrategy(bestStrategyRow.stockCode, bestStrategyRow.timeFrame);
        //            if (existingRow == null)
        //            {
        //                bestStrategyDataTable.Rows.Add(bestStrategyRow);
        //                databases.DbAccess.UpdateData(bestStrategyRow);
        //            }
        //            else
        //                if ((existingRow.strategyCode1 != bestStrategyRow.strategyCode1) ||
        //                    (existingRow.strategyCode2 != bestStrategyRow.strategyCode2) ||
        //                    (existingRow.strategyCode3 != bestStrategyRow.strategyCode3))
        //            {
        //                existingRow.strategyCode1 = bestStrategyRow.strategyCode1;
        //                existingRow.strategyCode2 = bestStrategyRow.strategyCode2;
        //                existingRow.strategyCode3 = bestStrategyRow.strategyCode3;
        //                databases.DbAccess.UpdateData(existingRow);
        //            }
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        commonClass.SysLibs.WriteSysLog(common.SysSeverityLevel.Error, "best Strategy", err);
        //    }

        //}
    //}
}
}
