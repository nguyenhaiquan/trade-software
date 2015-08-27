using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using wStock2.UserService;

namespace wStock2
{
    public class wUser
    {
        static StockServiceClient client = null;
        string username;
        public static wUser currentUser = null;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        string firstname;

        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }
        string lastname;

        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }
        string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        string company;

        public string Company
        {
            get { return company; }
            set { company = value; }
        }
        string stockCompany;

        public string StockCompany
        {
            get { return stockCompany; }
            set { stockCompany = value; }
        }
        string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        
        public wUser()
        {            
        }
        private void OpenConn()
        {
            if (client == null || client.State != System.ServiceModel.CommunicationState.Opened)
            {
                client = new StockServiceClient();
                client.ClientCredentials.Windows.ClientCredential.UserName = "test";
                client.ClientCredentials.Windows.ClientCredential.Password = "123";
            }
        }
        public static void tryOpenConnect()
        {
            if (client == null || client.State != System.ServiceModel.CommunicationState.Opened)
            {
                client = new StockServiceClient();
                client.ClientCredentials.Windows.ClientCredential.UserName = "test";
                client.ClientCredentials.Windows.ClientCredential.Password = "123";
            }
        }
        //Tuan -  Check username is existing in database or not
        public static Boolean isValidUserName(string username)
        {
            tryOpenConnect();
            baseDS.investorDataTable table = client.GetInvestor_ByAccount(username);
            if ((table==null)||(table.Rows.Count==0))
            {
                return true;
            }
            return false;
        }
        //Tuan - Login 
        public bool Login()
        {
            OpenConn();
            baseDS.investorDataTable table = client.GetInvestor_ByAccount(this.Username);
            if (table!=null&&table.Count==1&&table[0].password.Trim()==this.Password.Trim())
            {
                return true;
            }
            return false;
        }
        //Tuan - Insert new user to database, no parametter
        public void InsertNew()
        {
            OpenConn();

            DateTime time = DateTime.Now;
            //baseDS.investorDataTable dt=client.GetInvestor_ByCode("A00000004");
            baseDS.investorDataTable dt = new baseDS.investorDataTable();
            baseDS.investorRow row = dt.NewinvestorRow();
            row.code = commonTypes.Consts.constNotMarkerNEW;            
            row.catCode = string.Empty;
            row.account = Username;
            row.firstName = Firstname;
            row.email = Email;
            row.password = Password;
            row.lastName = Lastname;
            row.address1 = Company;
            row.address2 = StockCompany;
            row.city = string.Empty;
            row.country = string.Empty;
            row.displayName = row.firstName;
            row.phone = Phone;
            row.sex = 0;
            row.status = 0;
            row.type = 0;
            row.expireDate = DateTime.Now.AddMonths(3);
            dt.AddinvestorRow(row);
            client.UpdateInvestor(ref dt);

            // Use the 'client' variable to call operations on the service.

            // Always close the client.
            client.Close();
        }
    }
}
