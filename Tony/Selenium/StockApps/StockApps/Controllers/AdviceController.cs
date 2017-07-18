using System.Collections.Generic;
using System.Web.Http;
using StockApps.Models;

namespace StockApps.Controllers
{
    public class AdviceController : ApiController
    {
        // Call
        // http://localhost:63471/api/Advice
        public Advice Get()
        {
            return new Advice().Get();
        }
    }
}