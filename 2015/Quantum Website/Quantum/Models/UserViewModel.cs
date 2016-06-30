using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Quantum.Framework;
using System.Data.SqlClient;
//using System.Data.Entity.Infrastructure;
using databaseEntity;
using System.ComponentModel.DataAnnotations;

namespace Quantum.Models
{
    public class UserViewModel
    {
        static private databaseEntity.StockDb _db= null;
        public databaseEntity.StockDb db {
            get{ 
                if (_db==null) _db=new databaseEntity.StockDb();
                return _db; 
            }
            private set{}
        }

        public UserViewModel()
        {
        }


        [Display(Name = "Tài khoản")]
        [Required(ErrorMessage = "Nhập tên tài khoản")]
        [StringLength(20, ErrorMessage = "Chiều dài tài khoản không quá 20 ký tự")]
        public string accountname { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Nhập mật khẩu")]
        [StringLength(20, ErrorMessage = "Chiều dài mật khẩu không quá 32 ký tự")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name = "Địa chỉ Email")]
        [Required(ErrorMessage = "Nhập email")]
        [StringLength(50, ErrorMessage = "Chiều dài mật khẩu không quá 50 ký tự")]
        [EmailAddress(ErrorMessage = "Email nhập không đúng")]
        public string email { get; set; }
        
        public void addUser(string username, string password,string email)
        {
            db.AddInvestor(username, password, email);
        }

        public bool IsUserExiste(string username)
        {
            return db.IsUserExiste(username);
        }

        public bool IsEmailExiste(string email)
        {
            return db.IsEmailExiste(email);
        }
    }

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

        static private databaseEntity.StockDb _db= null;
        public databaseEntity.StockDb db {
            get{ 
                if (_db==null) _db=new databaseEntity.StockDb();
                return _db; 
            }
            private set{}
        }

        public LoginModel()
        {
        }

        public bool IsUserExiste(string username)
        {
            return db.IsUserExiste(username);
        }

        public bool IsUserExisteWithPassword(string username,string password)
        {
            return db.IsUserExisteWithPassword(username,password);
        }
    }
}