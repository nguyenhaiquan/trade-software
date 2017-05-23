using System.Collections.Generic;
using System.Web.Http;
using StockApps.Models;

namespace StockApps.Controllers
{
    public class AlarmController : ApiController
    {
        // Call API
        // http://localhost:63471/api/Alarm?investor=test
        [HttpGet]
        public List<Alarm> Get(string investor)
        {
            return new Alarm().GetAll(investor);
        }
    }
}
