using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace CAOP
{
    public partial class PendingCIF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User LogedUser = Session["User"] as User;
            if (LogedUser == null)
                Response.Redirect("~/Login.aspx");
            if (!Page.IsPostBack)
                loaddata();
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
                else if (Type == CifType.MINOR)
                    lbledit.PostBackUrl = "~/MinorCIF.aspx?ID="+ CIF_ID.Text;
                else
                    lbledit.PostBackUrl = "~/Business.aspx?ID=" + CIF_ID.Text;
            }
            
        }

        private void loaddata()
        {
            User LoggedUser = Session["User"] as User;
            if (LoggedUser == null)
                Response.Redirect("Login.aspx");
            CIF cf = new CIF(LoggedUser.USER_ID);
            grdPCif.DataSource = cf.GetPendingCifs();
            grdPCif.DataBind();

        }

        protected void lblDel_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            int CIFID = Convert.ToInt32(((Label)gvr.FindControl("btnCifID")).Text);

            CIF cif = new CIF(-1);
            cif.DeleteCif(CIFID);
            loaddata();
        }
    }
}