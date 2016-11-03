using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using ExtensionMethods;
using System.Configuration;

namespace CAOP.UserForms
{
    public partial class ChangeUserPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User LoggedUser = Session["User"] as User;
            if (LoggedUser == null)
                Response.Redirect("~/Login.aspx");

            if (!Page.IsPostBack)
            {
                SetRoles(LoggedUser);
            }
        }

        private void SetRoles(User LoggedUser)
        {
            if (!LoggedUser.Permissions.CheckAccess(Permissions.USER,Rights.Update))
                Response.Redirect("~/Main.aspx");
        }

        protected void btnSavePass_Click(object sender, EventArgs e)
        {
            User LoggedUser = Session["User"] as User;
            if (Page.IsValid)
            {
                if (Request.QueryString["ID"] != null)
                {
                    int USERID = Convert.ToInt32(Request.QueryString["ID"]);
                    string WConfigSetting = ConfigurationManager.AppSettings[2];
                    string email = WConfigSetting.Split(',')[0];
                    string pass = WConfigSetting.Split(',')[1];

                    ChangeLogUser UL = new ChangeLogUser();
                    UL.MakePassLog(txtNewPass.Text, LoggedUser.USER_ID, USERID);

                    LoggedUser.ChangePasswordWithEmail(USERID, txtNewPass.Text,email,pass);

                   

                    Response.Redirect("~/UserForms/Users.aspx");
                }
               
            }
        }

       
    }
}