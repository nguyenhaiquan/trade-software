using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using databaseEntity;

namespace databaseEntity.Tests
{
    [TestClass]
    [PexClass(typeof(StockDb))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class StockDbTest
    {

        [PexMethod(MaxBranches = 20000)]
        public investor AddInvestor(
            [PexAssumeUnderTest]StockDb target,
            string username,
            string password,
            string email
        )
        {
            username = "Quan20180826";
            password = "123";
            email = username+"@gmail.com";
            investor result = target.AddInvestor(username, password, email);
            return result;
            // TODO: add assertions to method StockDbTest.AddInvestor(StockDb, String, String, String)
        }
    }
}
