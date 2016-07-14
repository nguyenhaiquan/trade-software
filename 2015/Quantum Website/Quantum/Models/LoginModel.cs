using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using databaseEntity;
using System.ComponentModel.DataAnnotations;

namespace Quantum.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Tài khoản")]
        public string accountname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string password { get; set; }

        [Display(Name = "Lưu mật khẩu?")]
        public bool rememberMe { get; set; }

        static private databaseEntity.StockDb _db = null;
        public databaseEntity.StockDb db
        {
            get
            {
                if (_db == null) _db = new databaseEntity.StockDb();
                return _db;
            }
            private set { }
        }

        public LoginModel(string _accountname,string _password)
        {
            accountname = _accountname;
            password = _password;
        }

        public LoginModel()
        {
        }

        public bool IsUserExiste(string username)
        {
            return db.IsUserExiste(username);
        }

        public bool IsUserExisteWithPassword(string username, string password)
        {
            return db.IsUserExisteWithPassword(username, password);
        }
    }
}