using System.Collections.Generic;
using System.Web.Http;
using StockApps.Models;

namespace StockApps.Controllers
{
    public class PortfolioController : ApiController
    {
        // Call API
        // http://localhost:63471/api/Portfolio?investor=test
        public List<Portfolio> Get(string investor)
        {
            return new Portfolio().GetAll(investor);
        }
    }
}
