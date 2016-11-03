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

namespace CAOP.AOF
{
    public partial class BioMetricReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User LogedUser = Session["User"] as User;
            if (LogedUser == null)
                Response.Redirect("Login.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtCnic.Text.Length > 0)
            {
                CreatePDF(txtCnic.Text);
            }
        }

        private void CreatePDF(string CNIC)
        {

            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            using (SqlConnection dbCon = new SqlConnection(ConfigurationManager.ConnectionStrings["threetier"].ConnectionString))
            {
                DataSet ds = SqlHelper.ExecuteDataset(dbCon, "spGetBioMetric", new SqlParameter("CNIC", CNIC));

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = "AOF/BioMetric.rdlc";

                ReportViewer1.LocalReport.DisplayName = "BioMEtric";

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsBio", ds.Tables[0]));
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

        private void GetData(DataTable dt, string sql)
        {
            //DataTable results = new DataTable();
            SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["threetier"].ToString());
            try
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand(sql, myConnection);
                SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

                adapter.Fill(dt);
                myConnection.Close();

                // return results;


            }
            catch (Exception e)
            {
                //  return new DataTable();
            }

        }
    }
}