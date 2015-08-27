using System;
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
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Globals.formTitle = "Sign In";
            Globals.subTitle = "Please complete the Sign In form below for downloading";
            Globals.vContent = string.Empty;
            if (IsPostBack)
            {             
                bool isDownload = true;
                Globals.vContent = string.Empty;
                 string password = Request.Form["password"];
                if (password.Length ==0)
                {
                    Globals.vContent += "<li>Password is not valid</li>";
                    isDownload = false;
                    return;
                }
                string username = Request.Form["username"];
                if (username.Length == 0)
                {
                    Globals.vContent += "<li>username is not valid</li>";
                    isDownload = false;
                    return;
                }
                wUser user = new wUser();
                user.Username = username;
                user.Password = password;
                if (!user.Login())
                {
                    Globals.vContent += "<li>Username is not correct or password is wrong!!!</li>";
                    isDownload = false;
                    return;
                }
                if (isDownload)
                {
                    //FormsAuthentication.SetAuthCookie(username, false);
                    //FormsAuthenticationTicket fat = new FormsAuthenticationTicket(1,
                    //username, DateTime.Now,
                    //DateTime.Now.AddMinutes(10), false, "member",
                    //FormsAuthentication.FormsCookiePath);
                    //Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName,
                    //    FormsAuthentication.Encrypt(fat))); 
                    wUser.currentUser = user;
                    Response.Redirect("DownloadSuccess.aspx");                    
                }
            }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            
        }
    }
}
