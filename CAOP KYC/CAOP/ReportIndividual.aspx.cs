using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
//using Microsoft.Reporting.WebForms;

namespace CAOP
{
    public partial class ReportIndividual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           // CAOPDataTableAdapters.GetBasicInfoTableAdapter ta = new CAOPDataTableAdapters.GetBasicInfoTableAdapter();
           // CAOPData.GetBasicInfoDataTable dt = new CAOPData.GetBasicInfoDataTable();
           // //CAOPDataSetTableAdapters.BASIC_INFORMATIONSTableAdapter ta = new CAOPDataSetTableAdapters.BASIC_INFORMATIONSTableAdapter();
           //// CAOPDataSet.BASIC_INFORMATIONSDataTable dt = new CAOPDataSet.BASIC_INFORMATIONSDataTable();
           // ta.Fill(dt, Convert.ToInt32(TextBox1.Text));
           // //MyDataSetTableAdapters.TestProcedureTableAdapter ta = new MyDataSetTableAdapters.TestProcedureTableAdapter();
           // //MyDataSet.TestProcedureDataTable dt = new MyDataSet.TestProcedureDataTable();
           // //ta.Fill(dt, Convert.ToInt16(TextBox1.Text));
           // //
           // ReportDataSource rds = new ReportDataSource();
           // rds.Name = "DataSet1";
           // rds.Value = dt;

           // ReportParameter rp = new ReportParameter("id", TextBox1.Text.ToString());
           // ReportViewer1.LocalReport.DataSources.Clear();


           // ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RDLC/Report1.rdlc");
           // ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
           // ReportViewer1.LocalReport.DataSources.Add(rds);
           // ReportViewer1.LocalReport.Refresh();
        }
    }
}