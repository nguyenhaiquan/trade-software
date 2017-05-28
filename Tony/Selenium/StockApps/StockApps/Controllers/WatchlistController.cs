using System.Collections.Generic;
using System.Web.Http;
using StockApps.Models;

namespace StockApps.Controllers
{
    public class WatchlistController : ApiController
    {
        // Call
        // http://localhost:63471/api/Watchlist?investor=test
        public List<Watchlist> Get(string investor)
        {
            return new Watchlist().GetAll(investor);
        }

        // Call
        // http://localhost:63471/api/Watchlist/Insert?stockI=HSG&investor=test
        [HttpPost]
        public bool Insert(string stockI, string investor)
        {
            return new Watchlist().InsertStock(stockI, investor);
        }

        // Call
        // http://localhost:63471/api/Watchlist/Delete?stockD=HPG&investor=test
        [HttpPost]
        public bool Delete(string stockD, string investor)
        {
            return new Watchlist().DeleteStock(stockD, investor);
        }
    }
}
