using System.Collections.Generic;
using System.Web.Http;
using StockApps.Models;

namespace StockApps.Controllers
{
    public class AdvideController : ApiController
    {
        // Call
        // http://localhost:63471/api/Advide
        public Advide Get()
        {
            return new Advide().Get();
        }
    }
}