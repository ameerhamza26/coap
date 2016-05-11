using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using ExtensionMethods;
using CAOP.ExistingCif;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;



namespace CAOP.LOT
{
    public partial class LOT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User LogedUser = Session["User"] as User;
            if (LogedUser == null)
                Response.Redirect("Login.aspx");

            List<AccountNatureCurrency> b = new List<AccountNatureCurrency>();
            grdAccounts.DataSource = b;
            grdAccounts.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtAccountNumber.Text.Length > 0)
            {
                User LogedUser = Session["User"] as User;
                AccOpen acopen = new AccOpen(-1);
                List<AccountNatureCurrency> anc = acopen.GetAccountByNumber(txtAccountNumber.Text, LogedUser.Branch.BRANCH_CODE);
                grdAccounts.DataSource = anc;
                grdAccounts.DataBind();
            }

        }



        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //Get the button that raised the event
            Button btn = (Button)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            string AccountNum = (((Label)gvr.FindControl("btnAccountNum")).Text);
            CreatePDF(AccountNum.ToString());

        }

        private void CreatePDF(string accountNo)
        {

            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            using (SqlConnection dbCon = new SqlConnection(ConfigurationManager.ConnectionStrings["threetier"].ConnectionString))
            {
                DataSet ds = SqlHelper.ExecuteDataset(dbCon, "spGetDataForForm", new SqlParameter[] { new SqlParameter("MODE", "3"), new SqlParameter("ACN", accountNo) });

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = "LOT/LOT.rdlc";

                ReportViewer1.LocalReport.DisplayName = "Letter of Thanks - " + accountNo;

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsLOT", ds.Tables[0]));
                ReportViewer1.LocalReport.Refresh();

                byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);


                // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + ReportViewer1.LocalReport.DisplayName + "." + extension);
                Response.BinaryWrite(bytes); // create the file
                Response.Flush(); // send it to the client to download
            }
        }


    }
}

    
