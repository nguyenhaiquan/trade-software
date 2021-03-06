using System.Data.Entity;
using databaseEntity;
using System;
using Microsoft.Pex.Framework;

namespace databaseEntity
{
    /// <summary>A factory for databaseEntity.StockImportDB instances</summary>
    public static partial class StockImportDBFactory
    {
        /// <summary>A factory for databaseEntity.StockImportDB instances</summary>
        [PexFactoryMethod(typeof(StockImportDB))]
        public static StockImportDB Create(DbSet<importPrice> value_dbSet)
        {
            StockImportDB stockImportDB = new StockImportDB();
            stockImportDB.ImportPrices = value_dbSet;
            return stockImportDB;

            // TODO: Edit factory method of StockImportDB
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
