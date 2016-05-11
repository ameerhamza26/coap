using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using ExtensionMethods;

namespace CAOP.ExistingCif
{
    public partial class Existing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User LoggedUser = Session["User"] as User;

            if (LoggedUser == null)
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                CheckRole(LoggedUser);
                grdPCif.DataSource = new List<ProfileCif>();
                grdPCif.DataBind();
            }
        }

      
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtCnic.Text.Length > 0)
            {
                if (txtCnic.Text.Length == 15)
                {
                    BasicInformations BI = new BasicInformations();
                    if (BI.IsCnicExists(txtCnic.Text))
                    {
                        lblError.Text = "CIF Already Exists, Proceed to Account Creation";
                        lblError.Visible = true;
                        return;
                    }
                    
                }

               string CifNums = GetProfileCifNumber(txtCnic.Text);
               if (CifNums != "-1" && CifNums != "error")
               {
                   grdPCif.DataSource = GetProfileData(CifNums);
                   grdPCif.DataBind();
                   lblError.Visible = false;
               }
               else
               {
                   if (CifNums == "error")
                   {

                   }
                   else
                   {
                       lblError.Text = "No Cif Exists, Proceed to CIF Creation";
                       lblError.Visible = true;
                   }
                   
               }
                
            }
        }

        public List<ProfileCif> GetProfileData(string Cifnums)
        {
            var CifNumArray = Cifnums.Split(',');
            List<ProfileCif> ProfileData = new List<ProfileCif>();

            string strQuery = "";
            string XMLDataToString = "";
            StringReader sr = new StringReader("");
            DataSet ds = new DataSet();
            CAOP.CustomerInquiry.ProfileConnectorTest connector = new CustomerInquiry.ProfileConnectorTest();


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

            }

            foreach (string cif in CifNumArray)
            {
              
                try
                {
                    XMLDataToString = connector.CIFEnquiryCIFNo("CIF.ACN="+ cif);
                    sr = new StringReader(XMLDataToString.Replace("&", " and ").Replace("<2", ""));
                    ds = new DataSet();
                    ds.ReadXml(sr);
                }
                catch (Exception et)
                {
                    //WriteActions("Error." + DateTime.Now.ToString() + e.Message);
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    var TotalCif = ds.Tables[0].Rows[i][0].ToString().Split('|');

                    ProfileCif newCif = new ProfileCif();
                    newCif.CIF_NO = cif;
                    newCif.NAME = TotalCif[11];
                    newCif.FATHER_NAME = TotalCif[32];
                    newCif.MOTHER_NAME = TotalCif[33]; 
                    newCif.DOB = TotalCif[12];

                    Branch b = new Branch();
                    newCif.BRANCH_NAME = b.GetBranchNameWithCode(TotalCif[69].PadLeft(4, '0'));

                    int jDate = Convert.ToInt32(TotalCif[12]);
                    int day = jDate % 1000;
                    int year = (jDate - day + 2000000) / 1000;
                    var date1 = new DateTime(year, 1, 1);
                    var result = date1.AddDays(day - 1);

                    newCif.EXPIRY_DATE = TotalCif[26];
                    ProfileData.Add(newCif);

                }
            }

            return ProfileData;
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
                lblError.Text = ex.Message;
                lblError.Visible = true;
                return "error";
                
            }

            try
            {
                XMLDataToString = connector.CIFEnquiryCNIC("CIF.ZCNIC=" + txtCnic.Text + ",CIF.TYPE=0");
                sr = new StringReader(XMLDataToString.Replace("&", " and ").Replace("<2", ""));
                ds = new DataSet();
                ds.ReadXml(sr);
            }
            catch (Exception ey)
            {
                lblError.Text = ey.Message;
                lblError.Visible = true;
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
      

       
        private void CheckRole(User LoggedUser)
        {
            if (!LoggedUser.Permissions.CheckAccess(Permissions.CIF,Rights.Create))
                Response.Redirect("~/Main.aspx");
        }
        public void MRPC()
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
                //WriteActions("Profile T-1 Not Available " + DateTime.Now.ToString() + e.Message);
                return;
            }

            try
            {
                XMLDataToString = connector.CIFEnquiryCNIC("CIF.ZCNIC=35202-8427243-9,CIF.TYPE=0");
                sr = new StringReader(XMLDataToString.Replace("&", " and ").Replace("<2", ""));
                ds = new DataSet();
                ds.ReadXml(sr);
            }
            catch (Exception ey)
            {
                //WriteActions("Error." + DateTime.Now.ToString() + e.Message);
            }

            try
            {
                XMLDataToString = connector.CIFEnquiryCIFNo("CIF.ACN=2288574");
                sr = new StringReader(XMLDataToString.Replace("&", " and ").Replace("<2", ""));
                ds = new DataSet();
                ds.ReadXml(sr);
            }
            catch (Exception et)
            {
                //WriteActions("Error." + DateTime.Now.ToString() + e.Message);
            }
        }

        protected void lblIncor_Click(object sender, EventArgs e)
        {
            //LinkButton btn = (LinkButton)sender;

            ////Get the row that contains this button
            //GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            //string PCNum = ((Label)gvr.FindControl("btnCifno")).Text;
            //IncorporateCif(PCNum);
            //lblError.Text = "Cif Incorporated";
            //lblError.Visible = true;


        }

        public void IncorporateCif(string CifNum)
        {
            string strQuery = "";
            string XMLDataToString = "";
            StringReader sr = new StringReader("");
            DataSet ds = new DataSet();
            CAOP.CustomerInquiry.ProfileConnectorTest connector = new CustomerInquiry.ProfileConnectorTest();


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

            }

            try
            {
                XMLDataToString = connector.CIFEnquiryCIFNo("CIF.ACN=" + CifNum);
                sr = new StringReader(XMLDataToString.Replace("&", " and ").Replace("<2", ""));
                ds = new DataSet();
                ds.ReadXml(sr);
            }
            catch (Exception et)
            {
                //WriteActions("Error." + DateTime.Now.ToString() + e.Message);
            }

            var Cif = ds.Tables[0].Rows[0][0].ToString().Split('|');
            CIF newCif = new CIF(-1);
            newCif.DumpCif(Cif, CifNum);

            grdPCif.DataSource = new List<ProfileCif>();
            grdPCif.DataBind();
          //  InsertCif(Cif);


        }

        //private void InsertCif(string[] Cif)
        //{
        //    BasicInformations BI = new BasicInformations();
        //  //  BI.CIF_ENTRY_DATE = DateTime.Now;
        //    BI.PRIMARY_DOCUMENT_TYPE = new PrimaryDocumentType { ID = new PrimaryDocumentType().GetDocValue("CNIC")};
        //    BI.CIF_TYPE = new CifTypes() { ID = 1 };
        //    BI.CNIC = Cif[0];
        // //   BI.TITLE = new Title() { ID = Convert.ToInt32(lstTitle.SelectedItem.Value), Name = lstTitle.SelectedItem.Text };
        //      BI.NAME = txtName.Text;
        //    BI.FIRST_NAME = txtFirstName.Text;
        //    BI.MIDDLE_NAME = txtMiddleName.Text;
        //    BI.LAST_NAME = txtLastName.Text;
        //    BI.TITLE_FH = new Title() { ID = Convert.ToInt32(lstTitleFather.SelectedItem.Value), Name = lstTitleFather.SelectedItem.Text };
        //    BI.NAME_FH = txtFatherName.Text;
        //    BI.CNIC_FH = txtFatherCnic.Text;
        //    BI.CIF_FH = txtFatherCif.Text;
        //    BI.NAME_MOTHER = txtMotherName.Text;
        //    BI.CNIC_MOTHER = txtMotherCnic.Text;
        //    BI.CNIC_MOTHER_OLD = txtMotherCnicOld.Text;
        //    BI.DATE_BIRTH = txtDOB.Text;
        //    BI.PLACE_BIRTH = txtBithPlace.Text;
        //    BI.Country_Birth = new Country { ID = Convert.ToInt32(lstCOB.SelectedItem.Value), Name = lstCOB.SelectedItem.Text };
        //    BI.MARTIAL_STATUS = new MartialStatus { ID = Convert.ToInt32(lstMartialStatus.SelectedItem.Value), Name = lstMartialStatus.SelectedItem.Text };
        //    BI.GENDER = new Gender { ID = Convert.ToInt32(lstGender.SelectedItem.Value), Name = lstGender.SelectedItem.Text };
        //    BI.RELIGION = new Religion { ID = Convert.ToInt32(lstReligion.SelectedItem.Value), Name = lstReligion.SelectedItem.Text };
        //    BI.RESIDENT_TYPE = new ResidentType { ID = Convert.ToInt32(lstResident.SelectedItem.Value), Name = lstResident.SelectedItem.Text };
        //    BI.NATIONALITIES = lstNationality.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => new Nationality { CountryID = Convert.ToInt32(i.Value), Country = i.Text }).ToList();
        //    BI.MONTHLY_INCOME = txtIncome.Text;
        //    BI.COUNTRY_RESIDENCE = new Country { ID = ListExtensions.getSelectedValue(lstCOR), Name = lstCOR.SelectedItem.Text };
        //    BI.CUSTOMER_DEAL = new CustomerDeal { ID = Convert.ToInt32(lstCustomerDeals.SelectedItem.Value), Name = lstCustomerDeals.SelectedItem.Text };
        //    BI.DOCUMENT_VERIFIED = chkDocument.Checked;

        //    User LogedUser = Session["User"] as User;
        //    BI.UserId = LogedUser.USER_ID;
        //    BI.BRANCH_CODE = LogedUser.Branch.BRANCH_CODE;
        //}

        bool IsEmpty(DataSet dataSet)
        {
            return !dataSet.Tables.Cast<DataTable>().Any(x => x.DefaultView.Count > 0);
        }
        protected void grdPCif_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdPCif_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label CIF_ID = e.Row.FindControl("btnCifno") as Label;
                LinkButton IncorporateBtn = e.Row.FindControl("lblIncor") as LinkButton;
                IncorporateBtn.PostBackUrl = "~/Individual.aspx?PROFILECIF=" + CIF_ID.Text;

            }
        }




    }
}
