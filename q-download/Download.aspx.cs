﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace wStock2
{
    public partial class Download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Globals.vContent = string.Empty;
            Globals.formTitle = "Sign Up";
            Globals.subTitle = "Please complete the register form below for downloading";
            if (IsPostBack)
            {
                bool isDownload = true;
                Globals.vContent = string.Empty;                
                string firstname = Request.Form["firstname"];
                if (firstname.Length == 0)
                {
                    Globals.vContent += "<li>'Họ' không hợp lệ.</li>";
                    isDownload = false;
                }
                string lastname = Request.Form["lastname"];
                if (lastname.Length ==0)
                {
                    Globals.vContent += "<li>'Tên' không hợp lệ.</li>";
                    isDownload = false;
                }
                string email = Request.Form["email"];
                if (!Globals.emailValid(email))
                {
                    Globals.vContent += "<li>'Email' không hợp lệ.</li>";
                    isDownload = false;
                }
                string password = Request.Form["password"];
                if (password.Length <1)
                {
                    Globals.vContent += "<li>Mật khẩu ít nhất phải 1 ký tự</li>";
                    isDownload = false;
                }
                string repassword = Request.Form["repassword"];
                if (repassword!=password)
                {
                    Globals.vContent += "<li>Nhập lại mật khẩu sai</li>";
                    isDownload = false;
                }
                string username = Request.Form["username"];
                if (username.Length < 1)
                {
                    Globals.vContent += "<li>Tên tài khoản ít nhất phải 1 ký tự</li>";
                    isDownload = false;
                }
                else
                if (!wUser.isValidUserName(username))
                {
                    Globals.vContent += "<li>Tên người dùng đã tồn tại</li>";
                    isDownload = false;
                }
                
                if (isDownload)
                {
                    wUser user = new wUser();
                    user.Email = email;
                    user.Firstname = firstname;
                    user.Lastname = lastname;
                    user.Password = password;
                    user.Username = username;
                    try
                    {
                        user.InsertNew();
                    }
                    catch
                    {                       
                        Globals.vContent = "Có lỗi trong quá trình tải về. Xin vui lòng thử lại, cảm ơn!";
                        return;
                    }
                    wUser.currentUser = user;
                    Response.Redirect("DownloadSuccess.aspx");                 
                }
            }
        }
    }
}