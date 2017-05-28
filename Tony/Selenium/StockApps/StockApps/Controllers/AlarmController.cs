using System.Collections.Generic;
using System.Web.Http;
using StockApps.Models;

namespace StockApps.Controllers
{
    public class AlarmController : ApiController
    {
        // Call
        // http://localhost:63471/api/Alarm?investor=test
        public List<Alarm> Get(string investor)
        {
            return new Alarm().GetAll(investor);
        }

        // Call
        // http://localhost:63471/api/Alarm/Insert?stock=HSG&investor=test&type=Volume&condition=Greater&value=9&comment=&status=0&expiry=2017-01-01&notification=3
        [HttpPost]
        public bool Insert(string stock, string investor, string type, string condition, int value, string comment, int status, string expiry, int notification)
        {
            return new Alarm().InsertAlarm(stock, investor, type, condition, value, comment, status, expiry, notification);
        }

        // Call
        // http://localhost:63471/api/Alarm/Delete?stock=HPG&investor=test
        [HttpPost]
        public bool Delete(string stock, string investor)
        {
            return new Alarm().DeleteAlarm(stock, investor);
        }
    }
}
