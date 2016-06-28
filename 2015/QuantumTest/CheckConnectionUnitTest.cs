using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using commonTypes;

namespace QuantumTest
{
    [TestClass]
    public class CheckConnectionUnitTest 
    {
        /// <summary>
        /// Test for connection using one specific account
        /// Account: quan_nh
        /// Password: 3
        /// </summary>
        [TestMethod]
        public void TestCheckConnection()
        {
            string account = "dungvq";
            string password = "";

            //Load all setting
            common.Settings.sysProductOwner = Consts.constProductOwner;
            common.Settings.sysProductCode = Consts.constProductCode;

            common.Settings.sysConfigFile = "client.xml";
            //common.Settings.myWsConInfos = new common.wsConnectionInfo();

            common.wsConnectionInfo ws = new common.wsConnectionInfo();
            ws.account = "test"; ws.password = "123";
            ws.isWindowAuthentication = true;
            ws.proxyAddress = "http://127.0.0.1";
            ws.proxyPort = "9666";
            ws.timeoutInSecs = 120;
            //ws.URI = "http://210.211.116.4:8080/datalibs.svc";
            ws.URI = "http://locahost/datalibs.svc";
            ws.useDefaultProxy = true;
            ws.useProxy = false;
            DataAccess.Libs.OpenConnection(ws);
            //if (!DataAccess.Libs.Load_Global_Settings())
            //    throw new Exception("Webservice configuration failed");
            databases.baseDS.investorDataTable investorTbl = DataAccess.Libs.GetInvestor_ByAccount(account);
            if (investorTbl == null)
                throw new Exception("investor table cannot be connected");

            if (investorTbl.Count == 0)
            {
                throw new Exception("Invalid login");
            }
            if (investorTbl[0].password.Trim() != password.Trim())
            {
                throw new Exception("Invalid Password");
            }
            if (!commonClass.SysLibs.isSupperAdminLogin(investorTbl[0].account) &&
                 investorTbl[0].expireDate < DataAccess.Libs.GetServerDateTime())
            {
                throw new Exception("Account Expired");
            }            
        }
    }
}
