using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace CAOP.PasswordForms
{
    public partial class FirstLoginPassword : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            User LoggedUser = Session["User"] as User;
            if (LoggedUser == null)
                Response.Redirect("~/Login.aspx");

            SetUserHeader();
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
            args.IsValid = LoggedUser.CheckPasswordExixts(LoggedUser.USER_ID, txtOldPass.Text);
        }

        private void SetUserHeader()
        {
            User LogedUser = Session["User"] as User;

            lblDate.Text = DateTime.Now.ToLongDateString();
            lblUser.Text = LogedUser.DISPLAY_NAME;

            if (LogedUser.Region != null)
                lblRegion.Text = LogedUser.Region.NAME;
            else
                lblRegion.Text = "";

            if (LogedUser.USER_TYPE == UserType.Branch)
                lblBranch.Text = LogedUser.Branch.NAME;
            else
                lblBranch.Text = "";
            lblRole.Text = LogedUser.Role.Name.Replace("_", " ");

        }
    }
}