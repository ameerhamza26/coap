using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using ExtensionMethods;
using System.DirectoryServices;
using System.Text;
using System.Configuration;

namespace CAOP.UserForms
{
    public partial class EditUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User LoggedUser = Session["User"] as User;

            if (LoggedUser == null)
                Response.Redirect("~/Login.aspx");

            if (!Page.IsPostBack)
            {
                SetRoles(LoggedUser);
                 SetData();
                 FillData();
            }

           
               
        }

        private void FillData()
        {
            User EditUser = new User();
            int UserID = Convert.ToInt32(Request.QueryString["ID"]);
            EditUser.GetUser(UserID);

            txtUName.Text = EditUser.USER_NAME;
            txtName.Text = EditUser.DISPLAY_NAME;
            txtemail.Text = EditUser.EMAIL;
            txtdesig.Text = EditUser.DESIGNATION;
            txtSap.Text = EditUser.SAPID.ToString();
            if ((bool)EditUser.ACTIVE)
                ddlAD.Items.FindByValue("0").Selected = true;
            else
                ddlAD.Items.FindByValue("1").Selected = true;

            if (EditUser.Region != null)
            {
                ddlRegions.Items.FindByValue(EditUser.Region.REGION_ID.ToString()).Selected = true;
                chkRegion.Checked = true;
                txtBranchCode.Enabled = false;
                RequiredFieldValidatorBranchCode.Enabled = false;
                ddlUserRole.Enabled = false;
                RequiredFieldValidatorRole.Enabled = false;
            }
            else if (EditUser.Branch != null)
            {
                txtBranchCode.Text = EditUser.Branch.BRANCH_CODE;
                ddlUserRole.Items.FindByValue(EditUser.Role.ID.ToString()).Selected = true;

                ddlRegions.Enabled = false;
                RequiredFieldValidatorRegions.Enabled = false;
            }

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

            ddlAD.Items.Insert(0, new ListItem("DEACTIVE", "1"));
            ddlAD.Items.Insert(0, new ListItem("ACTIVE", "0"));

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                User userobj = new User();

                if (chkRegion.Checked)
                {
                    userobj.Region = new Region() { REGION_ID = Convert.ToInt32(ddlRegions.SelectedItem.Value) };
                    userobj.USER_TYPE = UserType.Region;

                }
                else
                {
                    if (txtBranchCode.Text.Length < 4)
                    {
                        int padding = 4 - txtBranchCode.Text.Length;
                        txtBranchCode.Text = txtBranchCode.Text.PadLeft(padding, '0');
                    }

                    userobj.Branch = new Branch() { BRANCH_CODE = txtBranchCode.Text };
                }

                userobj.DISPLAY_NAME = txtName.Text;
                userobj.EMAIL = txtemail.Text;
                userobj.DESIGNATION = txtdesig.Text;
                userobj.SAPID = Convert.ToDecimal(txtSap.Text);
                userobj.Role = new Role() { ID = Convert.ToInt32(ddlUserRole.SelectedItem.Value) };
                if (ddlAD.SelectedItem.Value == "0")
                    userobj.ACTIVE = true;
                else
                    userobj.ACTIVE = false;

                userobj.USER_ID = Convert.ToInt32(Request.QueryString["ID"]);
                int UserID = Convert.ToInt32(Request.QueryString["ID"]);

                User LoggedUser = Session["User"] as User;
                ChangeLogUser UL = new ChangeLogUser();
                UL.MakeUserLog(userobj, LoggedUser.USER_ID);

                userobj.UpdateUser(UserID);

               

                Response.Redirect("~/UserForms/Users.aspx");
            }
          

           
        }

        protected void CustomValidatorEmailUnique_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int ID = Convert.ToInt32(Request.QueryString["ID"]);
            User userobj = new User();
            args.IsValid = !userobj.CheckEmailExixts(txtemail.Text, ID);
    
        }

        protected void CustomValidatorSapIdUnique_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int ID = Convert.ToInt32(Request.QueryString["ID"]);
            User userobj = new User();
            args.IsValid = !userobj.CheckSapIdExixts(Convert.ToDecimal(txtSap.Text), ID);
          
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

        private void SetRoles(User LoggedUser)
        {
            if (!LoggedUser.Permissions.CheckAccess(Permissions.USER, Rights.Update))
                Response.Redirect("~/Main.aspx");
        }

        protected void CustomValidatorValid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string WConfigSetting = ConfigurationManager.AppSettings[2];
            string email = WConfigSetting.Split(',')[0];
            string pass = WConfigSetting.Split(',')[1];

            args.IsValid = VerifyEmailUserID(txtemail.Text, email, pass);
           
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
    }
}