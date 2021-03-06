using System;
using System.Data.Entity;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using databaseEntity;

namespace databaseEntity.Tests
{
    /// <summary>This class contains parameterized unit tests for StockDb</summary>
    [PexClass(typeof(StockDb))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClassAttribute]
    public partial class StockDbTest
    {
        /// <summary>Test stub for GetAllStockCodes()</summary>
        [PexMethod]
        public DbSet<stockCode> GetAllStockCodesTest([PexAssumeUnderTest]StockDb target)
        {
            DbSet<stockCode> result = target.GetAllStockCodes();
            return result;
            // TODO: add assertions to method StockDbTest.GetAllStockCodesTest(StockDb)
        }
    }
}
