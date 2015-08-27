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
                if (password.Length <=5)
                {
                    Globals.vContent += "<li>Mật khẩu ít nhất phải 6 ký tự</li>";
                    isDownload = false;
                }
                string repassword = Request.Form["repassword"];
                if (repassword!=password)
                {
                    Globals.vContent += "<li>Nhập lại mật khẩu sai</li>";
                    isDownload = false;
                }
                string username = Request.Form["username"];
                if (username.Length <= 5)
                {
                    Globals.vContent += "<li>Tên tài khoản ít nhất phải 6 ký tự</li>";
                    isDownload = false;
                }
                string phone = Request.Form["phone"];
                //if (phone.Length < 10)
                //{
                //    Globals.vContent += "<li>Số điện thoại không hợp lệ</li>";
                //    isDownload = false;
                //}
                string company = Request.Form["company"];
                //if (company.Length==0)
                //{
                //    Globals.vContent += "<li>Xin vui lòng nhập tên công ty</li>";
                //    isDownload = false;
                //}
                string stockcompany = Request.Form["stockcompany"];
                if (stockcompany.Length == 0)
                {
                    Globals.vContent += "<li>Xin vui lòng nhập tên công ty chứng khoán nơi bạn mở tài khoản.</li>";
                    isDownload = false;
                }
                else if (!wUser.isValidUserName(username))
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
                    user.StockCompany = stockcompany;
                    user.Company = company;
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
                //if (firstname!=null)
                //{
                //    Response.Redirect("DownloadHandler.ashx?File=Quantum_0202beta.zip");
                //}                 
            }
        }
    }
}
