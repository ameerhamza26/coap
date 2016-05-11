using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using ExtensionMethods;
using System.Configuration;
using System.Text;
using System.DirectoryServices;

namespace CAOP.UserForms
{
    public partial class AddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User LoggedUser = Session["User"] as User;

            if (LoggedUser == null)
                Response.Redirect("~/Login.aspx");

            if (!Page.IsPostBack)
                SetData();
        }

        private void SetData()
        {
            User userobj = new User();

            ddlUserRole.DataSource = userobj.GetAllRoles();
            ddlUserRole.DataValueField = "ID";
            ddlUserRole.DataTextField = "Name";
            ddlUserRole.DataBind();
            ddlUserRole.Items.Insert(0, new ListItem("Select", "0"));

            ddlRegions.DataSource = userobj.GetAllRegions();
            ddlRegions.DataValueField = "REGION_ID";
            ddlRegions.DataTextField = "Name";
            ddlRegions.DataBind();
            ddlRegions.Items.Insert(0, new ListItem("Select", "0"));

        }

        protected void txtemail_TextChanged(object sender, EventArgs e)
        {
            if (txtemail.Text.Contains('@'))
            {
                txtUName.Text = txtemail.Text.Split('@')[0];
            }
        }

        protected void chkRegion_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRegion.Checked)
            {
                ddlUserRole.SelectedIndex = 0;
                ddlUserRole.Enabled = false;
                RequiredFieldValidatorRole.Enabled = false;

                ddlRegions.Enabled = true;
                RequiredFieldValidatorRegions.Enabled = true;


                txtBranchCode.Text = "";
                txtBranchCode.Enabled = false;
                RequiredFieldValidatorBranchCode.Enabled = false;



            }
            else
            {
                ddlUserRole.Enabled = true;
                RequiredFieldValidatorRole.Enabled = true;

                txtBranchCode.Enabled = true;
                RequiredFieldValidatorBranchCode.Enabled = true;

                ddlRegions.Enabled = false;
                RequiredFieldValidatorRegions.Enabled = false;
            }
        }

        protected void CustomValidatorEmailUnique_ServerValidate(object source, ServerValidateEventArgs args)
        {
            User userobj = new User();
            args.IsValid = !userobj.CheckEmailExixts(txtemail.Text);
        }

        protected void CustomValidatorSapIdUnique_ServerValidate(object source, ServerValidateEventArgs args)
        {
            User userobj = new User();
            args.IsValid = !userobj.CheckSapIdExixts(Convert.ToDecimal(txtSap.Text));
        }

        protected void CustomValidatorBranchCode_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtBranchCode.Text.Length > 0)
            {
                User userobj = new User();
                args.IsValid = userobj.CheckBranchcodeExists(txtBranchCode.Text);
            }
            else
                args.IsValid = true;
          
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                User LoggedUser = Session["User"] as User;
                User userobj = new User();
                if (chkRegion.Checked)
                    userobj.Region = new Region() { REGION_ID = Convert.ToInt32(ddlRegions.SelectedItem.Value) };
                userobj.USER_NAME = txtUName.Text;
                userobj.DISPLAY_NAME = txtName.Text;
                userobj.EMAIL = txtemail.Text;
                userobj.DESIGNATION = txtdesig.Text;
                userobj.SAPID = Convert.ToDecimal(txtSap.Text);
                userobj.Role = new Role() { ID = Convert.ToInt32(ddlUserRole.SelectedItem.Value) };

                if (txtBranchCode.Text.Length < 4)
                {
                    int padding = 4 - txtBranchCode.Text.Length;
                    txtBranchCode.Text = txtBranchCode.Text.PadLeft(padding, '0');
                }
                string WConfigSetting = ConfigurationManager.AppSettings[2];
                string email = WConfigSetting.Split(',')[0];
                string pass = WConfigSetting.Split(',')[1];

                userobj.PASSWORD = encrypt("New@2016");
                userobj.InsertUser(txtBranchCode.Text, LoggedUser.USER_ID,email,pass);

                Response.Redirect("Users.aspx");

            }
        }

        public string encrypt(string str)
        {
            string encodedData = "";

            if (str.Trim().Length > 0)
            {
                byte[] encData_byte = new byte[str.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(str);
                encodedData = Convert.ToBase64String(encData_byte);
            }

            return encodedData;
        }

       

        private void CheckPermissions(User LoggedUser)
        {
            if (LoggedUser.Role.Name == Roles.IT_OPERATIONS.ToString())
            {
                if (!LoggedUser.Permissions.CheckAccess(Permissions.USER, Rights.Create))
                    Response.Redirect("../Main.aspx");
            }
           
        }

        public bool VerifyEmailUserID(string UserName, string remail, string pass)
    {
        try
        {
            if (UserName.Contains("@"))
                UserName = UserName.Substring(0, UserName.LastIndexOf('@')).Trim();

            string domainAndUsername = string.Empty;
            string userName = string.Empty;
            string passWord = string.Empty;

            AuthenticationTypes at = AuthenticationTypes.Anonymous;
            StringBuilder sb = new StringBuilder();

            domainAndUsername = @"LDAP://nbp.com.pk";

                userName = remail;
                passWord = pass;
            
            at = AuthenticationTypes.Secure;
            DirectoryEntry entry = new DirectoryEntry(domainAndUsername, userName, passWord, at);
            DirectorySearcher mySearcher = new DirectorySearcher(entry);
            SearchResult results;
            mySearcher.Filter = "(&(objectClass=user)(sAMAccountName=" + UserName + "))";

            try
            {
                results = mySearcher.FindOne();
                if (results != null)
                {
                    //string strQuery = "IF EXISTS (SELECT * FROM InValidEmails where EmailIDs = '" + UserName + "@nbp.com.pk')";
                    //strQuery += " Delete from InValidEmails where EmailIDs = '" + UserName + "@nbp.com.pk'";
                    //DAL.updateByQuerySQL(strQuery);
                    return true;
                }
                else
                {
                    mySearcher.Filter = "(&(objectClass=user)(mail=" + UserName + "@nbp.com.pk))";
                    results = mySearcher.FindOne();
                    if (results != null)
                    {
                        //string strQuery = "IF EXISTS (SELECT * FROM InValidEmails where EmailIDs = '" + UserName + "@nbp.com.pk')";
                        //strQuery += " Delete from InValidEmails where EmailIDs = '" + UserName + "@nbp.com.pk'";
                        //DAL.updateByQuerySQL(strQuery);
                        return true;
                    }
                    else
                    {
                        //string strQuery = "IF NOT EXISTS (SELECT * FROM InValidEmails where EmailIDs = '" + UserName + "@nbp.com.pk')";
                        //strQuery += " Insert into InValidEmails Select '" + UserName + "@nbp.com.pk', getdate()";
                        //DAL.updateByQuerySQL(strQuery);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
        return false;
    }

        protected void CustomValidatorValid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string WConfigSetting = ConfigurationManager.AppSettings[2];
            string email = WConfigSetting.Split(',')[0];
            string pass = WConfigSetting.Split(',')[1];

           args.IsValid = VerifyEmailUserID(txtemail.Text, email, pass);
        }
    }
}