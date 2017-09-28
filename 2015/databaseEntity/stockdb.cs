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
        public virtual DbSet<investor> Investors { get; set; }

        public StockDb()
            : base("name=stockEntities")
        {
        }

        public investor AddInvestor(string username,string password,string email){
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
                Entry(user).State = (EntityState)System.Data.EntityState.Added;
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
        public bool IsUserExisteWithPassword(string username,string password)
        {
            //return db.IsExisteInvestor(username);
            var investor = from i in Investors
                           where ((i.account == username)&&(i.password==password))
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
    }
}
