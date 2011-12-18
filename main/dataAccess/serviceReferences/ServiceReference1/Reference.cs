﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IStockService")]
    public interface IStockService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/LoadAnalysisData", ReplyAction="http://tempuri.org/IStockService/LoadAnalysisDataResponse")]
        string LoadAnalysisData(commonClass.AppTypes.TimeRanges timeRange, string timeScaleCode, string code, bool forceReadNew);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetAnalysis_Data_ByKey", ReplyAction="http://tempuri.org/IStockService/GetAnalysis_Data_ByKeyResponse")]
        data.baseDS.priceDataDataTable GetAnalysis_Data_ByKey(out int firstData, string dataKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetAnalysis_Data", ReplyAction="http://tempuri.org/IStockService/GetAnalysis_DataResponse")]
        data.baseDS.priceDataDataTable GetAnalysis_Data(out int firstData, commonClass.AppTypes.TimeRanges timeRanges, string timeScaleCode, string stockCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/Estimate_Matrix_Profit", ReplyAction="http://tempuri.org/IStockService/Estimate_Matrix_ProfitResponse")]
        decimal[][] Estimate_Matrix_Profit(commonClass.AppTypes.TimeRanges timeRange, string timeScaleCode, string[] stockCodeList, string[] strategyList, commonClass.EstimateOptions option);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/Estimate_Matrix_LastBizWeight", ReplyAction="http://tempuri.org/IStockService/Estimate_Matrix_LastBizWeightResponse")]
        double[][] Estimate_Matrix_LastBizWeight(commonClass.AppTypes.TimeRanges timeRange, string timeScaleCode, string[] stockCodeList, string[] strategyList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/Analysis", ReplyAction="http://tempuri.org/IStockService/AnalysisResponse")]
        commonClass.TradePointInfo[] Analysis(string dataKey, string strategyCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/MakeTransaction", ReplyAction="http://tempuri.org/IStockService/MakeTransactionResponse")]
        data.baseDS.transactionsDataTable MakeTransaction(out string errorText, commonClass.AppTypes.TradeActions type, string stockCode, string portfolioCode, int qty, decimal feePerc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetTradePointWithEstimationDetail", ReplyAction="http://tempuri.org/IStockService/GetTradePointWithEstimationDetailResponse")]
        commonClass.TradePointInfo[] GetTradePointWithEstimationDetail(out data.tmpDS.tradeEstimateDataTable toTbl, commonClass.AppTypes.TimeRanges timeRange, string timeScaleCode, string stockCode, string strategyCode, commonClass.EstimateOptions options);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetXml", ReplyAction="http://tempuri.org/IStockService/GetXmlResponse")]
        string GetXml(string filePath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/Load_Global_Settings", ReplyAction="http://tempuri.org/IStockService/Load_Global_SettingsResponse")]
        void Load_Global_Settings();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/Save_Global_Settings", ReplyAction="http://tempuri.org/IStockService/Save_Global_SettingsResponse")]
        void Save_Global_Settings();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetBizSubSectorByIndustry", ReplyAction="http://tempuri.org/IStockService/GetBizSubSectorByIndustryResponse")]
        data.baseDS.bizSubSectorDataTable GetBizSubSectorByIndustry(string industryCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetBizSubSectorBySuperSector", ReplyAction="http://tempuri.org/IStockService/GetBizSubSectorBySuperSectorResponse")]
        data.baseDS.bizSubSectorDataTable GetBizSubSectorBySuperSector(string superSectorCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetBizSubSectorBySector", ReplyAction="http://tempuri.org/IStockService/GetBizSubSectorBySectorResponse")]
        data.baseDS.bizSubSectorDataTable GetBizSubSectorBySector(string sectorCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetCountry", ReplyAction="http://tempuri.org/IStockService/GetCountryResponse")]
        data.baseDS.countryDataTable GetCountry();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetCurrency", ReplyAction="http://tempuri.org/IStockService/GetCurrencyResponse")]
        data.baseDS.currencyDataTable GetCurrency();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetInvestorCat", ReplyAction="http://tempuri.org/IStockService/GetInvestorCatResponse")]
        data.baseDS.investorCatDataTable GetInvestorCat();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetSysCode", ReplyAction="http://tempuri.org/IStockService/GetSysCodeResponse")]
        data.baseDS.sysCodeDataTable GetSysCode(string catCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetSysCodeCat", ReplyAction="http://tempuri.org/IStockService/GetSysCodeCatResponse")]
        data.baseDS.sysCodeCatDataTable GetSysCodeCat();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetInvestor_ByCode", ReplyAction="http://tempuri.org/IStockService/GetInvestor_ByCodeResponse")]
        data.baseDS.investorDataTable GetInvestor_ByCode(string code);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetInvestor_BySQL", ReplyAction="http://tempuri.org/IStockService/GetInvestor_BySQLResponse")]
        data.baseDS.investorDataTable GetInvestor_BySQL(string sql);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetStock_InPortfolio", ReplyAction="http://tempuri.org/IStockService/GetStock_InPortfolioResponse")]
        data.tmpDS.stockCodeDataTable GetStock_InPortfolio(string[] portfolios);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetStock_ByBizSector", ReplyAction="http://tempuri.org/IStockService/GetStock_ByBizSectorResponse")]
        data.tmpDS.stockCodeDataTable GetStock_ByBizSector(string[] bizSector);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetBizSubSector_ByIndustry", ReplyAction="http://tempuri.org/IStockService/GetBizSubSector_ByIndustryResponse")]
        data.baseDS.bizSubSectorDataTable GetBizSubSector_ByIndustry(string code);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetBizSubSector_BySuperSector", ReplyAction="http://tempuri.org/IStockService/GetBizSubSector_BySuperSectorResponse")]
        data.baseDS.bizSubSectorDataTable GetBizSubSector_BySuperSector(string code);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetBizSubSector_BySector", ReplyAction="http://tempuri.org/IStockService/GetBizSubSector_BySectorResponse")]
        data.baseDS.bizSubSectorDataTable GetBizSubSector_BySector(string code);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetPortfolio_ByInvestorAndType", ReplyAction="http://tempuri.org/IStockService/GetPortfolio_ByInvestorAndTypeResponse")]
        data.baseDS.portfolioDataTable GetPortfolio_ByInvestorAndType(string investorCode, commonClass.AppTypes.PortfolioTypes type);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetPortfolio_ByType", ReplyAction="http://tempuri.org/IStockService/GetPortfolio_ByTypeResponse")]
        data.baseDS.portfolioDataTable GetPortfolio_ByType(commonClass.AppTypes.PortfolioTypes type);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetPortfolio_ByCode", ReplyAction="http://tempuri.org/IStockService/GetPortfolio_ByCodeResponse")]
        data.baseDS.portfolioDataTable GetPortfolio_ByCode(string portfolioCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetPortfolio_ByInvestor", ReplyAction="http://tempuri.org/IStockService/GetPortfolio_ByInvestorResponse")]
        data.baseDS.portfolioDataTable GetPortfolio_ByInvestor(string investorCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetTradeAlert_BySQL", ReplyAction="http://tempuri.org/IStockService/GetTradeAlert_BySQLResponse")]
        data.baseDS.tradeAlertDataTable GetTradeAlert_BySQL(string alertSql);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetTransaction_BySQL", ReplyAction="http://tempuri.org/IStockService/GetTransaction_BySQLResponse")]
        data.baseDS.transactionsDataTable GetTransaction_BySQL(string transSql);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetPortfolioDetail_ByType", ReplyAction="http://tempuri.org/IStockService/GetPortfolioDetail_ByTypeResponse")]
        data.baseDS.portfolioDetailDataTable GetPortfolioDetail_ByType(commonClass.AppTypes.PortfolioTypes[] types);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetPortfolioDetail_ByCode", ReplyAction="http://tempuri.org/IStockService/GetPortfolioDetail_ByCodeResponse")]
        data.baseDS.portfolioDetailDataTable GetPortfolioDetail_ByCode(string portfolioCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetOwnedStock", ReplyAction="http://tempuri.org/IStockService/GetOwnedStockResponse")]
        data.baseDS.investorStockDataTable GetOwnedStock(string portfolioCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetLastPrice", ReplyAction="http://tempuri.org/IStockService/GetLastPriceResponse")]
        data.baseDS.priceDataDataTable GetLastPrice();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetLastAlertTime", ReplyAction="http://tempuri.org/IStockService/GetLastAlertTimeResponse")]
        System.DateTime GetLastAlertTime(string investorCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetData_ByTimeScale_Code_FrDate", ReplyAction="http://tempuri.org/IStockService/GetData_ByTimeScale_Code_FrDateResponse")]
        data.baseDS.priceDataDataTable GetData_ByTimeScale_Code_FrDate(string timeScaleCode, string stockCode, System.DateTime fromDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetData_ByTimeScale_Code_DateRange", ReplyAction="http://tempuri.org/IStockService/GetData_ByTimeScale_Code_DateRangeResponse")]
        data.baseDS.priceDataDataTable GetData_ByTimeScale_Code_DateRange(string timeScaleCode, string stockCode, System.DateTime frDate, System.DateTime toDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetMarketData_BySQL", ReplyAction="http://tempuri.org/IStockService/GetMarketData_BySQLResponse")]
        data.tmpDS.marketDataDataTable GetMarketData_BySQL(string sqlCmd);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetData_TotalRow", ReplyAction="http://tempuri.org/IStockService/GetData_TotalRowResponse")]
        int GetData_TotalRow(string timeScaleCode, string stockCode, System.DateTime frDate, System.DateTime toDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetTransactionInfo", ReplyAction="http://tempuri.org/IStockService/GetTransactionInfoResponse")]
        bool GetTransactionInfo(ref commonClass.TransactionInfo transInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetPriceByCode", ReplyAction="http://tempuri.org/IStockService/GetPriceByCodeResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(string[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(decimal[][]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(decimal[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(double[][]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(double[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(commonClass.AppTypes.TimeRanges))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(commonClass.EstimateOptions))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(commonClass.TradePointInfo[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(commonClass.TradePointInfo))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(commonClass.BusinessInfo))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(commonClass.AppTypes.MarketTrend))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(commonClass.AppTypes.TradeActions))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(commonClass.AppTypes.PortfolioTypes))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(commonClass.AppTypes.PortfolioTypes[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(commonClass.TransactionInfo))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(commonClass.AppTypes.CommonStatus))]
        object[] GetPriceByCode(string stockCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/Reset", ReplyAction="http://tempuri.org/IStockService/ResetResponse")]
        void Reset();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/ClearCache", ReplyAction="http://tempuri.org/IStockService/ClearCacheResponse")]
        void ClearCache();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/DeleteCache", ReplyAction="http://tempuri.org/IStockService/DeleteCacheResponse")]
        void DeleteCache(string cacheName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetServerDateTime", ReplyAction="http://tempuri.org/IStockService/GetServerDateTimeResponse")]
        System.DateTime GetServerDateTime();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/UpdateSysCodeCat", ReplyAction="http://tempuri.org/IStockService/UpdateSysCodeCatResponse")]
        void UpdateSysCodeCat(ref data.baseDS.sysCodeCatDataTable sysCodeCatTbl);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/UpdateSysCode", ReplyAction="http://tempuri.org/IStockService/UpdateSysCodeResponse")]
        void UpdateSysCode(ref data.baseDS.sysCodeDataTable sysCodeTbl);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/UpdateStock", ReplyAction="http://tempuri.org/IStockService/UpdateStockResponse")]
        void UpdateStock(ref data.baseDS.stockCodeDataTable stockCodeTbl);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/UpdateInvestor", ReplyAction="http://tempuri.org/IStockService/UpdateInvestorResponse")]
        void UpdateInvestor(ref data.baseDS.investorDataTable investorTbl);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/UpdatePortfolio", ReplyAction="http://tempuri.org/IStockService/UpdatePortfolioResponse")]
        void UpdatePortfolio(ref data.baseDS.portfolioDataTable portfolioTbl);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/UpdatePortfolioDetail", ReplyAction="http://tempuri.org/IStockService/UpdatePortfolioDetailResponse")]
        void UpdatePortfolioDetail(ref data.baseDS.portfolioDetailDataTable portfolioDetailTbl);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/UpdateStockExchange", ReplyAction="http://tempuri.org/IStockService/UpdateStockExchangeResponse")]
        void UpdateStockExchange(ref data.baseDS.stockExchangeDataTable stockExchangeTbl);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/UpdateTransactions", ReplyAction="http://tempuri.org/IStockService/UpdateTransactionsResponse")]
        void UpdateTransactions(ref data.baseDS.transactionsDataTable transactionsTbl);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/UpdateInvestorStock", ReplyAction="http://tempuri.org/IStockService/UpdateInvestorStockResponse")]
        void UpdateInvestorStock(ref data.baseDS.investorStockDataTable investorStockTbl);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/UpdateTradeAlert", ReplyAction="http://tempuri.org/IStockService/UpdateTradeAlertResponse")]
        void UpdateTradeAlert(ref data.baseDS.tradeAlertDataTable tradeAlertTbl);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/UpdateSysAutoKeyPending", ReplyAction="http://tempuri.org/IStockService/UpdateSysAutoKeyPendingResponse")]
        void UpdateSysAutoKeyPending(ref data.baseDS.sysAutoKeyPendingDataTable sysAutoKeyPendingTbl);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/DeleteStock", ReplyAction="http://tempuri.org/IStockService/DeleteStockResponse")]
        void DeleteStock(string stockCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/DeleteStockExchange", ReplyAction="http://tempuri.org/IStockService/DeleteStockExchangeResponse")]
        void DeleteStockExchange(string code);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/DeleteInvestor", ReplyAction="http://tempuri.org/IStockService/DeleteInvestorResponse")]
        void DeleteInvestor(string investorCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/DeletePortfolio", ReplyAction="http://tempuri.org/IStockService/DeletePortfolioResponse")]
        void DeletePortfolio(string portfolioCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/DeleteSysCodeCat", ReplyAction="http://tempuri.org/IStockService/DeleteSysCodeCatResponse")]
        void DeleteSysCodeCat(string catCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/DeleteSysCode", ReplyAction="http://tempuri.org/IStockService/DeleteSysCodeResponse")]
        void DeleteSysCode(string catCode, string code);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/DeleteTradeAlert", ReplyAction="http://tempuri.org/IStockService/DeleteTradeAlertResponse")]
        void DeleteTradeAlert(int alertId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetStockByStatus", ReplyAction="http://tempuri.org/IStockService/GetStockByStatusResponse")]
        data.tmpDS.stockCodeDataTable GetStockByStatus(commonClass.AppTypes.CommonStatus status);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetStockFull", ReplyAction="http://tempuri.org/IStockService/GetStockFullResponse")]
        data.baseDS.stockCodeDataTable GetStockFull();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetStockList_ByWatchList", ReplyAction="http://tempuri.org/IStockService/GetStockList_ByWatchListResponse")]
        string[] GetStockList_ByWatchList(string[] watchList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetStockList_ByBizSector", ReplyAction="http://tempuri.org/IStockService/GetStockList_ByBizSectorResponse")]
        string[] GetStockList_ByBizSector(string[] sectors);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetStockExchange", ReplyAction="http://tempuri.org/IStockService/GetStockExchangeResponse")]
        data.baseDS.stockExchangeDataTable GetStockExchange();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetEmployeeRange", ReplyAction="http://tempuri.org/IStockService/GetEmployeeRangeResponse")]
        data.baseDS.employeeRangeDataTable GetEmployeeRange();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetBizIndustry", ReplyAction="http://tempuri.org/IStockService/GetBizIndustryResponse")]
        data.baseDS.bizIndustryDataTable GetBizIndustry();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetBizSuperSector", ReplyAction="http://tempuri.org/IStockService/GetBizSuperSectorResponse")]
        data.baseDS.bizSuperSectorDataTable GetBizSuperSector();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetBizSector", ReplyAction="http://tempuri.org/IStockService/GetBizSectorResponse")]
        data.baseDS.bizSectorDataTable GetBizSector();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetBizSubSector", ReplyAction="http://tempuri.org/IStockService/GetBizSubSectorResponse")]
        data.baseDS.bizSubSectorDataTable GetBizSubSector();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IStockServiceChannel : DataAccess.ServiceReference1.IStockService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class StockServiceClient : System.ServiceModel.ClientBase<DataAccess.ServiceReference1.IStockService>, DataAccess.ServiceReference1.IStockService {
        
        public StockServiceClient() {
        }
        
        public StockServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public StockServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StockServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StockServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string LoadAnalysisData(commonClass.AppTypes.TimeRanges timeRange, string timeScaleCode, string code, bool forceReadNew) {
            return base.Channel.LoadAnalysisData(timeRange, timeScaleCode, code, forceReadNew);
        }
        
        public data.baseDS.priceDataDataTable GetAnalysis_Data_ByKey(out int firstData, string dataKey) {
            return base.Channel.GetAnalysis_Data_ByKey(out firstData, dataKey);
        }
        
        public data.baseDS.priceDataDataTable GetAnalysis_Data(out int firstData, commonClass.AppTypes.TimeRanges timeRanges, string timeScaleCode, string stockCode) {
            return base.Channel.GetAnalysis_Data(out firstData, timeRanges, timeScaleCode, stockCode);
        }
        
        public decimal[][] Estimate_Matrix_Profit(commonClass.AppTypes.TimeRanges timeRange, string timeScaleCode, string[] stockCodeList, string[] strategyList, commonClass.EstimateOptions option) {
            return base.Channel.Estimate_Matrix_Profit(timeRange, timeScaleCode, stockCodeList, strategyList, option);
        }
        
        public double[][] Estimate_Matrix_LastBizWeight(commonClass.AppTypes.TimeRanges timeRange, string timeScaleCode, string[] stockCodeList, string[] strategyList) {
            return base.Channel.Estimate_Matrix_LastBizWeight(timeRange, timeScaleCode, stockCodeList, strategyList);
        }
        
        public commonClass.TradePointInfo[] Analysis(string dataKey, string strategyCode) {
            return base.Channel.Analysis(dataKey, strategyCode);
        }
        
        public data.baseDS.transactionsDataTable MakeTransaction(out string errorText, commonClass.AppTypes.TradeActions type, string stockCode, string portfolioCode, int qty, decimal feePerc) {
            return base.Channel.MakeTransaction(out errorText, type, stockCode, portfolioCode, qty, feePerc);
        }
        
        public commonClass.TradePointInfo[] GetTradePointWithEstimationDetail(out data.tmpDS.tradeEstimateDataTable toTbl, commonClass.AppTypes.TimeRanges timeRange, string timeScaleCode, string stockCode, string strategyCode, commonClass.EstimateOptions options) {
            return base.Channel.GetTradePointWithEstimationDetail(out toTbl, timeRange, timeScaleCode, stockCode, strategyCode, options);
        }
        
        public string GetXml(string filePath) {
            return base.Channel.GetXml(filePath);
        }
        
        public void Load_Global_Settings() {
            base.Channel.Load_Global_Settings();
        }
        
        public void Save_Global_Settings() {
            base.Channel.Save_Global_Settings();
        }
        
        public data.baseDS.bizSubSectorDataTable GetBizSubSectorByIndustry(string industryCode) {
            return base.Channel.GetBizSubSectorByIndustry(industryCode);
        }
        
        public data.baseDS.bizSubSectorDataTable GetBizSubSectorBySuperSector(string superSectorCode) {
            return base.Channel.GetBizSubSectorBySuperSector(superSectorCode);
        }
        
        public data.baseDS.bizSubSectorDataTable GetBizSubSectorBySector(string sectorCode) {
            return base.Channel.GetBizSubSectorBySector(sectorCode);
        }
        
        public data.baseDS.countryDataTable GetCountry() {
            return base.Channel.GetCountry();
        }
        
        public data.baseDS.currencyDataTable GetCurrency() {
            return base.Channel.GetCurrency();
        }
        
        public data.baseDS.investorCatDataTable GetInvestorCat() {
            return base.Channel.GetInvestorCat();
        }
        
        public data.baseDS.sysCodeDataTable GetSysCode(string catCode) {
            return base.Channel.GetSysCode(catCode);
        }
        
        public data.baseDS.sysCodeCatDataTable GetSysCodeCat() {
            return base.Channel.GetSysCodeCat();
        }
        
        public data.baseDS.investorDataTable GetInvestor_ByCode(string code) {
            return base.Channel.GetInvestor_ByCode(code);
        }
        
        public data.baseDS.investorDataTable GetInvestor_BySQL(string sql) {
            return base.Channel.GetInvestor_BySQL(sql);
        }
        
        public data.tmpDS.stockCodeDataTable GetStock_InPortfolio(string[] portfolios) {
            return base.Channel.GetStock_InPortfolio(portfolios);
        }
        
        public data.tmpDS.stockCodeDataTable GetStock_ByBizSector(string[] bizSector) {
            return base.Channel.GetStock_ByBizSector(bizSector);
        }
        
        public data.baseDS.bizSubSectorDataTable GetBizSubSector_ByIndustry(string code) {
            return base.Channel.GetBizSubSector_ByIndustry(code);
        }
        
        public data.baseDS.bizSubSectorDataTable GetBizSubSector_BySuperSector(string code) {
            return base.Channel.GetBizSubSector_BySuperSector(code);
        }
        
        public data.baseDS.bizSubSectorDataTable GetBizSubSector_BySector(string code) {
            return base.Channel.GetBizSubSector_BySector(code);
        }
        
        public data.baseDS.portfolioDataTable GetPortfolio_ByInvestorAndType(string investorCode, commonClass.AppTypes.PortfolioTypes type) {
            return base.Channel.GetPortfolio_ByInvestorAndType(investorCode, type);
        }
        
        public data.baseDS.portfolioDataTable GetPortfolio_ByType(commonClass.AppTypes.PortfolioTypes type) {
            return base.Channel.GetPortfolio_ByType(type);
        }
        
        public data.baseDS.portfolioDataTable GetPortfolio_ByCode(string portfolioCode) {
            return base.Channel.GetPortfolio_ByCode(portfolioCode);
        }
        
        public data.baseDS.portfolioDataTable GetPortfolio_ByInvestor(string investorCode) {
            return base.Channel.GetPortfolio_ByInvestor(investorCode);
        }
        
        public data.baseDS.tradeAlertDataTable GetTradeAlert_BySQL(string alertSql) {
            return base.Channel.GetTradeAlert_BySQL(alertSql);
        }
        
        public data.baseDS.transactionsDataTable GetTransaction_BySQL(string transSql) {
            return base.Channel.GetTransaction_BySQL(transSql);
        }
        
        public data.baseDS.portfolioDetailDataTable GetPortfolioDetail_ByType(commonClass.AppTypes.PortfolioTypes[] types) {
            return base.Channel.GetPortfolioDetail_ByType(types);
        }
        
        public data.baseDS.portfolioDetailDataTable GetPortfolioDetail_ByCode(string portfolioCode) {
            return base.Channel.GetPortfolioDetail_ByCode(portfolioCode);
        }
        
        public data.baseDS.investorStockDataTable GetOwnedStock(string portfolioCode) {
            return base.Channel.GetOwnedStock(portfolioCode);
        }
        
        public data.baseDS.priceDataDataTable GetLastPrice() {
            return base.Channel.GetLastPrice();
        }
        
        public System.DateTime GetLastAlertTime(string investorCode) {
            return base.Channel.GetLastAlertTime(investorCode);
        }
        
        public data.baseDS.priceDataDataTable GetData_ByTimeScale_Code_FrDate(string timeScaleCode, string stockCode, System.DateTime fromDate) {
            return base.Channel.GetData_ByTimeScale_Code_FrDate(timeScaleCode, stockCode, fromDate);
        }
        
        public data.baseDS.priceDataDataTable GetData_ByTimeScale_Code_DateRange(string timeScaleCode, string stockCode, System.DateTime frDate, System.DateTime toDate) {
            return base.Channel.GetData_ByTimeScale_Code_DateRange(timeScaleCode, stockCode, frDate, toDate);
        }
        
        public data.tmpDS.marketDataDataTable GetMarketData_BySQL(string sqlCmd) {
            return base.Channel.GetMarketData_BySQL(sqlCmd);
        }
        
        public int GetData_TotalRow(string timeScaleCode, string stockCode, System.DateTime frDate, System.DateTime toDate) {
            return base.Channel.GetData_TotalRow(timeScaleCode, stockCode, frDate, toDate);
        }
        
        public bool GetTransactionInfo(ref commonClass.TransactionInfo transInfo) {
            return base.Channel.GetTransactionInfo(ref transInfo);
        }
        
        public object[] GetPriceByCode(string stockCode) {
            return base.Channel.GetPriceByCode(stockCode);
        }
        
        public void Reset() {
            base.Channel.Reset();
        }
        
        public void ClearCache() {
            base.Channel.ClearCache();
        }
        
        public void DeleteCache(string cacheName) {
            base.Channel.DeleteCache(cacheName);
        }
        
        public System.DateTime GetServerDateTime() {
            return base.Channel.GetServerDateTime();
        }
        
        public void UpdateSysCodeCat(ref data.baseDS.sysCodeCatDataTable sysCodeCatTbl) {
            base.Channel.UpdateSysCodeCat(ref sysCodeCatTbl);
        }
        
        public void UpdateSysCode(ref data.baseDS.sysCodeDataTable sysCodeTbl) {
            base.Channel.UpdateSysCode(ref sysCodeTbl);
        }
        
        public void UpdateStock(ref data.baseDS.stockCodeDataTable stockCodeTbl) {
            base.Channel.UpdateStock(ref stockCodeTbl);
        }
        
        public void UpdateInvestor(ref data.baseDS.investorDataTable investorTbl) {
            base.Channel.UpdateInvestor(ref investorTbl);
        }
        
        public void UpdatePortfolio(ref data.baseDS.portfolioDataTable portfolioTbl) {
            base.Channel.UpdatePortfolio(ref portfolioTbl);
        }
        
        public void UpdatePortfolioDetail(ref data.baseDS.portfolioDetailDataTable portfolioDetailTbl) {
            base.Channel.UpdatePortfolioDetail(ref portfolioDetailTbl);
        }
        
        public void UpdateStockExchange(ref data.baseDS.stockExchangeDataTable stockExchangeTbl) {
            base.Channel.UpdateStockExchange(ref stockExchangeTbl);
        }
        
        public void UpdateTransactions(ref data.baseDS.transactionsDataTable transactionsTbl) {
            base.Channel.UpdateTransactions(ref transactionsTbl);
        }
        
        public void UpdateInvestorStock(ref data.baseDS.investorStockDataTable investorStockTbl) {
            base.Channel.UpdateInvestorStock(ref investorStockTbl);
        }
        
        public void UpdateTradeAlert(ref data.baseDS.tradeAlertDataTable tradeAlertTbl) {
            base.Channel.UpdateTradeAlert(ref tradeAlertTbl);
        }
        
        public void UpdateSysAutoKeyPending(ref data.baseDS.sysAutoKeyPendingDataTable sysAutoKeyPendingTbl) {
            base.Channel.UpdateSysAutoKeyPending(ref sysAutoKeyPendingTbl);
        }
        
        public void DeleteStock(string stockCode) {
            base.Channel.DeleteStock(stockCode);
        }
        
        public void DeleteStockExchange(string code) {
            base.Channel.DeleteStockExchange(code);
        }
        
        public void DeleteInvestor(string investorCode) {
            base.Channel.DeleteInvestor(investorCode);
        }
        
        public void DeletePortfolio(string portfolioCode) {
            base.Channel.DeletePortfolio(portfolioCode);
        }
        
        public void DeleteSysCodeCat(string catCode) {
            base.Channel.DeleteSysCodeCat(catCode);
        }
        
        public void DeleteSysCode(string catCode, string code) {
            base.Channel.DeleteSysCode(catCode, code);
        }
        
        public void DeleteTradeAlert(int alertId) {
            base.Channel.DeleteTradeAlert(alertId);
        }
        
        public data.tmpDS.stockCodeDataTable GetStockByStatus(commonClass.AppTypes.CommonStatus status) {
            return base.Channel.GetStockByStatus(status);
        }
        
        public data.baseDS.stockCodeDataTable GetStockFull() {
            return base.Channel.GetStockFull();
        }
        
        public string[] GetStockList_ByWatchList(string[] watchList) {
            return base.Channel.GetStockList_ByWatchList(watchList);
        }
        
        public string[] GetStockList_ByBizSector(string[] sectors) {
            return base.Channel.GetStockList_ByBizSector(sectors);
        }
        
        public data.baseDS.stockExchangeDataTable GetStockExchange() {
            return base.Channel.GetStockExchange();
        }
        
        public data.baseDS.employeeRangeDataTable GetEmployeeRange() {
            return base.Channel.GetEmployeeRange();
        }
        
        public data.baseDS.bizIndustryDataTable GetBizIndustry() {
            return base.Channel.GetBizIndustry();
        }
        
        public data.baseDS.bizSuperSectorDataTable GetBizSuperSector() {
            return base.Channel.GetBizSuperSector();
        }
        
        public data.baseDS.bizSectorDataTable GetBizSector() {
            return base.Channel.GetBizSector();
        }
        
        public data.baseDS.bizSubSectorDataTable GetBizSubSector() {
            return base.Channel.GetBizSubSector();
        }
    }
}
