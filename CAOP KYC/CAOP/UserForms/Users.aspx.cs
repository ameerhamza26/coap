using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using ExtensionMethods;

namespace CAOP
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User LoggedUser = Session["User"] as User;

            if (LoggedUser == null)
                Response.Redirect("../Login.aspx");

            CheckPermissions(LoggedUser);
            grdPUser.DataSource = new List<User>();
            grdPUser.DataBind();
        }

        private void CheckPermissions(User LoggedUser)
        {
            if (LoggedUser.Role.Name == Roles.IT_OPERATIONS.ToString())
            {
                if (!LoggedUser.Permissions.CheckAccess(Permissions.USER, Rights.Create))
                    Response.Redirect("Main.aspx");
            }
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BLL.User USerObj = new User();
            if (txtUserSearch.Text.Length > 0)
            {
                if (radioSapId.Checked)
                {
                    try
                    {
                        decimal sapid = Convert.ToDecimal(txtUserSearch.Text);
                    }
                    catch (Exception ex)
                    {
                        grdPUser.DataSource = new List<User>();
                        grdPUser.DataBind();
                        return;
                    }
                }
                grdPUser.DataSource = USerObj.SearchUser(txtUserSearch.Text, radioUName.Checked, radioEmail.Checked, radioSapId.Checked, radioBCode.Checked);
                grdPUser.DataBind();
            }
        
        }

        protected void grdPUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label USERID = e.Row.FindControl("lblID") as Label;
                LinkButton lbledit = e.Row.FindControl("lbledit") as LinkButton;
                LinkButton lblPass = e.Row.FindControl("lblCPass") as LinkButton;

                lbledit.PostBackUrl = "~/UserForms/EditUser.aspx?ID=" + USERID.Text;

                lblPass.PostBackUrl = "~/UserForms/ChangeUserPass.aspx?ID=" + USERID.Text;
            }
        }
    }
}