using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace CAOP
{
    public partial class PendingAccounts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loaddata();
        }

        protected void grdPAccounts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPAccounts.PageIndex = e.NewPageIndex;
            loaddata();
        }

        protected void grdPAccounts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton CIF_ID = e.Row.FindControl("btnAccountID") as LinkButton;
                User LoggedUser = Session["User"] as User;
                AccOpen a = new AccOpen(LoggedUser.USER_ID);
                AccountOpenTypes type = a.GetAccountOpenType(Convert.ToInt32(CIF_ID.Text));
                LinkButton lbledit = e.Row.FindControl("lbledit") as LinkButton;
                
                    
                
                
                if (type == AccountOpenTypes.INDIVIDUAL)
                {
                    CIF_ID.PostBackUrl = "~/Account_Individual.aspx?ID=" + CIF_ID.Text;
                    lbledit.PostBackUrl = "~/Account_Individual.aspx?ID=" + CIF_ID.Text;

                }
                else if (type == AccountOpenTypes.GOVERNMENT)
                {
                    CIF_ID.PostBackUrl = "~/Account_Government.aspx?ID=" + CIF_ID.Text;
                    lbledit.PostBackUrl = "~/Account_Government.aspx?ID=" + CIF_ID.Text;
                }
                else
                {
                    CIF_ID.PostBackUrl = "~/Account_Business.aspx?ID=" + CIF_ID.Text;
                    lbledit.PostBackUrl = "~/Account_Business.aspx?ID=" + CIF_ID.Text;
                }

            }
        }

        private void loaddata()
        {
            User LoggedUser = Session["User"] as User;
            if (LoggedUser == null)
                Response.Redirect("Login.aspx");

            AccOpen a = new AccOpen(LoggedUser.USER_ID);
            grdPAccounts.DataSource = a.GetPendingAccounts();
            grdPAccounts.DataBind();
            

        }

        protected void lbldel_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            int accountid = Convert.ToInt32(((LinkButton)gvr.FindControl("btnAccountID")).Text);
            AccOpen AOpen = new AccOpen(-1);
            AOpen.DelAccount(accountid);
            
            loaddata();
        }
    }
}