using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace CAOP
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count > 0)
            {
                Response.Redirect("Main.aspx");
            }         
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string UserName = txtUserName.Text;
            string Password = txtPassword.Text;

            User LogedUser = new User();
            if (!LogedUser.Login(UserName, Password))
            {
                lblerror.Visible = true;
            }
            else
            {
                Session["User"] = LogedUser;
                if (LogedUser.CheckFirstLoginFlag(LogedUser.USER_ID))
                    Response.Redirect("~/PasswordForms/FirstLoginPassword.aspx");
                else
                Response.Redirect("Main.aspx");
            }
        }
    }
}