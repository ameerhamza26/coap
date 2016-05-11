using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data.SqlClient;
using System.Drawing;
using Profile;


namespace CAOP.CrmTaggingForms
{
        
    public partial class CRMTagging : System.Web.UI.Page
    {
        string ConnectionStringSql = "";

        protected void Page_Load(object sender, EventArgs e)
        {
           // User LoggedUser = Session["User"] as User;
           // if (LoggedUser == null)
            //    Response.Redirect("~/Login.aspx");

            ConnectionStringSql = "Server=10.11.5.58;user=sa;Password=saSRVS01;Initial Catalog=Profile";
        }

        protected void btnTagAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (GetBranchType() == "C")
                    TagAccount();
                else
                    TagAccountIslamic();
            }
            catch (Exception exc) { lblMessage.ForeColor = Color.Red; lblMessage.Text = exc.Message; }
        }

        public void TagAccount()
        {

            String Sql = "", XMLDataToString = "";
            string IsNewBranch = "", acn = "", ArmyAcNo = "";
            string DefaultCheques = "", ChequeIssue = "", ATMTag = "";
            clsDataBaseSql serv = new clsDataBaseSql();
            DataTable dtACN = new DataTable();
            DataTable dtCIF = new DataTable();
            DataTable dtRelCIF = new DataTable();
            DataTable dtChkArmy = new DataTable();
            DataTable dtChkProductCon = new DataTable();
            DataTable dtChkDataSend = new DataTable();
            //Sql = "select zcnic,ZNTN,zconcnic,nam,mad1,mad2,mad3,mad4,pcity,mmname,dob,sex,zmobile,odt,b.boo,cid,B.TYPE,TITLE1,IBAN,CRCD,DESC,a.ZUPDCRMDT,ZMOBILEREG,HPH,BPH,ZHOMEREG,ZBUSINREG,CONVACN,'No Error' FROM CIF A,ACN B,UTBLBRCD BR WHERE A.ACN=B.ACN AND A.BOO=BR.BRCD AND B.BOO=BR.BRCD " +
            //                                " AND B.BOO=" + Convert.ToInt32(txtBranchCode.Text.Trim()) + " AND STAT<>4 and CID in (" + txtAccount.Text.Trim() + ")   order by CID ";

            // get data frim acn table

            try
            {
                JavaWebReferenceTagging.ProfileConnector moobjProfileCS = new JavaWebReferenceTagging.ProfileConnector();

                //changes done by asif on 11-august-2015 for related cif issue
                // get all CIF attached with Account from relCIF
                Sql = "SELECT ACN FROM RELCIF WHERE CID IN (" + txtAccount.Text.Trim() + ")";

                clsUtility.WriteLog("Account Tagging Request (acn)" + txtAccount.Text.Trim());
                XMLDataToString = moobjProfileCS.GetXMLData(Sql);
                clsUtility.WriteLog("Account Tagging Response (acn)" + txtAccount.Text.Trim());

                XMLDataToString = XMLDataToString.Replace("&", "and");
                XMLDataToString = XMLDataToString.Replace("'", "''");


                StringReader sr = new StringReader(XMLDataToString);
                DataSet ds = new DataSet();
                ds.ReadXml(sr);

                if (ds.Tables.Count > 0)
                {
                    ArmyAcNo = "N";
                    DefaultCheques = "";
                    ChequeIssue = "N";
                    ATMTag = "N";
                    string ArmyProduct = "N", StrUpdate = "0", StrInsert = "0";


                    dtRelCIF = ds.Tables[0];
                    // get all acn from the data
                    for (int ac = 0; ac < dtRelCIF.Rows.Count; ac++)
                        acn = acn + dtRelCIF.Rows[ac]["acn"].ToString() + ",";

                    acn = acn.Trim(',');


                    Sql = "select acn,odt,b.boo,cid,B.TYPE,TITLE1,IBAN,CRCD,DESC,CONVACN,'No Error' FROM ACN B,UTBLBRCD BR WHERE B.BOO=BR.BRCD  AND B.BOO=" + txtBranchCode.Text.Trim() + " AND STAT<>4 and ACN in (" + acn + ")  and CID in (" + txtAccount.Text.Trim() + " ) ";
                    //and CID in (" +txtAccount.Text.Trim() + " )

                    //localhost.ProfileConnector moobjProfileCS = new localhost.ProfileConnector();

                    clsUtility.WriteLog("Account Tagging Request (acn)" + txtAccount.Text.Trim());
                    XMLDataToString = moobjProfileCS.GetXMLData(Sql);
                    clsUtility.WriteLog("Account Tagging Response (acn)" + txtAccount.Text.Trim());

                    XMLDataToString = XMLDataToString.Replace("&", "and");
                    XMLDataToString = XMLDataToString.Replace("'", "''");


                    sr = new StringReader(XMLDataToString);
                    ds = new DataSet();
                    ds.ReadXml(sr);


                    dtACN = ds.Tables[0];
                    GC.Collect();


                    dtACN.Columns.Add("ChequeIssue", typeof(string));
                    dtACN.Columns.Add("DefaultCheques", typeof(string));
                    dtACN.Columns.Add("ATMTag", typeof(string));
                    dtACN.Columns.Add("ArmyAcNo", typeof(string));
                    dtACN.Columns.Add("InsertTag", typeof(string));
                    dtACN.Columns.Add("UpdateTag", typeof(string));

                    for (int t = 0; t < dtACN.Rows.Count; t++)
                    {
                        string AccountNumner = dtACN.Rows[t]["cid"].ToString();
                        string ODTDate = Convert.ToDateTime(dtACN.Rows[t]["odt"]).ToString("MM/dd/yyyy");

                        //Product Confriguration
                        Sql = "select * from ProductConfiguration where ProductCode = " + dtACN.Rows[t]["TYPE"].ToString() + "  ";
                        dtChkProductCon = serv.GetTable(ConnectionStringSql, Sql);
                        if (dtChkProductCon.Rows.Count > 0)
                        {
                            ArmyProduct = dtChkProductCon.Rows[0]["ArmyProduct"].ToString();

                            if (ArmyProduct == "N")
                            {
                                //checking Army accounts 
                                Sql = "select * from ArmyTaggingFile where AccountNumber = '" + AccountNumner + "' and Tag = 'S' ";
                                dtChkArmy = serv.GetTable(ConnectionStringSql, Sql);
                                if (dtChkArmy.Rows.Count > 0)
                                {
                                    ArmyAcNo = "Y";
                                }
                            }
                            else
                                ArmyAcNo = "Y";


                            //check data already send 
                            Sql = "select * from ZZ_UPLOADEDACCOUNTS where AccountNumber = '" + AccountNumner + "' ";
                            dtChkDataSend = serv.GetTable(ConnectionStringSql, Sql);
                            if ((dtChkDataSend.Rows.Count == 0) && (ArmyAcNo == "Y"))
                            {
                                StrUpdate = "0";
                                StrInsert = "1";
                                ChequeIssue = dtChkProductCon.Rows[0]["ChequeIssue"].ToString();
                                if (ChequeIssue == "Y")
                                    DefaultCheques = dtChkProductCon.Rows[0]["DefaultCheques"].ToString();

                                ATMTag = dtChkProductCon.Rows[0]["ATMTag"].ToString();

                            }
                            else
                                StrUpdate = "1";

                        }
                        else
                        {
                            Sql = "select * from ArmyTaggingFile where AccountNumber = '" + AccountNumner + "' and Tag = 'S' ";
                            dtChkArmy = serv.GetTable(ConnectionStringSql, Sql);
                            if (dtChkArmy.Rows.Count > 0)
                            {
                                ArmyAcNo = "Y";
                                StrUpdate = "1";
                            }
                            else
                            {
                                string ChkDate = ConfigurationManager.AppSettings["CutoffDate"].ToString();
                                //string ChkDate = "03/17/2016";
                                if (Convert.ToDateTime(ODTDate) <= Convert.ToDateTime(ChkDate))
                                {

                                    StrUpdate = "1";
                                    StrInsert = "0";
                                }
                                else
                                {
                                    Sql = "select * from ZZ_UPLOADEDACCOUNTS where AccountNumber = '" + AccountNumner + "' ";
                                    dtChkDataSend = serv.GetTable(ConnectionStringSql, Sql);
                                    if (dtChkDataSend.Rows.Count == 0)
                                    {
                                        StrUpdate = "0";
                                        StrInsert = "1";
                                    }
                                    else
                                    {
                                        StrUpdate = "1";
                                        StrInsert = "0";

                                    }
                                }
                            }
                        }


                        dtACN.Rows[t]["ChequeIssue"] = ChequeIssue;
                        dtACN.Rows[t]["DefaultCheques"] = DefaultCheques;
                        dtACN.Rows[t]["ATMTag"] = ATMTag;
                        dtACN.Rows[t]["ArmyAcNo"] = ArmyAcNo;
                        dtACN.Rows[t]["InsertTag"] = StrInsert;
                        dtACN.Rows[t]["UpdateTag"] = StrUpdate;

                        dtACN.AcceptChanges();

                    }

                    //take type , EMPNUM, zbizsec, zbiztyp
                    Sql = "select acn,boo,zcnic,ZNTN,zconcnic,nam,mad1,mad2,mad3,mad4,pcity,mmname,dob,sex,zmobile,ZUPDCRMDT,ZMOBILEREG,HPH,BPH,ZHOMEREG,ZBUSINREG, type , EMPNUM, zbizseg, zbiztyp from CIF where acn in (" + acn + ")";
                    clsUtility.WriteLog("Account Tagging Request (CIF)" + txtAccount.Text.Trim());
                    XMLDataToString = moobjProfileCS.GetXMLData(Sql);
                    clsUtility.WriteLog("Account Tagging Response (CIF)" + txtAccount.Text.Trim());

                    XMLDataToString = XMLDataToString.Replace("&", "and");
                    XMLDataToString = XMLDataToString.Replace("'", "''");

                    sr = new StringReader(XMLDataToString);
                    ds = new DataSet();
                    ds.ReadXml(sr);

                    //DataTable dtCIFNew = new DataTable();
                    //dtCIFNew = ds.Tables[0];
                    dtCIF = ds.Tables[0];
                    //if (dtCIF.Columns.Count > 22)
                    //{
                    //    dtCIF.Columns.RemoveAt(22);
                    //}

                    if (Convert.ToString(dtCIF.Rows[0]["zcnic"]).Trim() == "" && Convert.ToString(dtCIF.Rows[0]["zntn"]).Trim() == "" && Convert.ToString(dtCIF.Rows[0]["zconcnic"]).Trim() == "")
                    {
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Account has no CNIC/NTN or Contact person cnic";
                        return;
                    }

                    clsUtility.WriteLog("insertDataBulkCopy Request" + txtAccount.Text);
                    serv.insertDataBulkCopy(ConnectionStringSql, dtACN, dtCIF, "acn", "cif", txtBranchCode.Text.Trim(), txtAccount.Text.Trim(), acn.Trim(','));
                    //serv.insertDataBulkCopyTest(ConnectionStringSql, dtACN, dtCIF, "acnTest", "cifTest", txtBranchCode.Text.Trim(), txtAccount.Text.Trim(), acn.Trim(','));
                    clsUtility.WriteLog("insertDataBulkCopy Response" + txtAccount.Text);
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Account Successfully tagged";

                    Sql = "SELECT 'OK' as StatusTag, ZCNIC CNIC,      [NAM] NAME      ,isnull([MAD1],'')+' '+isnull([MAD2],'')+' '+isnull([MAD3],'')+' '+isnull([MAD4],'') ADDRESS1      ,[PCITY] CITY,      [MMNAME] [MOTHER NAME],      [DOB] [DATE OF BIRTH],      [SEX] [GENDER],      [ZMOBILE] [MOBILE NUMBER],      [ODT] [ACCOUNT OPEN DATE],      [BOO] [BR CODE]      ,[CID] [ACC NO]      ,[TYPE] [ACC TYPE]      ,[TITLE1] [ACCOUNT TITLE]      ,[IBAN] [IBAN]      ,[CRCD] [CURRENCY]      ,[DESC] [BRANCH ADDRESS]      ,[ZUPDCRMDT] [UPDATEDDATE]      ,[ZMOBILEREG] [REGISTEREDPNO]      ,[HPH] [HOME PHONE]      ,[BPH] [OFFICE PHONE]      ,[ZHOMEREG] [REGISTEREDHPNO]      ,[ZBUSINREG] [REGISTEREDBPNO]      ,[CONVACN] [CONVACN]      ,[TMP_26] [ERROR]  FROM [Profile].[dbo].[ProfileData] where CID in (" + txtAccount.Text.Trim() + ")" +
            "union all " +
            "SELECT (case b.Status when 'P' then 'Pending' when 'R' then 'Rejected' when 'O' then 'OK' else 'Failure' end) 'StatusTag', a.ZCNIC CNIC,     a.[NAM] NAME      ,isnull(a.[MAD1],'')+' '+isnull(a.[MAD2],'')+' '+isnull(a.[MAD3],'')+' '+isnull(a.[MAD4],'') ADDRESS1      ,a.[PCITY] CITY,      a.[MMNAME] [MOTHER NAME],      a.[DOB] [DATE OF BIRTH],      [SEX] [GENDER],      a.[ZMOBILE] [MOBILE NUMBER],      [ODT] [ACCOUNT OPEN DATE],      a.[BOO] [BR CODE]      ,a.[CID] [ACC NO]      ,a.[TYPE] [ACC TYPE]      ,a.[TITLE1] [ACCOUNT TITLE]      ,a.[IBAN] [IBAN]      ,a.[CRCD] [CURRENCY]      ,a.[DESC] [BRANCH ADDRESS]      ,[ZUPDCRMDT] [UPDATEDDATE]      ,[ZMOBILEREG] [REGISTEREDPNO]      ,a.[HPH] [HOME PHONE]      ,a.[BPH] [OFFICE PHONE]      ,[ZHOMEREG] [REGISTEREDHPNO]      ,[ZBUSINREG] [REGISTEREDBPNO]      ,[CONVACN] [CONVACN]      ,[TMP_26] [ERROR]  FROM [Profile].[dbo].[ProfileDataAassan] a, AassanPFData b where a.CID in  (" + txtAccount.Text.Trim() + ") and a.CID=b.CID";

                    DataTable dtTagged = serv.GetTable(ConnectionStringSql, Sql);
                    gdTaggedAccounts.DataSource = dtTagged;
                    gdTaggedAccounts.DataBind();

                    lblMessage.Text = dtTagged.Rows.Count + " Account(s) Successfully tagged";
                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Account does not exist or does not pertain to this branch";
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message;
            }
        }
        public void TagAccountIslamic()
        {
            String Sql = "", XMLDataToString = "";
            string IsNewBranch = "", acn = "";
            clsDataBaseSql serv = new clsDataBaseSql();
            DataTable dtACN = new DataTable();
            DataTable dtCIF = new DataTable();
            DataTable dtRelCIF = new DataTable();
            //Sql = "select zcnic,ZNTN,zconcnic,nam,mad1,mad2,mad3,mad4,pcity,mmname,dob,sex,zmobile,odt,b.boo,cid,B.TYPE,TITLE1,IBAN,CRCD,DESC,a.ZUPDCRMDT,ZMOBILEREG,HPH,BPH,ZHOMEREG,ZBUSINREG,CONVACN,'No Error' FROM CIF A,ACN B,UTBLBRCD BR WHERE A.ACN=B.ACN AND A.BOO=BR.BRCD AND B.BOO=BR.BRCD " +
            //                                " AND B.BOO=" + Convert.ToInt32(txtBranchCode.Text.Trim()) + " AND STAT<>4 and CID in (" + txtAccount.Text.Trim() + ")   order by CID ";

            // get data frim acn table

            try
            {
                JavaWebReferenceTaggingIslamic.ProfileConnectorIslamic moobjProfileCS = new JavaWebReferenceTaggingIslamic.ProfileConnectorIslamic();

                //changes done by asif on 11-august-2015 for related cif issue
                // get all CIF attached with Account from relCIF
                Sql = "SELECT ACN FROM RELCIF WHERE CID IN (" + txtAccount.Text.Trim() + ")";

                clsUtility.WriteLog("Account Tagging Request (acn)" + txtAccount.Text.Trim());
                XMLDataToString = moobjProfileCS.GetXMLData(Sql);
                clsUtility.WriteLog("Account Tagging Response (acn)" + txtAccount.Text.Trim());

                XMLDataToString = XMLDataToString.Replace("&", "and");
                XMLDataToString = XMLDataToString.Replace("'", "''");


                StringReader sr = new StringReader(XMLDataToString);
                DataSet ds = new DataSet();
                ds.ReadXml(sr);

                if (ds.Tables.Count > 0)
                {
                    dtRelCIF = ds.Tables[0];
                    // get all acn from the data
                    for (int ac = 0; ac < dtRelCIF.Rows.Count; ac++)
                        acn = acn + dtRelCIF.Rows[ac]["acn"].ToString() + ",";

                    acn = acn.Trim(',');

                    Sql = "select acn,odt,b.boo,cid,B.TYPE,TITLE1,IBAN,CRCD,DESC,CONVACN,'No Error' FROM ACN B,UTBLBRCD BR WHERE B.BOO=BR.BRCD  AND B.BOO=" + txtBranchCode.Text.Trim() + " AND STAT<>4 and ACN in (" + acn + ")";

                    //localhost.ProfileConnector moobjProfileCS = new localhost.ProfileConnector();

                    clsUtility.WriteLog("Account Tagging Request (acn)" + txtAccount.Text.Trim());
                    XMLDataToString = moobjProfileCS.GetXMLData(Sql);
                    clsUtility.WriteLog("Account Tagging Response (acn)" + txtAccount.Text.Trim());

                    XMLDataToString = XMLDataToString.Replace("&", "and");
                    XMLDataToString = XMLDataToString.Replace("'", "''");


                    sr = new StringReader(XMLDataToString);
                    ds = new DataSet();
                    ds.ReadXml(sr);


                    dtACN = ds.Tables[0];

                    Sql = "select acn,boo,zcnic,ZNTN,zconcnic,nam,mad1,mad2,mad3,mad4,pcity,mmname,dob,sex,zmobile,a.ZUPDCRMDT,ZMOBILEREG,HPH,BPH,ZHOMEREG,ZBUSINREG from CIF A where acn in (" + acn + ")";
                    clsUtility.WriteLog("Account Tagging Request (CIF)" + txtAccount.Text.Trim());
                    XMLDataToString = moobjProfileCS.GetXMLData(Sql);
                    clsUtility.WriteLog("Account Tagging Response (CIF)" + txtAccount.Text.Trim());

                    XMLDataToString = XMLDataToString.Replace("&", "and");
                    XMLDataToString = XMLDataToString.Replace("'", "''");

                    sr = new StringReader(XMLDataToString);
                    ds = new DataSet();
                    ds.ReadXml(sr);

                    dtCIF = ds.Tables[0];

                    if (Convert.ToString(dtCIF.Rows[0]["zcnic"]).Trim() == "" && Convert.ToString(dtCIF.Rows[0]["zntn"]).Trim() == "" && Convert.ToString(dtCIF.Rows[0]["zconcnic"]).Trim() == "")
                    {
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Account has no CNIC/NTN or Contact person cnic";
                        return;
                    }

                    clsUtility.WriteLog("insertDataBulkCopy Request" + txtAccount.Text);
                    serv.insertDataBulkCopy(ConnectionStringSql, dtACN, dtCIF, "acn", "cif", txtBranchCode.Text.Trim(), txtAccount.Text.Trim(), acn.Trim(','));
                    clsUtility.WriteLog("insertDataBulkCopy Response" + txtAccount.Text);
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Account Successfully tagged";

                    Sql = "SELECT ZCNIC CNIC,      [NAM] NAME      ,isnull([MAD1],'')+' '+isnull([MAD2],'')+' '+isnull([MAD3],'')+' '+isnull([MAD4],'') ADDRESS1      ,[PCITY] CITY,      [MMNAME] [MOTHER NAME],      [DOB] [DATE OF BIRTH],      [SEX] [GENDER],      [ZMOBILE] [MOBILE NUMBER],      [ODT] [ACCOUNT OPEN DATE],      [BOO] [BR CODE]      ,[CID] [ACC NO]      ,[TYPE] [ACC TYPE]      ,[TITLE1] [ACCOUNT TITLE]      ,[IBAN] [IBAN]      ,[CRCD] [CURRENCY]      ,[DESC] [BRANCH ADDRESS]      ,[ZUPDCRMDT] [UPDATEDDATE]      ,[ZMOBILEREG] [REGISTEREDPNO]      ,[HPH] [HOME PHONE]      ,[BPH] [OFFICE PHONE]      ,[ZHOMEREG] [REGISTEREDHPNO]      ,[ZBUSINREG] [REGISTEREDBPNO]      ,[CONVACN] [CONVACN]      ,[TMP_26] [ERROR]  FROM [Profile].[dbo].[ProfileData] where CID in (" + txtAccount.Text.Trim() + ")";
                    DataTable dtTagged = serv.GetTable(ConnectionStringSql, Sql);
                    gdTaggedAccounts.DataSource = dtTagged;
                    gdTaggedAccounts.DataBind();

                    lblMessage.Text = dtTagged.Rows.Count + " Account(s) Successfully tagged";
                }
                else
                {
                    gdTaggedAccounts.DataSource = null;
                    gdTaggedAccounts.DataBind();
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Account could not tagged , please check if branch code and account are correct.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message;
            }
        }
        private string GetBranchType()
        {
            try
            {
                clsDataBaseSql serv = new clsDataBaseSql();
                List<clsParameters> Params = new List<clsParameters>();
                Params.Add(new clsParameters("@BranchCode", Convert.ToInt32(txtBranchCode.Text.Trim()).ToString(), "IN"));
                return serv.GetTableUsingSP(ConnectionStringSql, "GetBranchType", Params).Rows[0][0].ToString();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public void TagAccountOLDCode()
        {
            String Sql = "", XMLDataToString = "";
            string IsNewBranch = "", acn = "";
            clsDataBaseSql serv = new clsDataBaseSql();
            DataTable dtACN = new DataTable();
            DataTable dtCIF = new DataTable();
            DataTable dtRelCIF = new DataTable();
            //Sql = "select zcnic,ZNTN,zconcnic,nam,mad1,mad2,mad3,mad4,pcity,mmname,dob,sex,zmobile,odt,b.boo,cid,B.TYPE,TITLE1,IBAN,CRCD,DESC,a.ZUPDCRMDT,ZMOBILEREG,HPH,BPH,ZHOMEREG,ZBUSINREG,CONVACN,'No Error' FROM CIF A,ACN B,UTBLBRCD BR WHERE A.ACN=B.ACN AND A.BOO=BR.BRCD AND B.BOO=BR.BRCD " +
            //                                " AND B.BOO=" + Convert.ToInt32(txtBranchCode.Text.Trim()) + " AND STAT<>4 and CID in (" + txtAccount.Text.Trim() + ")   order by CID ";

            // get data frim acn table

            try
            {
                JavaWebReferenceTagging.ProfileConnector moobjProfileCS = new JavaWebReferenceTagging.ProfileConnector();

                //changes done by asif on 11-august-2015 for related cif issue
                // get all CIF attached with Account from relCIF
                Sql = "SELECT ACN FROM RELCIF WHERE CID IN (" + txtAccount.Text.Trim() + ")";

                clsUtility.WriteLog("Account Tagging Request (acn)" + txtAccount.Text.Trim());
                XMLDataToString = moobjProfileCS.GetXMLData(Sql);
                clsUtility.WriteLog("Account Tagging Response (acn)" + txtAccount.Text.Trim());

                XMLDataToString = XMLDataToString.Replace("&", "and");
                XMLDataToString = XMLDataToString.Replace("'", "''");


                StringReader sr = new StringReader(XMLDataToString);
                DataSet ds = new DataSet();
                ds.ReadXml(sr);

                if (ds.Tables.Count > 0)
                {
                    dtRelCIF = ds.Tables[0];
                    // get all acn from the data
                    for (int ac = 0; ac < dtRelCIF.Rows.Count; ac++)
                        acn = acn + dtRelCIF.Rows[ac]["acn"].ToString() + ",";

                    acn = acn.Trim(',');

                    Sql = "select acn,odt,b.boo,cid,B.TYPE,TITLE1,IBAN,CRCD,DESC,CONVACN,'No Error' FROM ACN B,UTBLBRCD BR WHERE B.BOO=BR.BRCD  AND B.BOO=" + txtBranchCode.Text.Trim() + " AND STAT<>4 and ACN in (" + acn + ")";

                    //localhost.ProfileConnector moobjProfileCS = new localhost.ProfileConnector();

                    clsUtility.WriteLog("Account Tagging Request (acn)" + txtAccount.Text.Trim());
                    XMLDataToString = moobjProfileCS.GetXMLData(Sql);
                    clsUtility.WriteLog("Account Tagging Response (acn)" + txtAccount.Text.Trim());

                    XMLDataToString = XMLDataToString.Replace("&", "and");
                    XMLDataToString = XMLDataToString.Replace("'", "''");


                    sr = new StringReader(XMLDataToString);
                    ds = new DataSet();
                    ds.ReadXml(sr);


                    dtACN = ds.Tables[0];

                    //Sql = "select acn,boo,zcnic,ZNTN,zconcnic,nam,mad1,mad2,mad3,mad4,pcity,mmname,dob,sex,zmobile,a.ZUPDCRMDT,ZMOBILEREG,HPH,BPH,ZHOMEREG,ZBUSINREG from CIF A where acn in (" + acn + ")";
                    Sql = "select acn,boo,zcnic,ZNTN,zconcnic,nam,mad1,mad2,mad3,mad4,pcity,mmname,dob,sex,zmobile,ZUPDCRMDT,ZMOBILEREG,HPH,BPH,ZHOMEREG,ZBUSINREG from CIF where acn in (" + acn + ")";
                    clsUtility.WriteLog("Account Tagging Request (CIF)" + txtAccount.Text.Trim());
                    XMLDataToString = moobjProfileCS.GetXMLData(Sql);
                    clsUtility.WriteLog("Account Tagging Response (CIF)" + txtAccount.Text.Trim());

                    XMLDataToString = XMLDataToString.Replace("&", "and");
                    XMLDataToString = XMLDataToString.Replace("'", "''");

                    sr = new StringReader(XMLDataToString);
                    ds = new DataSet();
                    ds.ReadXml(sr);

                    dtCIF = ds.Tables[0];

                    if (Convert.ToString(dtCIF.Rows[0]["zcnic"]).Trim() == "" && Convert.ToString(dtCIF.Rows[0]["zntn"]).Trim() == "" && Convert.ToString(dtCIF.Rows[0]["zconcnic"]).Trim() == "")
                    {
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Account has no CNIC/NTN or Contact person cnic";
                        return;
                    }

                    clsUtility.WriteLog("insertDataBulkCopy Request" + txtAccount.Text);
                    serv.insertDataBulkCopy(ConnectionStringSql, dtACN, dtCIF, "acn", "cif", txtBranchCode.Text.Trim(), txtAccount.Text.Trim(), acn.Trim(','));
                    clsUtility.WriteLog("insertDataBulkCopy Response" + txtAccount.Text);
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Account Successfully tagged";

                    //Sql = "SELECT ZCNIC CNIC,      [NAM] NAME      ,isnull([MAD1],'')+' '+isnull([MAD2],'')+' '+isnull([MAD3],'')+' '+isnull([MAD4],'') ADDRESS1      ,[PCITY] CITY,      [MMNAME] [MOTHER NAME],      [DOB] [DATE OF BIRTH],      [SEX] [GENDER],      [ZMOBILE] [MOBILE NUMBER],      [ODT] [ACCOUNT OPEN DATE],      [BOO] [BR CODE]      ,[CID] [ACC NO]      ,[TYPE] [ACC TYPE]      ,[TITLE1] [ACCOUNT TITLE]      ,[IBAN] [IBAN]      ,[CRCD] [CURRENCY]      ,[DESC] [BRANCH ADDRESS]      ,[ZUPDCRMDT] [UPDATEDDATE]      ,[ZMOBILEREG] [REGISTEREDPNO]      ,[HPH] [HOME PHONE]      ,[BPH] [OFFICE PHONE]      ,[ZHOMEREG] [REGISTEREDHPNO]      ,[ZBUSINREG] [REGISTEREDBPNO]      ,[CONVACN] [CONVACN]      ,[TMP_26] [ERROR]  FROM [Profile].[dbo].[ProfileData] where CID in (" + txtAccount.Text.Trim() + ")";

                    Sql = "SELECT 'OK' as StatusTag, ZCNIC CNIC,      [NAM] NAME      ,isnull([MAD1],'')+' '+isnull([MAD2],'')+' '+isnull([MAD3],'')+' '+isnull([MAD4],'') ADDRESS1      ,[PCITY] CITY,      [MMNAME] [MOTHER NAME],      [DOB] [DATE OF BIRTH],      [SEX] [GENDER],      [ZMOBILE] [MOBILE NUMBER],      [ODT] [ACCOUNT OPEN DATE],      [BOO] [BR CODE]      ,[CID] [ACC NO]      ,[TYPE] [ACC TYPE]      ,[TITLE1] [ACCOUNT TITLE]      ,[IBAN] [IBAN]      ,[CRCD] [CURRENCY]      ,[DESC] [BRANCH ADDRESS]      ,[ZUPDCRMDT] [UPDATEDDATE]      ,[ZMOBILEREG] [REGISTEREDPNO]      ,[HPH] [HOME PHONE]      ,[BPH] [OFFICE PHONE]      ,[ZHOMEREG] [REGISTEREDHPNO]      ,[ZBUSINREG] [REGISTEREDBPNO]      ,[CONVACN] [CONVACN]      ,[TMP_26] [ERROR]  FROM [Profile].[dbo].[ProfileData] where CID in (" + txtAccount.Text.Trim() + ")" +
            "union all " +
            "SELECT (case b.Status when 'P' then 'Pending' when 'R' then 'Rejected' when 'O' then 'OK' else 'Failure' end) 'StatusTag', a.ZCNIC CNIC,     a.[NAM] NAME      ,isnull(a.[MAD1],'')+' '+isnull(a.[MAD2],'')+' '+isnull(a.[MAD3],'')+' '+isnull(a.[MAD4],'') ADDRESS1      ,a.[PCITY] CITY,      a.[MMNAME] [MOTHER NAME],      a.[DOB] [DATE OF BIRTH],      [SEX] [GENDER],      a.[ZMOBILE] [MOBILE NUMBER],      [ODT] [ACCOUNT OPEN DATE],      a.[BOO] [BR CODE]      ,a.[CID] [ACC NO]      ,a.[TYPE] [ACC TYPE]      ,a.[TITLE1] [ACCOUNT TITLE]      ,a.[IBAN] [IBAN]      ,a.[CRCD] [CURRENCY]      ,a.[DESC] [BRANCH ADDRESS]      ,[ZUPDCRMDT] [UPDATEDDATE]      ,[ZMOBILEREG] [REGISTEREDPNO]      ,a.[HPH] [HOME PHONE]      ,a.[BPH] [OFFICE PHONE]      ,[ZHOMEREG] [REGISTEREDHPNO]      ,[ZBUSINREG] [REGISTEREDBPNO]      ,[CONVACN] [CONVACN]      ,[TMP_26] [ERROR]  FROM [Profile].[dbo].[ProfileDataAassan] a, AassanPFData b where a.CID in  (" + txtAccount.Text.Trim() + ") and a.CID=b.CID";

                    DataTable dtTagged = serv.GetTable(ConnectionStringSql, Sql);
                    gdTaggedAccounts.DataSource = dtTagged;
                    gdTaggedAccounts.DataBind();

                    lblMessage.Text = dtTagged.Rows.Count + " Account(s) Successfully tagged";
                }
                else
                {
                    gdTaggedAccounts.DataSource = null;
                    gdTaggedAccounts.DataBind();
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Account could not tagged , please check if branch code and account are correct.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message;
            }
        }

    }
}