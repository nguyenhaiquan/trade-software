using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using databaseEntity;

namespace databaseEntity.Tests
{
    /// <summary>This class contains parameterized unit tests for StockImportDB</summary>
    [PexClass(typeof(StockImportDB))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class StockImportDBTest
    {
        /// <summary>Test stub for .ctor()</summary>
        [PexMethod]
        public StockImportDB ConstructorTest()
        {
            StockImportDB target = new StockImportDB();
            return target;
            // TODO: add assertions to method StockImportDBTest.ConstructorTest()
        }

        [PexMethod]
        public importPrice GetByDateAndCodeDescending(
            [PexAssumeUnderTest]StockImportDB target,
            DateTime start,
            DateTime end,
            string code
        )
        {
            importPrice result = target.GetByDateAndCodeDescending(new DateTime(2018,08,24), new DateTime(2018,08,25), "VN30F1809");
            Assert.AreEqual(result.totalVolume,73122);
            return result;
            // TODO: add assertions to method StockImportDBTest.GetByDateAndCodeDescending(StockImportDB, DateTime, DateTime, String)
        }
    }
}
