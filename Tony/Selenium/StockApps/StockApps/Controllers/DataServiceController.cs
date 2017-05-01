using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using StockApps.Models;

namespace StockApps.Controllers
{
    public class DataServiceController : ApiController
    {
        public IEnumerable<Stock> Get()
        {
            return new Stock().GetAll();
        }

        public Stock Get(int id)
        {
            return new Stock().GetStock(id);
        }

        public IEnumerable<Stock> Post(Stock s)
        {
            return new Stock().Create(s);
        }

        public IEnumerable<Stock> Put(Stock s)
        {
            return new Stock().Update(s);
        }

        public IEnumerable<Stock> Delete(int id)
        {
            return new Stock().Delete(id);
        }

        [HttpGet]
        public string[,] Details(int id)
        {
            return new FinancialData().Get(id);
        }

        [HttpPost]
        public void SaveDB(int id)
        {
            new FinancialData().SaveDB(id);
        }

        // Call API
        // http://localhost:63471/api/DataService/LogIn?account=test&password=1234
        [HttpGet]
        public string LogIn(string account, string password)
        {
            return new Investor().LogIn(account, password);
        }
    }
}
