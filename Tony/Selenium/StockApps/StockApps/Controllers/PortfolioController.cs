using System.Collections.Generic;
using System.Web.Http;
using StockApps.Models;

namespace StockApps.Controllers
{
    public class PortfolioController : ApiController
    {
        // Call
        // http://localhost:63471/api/Portfolio?investor=test
        public List<Portfolio> Get(string investor)
        {
            return new Portfolio().GetAll(investor);
        }

        // Call
        // http://localhost:63471/api/Portfolio?stock=HSG&investor=test&quantity=1&price=1
        public bool Post(string stock, string investor, int quantity, int price)
        {
            return new Portfolio().InsertStock(stock, investor, quantity, price);
        }
    }
}
