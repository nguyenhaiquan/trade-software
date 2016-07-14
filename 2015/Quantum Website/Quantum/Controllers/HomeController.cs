using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quantum.Models;
using WebMatrix.WebData;
using System.Web.Security;
 
namespace Quantum.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        // GET: Gioithieu
        public ActionResult Gioithieu()
        {
            return View();
        }
        // GET: Sanpham
        public ActionResult Sanpham()
        {
            return View();
        }

        // GET: Taiphanmem
        //[Authorize]
        public ActionResult download()
        {
            if ((Session!=null)&&(Session["AccountName"]!=null))
                return View("Download");
            return RedirectToAction("Dangnhap", "Home", new { returnUrl="DownLoad" });
        }

        // GET: Taiphanmem
        //[Authorize]
        public ActionResult clickoncedownload()
        {
            if ((Session!=null)&&(Session["AccountName"] != null))
            {
                //string path = @"~/Downloads/MyApplication.exe.manifest";
                string path = @"C:\Temp\clickonce_quantum20160701\setup.exe";
                string content = "application/x-ms-application";
                return new FilePathResult(path, content); 
            }
            return RedirectToAction("Dangnhap", "Home", new { returnUrl = "DownLoad" });
        }

        // GET: Trogiup
        public ActionResult Trogiup()
        {
            return View();
        }
        // GET: Lienhe
        public ActionResult Lienhe()
        {
            return View();
        }
        
        //
        // GET: /Home/Dangnhap

        [AllowAnonymous]
        public ActionResult Dangnhap(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // GET: /Home/Recommendation?stockcode='SSI'
        public ActionResult Recommendation(string stockcode)
        {
            return View();
        }

        //
        // POST: /Home/DangXuat

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Dangxuat()
        {
            if (Session != null)
            {
                Session["AccountName"] = null;
            }
            return View("Index");
        }

        // POST: /Home/Dangnhap
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Dangnhap(LoginModel model,string returnUrl)
        {
            //if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            if (ModelState.IsValid && model.IsUserExisteWithPassword(model.accountname, model.password))
            {
                Session["AccountName"] = model.accountname;
                if (returnUrl!=null)
                    return RedirectToAction(returnUrl);
                return RedirectToAction("Index");
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Tên tài khoản hoặc mật khẩu không đúng...");
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Dangky(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsUserExiste(model.accountname))
                {
                    ModelState.AddModelError("accountname", "Tài khoản đã tồn tại");
                    return View("Dangky");
                }
                else
                {
                    if (model.IsEmailExiste(model.email))
                    {
                        ModelState.AddModelError("email", "Email đã tồn tại");
                        return View("Dangky");
                    }
                    model.addUser(model.accountname, model.password, model.email);
                    ModelState.Clear();
                    ViewBag.Message = "Đăng ký thành công tài khoản " +model.accountname;
                    Session["AccountName"] = model.accountname;
                    return RedirectToAction("Index");
                }
            }
            else
                return View("Dangky");
        }

        //[HttpPost]
        //public JsonResult CheckLogin(string data){
        //    string name = Request.Form["name"];
        //    string password = Request.Form["password"];
        //    string email = Request.Form["email"];
        //    UserViewModel user = new UserViewModel();
        //    //bool authorize = user.checkLogin(username, password,email);
        //    bool authorize = true;
        //    var status = false;
        //    string message = "Dang nhap ko thanh cong";
        //    if (authorize)
        //    {
        //        message = "Đăng nhập thành công";
        //        status = true;
        //    }
        //    var output = new { status = status, message = message };
        //    return Json(output);
        //}

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion 
    }
}