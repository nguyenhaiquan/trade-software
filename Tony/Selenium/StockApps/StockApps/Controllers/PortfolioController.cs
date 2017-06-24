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
        // http://localhost:63471/api/Portfolio/Buy?buyStock=HSG&investor=test&quantity=1&price=1
        [HttpPost]
        public bool Buy(string buyStock, string investor, int quantity, int price)
        {
            return new Portfolio().InsertStock(buyStock, investor, quantity, price);
        }

        // Call
        // http://localhost:63471/api/Portfolio/Sell?sellStock=HSG&investor=test&quantity=1&price=1
        [HttpPost]
        public bool Sell(string sellStock, string investor, int quantity, int price)
        {
            return new Portfolio().DeleteStock(sellStock, investor, quantity, price);
        }
    }
}
