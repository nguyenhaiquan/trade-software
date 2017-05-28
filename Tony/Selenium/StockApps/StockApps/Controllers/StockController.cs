﻿using System.Collections.Generic;
using System.Web.Http;
using StockApps.Models;

namespace StockApps.Controllers
{
    public class StockController : ApiController
    {
        // Call
        // http://localhost:63471/api/Stock
        public List<Stock> Get()
        {
            return new Stock().GetAll();
        }

        // Call
        // http://localhost:63471/api/Stock?code=HPG
        public Stock Get(string code)
        {
            return new Stock().GetStock(code);
        }

        public List<Stock> Post(Stock s)
        {
            return new Stock().Create(s);
        }

        public List<Stock> Put(Stock s)
        {
            return new Stock().Update(s);
        }

        public List<Stock> Delete(string code)
        {
            return new Stock().Delete(code);
        }
    }
}
