using System.Collections.Generic;
using System.Web.Http;
using StockApps.Models;

namespace StockApps.Controllers
{
    public class InvestmentController : ApiController
    {
        // Call
        // http://localhost:63471/api/Investment?investor=test
        public List<Investment> Get(string investor)
        {
            return new Investment().GetAll(investor);
        }
    }
}
