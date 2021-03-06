﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using ExtensionMethods;

namespace CAOP.CifForms
{
    public partial class PendingUpdatedCIF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User LogedUser = Session["User"] as User;
            CheckPermissions(LogedUser);

            if (!IsPostBack)
            {
                loaddata();

            }
        }

        private void loaddata()
        {
            User LoggedUser = Session["User"] as User;
            if (LoggedUser == null)
                Response.Redirect("Login.aspx");

            CIF cf = new CIF(LoggedUser.USER_ID);
            if (LoggedUser.USER_TYPE == UserType.Branch)
                grdPCif.DataSource = cf.GetPendingUpdatedCifsByRole(LoggedUser.Role.Name, false);
            else
                grdPCif.DataSource = cf.GetPendingUpdatedCifsByRole(LoggedUser.Role.Name, true);
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
                LinkButton lblEdit = e.Row.FindControl("lbledit") as LinkButton;
                User LoggedUser = Session["User"] as User;
                CIF cf = new CIF(LoggedUser.USER_ID);
                CifType Type = cf.GetCifType(Convert.ToInt32(CIF_ID.Text));
                string status = cf.GetCifStatus(Convert.ToInt32(CIF_ID.Text));

                if (LoggedUser.Role.Name == Roles.BRANCH_OPERATOR.ToString())
                {

                    if (Type == CifType.INDIVIDUAL) {
                        if (status == Status.UPDATED_BY_BRANCH_OPERATOR.ToString() || status == Status.UPDATED_CIF_REJECTED_BY_COMPAINCE_OFFICER.ToString() || status == Status.UPDATED_CIF_REJECTED_BY_BRANCH_MANAGER.ToString())
                        {
                            lblEdit.PostBackUrl = "~/UpdateIndividual.aspx?ID=" + CIF_ID.Text;
                        }
                        else
                        {
                            lblEdit.PostBackUrl = "~/Individual.aspx?ID=" + CIF_ID.Text;
                        }
                    }
                    else if (Type == CifType.NEXT_OF_KIN)
                        lblEdit.PostBackUrl = "~/NextOfKin.aspx?ID=" + CIF_ID.Text;
                    else if (Type == CifType.GOVERNMENT) {
                        if (status == Status.UPDATED_BY_BRANCH_OPERATOR.ToString() || status == Status.UPDATED_CIF_REJECTED_BY_COMPAINCE_OFFICER.ToString() || status == Status.UPDATED_CIF_REJECTED_BY_BRANCH_MANAGER.ToString())
                        {
                            lblEdit.PostBackUrl = "~/UpdateGovernment.aspx?ID=" + CIF_ID.Text;
                        }
                        else
                        {
                            lblEdit.PostBackUrl = "~/Government.aspx?ID=" + CIF_ID.Text;
                        }
                    }
                    else if (Type == CifType.OFFICE) {
                        if (status == Status.UPDATED_BY_BRANCH_OPERATOR.ToString() || status == Status.UPDATED_CIF_REJECTED_BY_COMPAINCE_OFFICER.ToString() || status == Status.UPDATED_CIF_REJECTED_BY_BRANCH_MANAGER.ToString())
                        {
                            lblEdit.PostBackUrl = "~/UpdateOffice.aspx?ID=" + CIF_ID.Text;
                        }
                        else
                        {
                            lblEdit.PostBackUrl = "~/Office.aspx?ID=" + CIF_ID.Text;
                        }
                    }
                    else if (Type == CifType.MINOR) {
                        if (status == Status.UPDATED_BY_BRANCH_OPERATOR.ToString() || status == Status.UPDATED_CIF_REJECTED_BY_COMPAINCE_OFFICER.ToString() || status == Status.UPDATED_CIF_REJECTED_BY_BRANCH_MANAGER.ToString())
                        {
                            lblEdit.PostBackUrl = "~/UpdateMinorCIF.aspx?ID=" + CIF_ID.Text;
                        }
                        else
                        {
                            lblEdit.PostBackUrl = "~/MinorCIF.aspx?ID=" + CIF_ID.Text;
                        }
                    }
                    else if (Type == CifType.BUSINESS)
                    {
                        if (status == Status.UPDATED_BY_BRANCH_OPERATOR.ToString() || status == Status.UPDATED_CIF_REJECTED_BY_COMPAINCE_OFFICER.ToString() || status == Status.UPDATED_CIF_REJECTED_BY_BRANCH_MANAGER.ToString())
                        {
                            lblEdit.PostBackUrl = "~/UpdateBusiness.aspx?ID=" + CIF_ID.Text;
                        }
                        else
                        {
                            lblEdit.PostBackUrl = "~/Business.aspx?ID=" + CIF_ID.Text;
                        }
                    }
                }
                else
                {


                    if (Type == CifType.INDIVIDUAL)
                        lblEdit.PostBackUrl = "~/UpdateIndividual.aspx?ID=" + CIF_ID.Text + "&Action=review";
                    else if (Type == CifType.NEXT_OF_KIN)
                        lblEdit.PostBackUrl = "~/NextOfKin.aspx?ID=" + CIF_ID.Text + "&Action=review";
                    else if (Type == CifType.GOVERNMENT)
                        lblEdit.PostBackUrl = "~/UpdateGovernment.aspx?ID=" + CIF_ID.Text + "&Action=review";
                    else if (Type == CifType.OFFICE)
                        lblEdit.PostBackUrl = "~/UpdateOffice.aspx?ID=" + CIF_ID.Text + "&Action=review";
                    else if (Type == CifType.MINOR)
                        lblEdit.PostBackUrl = "~/UpdateMinorCIF.aspx?ID=" + CIF_ID.Text + "&Action=review";
                    else
                        lblEdit.PostBackUrl = "~/UpdateBusiness.aspx?ID=" + CIF_ID.Text + "&Action=review";
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