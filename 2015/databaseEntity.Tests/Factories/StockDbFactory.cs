using System.Data.Entity;
using databaseEntity;
using System;
using Microsoft.Pex.Framework;

namespace databaseEntity
{
    /// <summary>A factory for databaseEntity.StockDb instances</summary>
    public static partial class StockDbFactory
    {
        /// <summary>A factory for databaseEntity.StockDb instances</summary>
        [PexFactoryMethod(typeof(StockDb))]
        public static StockDb Create(
            DbSet<investor> value_dbSet,
            DbSet<investorStock> value_dbSet1_,
            DbSet<stockCode> value_dbSet2_,
            DbSet<tradeAlert> value_dbSet3_,
            DbSet<priceDataSum> value_dbSet4_,
            DbSet<priceData> value_dbSet5_,
            DbSet<transaction> value_dbSet6_
        )
        {
            StockDb stockDb = new StockDb();
            stockDb.Investors = value_dbSet;
            stockDb.InvestorStocks = value_dbSet1_;
            stockDb.StockCodes = value_dbSet2_;
            stockDb.TradeAlerts = value_dbSet3_;
            stockDb.PriceDataSums = value_dbSet4_;
            stockDb.PriceData = value_dbSet5_;
            stockDb.Transactions = value_dbSet6_;
            return stockDb;

            // TODO: Edit factory method of StockDb
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
