using System.Collections.Generic;
using System.Web.Http;
using StockApps.Models;

namespace StockApps.Controllers
{
    public class WatchlistController : ApiController
    {
        // http://localhost:63471/api/Watchlist?investor=test
        public List<Watchlist> Get(string investor)
        {
            return new Watchlist().GetAll(investor);
        }

        // http://localhost:63471/api/Watchlist?stock=HSG&investor=test
        public bool Post(string stock, string investor)
        {
            return new Watchlist().InsertStock(stock, investor);
        }
    }
}
