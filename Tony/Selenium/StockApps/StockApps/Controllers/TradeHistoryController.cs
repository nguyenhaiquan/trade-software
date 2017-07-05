using System.Collections.Generic;
using System.Web.Http;
using StockApps.Models;

namespace StockApps.Controllers
{
    public class TradeHistoryController : ApiController
    {
        // Call
        // http://localhost:63471/api/TradeHistory?stock=HPG&days=10
        public List<TradeHistory> Get(string stock, int days)
        {
            return new TradeHistory().GetAll(stock, days);
        }
    }
}
