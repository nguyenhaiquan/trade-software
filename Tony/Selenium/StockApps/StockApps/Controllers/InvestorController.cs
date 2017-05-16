using System.Web.Http;
using StockApps.Models;

namespace StockApps.Controllers
{
    public class InvestorController : ApiController
    {
        // Call API
        // http://localhost:63471/api/Investor/LogIn?account=test&password=1234
        [HttpGet]
        public bool LogIn(string account, string password)
        {
            return new Investor().LogIn(account, password);
        }
    }
}
