using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace databaseEntity
{


    public class StockImportDB : DbContext
    {
        public virtual DbSet<importPrice> ImportPrices { get; set; }

        public StockImportDB()
            : base("name=importPrices")
        {
        }

        public importPrice GetByDateAndCodeDescending(DateTime start,DateTime end,string code)
        {
            var import = (from i in ImportPrices
                         where ((i.stockCode == code) && (i.onDate>=start) && (i.onDate<end))
                         select i).OrderByDescending(d=>d.onDate).FirstOrDefault();
            return import;
        }
    }
}
