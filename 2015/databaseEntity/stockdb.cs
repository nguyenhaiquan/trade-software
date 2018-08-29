using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;


namespace databaseEntity
{
    /// <summary>
    /// Connect directly to Database using Entity Framework
    /// </summary>
    public class StockDb : DbContext
    {
        //private static readonly Lazy<StockDb> lazy =
        //new Lazy<StockDb>(() => new StockDb());

        ////public static StockDb stockdb { get { return lazy.Value; } }


        public virtual DbSet<investor> Investors { get; set; }

        public virtual DbSet<investorStock> InvestorStocks { get; set; }

        public virtual DbSet<stockCode> StockCodes { get; set; }
        public virtual DbSet<tradeAlert> TradeAlerts { get; set; }
        public virtual DbSet<priceDataSum> PriceDataSums { get; set; }
        public virtual DbSet<priceData> PriceData { get; set; }
        public virtual DbSet<transaction> Transactions { get; set; }

        public StockDb()
            : base("name=stockEntities")
        {
        }

        public investor AddInvestor(string username, string password, string email)
        {
            try
            {
                var user = new investor();
                //user.code = commonTypes.Consts.constMarkerNEW;            
                user.code = Guid.NewGuid().ToString();
                user.account = username;
                user.password = password;
                user.email = email;

                user.catCode = "";
                user.firstName = "";
                user.lastName = "";
                user.address1 = "";
                user.address2 = "";
                user.city = "";
                user.country = "";
                user.displayName = username;
                user.phone = "";
                user.sex = 0;
                user.status = 0;
                user.type = 0;
                user.expireDate = DateTime.Now.AddMonths(6);
                Entry(user).State = (EntityState)System.Data.Entity.EntityState.Added;
                //Investors.Add(user);
                this.SaveChanges();
                return user;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Check if User exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsUserExiste(string username)
        {
            //return db.IsExisteInvestor(username);
            var investor = from i in Investors
                           where (i.account == username)
                           select i;
            if (investor.Count() > 0) return true;
            return false;
        }

        /// Check if User exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsUserExisteWithPassword(string username, string password)
        {
            //return db.IsExisteInvestor(username);
            var investor = from i in Investors
                           where ((i.account == username) && (i.password == password))
                           select i;
            if (investor.Count() > 0) return true;
            return false;
        }

        public bool IsEmailExiste(string email)
        {
            //return db.IsExisteInvestor(username);
            var investor = from i in Investors
                           where (i.email == email)
                           select i;
            if (investor.Count() > 0) return true;
            return false;
        }

        /// <summary>
        /// Delete Alerts having condition =stock code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool DeleteTradeAlerts(string code)
        {
            TradeAlerts.RemoveRange(TradeAlerts.Where(x => x.stockCode == code));
            this.SaveChanges();
            return true;
        }

        public bool DeletePriceDataSum(string code)
        {
            PriceDataSums.RemoveRange(PriceDataSums.Where(x => x.stockCode == code));
            this.SaveChanges();
            return true;
        }

        public bool DeletePriceData(string code)
        {
            PriceData.RemoveRange(PriceData.Where(x => x.stockCode == code));
            this.SaveChanges();
            return true;
        }

        public bool DeleteTransactions(string code)
        {
            Transactions.RemoveRange(Transactions.Where(x => x.stockCode == code));
            this.SaveChanges();
            return true;
        }

        public bool DeleteInvestorStocks(string code)
        {
            InvestorStocks.RemoveRange(InvestorStocks.Where(x => x.stockCode == code));
            this.SaveChanges();
            return true;
        }

        public bool DeleteStockCode(string stockcode)
        {
            try
            {
                //1. Delete TradeAlert
                DeleteTradeAlerts(stockcode);

                //2. Delete priceDataSum
                DeletePriceDataSum(stockcode);
                //3. delete priceData
                DeletePriceData(stockcode);

                //4.Delete Transations

                DeleteTransactions(stockcode);
                //5. Delete InvestorStock
                DeleteInvestorStocks(stockcode);

                //6. Delete Stock Code
                //var stock = (from s in StockCodes
                //            where (s.code == stockcode)
                //            select s).FirstOrDefault();
                //StockCodes.Remove(stock);

                StockCodes.RemoveRange(StockCodes.Where(x => x.code == stockcode));
                this.SaveChanges();

                //this.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
                return false;
            }
        }

        public DbSet<stockCode> GetStockCodes(string market)
        {
            return (DbSet < stockCode >) StockCodes.Where(x => x.stockExchange == "HOSE");
        }

        public DbSet<databaseEntity.stockCode> GetAllStockCodes()
        {
            return StockCodes;
        }
    }
}
