using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using ExtensionMethods;

namespace CAOP.AccountForms
{
    public partial class RejectedAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User LogedUser = Session["User"] as User;
            if (LogedUser == null)
                Response.Redirect("Login.aspx");
            CheckPermissions(LogedUser);

            if (!IsPostBack)
            {
                loaddata();
               
            }
        }

        private void loaddata()
        {
            User LoggedUser = Session["User"] as User;

            AccOpen ac = new AccOpen(LoggedUser.USER_ID);
            //CIF cf = new CIF(LoggedUser.USER_ID);
            if (LoggedUser.USER_TYPE == UserType.Branch)
                grdPCif.DataSource = ac.GetRejectedAccountsByRole(LoggedUser.Role.Name, false);
            else
                grdPCif.DataSource = ac.GetRejectedAccountsByRole(LoggedUser.Role.Name, true);
            grdPCif.DataBind();

        }

        protected void grdPCif_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPCif.PageIndex = e.NewPageIndex;
                loaddata();
           
        }

        protected void grdPCif_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label CIF_ID = e.Row.FindControl("btnID") as Label;
                LinkButton lblEdit = e.Row.FindControl("lbledit") as LinkButton;
                User LoggedUser = Session["User"] as User;
                //CIF cf = new CIF(LoggedUser.USER_ID);
                AccOpen ac = new AccOpen(LoggedUser.USER_ID);
                AccountOpenTypes type = ac.GetAccountOpenType(Convert.ToInt32(CIF_ID.Text));
                //CifType Type = cf.GetCifType(Convert.ToInt32(CIF_ID.Text));

                if (LoggedUser.Role.Name == Roles.BRANCH_OPERATOR.ToString())
                {

                    if (type == AccountOpenTypes.INDIVIDUAL)
                        lblEdit.PostBackUrl = "~/Account_Individual.aspx?ID=" + CIF_ID.Text;
                    else if (type == AccountOpenTypes.GOVERNMENT)
                        lblEdit.PostBackUrl = "~/Account_Government.aspx?ID=" + CIF_ID.Text;
                    else if (type == AccountOpenTypes.OFFICE)
                        lblEdit.PostBackUrl = "~/Account_office.aspx?ID=" + CIF_ID.Text;

                    else
                        lblEdit.PostBackUrl = "~/Account_Business.aspx?ID=" + CIF_ID.Text;
                }
                else
                {


                    if (type == AccountOpenTypes.INDIVIDUAL)
                        lblEdit.PostBackUrl = "~/Account_Individual.aspx?ID=" + CIF_ID.Text + "&Action=review";
                    else if (type == AccountOpenTypes.GOVERNMENT)
                        lblEdit.PostBackUrl = "~/Account_Government.aspx?ID=" + CIF_ID.Text + "&Action=review";
                    else if (type == AccountOpenTypes.OFFICE)
                        lblEdit.PostBackUrl = "~/Account_office.aspx?ID=" + CIF_ID.Text + "&Action=review";
                    else
                        lblEdit.PostBackUrl = "~/Account_Business.aspx?ID=" + CIF_ID.Text + "&Action=review";
                }



            }
        }



        private void CheckPermissions(User LoggedUser)
        {
            if (LoggedUser.Role.Name == Roles.BRANCH_OPERATOR.ToString())
            {
                if (!LoggedUser.Permissions.CheckAccess(Permissions.CIF, Rights.Create))
                    Response.Redirect("Main.aspx");
            }
            else if (LoggedUser.Role.Name == Roles.COMPLIANCE_OFFICER.ToString())
            {
                if (!LoggedUser.Permissions.CheckAccess(Permissions.CIF, Rights.Read))
                    Response.Redirect("Main.aspx");
            }
            else if (LoggedUser.Role.Name == Roles.BRANCH_MANAGER.ToString())
            {
                if (!LoggedUser.Permissions.CheckAccess(Permissions.CIF, Rights.Read))
                    Response.Redirect("Main.aspx");
            }
        }
    }
}