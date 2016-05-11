using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExtensionMethods;
using BLL;

namespace CAOP
{
    public partial class SearchCif : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             var type = this.Master.FindControl("img");
            User LogedUser = Session["User"] as User;
            if (LogedUser == null)
                Response.Redirect("Login.aspx");
            CheckPermissions(LogedUser);

            List<BasicInformations> b = new List<BasicInformations>();
            grdPCif.DataSource = b;
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
                Label CIF_ID = e.Row.FindControl("btnCifID") as Label;
                LinkButton lbledit = e.Row.FindControl("lbledit") as LinkButton;
                User LoggedUser = Session["User"] as User;
                CIF cf = new CIF(LoggedUser.USER_ID);
                CifType Type = cf.GetCifType(Convert.ToInt32(CIF_ID.Text));

                if (Type == CifType.INDIVIDUAL)
                    lbledit.PostBackUrl = "~/Individual.aspx?ID=" + CIF_ID.Text;
                else if (Type == CifType.NEXT_OF_KIN)
                    lbledit.PostBackUrl = "~/NextOfKin.aspx?ID=" + CIF_ID.Text;
                else if (Type == CifType.GOVERNMENT)
                    lbledit.PostBackUrl = "~/Government.aspx?ID=" + CIF_ID.Text;
                else
                    lbledit.PostBackUrl = "~/Business.aspx?ID=" + CIF_ID.Text;
            }
        }

        private void loaddata()
        {
            if (txtCif.Text.Length > 0)
            {
                User LoggedUser = Session["User"] as User;
                CIF cf = new CIF(LoggedUser.USER_ID);
                if (radioCNIC.Checked)
                    grdPCif.DataSource = cf.SearchCif(txtCif.Text, true, false, false, false);
                else if (radioNIC.Checked)
                    grdPCif.DataSource = cf.SearchCif(txtCif.Text, false, true, false, false);
                else if (radioNTN.Checked)
                    grdPCif.DataSource = cf.SearchCif(txtCif.Text, false, false, true, false);
                else
                    grdPCif.DataSource = cf.SearchCif(txtCif.Text, false, false, false, true);
                grdPCif.DataBind();
            }


        }

        private void CheckPermissions(User LoggedUser)
        {
            
                if (!LoggedUser.Permissions.CheckAccess(Permissions.CIF, Rights.Read))
                    Response.Redirect("Main.aspx");
          

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loaddata();
        }
    }
}