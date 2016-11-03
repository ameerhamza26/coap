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
    public partial class CifAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User LogedUser = Session["User"] as User;
            CheckPermissions(LogedUser);

            if (!IsPostBack)
            {
                loaddata();
                if (LogedUser.USER_TYPE == UserType.Region)
                {
                    CifTypes cType = new CifTypes();
                    var cTypeList = cType.GetCifTypes();
                    cTypeList.Remove(cTypeList.FirstOrDefault(c => c.Name.Trim() == "NEXT_OF_KIN"));

                    ddlCifTypes.DataSource = cTypeList;
                    ddlCifTypes.DataValueField = "ID";
                    ddlCifTypes.DataTextField = "NAME";
                    ddlCifTypes.DataBind();

                    SearchCriteria.Visible = true;

            }
            

            
            }
        }

        private void loaddata()
        {
            User LoggedUser = Session["User"] as User;
            if (LoggedUser == null)
                Response.Redirect("Login.aspx");

            CIF cf = new CIF(LoggedUser.USER_ID);
            if (LoggedUser.USER_TYPE == UserType.Branch)
                 grdPCif.DataSource = cf.GeteCifsByRole(LoggedUser.Role.Name,false);  
            else
                grdPCif.DataSource = cf.GeteCifsByRole(LoggedUser.Role.Name, true);
            grdPCif.DataBind();

        }

        private void loaddataRegion()
        {
            User LoggedUser = Session["User"] as User;
            if (LoggedUser == null)
                Response.Redirect("Login.aspx");
            CIF cf = new CIF(LoggedUser.USER_ID);

            if (radioBCode.Checked)
                grdPCif.DataSource = cf.GetCifRegion(true, false, false, false, txtCif.Text, LoggedUser.PARENT_ID);
            else if (radioCNIC.Checked)
                grdPCif.DataSource = cf.GetCifRegion(false, true, false, false, txtCif.Text, LoggedUser.PARENT_ID);
            else if (radioName.Checked)
                grdPCif.DataSource = cf.GetCifRegion(false, false, true, false, txtCif.Text, LoggedUser.PARENT_ID);
            else
                grdPCif.DataSource = cf.GetCifRegion(false, false, false, true, ddlCifTypes.SelectedItem.Value, LoggedUser.PARENT_ID);
            grdPCif.DataBind();
        }

        protected void grdPCif_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPCif.PageIndex = e.NewPageIndex;

            if (Convert.ToBoolean(ViewState["isSearch"]) != true)
                loaddata();
            else
                loaddataRegion();
        }

        protected void grdPCif_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label CIF_ID = e.Row.FindControl("btnCifID") as Label;
                LinkButton lblEdit = e.Row.FindControl("lbledit") as LinkButton;
                User LoggedUser = Session["User"] as User;
                CIF cf = new CIF(LoggedUser.USER_ID);
                CifType Type = cf.GetCifType(Convert.ToInt32(CIF_ID.Text));

                if (LoggedUser.Role.Name == Roles.BRANCH_OPERATOR.ToString())
                {
                    //if (Type == CifType.INDIVIDUAL)
                    //    CIF_ID.PostBackUrl = "~/Individual.aspx?ID=" + CIF_ID.Text;
                    //else if (Type == CifType.NEXT_OF_KIN)
                    //    CIF_ID.PostBackUrl = "~/NextOfKin.aspx?ID=" + CIF_ID.Text;
                    //else
                    //    CIF_ID.PostBackUrl = "~/Business.aspx?ID=" + CIF_ID.Text;
                    if (Type == CifType.INDIVIDUAL)
                        lblEdit.PostBackUrl = "~/Individual.aspx?ID=" + CIF_ID.Text;
                    else if (Type == CifType.NEXT_OF_KIN)
                        lblEdit.PostBackUrl = "~/NextOfKin.aspx?ID=" + CIF_ID.Text;
                    else if (Type == CifType.GOVERNMENT)
                        lblEdit.PostBackUrl = "~/Government.aspx?ID=" + CIF_ID.Text;
                    else if (Type == CifType.OFFICE)
                        lblEdit.PostBackUrl = "~/Office.aspx?ID=" + CIF_ID.Text;
                    else if (Type == CifType.MINOR)
                        lblEdit.PostBackUrl = "~/MinorCIF.aspx?ID=" + CIF_ID.Text;
                    else
                        lblEdit.PostBackUrl = "~/Business.aspx?ID=" + CIF_ID.Text;
                }
                else
                {
                    //if (Type == CifType.INDIVIDUAL)
                    //    CIF_ID.PostBackUrl = "~/Individual.aspx?ID=" + CIF_ID.Text + "&Action=review";
                    //else if (Type == CifType.NEXT_OF_KIN)
                    //    CIF_ID.PostBackUrl = "~/NextOfKin.aspx?ID=" + CIF_ID.Text + "&Action=review";
                    //else
                    //    CIF_ID.PostBackUrl = "~/Business.aspx?ID=" + CIF_ID.Text + "&Action=review";

                    if (Type == CifType.INDIVIDUAL)
                        lblEdit.PostBackUrl = "~/Individual.aspx?ID=" + CIF_ID.Text + "&Action=review";
                    else if (Type == CifType.NEXT_OF_KIN)
                        lblEdit.PostBackUrl = "~/NextOfKin.aspx?ID=" + CIF_ID.Text + "&Action=review";
                    else if (Type == CifType.GOVERNMENT)
                        lblEdit.PostBackUrl = "~/Government.aspx?ID=" + CIF_ID.Text + "&Action=review";
                    else if (Type == CifType.OFFICE)
                        lblEdit.PostBackUrl = "~/Office.aspx?ID=" + CIF_ID.Text + "&Action=review";
                    else if (Type == CifType.MINOR)
                        lblEdit.PostBackUrl = "~/MinorCIF.aspx?ID=" + CIF_ID.Text + "&Action=review";
                    else
                        lblEdit.PostBackUrl = "~/Business.aspx?ID=" + CIF_ID.Text + "&Action=review";
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

        protected void radioCifSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (radioCifType.Checked)
            {
                txtCif.Visible = false;
                ddlCifTypes.Visible = true;
            }
            else
            {
                txtCif.Visible = true;
                ddlCifTypes.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            if (radioBCode.Checked == true || radioName.Checked == true || radioCNIC.Checked == true)
            {
                if (txtCif.Text.Length > 0)
                {
                    ViewState["isSearch"] = true;

                    //search
                    loaddataRegion();
                }
            }
            else
            {
                ViewState["isSearch"] = true;

                //search
                loaddataRegion();
            }
           
        }
    }
}