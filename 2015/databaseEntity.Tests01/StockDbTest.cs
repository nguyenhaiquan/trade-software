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
    [TestClass]
    public partial class StockDbTest
    {
        /// <summary>Test stub for AddInvestor(String, String, String)</summary>
        [PexMethod]
        public investor AddInvestorTest(
            [PexAssumeUnderTest]StockDb target,
            string username,
            string password,
            string email
        )
        {
            investor result = target.AddInvestor(username, password, email);
            return result;
            // TODO: add assertions to method StockDbTest.AddInvestorTest(StockDb, String, String, String)
        }

        /// <summary>Test stub for .ctor()</summary>
        [PexMethod]
        public StockDb ConstructorTest()
        {
            StockDb target = new StockDb();
            return target;
            // TODO: add assertions to method StockDbTest.ConstructorTest()
        }

        /// <summary>Test stub for DeleteInvestorStocks(String)</summary>
        [PexMethod]
        public bool DeleteInvestorStocksTest([PexAssumeUnderTest]StockDb target, string code)
        {
            bool result = target.DeleteInvestorStocks(code);
            return result;
            // TODO: add assertions to method StockDbTest.DeleteInvestorStocksTest(StockDb, String)
        }

        /// <summary>Test stub for DeletePriceDataSum(String)</summary>
        [PexMethod]
        public bool DeletePriceDataSumTest([PexAssumeUnderTest]StockDb target, string code)
        {
            bool result = target.DeletePriceDataSum(code);
            return result;
            // TODO: add assertions to method StockDbTest.DeletePriceDataSumTest(StockDb, String)
        }

        /// <summary>Test stub for DeletePriceData(String)</summary>
        [PexMethod]
        public bool DeletePriceDataTest([PexAssumeUnderTest]StockDb target, string code)
        {
            bool result = target.DeletePriceData(code);
            return result;
            // TODO: add assertions to method StockDbTest.DeletePriceDataTest(StockDb, String)
        }

        /// <summary>Test stub for DeleteStockCode(String)</summary>
        [PexMethod]
        public bool DeleteStockCodeTest([PexAssumeUnderTest]StockDb target, string stockcode)
        {
            bool result = target.DeleteStockCode(stockcode);
            return result;
            // TODO: add assertions to method StockDbTest.DeleteStockCodeTest(StockDb, String)
        }

        /// <summary>Test stub for DeleteTradeAlerts(String)</summary>
        [PexMethod]
        public bool DeleteTradeAlertsTest([PexAssumeUnderTest]StockDb target, string code)
        {
            bool result = target.DeleteTradeAlerts(code);
            return result;
            // TODO: add assertions to method StockDbTest.DeleteTradeAlertsTest(StockDb, String)
        }

        /// <summary>Test stub for DeleteTransactions(String)</summary>
        [PexMethod]
        public bool DeleteTransactionsTest([PexAssumeUnderTest]StockDb target, string code)
        {
            bool result = target.DeleteTransactions(code);
            return result;
            // TODO: add assertions to method StockDbTest.DeleteTransactionsTest(StockDb, String)
        }

        /// <summary>Test stub for GetAllStockCodes()</summary>
        [PexMethod]
        public DbSet<stockCode> GetAllStockCodesTest([PexAssumeUnderTest]StockDb target)
        {
            DbSet<stockCode> result = target.GetAllStockCodes();
            return result;
            // TODO: add assertions to method StockDbTest.GetAllStockCodesTest(StockDb)
        }

        /// <summary>Test stub for IsEmailExiste(String)</summary>
        [PexMethod]
        public bool IsEmailExisteTest([PexAssumeUnderTest]StockDb target, string email)
        {
            bool result = target.IsEmailExiste(email);
            return result;
            // TODO: add assertions to method StockDbTest.IsEmailExisteTest(StockDb, String)
        }

        /// <summary>Test stub for IsUserExiste(String)</summary>
        [PexMethod]
        public bool IsUserExisteTest([PexAssumeUnderTest]StockDb target, string username)
        {
            bool result = target.IsUserExiste(username);
            return result;
            // TODO: add assertions to method StockDbTest.IsUserExisteTest(StockDb, String)
        }

        /// <summary>Test stub for IsUserExisteWithPassword(String, String)</summary>
        [PexMethod]
        public bool IsUserExisteWithPasswordTest(
            [PexAssumeUnderTest]StockDb target,
            string username,
            string password
        )
        {
            bool result = target.IsUserExisteWithPassword(username, password);
            return result;
            // TODO: add assertions to method StockDbTest.IsUserExisteWithPasswordTest(StockDb, String, String)
        }
    }
}
