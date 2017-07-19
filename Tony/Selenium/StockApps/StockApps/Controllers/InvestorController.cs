using System.Web.Http;
using StockApps.Models;
using System.Collections.Generic;

namespace StockApps.Controllers
{
    public class InvestorController : ApiController
    {
        // Call API
        // http://localhost:63471/api/Investor/LogIn?account=test&password=1234
        [HttpGet]
        public Investor LogIn(string account, string password)
        {
            return new Investor().LogIn(account, password);
        }

        [HttpGet]
        public List<string> GetAsset(string code)
        {
            return new Investor().GetAsset(code);
        }
    }
}
