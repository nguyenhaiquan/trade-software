using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using commonClass;

namespace baseClass.forms
{
    public partial class baseApplication : common.forms.baseApplication 
    {
        public baseApplication()
        {
            InitializeComponent();
            loginRequired = true;
        }

        public MenuStrip mainMenuStrip = null;
        private bool _permissionRequired = false;
        protected bool permissionRequired
        {
            get { return _permissionRequired; }
            set { _permissionRequired = value; }
        }

        protected override bool LoadAppEnvironment() 
        {
            commonClass.SysLibs.SetAppEnvironment();
            return base.LoadAppEnvironment(); 
        }
        protected override bool LoadAppConfig()
        {
            common.settings.sysProductOwner = Consts.constProductOwner;
            common.settings.sysProductCode = Consts.constProductCode;

            common.settings.sysConfigFile = common.fileFuncs.GetFullPath(this.myConfigFileName);
            common.configuration.withEncryption = true;
            //Check data connection after db-setting were loaded
            //if (!data.system.CheckAllDbConnection())
            //{
            //    common.system.ShowErrorMessage(String.Format(Languages.Libs.GetString("runSetupToCreateConfigFile"), this.myConfigFileName));
            //    return false;
            //}
            DataAccess.Libs.Load_Global_Settings();
            application.Configuration.Load_User_Envir();
            application.Configuration.Load_Local_Settings();

            //Image
            if (common.fileFuncs.FileExist(commonClass.SysLibs.sysImgFilePathBackGround))
                this.BackgroundImage = common.images.LoadImage(commonClass.SysLibs.sysImgFilePathBackGround);
            else this.BackgroundImage = null;
            if (common.fileFuncs.FileExist(commonClass.SysLibs.sysImgFilePathIcon))
                this.Icon = common.images.LoadIcon(commonClass.SysLibs.sysImgFilePathIcon);
            else this.Icon = null;
            return true;
        }
        protected override void SaveAppEnvironment()
        {
            application.Configuration.Save_User_Envir();
        }

        protected override bool CheckValid()
        {
            string productVersion = common.settings.sysProductVersion.Trim();
            if (productVersion != "" && productVersion != common.settings.sysProductVersion.Trim())
            {
                common.system.ShowErrorMessage(String.Format(Languages.Libs.GetString("pleaseUseVersion"), common.settings.sysProductVersion));
                return false;
            }
            return true;
        }
        protected string LicenseFileName()
        {
            return common.fileFuncs.FileNameOnly(common.system.GetExecuteFullPath()) + ".lic";
        }
        protected string _myConfigFileName = null;
        protected virtual string myConfigFileName
        {
            get
            {
                if (_myConfigFileName == null) _myConfigFileName = common.fileFuncs.FileNameOnly(common.system.GetExecuteFullPath()) + ".xml";
                return _myConfigFileName;
            }
            set
            {
                _myConfigFileName = value;
            }
        }

        protected override bool CheckLicense()
        {
            return true;
        }

        protected override bool ShowLogin()
        {
            baseClass.forms.investorLogin myLogin = new investorLogin();
            this.ShowForm(myLogin, true);
            if (!myLogin.isLoginOk) return false;

            this.loginInfoLbl.Text = "[" + commonClass.SysLibs.sysLoginCode.Trim() + "]  " + "[" + commonClass.SysLibs.sysLoginType + "]  " +
                                     "[" + commonClass.SysLibs.sysLoginLocationName.Trim() + "]";
            if (permissionRequired) SetUserMenu(commonClass.SysLibs.sysLoginUserId, mainMenuStrip);

            // Load permission
            return true;
        }

        protected virtual void SetUserMenu(int workerId, MenuStrip myMenuStrip)
        {
        }
        private void SetUserAllMenu(int workerId, ToolStripMenuItem menuItem)
        {
            for (int idx = 0; idx < menuItem.DropDownItems.Count; idx++)
            {
                menuItem.DropDownItems[idx].Enabled = true;
            }
        }
    }
}