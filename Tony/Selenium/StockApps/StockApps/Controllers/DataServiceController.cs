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
        // Defautl get function
        // Call API
        // http://localhost:63471/api/DataService
        public IEnumerable<Stock> Get()
        {
            return new Stock().GetAll();
        }

        //public Stock Get(string code)
        //{
            //return new Stock().GetStock(code);
        //}

        public IEnumerable<Stock> Post(Stock s)
        {
            return new Stock().Create(s);
        }

        public IEnumerable<Stock> Put(Stock s)
        {
            return new Stock().Update(s);
        }

        public IEnumerable<Stock> Delete(string code)
        {
            return new Stock().Delete(code);
        }

        // Call API
        // http://localhost:63471/api/DataService/details?code=HPG
        [HttpGet]
        public string[,] Details(string code)
        {
            return new FinancialData().Get(code);
        }

        // Call API
        // http://localhost:63471/api/DataService/savedb?code=HPG
        [HttpPost]
        public void SaveDB(string code)
        {
            new FinancialData().SaveDB(code);
        }

        // Call API
        // http://localhost:63471/api/DataService/LogIn?account=test&password=1234
        [HttpPost]
        public string LogIn(string account, string password)
        {
            return new Investor().LogIn(account, password);
        }
    }
}
