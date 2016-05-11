using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace CAOP.PasswordForms
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User LoggedUser = Session["User"] as User;
            if (LoggedUser == null)
                Response.Redirect("~/Login.aspx");
        }

        protected void btnSavePass_Click(object sender, EventArgs e)
        {
            User LoggedUser = Session["User"] as User;
            if (Page.IsValid)
            {
                LoggedUser.ChangePassword(LoggedUser.USER_ID, txtNewPass.Text);
                Response.Redirect("~/Main.aspx");
            }
        }

        protected void CustomValidatoroldpass_ServerValidate(object source, ServerValidateEventArgs args)
        {
            User LoggedUser = Session["User"] as User;
            args.IsValid =  LoggedUser.CheckPasswordExixts(LoggedUser.USER_ID, txtOldPass.Text);
        }
    }
}