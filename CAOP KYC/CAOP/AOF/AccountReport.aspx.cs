using CAOP.DataSets;
//using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Microsoft.Reporting.WebForms;

namespace CAOP
{
    public partial class AccountReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User LogedUser = Session["User"] as User;
            if (LogedUser == null)
                Response.Redirect("Login.aspx");

            List<AccountNatureCurrency> b = new List<AccountNatureCurrency>();
            grdAccounts.DataSource = b;
            grdAccounts.DataBind();

            //byte[] bytes = System.IO.File.ReadAllBytes("E://Test.pdf");
            //System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            //response.Clear();
            //response.AddHeader("Content-Type", "binary/octet-stream");
            //response.AddHeader("Content-Disposition",
            //                "attachment; filename=filename.pdf; size=" + bytes.Length.ToString());
            //response.Flush();
            //response.BinaryWrite(bytes);
            //response.Flush();
            //response.End();

            ////  dsAOF obj = new dsAOF();
            //  DataRow dr = obj.dtAccInfo.NewRow();
            //  obj.dtAccInfo.Rows.Add(dr);
            //   int a = obj.dtAccInfo.Rows.Count;
            //  DataTable dt = new DataTable("Table1");
            //  obj.Tables.Add(dt);

            //  CrystalReportViewer1.ReportSource = rpt;
            //  CrystalReportViewer1.DataBind();




        }


        private void ReportData(int AccountId)
        {
            dsAOF obj = new dsAOF();
            string sql = @"Select b.BRANCH_CODE as [KYBRCODE], b.NAME as [KYBRNAME], 
            (SELECT NAME FROM ACCOUNT_TYPES WHERE ID = anc.ACCOUNT_TYPE) [KYACTYPE],
            anc.PROFILE_ACCOUNT_NO [KYACNUMB],anc.ACCOUNT_TITLE [AcTitle], anc.ACCOUNT_ENTRY_DATE [DateAcOpen],
            anc.INITIAL_DEPOSIT [InitialDeposit], 
            (SELECT NAME FROM ACCOUNT_OPEN_TYPE WHERE ID = anc.ACCOUNT_TYPE) [KYACTYPE],
            (SELECT NAME FROM CURRENCY WHERE ID = anc.CURRENCY) [Currency] ,
            (ai.BUILDING_SUITE + ' ' + ai.STREET + ' ' + ai.FLOOR + ' ' +  (SELECT NAME FROM CITIES WHERE ID = ai.CITY ) ) [MailAddress],
            (SELECT NAME FROM CITIES WHERE ID = ai.CITY) [MailCity],
            ai.POSTAL_CODE [MailPCode], ai.DISTRICT [MailTehsil],  ai.TEL_OFFICE [TelOff], ai.TEL_RESIDENCE [TelRes], ai.MOBILE_NO [Mob],
            ai.FAX_NO [Fax], 
             OI.AUTHORITY_TO_OPERATE [AcOperate],
            CASE anc.ACCOUNT_MODE WHEN 1 THEN 'Single' ELSE 'JOINT' END  as [OperateAuthority],
            (SELECT NAME FROM ACCOUNT_STATEMENT_FREQUENCY WHERE ID = OI.ACCOUNT_STATEMENT_FREQUENCY) [AcStatement],
            CASE OI.ATM_CARD_REQUIRED WHEN 1 THEN '1' ELSE '0' END as [ATMREQ],
            OI.CUSTOMER_NAME_ON_ATMCARD [ATMNAME],
            ai.SMS_ALERT_REQUIRED as [SMSREQ],
            (SELECT EMAIL FROM EMAILS e WHERE e.BI_ID = anc.ID) [Email],
            ( SELECT PROFILE_CIF_NO FROM BASIC_INFORMATIONS WHERE ID = (  Select CUSTOMER_CIF_NO from APPLICANT_INFORMATION_CIFS a where a.BI_ID = anc.ID and IS_PRIMARY_ACCOUNT_HOLDER = 1)) As MICIFNO,
            CASE OI.ZAKAT_DEDUCTION WHEN 1 THEN '1' ELSE '0' END as [ZakatTag]
            From ACCOUNT_NATURE_CURRENCY anc
            JOIN USERS u on u.USER_ID = anc.USERID
            join BRANCHES b on  u.PARENT_ID = b.BRANCH_ID
            join ADDRESS_INFORMATION ai on anc.ID = ai.BI_ID
            join OPERATING_INSTRUCTIONS OI on anc.ID = OI.BI_ID
            WHERE anc.ID = " + AccountId;

            GetData(obj.dtAccInfo, sql);

            sql = @"SELECT EMAIL,  
                    CASE REQUIRED_ESTATEMEN WHEN 1 THEN '1' ELSE '0' END  as [ESTAT] FROM EMAILS 
                    WHERE BI_ID = " + AccountId;
            GetData(obj.dtEmails, sql);

            sql = @"SELECT b.BRANCH_CODE as [KYBRCODE],
                    (SELECT NAME FROM ACCOUNT_TYPES WHERE ID = anc.ACCOUNT_TYPE) [KYACTYPE],anc.PROFILE_ACCOUNT_NO [KYACNUMB],BI.PROFILE_CIF_NO As CIFNO,
                    (SELECT NAME FROM TITLES WHERE ID = BI.TITLE) [Title],
                    BI.NAME [Name], BI.NAME_FH [FName], BI.NAME_MOTHER [MName], BI.DOB [DOB], 
                    (SELECT TOP 1 COUNTRY FROM NATIONALITIES_BASIC_INFORMATION n WHERE n.BI_ID = BI.ID) [Nationality],
                    CASE BI.DOCUMENT_TYPE_PRIMARY WHEN 1 THEN BI.CNIC ELSE '' END as [CNIC],
                    IDEN.PLACE_ISSUE_CNIC [PIssue], LEFT(CONVERT(VARCHAR, CNIC_DATE_ISSUE, 120), 10) [DIssue], IDEN.EXPIRY_DATE [Expiry],IDEN.NTN,
                    (SELECT NAME FROM IDENTITY_TYPES i WHERE i.ID = IDEN.IDENTITY_TYPE) [OID], 
                    IDEN.IDENTITY_NO [OIDNO], IDEN.PLACE_ISSUE [OIDISSUEPLACE], IDEN.OTHER_IDENTITY_EXPIRY_DATE [OIDEXPDATE],
                    (SELECT NAME FROM COUNTRIES WHERE ID = IDEN.COUNTRY_ISSUE) [OIDCOUNTRY] ,
                    cinfo.BIULDING_SUITE + ' ' + cinfo.FLOOR + ' ' + cinfo.STREET As PAddress,
                    (SELECT NAME FROM CITIES WHERE CITIES.ID = cinfo.CITY_PERMANENT) [City],
                    cinfo.EMAIL [Email], cinfo.OFFICE_CONTACT [TelOff], cinfo.RESIDENCE_CONTACT [TelRes], cinfo.MOBILE_NO [Mob],
                    cinfo.FAX_NO [Fax],
                    (SELECT NAME FROM EMPLOYMENT_DETAILS ed WHERE ed.ID = ei.EMPLOYMENT_DETAIL) [EmploymentDetail],
                    (SELECT ec.CODE FROM EMPLOYER_CODES ec WHERE ec.ID = ei.EMPLOYER_CODE) [Employer], ei.EMPLOYER_BUSINESS_ADDRESS [EAddress],
                    (SELECT TOP 1 'YES - ' + CONVERT(varchar(23), DATETIME, 121) FROM NADRA_INFO WHERE CNIC = REPLACE(BI.CNIC,'-','') and STATUSCODE = '100' order by DATETIME DESC) as GLCODE
                    FROM  ACCOUNT_NATURE_CURRENCY anc
                    JOIN USERS u on u.USER_ID = anc.USERID
                    join BRANCHES b on  u.PARENT_ID = b.BRANCH_ID
                    join APPLICANT_INFORMATION_CIFS aicifs on aicifs.BI_ID = anc.ID
                    JOIN BASIC_INFORMATIONS BI ON BI.ID = aicifs.CUSTOMER_CIF_NO
                    JOIN IDENTITIES IDEN on IDEN.BI_ID = BI.ID
                    JOIN CONTACT_INFOS cinfo on cinfo.BI_ID = BI.ID
                    JOIN EMPLOYMENT_INFORMATIONS ei on ei.BI_ID = BI.ID
                    WHERE aicifs.IS_PRIMARY_ACCOUNT_HOLDER = 1 and anc.ID = " + AccountId;

            GetData(obj.dtAppInfo, sql);

            sql = @"SELECT NEXT_OF_KIN_NAME [NAME], NEXT_OF_KIN_CNIC [CNIC], BUILDING + ' ' + STREET + ' ' + [FLOOR] as MADDRESS, DISTRICT [MTEHSIL], 
                    (SELECT NAME FROM CITIES WHERE ID = CITY) [MCITY], OFFICE_NO [TELOFF], RESIDENCE_CONTACT [TELRES], MOB_NO [MOBILE],
                    (SELECT NAME FROM RELATIONSHIP WHERE ID = RELATIONSHIP) [RELATE]
                     FROM NEXT_OF_KIN_INFO WHERE BI_ID = " + AccountId;

            GetData(obj.dtNOKInfo, sql);

            sql = @" SELECT  CERTIFICATE_NUMBER [CertificateNo],  
                    CERTIFCATE_AMOUNT [Amount] FROM CERTIFICATE_DEPOSIT_INFO WHERE BI_ID = " + AccountId;
            GetData(obj.dtCerInfo, sql);


            sql = @"	SELECT SERIAL_NO [SNO], Descr [DESC],
				CASE value WHEN 'Yes' THEN '1' ELSE '0'END as [YES],
				CASE value WHEN 'No' THEN '1' ELSE '0'END as [NO],
				CASE value WHEN 'N/A' THEN '1' ELSE '0' END as [NA]
				 FROM DOCUMENTS d
				join Description_Documents dd on d.ID = dd.ID
				WHERE d.BI_ID = " + AccountId;
            GetData(obj.dtDOCS, sql);

            sql = @"SELECT (SELECT NAME FROM CUSTOMER_TYPE WHERE ID = kyn.CUSTOMER_TYPE ) as [CusType],
                  kyn.DESCRIPTION_IF_REFFERED [RefBy], 
                  (SELECT NAME FROM PURPOSE_OF_ACCOUNT WHERE ID = kyn.PURPOSE_OF_ACCOUNT ) as [AcPurpose],
                  kyn.DESCRIPTION_IF_OTHER [AcPurposeOther],
                  (SELECT NAME FROM SOURCE_OF_FUNDS WHERE ID = kyn.SOURCE_OF_FUNDS ) as [AcFundSource],
                  kyn.DESCRIPTION_OF_SOURCE [AcFundSourceOther], 
                  CASE SERVICE_CHARGES_EXEMPTED WHEN 1 THEN 'YES' ELSE 'NO' END as [SChargesTag],
                  (SELECT NAME FROM SERVICE_CHARGES_EXEMPTED_CODE WHERE ID = kyn.SERVICE_CHARGES_EXEMPTED_CODE) as [SChargesValue],
                  kyn.REASON_IF_EXEMPTED [SChargesDesc], kyn.EXPECTED_MONTHLY_INCOME [MonthlyIncome], 
                  (SELECT NAME FROM MODE_OF_TRANSACTIONS WHERE ID = kyn.MODE_OF_TRANSACTIONS) [TrMode],
                  kyn.OTHER_MODE_OF_TRANSACTIONS [TrModeOther], MAX_TRANS_AMOUNT_DR [MaxDrAmount], MAX_TRANS_AMOUNT_CR [MaxCrAmount],
                  kyn.NAME_OTHER [RealBeneName], kyn.CNIC_OTHER [RealBeneCNIC], 
                  (Select NAME FROM RELATIONSHIP where ID = kyn.RELATIONSHIP_WITH_ACCOUNTHOLDER) [RealBeneRelate] 
                   FROM KNOW_YOUR_CUSTOMER kyn WHERE BI_ID = " + AccountId;

            GetData(obj.dtKYC, sql);


            //DataTable AppInfo = GetData("");
            //AppInfo.TableName = "dtAccInfo";
            //obj.Tables.Add(AppInfo);

            //DataTable BankingRelationship = GetData("");
            //BankingRelationship.TableName = "dtBankRelationship";
            //obj.Tables.Add(BankingRelationship);

            //DataTable AccInfoBgo = GetData("");
            //AccInfoBgo.TableName = "dtAccInfoBGO";
            //obj.Tables.Add(AccInfoBgo);

            //DataTable AppInfoBgo = GetData("");
            //AppInfoBgo.TableName = "dtAppInfoBGO";
            //obj.Tables.Add(AppInfoBgo);

            //DataTable KYC = GetData("");
            //KYC.TableName = "dtKYC";
            //obj.Tables.Add(KYC);

            //DataTable NoKInfo = GetData("");
            //NoKInfo.TableName = "dtNOKInfo";
            //obj.Tables.Add(NoKInfo);

            //DataTable KycBgo = GetData("");
            //KycBgo.TableName = "dtKYCBGO";
            //obj.Tables.Add(KycBgo);

            //DataTable dtWhoAuth = GetData("");
            //dtWhoAuth.TableName = "dtWhoAuth";
            //obj.Tables.Add(dtWhoAuth);

            //DataTable Docs = GetData("");
            //Docs.TableName = "dtDOCS";
            //obj.Tables.Add(Docs);

            //DataTable Emails = GetData("");
            //Emails.TableName = "dtEmails";
            //obj.Tables.Add(Emails);

            //DataTable KYCBGO_SupCust = GetData("");
            //KYCBGO_SupCust.TableName = "dtKYCBGO_SupCust";
            //obj.Tables.Add(KYCBGO_SupCust);

            //DataTable LOT = GetData("");
            //LOT.TableName = "dtLOT";
            //obj.Tables.Add(LOT);

            //DataTable AccInfoOO = GetData("");
            //AccInfoOO.TableName = "dtAccInfoOO";
            //obj.Tables.Add(AccInfoOO);

            //DataTable WhoAuthOO = GetData("");
            //WhoAuthOO.TableName = "dtWhoAuthOO";
            //obj.Tables.Add(WhoAuthOO);

            //DataTable Sign = GetData("");
            //Sign.TableName = "dtSign";
            //obj.Tables.Add(Sign);

            //DataTable CertInfo = GetData("");
            //CertInfo.TableName = "dtCerInfo";
            //obj.Tables.Add(CertInfo);

            //DataTable Tdr = GetData("");
            //Tdr.TableName = "dtTDR";
            //obj.Tables.Add(Tdr);

            //ReportDocument rpt = new ReportDocument();
            string reportPath = Server.MapPath("~/CrystalReports/CR_AOF_IND_ISA.rpt");
            //rpt.Load(reportPath);
            //rpt.SetDataSource(obj);
            //// Stop buffering the response
            //Response.Buffer = false;
            //// Clear the response content and headers
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.Cache.SetCacheability(HttpCacheability.Public);
            //Response.ContentType = "application/pdf";

            //rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "AccountInfo");
            // rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "E://Test.pdf");

            //  byte[] bytes = System.IO.File.ReadAllBytes("E://Test.pdf");
            //System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
            //Response.Clear();
            //Response.AddHeader("Content-Disposition", "inline; filename=test");
            //Response.ContentType = "application/pdf";
            //Response.Buffer = true;
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.BinaryWrite(bytes);
            //Response.End();
            //Response.Close();

            //System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            //response.Clear();
            //response.AddHeader("Content-Type", "binary/octet-stream");
            //response.AddHeader("Content-Disposition",
            //                "attachment; filename=filename.pdf; size=" + bytes.Length.ToString());
            //response.Flush();
            //response.BinaryWrite(bytes);
            //response.Flush();
            //response.End();

            //  Response.Clear();

            //Response.AddHeader("content-disposition", "attachment; filename=" + "test.pdf");
            //Response.WriteFile("E://Test.pdf");
            //Response.ContentType = "";
            //Response.End();


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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtAccountNumber.Text.Length > 0)
            {
                User LogedUser = Session["User"] as User;
                AccOpen acopen = new AccOpen(-1);
                List<AccountNatureCurrency> anc = acopen.GetAccountByNumber(txtAccountNumber.Text,LogedUser.Branch.BRANCH_CODE);
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

            int AccountId = Convert.ToInt32(((Label)gvr.FindControl("btnAccountID")).Text);
            int AccountType = Convert.ToInt32(((Label)gvr.FindControl("btnAccountType")).Text);
          //  ReportData(AccountId);
            CreatePDF(AccountId, AccountType);
        }

        private void CreatePDF(int AccountId, int AccountType)
        {
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            using (SqlConnection dbCon = new SqlConnection(ConfigurationManager.ConnectionStrings["threetier"].ConnectionString))
            {
                if (AccountType == 1)
                {
                    DataSet ds = SqlHelper.ExecuteDataset(dbCon, "spGetDataForForm", new SqlParameter[] { new SqlParameter("MODE", "1"), new SqlParameter("ACN", AccountId) });
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = "AOF/AOFIndividual.rdlc";
                    ReportViewer1.LocalReport.DisplayName = "AccountOpeningForm - " + ds.Tables[0].Rows[0]["KYACNUMB"].ToString();
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsAOFI", ds.Tables[0]));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsCIF", ds.Tables[1]));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsKYC", ds.Tables[2]));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsNOK", ds.Tables[3]));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsCIFOthers", ds.Tables[4]));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dtFatcas", ds.Tables[5]));




                    ReportViewer1.LocalReport.Refresh();
                }
                else if (AccountType == 2)
                {
                    DataSet ds = SqlHelper.ExecuteDataset(dbCon, "spGetDataForForm", new SqlParameter[] { new SqlParameter("MODE", "2"), new SqlParameter("ACN", AccountId) });
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = "AOF/AOFBusiness.rdlc";
                    ReportViewer1.LocalReport.DisplayName = "AccountOpeningForm - " + ds.Tables[0].Rows[0]["KYACNUMB"].ToString();
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsAOFI", ds.Tables[0]));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsCIF", ds.Tables[2]));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsKYC", ds.Tables[4]));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dtRealBeneficiary", ds.Tables[5]));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dtShareHolderInfo", ds.Tables[6]));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dtAuthOperate", ds.Tables[3]));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dtBusinessInfo", ds.Tables[1]));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dtFatcas", ds.Tables[7]));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dtAuthGivenBy", ds.Tables[8]));

                    ReportViewer1.LocalReport.Refresh();
                }
                else
                {
                    DataSet ds = SqlHelper.ExecuteDataset(dbCon, "spGetDataForForm", new SqlParameter[] { new SqlParameter("MODE", "4"), new SqlParameter("ACN", AccountId) });
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = "AOF/Report.rdlc";
                    ReportViewer1.LocalReport.ReportPath = "Report.rdlc";
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));

                    //ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsNOK", ds.Tables[3]));
                    //ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsMI", ds.Tables[4]));

                    ReportViewer1.LocalReport.Refresh();
                }
                

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