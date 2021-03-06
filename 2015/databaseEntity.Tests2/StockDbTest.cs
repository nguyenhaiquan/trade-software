using System;
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
        /// <summary>Test stub for DeleteInvestorStocks(String)</summary>
        [PexMethod]
        public bool DeleteInvestorStocksTest([PexAssumeUnderTest]StockDb target, string code)
        {
            bool result = target.DeleteInvestorStocks(code);
            return result;
            // TODO: add assertions to method StockDbTest.DeleteInvestorStocksTest(StockDb, String)
        }
    }
}
