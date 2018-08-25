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
    }
}
