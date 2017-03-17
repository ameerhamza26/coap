using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using ExtensionMethods;
using System.IO;
using System.Data;

namespace CAOP
{
    public partial class UpdateAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User LogedUser = Session["User"] as User;
            if (LogedUser == null)
                Response.Redirect("Login.aspx");
            CheckPermissions(LogedUser);
            List<AccountNatureCurrency> ar = new List<AccountNatureCurrency>();
            grdPCif.DataSource = ar;
            grdPCif.DataBind();

            if (!IsPostBack) { 
                //loaddata();
                //if (LogedUser.USER_TYPE == UserType.Region)
                //{
                //    SearchCriteria.Visible = true;
                //}
            }
        }

        private void loaddata()
        {

            if (txtCif.Text.Length > 0)
            {
                User LoggedUser = Session["User"] as User;
                AccOpen ac = new AccOpen(LoggedUser.USER_ID);
                List<AccountNatureCurrency> aclist = new List<AccountNatureCurrency>();
                aclist = ac.SearchAccount(txtCif.Text, LoggedUser.USER_ID);
                if (aclist.Count > 0)
                {
                    grdPCif.DataSource = aclist;
                    grdPCif.DataBind();
                }
            }


        }

        private void loadDataRegion()
        {
            User LoggedUser = Session["User"] as User;

            AccOpen ac = new AccOpen(LoggedUser.USER_ID);

            if (radioBCode.Checked)
                grdPCif.DataSource = ac.GetAccountRegion(true, false, false, txtAccount.Text, LoggedUser.PARENT_ID);
            else if (radioAno.Checked)
                grdPCif.DataSource = ac.GetAccountRegion(false, true, false, txtAccount.Text, LoggedUser.PARENT_ID);
            else
                grdPCif.DataSource = ac.GetAccountRegion(false, false, true, txtAccount.Text, LoggedUser.PARENT_ID);
            grdPCif.DataBind();
        }

        public string GetProfileCifNumber(string CNIC)
        {
            CAOP.CustomerInquiry.ProfileConnectorTest connector = new CustomerInquiry.ProfileConnectorTest();


            string strQuery = "";
            string XMLDataToString = "";
            StringReader sr = new StringReader("");
            DataSet ds = new DataSet();


            try
            {
                strQuery = "Select TJD from CUVAR";
                XMLDataToString = connector.GetXMLData(strQuery);
                sr = new StringReader(XMLDataToString.Replace("&", " and ").Replace("<2", ""));
                ds = new DataSet();
                ds.ReadXml(sr);

            }
            catch (Exception ex)
            {
                //lblError.Text = ex.Message;
                //lblError.Visible = true;
                return "error";

            }

            try
            {
                XMLDataToString = connector.CIFEnquiryCNIC("CIF.ZCNIC=" + CNIC + ",CIF.TYPE=0");
                sr = new StringReader(XMLDataToString.Replace("&", " and ").Replace("<2", ""));
                ds = new DataSet();
                ds.ReadXml(sr);
            }
            catch (Exception ey)
            {
                //lblError.Text = ey.Message;
                //lblError.Visible = true;
                return "error";
            }

            if (IsEmpty(ds))
            {
                return "-1";
            }
            else
            {
                var CifsData = ds.Tables[0].Rows[0][0].ToString().Split('\n');



                string CifNums = "";
                for (int i = 0; i < CifsData.Length; i++)
                {
                    CifNums += CifsData[i].Split('|')[2] + ",";
                }

                return CifNums.TrimEnd(',');
            }
        }

        bool IsEmpty(DataSet dataSet)
        {
            return !dataSet.Tables.Cast<DataTable>().Any(x => x.DefaultView.Count > 0);
        }

        protected void grdPCif_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPCif.PageIndex = e.NewPageIndex;
            if (Convert.ToBoolean(ViewState["isSearch"]) != true)
                loaddata();
            else
                loadDataRegion();
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
                        lblEdit.PostBackUrl = "~/UpdateAccountIndividual.aspx?ID=" + CIF_ID.Text;
                    else if (type == AccountOpenTypes.GOVERNMENT)
                        lblEdit.PostBackUrl = "~/UpdateAccount_Government.aspx?ID=" + CIF_ID.Text;
                    else if (type == AccountOpenTypes.OFFICE)
                        lblEdit.PostBackUrl = "~/UpdateAccountOffice.aspx?ID=" + CIF_ID.Text;
                    else
                        lblEdit.PostBackUrl = "~/UpdateAccountBusiness.aspx?ID=" + CIF_ID.Text;
                }
                else
                {
                    if (type == AccountOpenTypes.INDIVIDUAL)
                        lblEdit.PostBackUrl = "~/UpdateAccountIndividual.aspx?ID=" + CIF_ID.Text;
                    else if (type == AccountOpenTypes.GOVERNMENT)
                        lblEdit.PostBackUrl = "~/UpdateAccount_Government.aspx?ID=" + CIF_ID.Text;
                    else if (type == AccountOpenTypes.OFFICE)
                        lblEdit.PostBackUrl = "~/UpdateAccountOffice.aspx?ID=" + CIF_ID.Text;
                    else
                        lblEdit.PostBackUrl = "~/UpdateAccountBusiness.aspx?ID=" + CIF_ID.Text;
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

        protected void btnSearchCriteria_Click(object sender, EventArgs e)
        {
            if (txtAccount.Text.Length > 0)
            {
                ViewState["isSearch"] = true;
                loadDataRegion();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loaddata();
        }
    }
}