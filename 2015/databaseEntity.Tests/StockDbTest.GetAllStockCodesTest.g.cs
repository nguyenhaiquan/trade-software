using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using databaseEntity;
using Microsoft.Pex.Framework.Generated;
// <auto-generated>
// This file contains automatically generated tests.
// Do not modify this file manually.
// 
// If the contents of this file becomes outdated, you can delete it.
// For example, if it no longer compiles.
// </auto-generated>
using System;

namespace databaseEntity.Tests
{
    public partial class StockDbTest
    {

[TestMethodAttribute]
[PexGeneratedBy(typeof(StockDbTest))]
[IgnoreAttribute]
[PexDescription("the test state was: path bounds exceeded")]
public void GetAllStockCodesTest01()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      StockDb stockDb;
      DbSet<stockCode> dbSet;
      stockDb = new StockDb();
      stockDb.Investors = (DbSet<investor>)null;
      stockDb.InvestorStocks = (DbSet<investorStock>)null;
      stockDb.StockCodes = (DbSet<stockCode>)null;
      stockDb.TradeAlerts = (DbSet<tradeAlert>)null;
      stockDb.PriceDataSums = (DbSet<priceDataSum>)null;
      stockDb.PriceData = (DbSet<priceData>)null;
      stockDb.Transactions = (DbSet<transaction>)null;
      disposables.Add((IDisposable)stockDb);
      dbSet = this.GetAllStockCodesTest(stockDb);
      disposables.Dispose();
    }
}

[TestMethodAttribute]
[PexGeneratedBy(typeof(StockDbTest))]
[IgnoreAttribute]
[PexDescription("the test state was: path bounds exceeded")]
public void GetAllStockCodesTest02()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      StockDb stockDb;
      DbSet<stockCode> dbSet;
      stockDb = new StockDb();
      stockDb.Investors = (DbSet<investor>)null;
      stockDb.InvestorStocks = (DbSet<investorStock>)null;
      stockDb.StockCodes = (DbSet<stockCode>)null;
      stockDb.TradeAlerts = (DbSet<tradeAlert>)null;
      stockDb.PriceDataSums = (DbSet<priceDataSum>)null;
      stockDb.PriceData = (DbSet<priceData>)null;
      stockDb.Transactions = (DbSet<transaction>)null;
      disposables.Add((IDisposable)stockDb);
      dbSet = this.GetAllStockCodesTest(stockDb);
      disposables.Dispose();
    }
}
    }
}
