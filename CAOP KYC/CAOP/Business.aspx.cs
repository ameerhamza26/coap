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
using System.Globalization;

namespace CAOP
{
    public partial class Business : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (ReadQueryStringID() == -1)
            //{
            //    if (!IsPostBack)
            //    {
            //        User LoggedUser = Session["User"] as User;
            //        CheckPermissions(LoggedUser);
            //        SetData();
            //    }
            //}
            //else
            //{
            //    if (!IsPostBack)
            //    {

            //        int queryid = ReadQueryStringID();
            //        User LoggedUser = Session["User"] as User;

            //        CheckPermissions(LoggedUser);
            //        Session["BID"] = queryid;
            //        SetData();
            //        SetDataOpen(queryid);
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showall()", true);
            //    }
            //}
            // CalculateRisk();
            SetRoles();
        }

        public void SetRoles()
        {
            User LoggedUser = Session["User"] as User;
            if (LoggedUser == null)
                Response.Redirect("Login.aspx");
            int queryid = ReadQueryStringID();

            if (LoggedUser.Role.Name == Roles.BRANCH_OPERATOR.ToString())
            {
                CheckPermissions(LoggedUser);

                if (queryid == -1)
                {
                    if (!IsPostBack)
                    {
                        SetData();
                    }

                }
                else
                {
                    if (!IsPostBack)
                    {
                        Session["BID"] = queryid;
                        SetData();
                        SetDataOpen(queryid);

                        CIF cif = new CIF(LoggedUser.USER_ID);

                        if (cif.CheckStatus(queryid, Status.REJECTEBY_COMPLIANCE_MANAGER.ToString()))
                        {
                            rev.Visible = true;
                            rev.Reviewer = false;
                            SetUpdateBtnVisible();
                            SetSibmitCifVisible();

                            btnGridAddSH.Visible = true;
                        }
                    }

                }

            }
            else if (LoggedUser.Role.Name == Roles.COMPLIANCE_OFFICER.ToString())
            {
                string action = ReadQueryStringAction();
                CheckPermissions(LoggedUser);

                if (action == "review" && queryid != -1)
                {
                    if (!IsPostBack)
                    {
                        Session["BID"] = queryid;
                        SetData();
                        SetDataOpen(queryid);
                        rev.Visible = true;
                        rev.Reviewer = false;
                        CIF cif = new CIF(LoggedUser.USER_ID);

                        if (cif.CheckStatus(queryid, Status.APPROVED_BY_COMPLIANCE_MANAGER.ToString()))
                            rev.Visible = false;

                        if (cif.CheckStatus(queryid, new string[] { Status.SUBMITTED.ToString(), Status.REJECTED_BY_BRANCH_MANAGER.ToString() }))
                        {
                            rev.Reviewer = true;
                        }
                    }
                    else
                    {
                        String mesg = "null";
                        checkBusinessTabCompleted(queryid, mesg);
                    }
                }

            }
            else if (LoggedUser.Role.Name == Roles.BRANCH_MANAGER.ToString())
            {
                string action = ReadQueryStringAction();
                CheckPermissions(LoggedUser);

                if (action == "review" && queryid != -1)
                {
                    if (!IsPostBack)
                    {
                        Session["BID"] = queryid;
                        SetData();
                        SetDataOpen(queryid);
                        rev.Visible = true;
                        rev.Reviewer = false;

                        CIF cif = new CIF(LoggedUser.USER_ID);
                        if (cif.CheckStatus(queryid, Status.APPROVED_BY_COMPLIANCE_MANAGER.ToString()))
                        {
                            rev.Reviewer = true;
                        }
                    }
                    else
                    {
                        String mesg = "null";
                        checkBusinessTabCompleted(queryid, mesg);
                    }
                }
            }
        }

        private void SetDataOpen(int id)
        {
            SetBasicInfoOpen(id);
            SetContactInfoOpen(id);
            SetHeadContactInfoOpen(id);
            SetBusinessDetailOpen(id);
            SetBankingRelationshipOpen(id);
            SetFinancialInfoOpen(id);
            SetShareHolderInfoOpen(id);
            SetPersonIdentificationOpen(id);

            String mesg = "null";
            checkBusinessTabCompleted(id, mesg);
        }



        private void SetBasicInfoOpen(int id)
        {
            BasicInformations b = new BasicInformations();
            if ( b.GetBusiness(id))
            {
                BiListCifType.Items.FindByValue(b.CIF_TYPE.ID.ToString()).Selected = true;
                BiCompany.Text = ListExtensions.RemoveNull(b.NAME_OFFICE);
                BiNtn.Text =  ListExtensions.RemoveNull(b.NTN);
                BiSalesTax.Text=  ListExtensions.RemoveNull(b.SALES_TAX_NO);
                ListExtensions.SetDropdownValue(b.Issuing_Agency.ID, BiListIssueAgency);
                BiIssueOther.Text = b.ISSUING_AGENCY_OTHER;
                BiRegistrationNo.Text =  ListExtensions.RemoveNull(b.REG_NO);
                try
                {
                  BiDateOfRegistration.Text =  DateTime.ParseExact(b.REG_DATE,
                                       "yyyy-MM-dd",
                                        CultureInfo.InvariantCulture).ToString("dd-MM-yyyy");
                  BiDateOfCommence.Text = DateTime.ParseExact(b.COMMENCEMENT_DATE,
                                     "yyyy-MM-dd",
                                      CultureInfo.InvariantCulture).ToString("dd-MM-yyyy");
                }
                catch (Exception e)
                {
                    BiDateOfRegistration.Text = b.REG_DATE;
                    BiDateOfCommence.Text = b.COMMENCEMENT_DATE;
                }
               
                BiPastBusiness.Text =  ListExtensions.RemoveNull(b.PAST_BUSS_EXP);
                ListExtensions.SetDropdownValue(b.ACCOUNT_NATURE.ID, BiListNatureOfAccount);
                ListExtensions.SetDropdownValue(b.CUSTOMER_CLASSIFICATION.ID, BiListCustomerClassification);
                BiCifNo.Text =  ListExtensions.RemoveNull(b.CIF_GROUP);
                ListExtensions.SetDropdownValue(b.NATURE_BUSINESS.ID, BiListNatureOfbusiness);
                BiNatureOfBusinessDescr.Text =  ListExtensions.RemoveNull(b.NATURE_BUSINESS_DESCRP);
                ListExtensions.SetDropdownValue(b.CATERGORY_NBP.ID, BiListCategoryNBP);
                ListExtensions.SetDropdownValue(b.CATERGORY_SBP.ID, BiListCategorySBP);
                ListExtensions.SetDropdownValue(b.CATERGORY_BASE.ID, BiListCategoryBasel);
                ListExtensions.SetDropdownValue(b.CUSTOMER_DEAL.ID, BiListCustomerDealIn);
                ListExtensions.SetDropdownValue(b.COUNTRY_INCORPORATION.ID, BiListCountryIncorporation);

                BusinessType bt = new BusinessType();
                BiListBusinessType.DataSource = bt.GetBusinessTypes(Convert.ToInt32(b.CUSTOMER_CLASSIFICATION.ID));
                BiListBusinessType.DataValueField = "ID";
                BiListBusinessType.DataTextField = "Name";
                BiListBusinessType.DataBind();
                BiListBusinessType.Items.Insert(0, new ListItem("Select", "0"));
                ListExtensions.SetDropdownValue(b.BUSINESS_TYPE.ID, BiListBusinessType);
                ListExtensions.SetDropdownValue(b.INSTITUTION_TYPE.ID, BiListInstitutionType);

                ListExtensions.SetDropdownValue(b.SIC_CODES.ID, BiListSicCode);
                SubIndustry sindus = new SubIndustry();
                BiListSubIndustry.DataSource = sindus.GetSubIndustrys(Convert.ToInt32(b.SIC_CODES.ID));
                BiListSubIndustry.DataValueField = "ID";
                BiListSubIndustry.DataTextField = "Name";
                BiListSubIndustry.DataBind();
                BiListSubIndustry.Items.Insert(0, new ListItem("Select", "0"));
                ListExtensions.SetDropdownValue(b.SUB_INDUSTRY.ID, BiListSubIndustry);
                chkDocument.Checked = b.DOCUMENT_VERIFIED;


                BibtnSubmitBaisc.Visible = false;

                if (BiListCountryIncorporation.SelectedItem.Value == "231")
                {
                    PiListCI.ClearSelection();
                    PiListCI.SelectedIndex = 1;
                }
                

            }
        }

        private void SetContactInfoOpen(int id)
        {
            ContactInfo c = new ContactInfo();

            if (c.GetBusinessContactInfo(id))
            {
                //CiOffcMailingAddr.Text = ListExtensions.RemoveNull(c.ADDRESS_PERMANENT);
            //    ListExtensions.SetDropdownValue(c.DISTRICT_PERMANENT.ID, CiListDistrict);
                ListExtensions.SetDropdownValue(c.COUNTRY_CODE.ID, CiListCountry);
                if (c.CITY_PERMANENT.ID != null)
                {
                    ListExtensions.SetDropdownValue(c.CITY_PERMANENT.ID, CiListCity);
                    ListExtensions.SetDropdownValue(c.PROVINCE.ID, CiListProvince);
                }
               
                CiPoBox.Text =  ListExtensions.RemoveNull(c.POST_OFFICE);                
                CiPostalCode.Text =  ListExtensions.RemoveNull(c.POSTAL_CODE);               
                CiTxtStreet.Text = c.STREET;
                CiTxtFloor.Text = c.FLOOR;
                CiTxtBuilding.Text = c.BIULDING_SUITE;
                CiTxtDistrict.Text = c.DISTRICT;                
                CiOfficeNo.Text =  ListExtensions.RemoveNull(c.OFFICE_CONTACT);
                CiMobileNo.Text =  ListExtensions.RemoveNull(c.MOBILE_NO);
                CiFaxNo.Text =  ListExtensions.RemoveNull(c.FAX_NO);
                CiEmail.Text =  ListExtensions.RemoveNull(c.EMAIL);
                CiWeb.Text =  ListExtensions.RemoveNull(c.WEB);
                CiPName.Text = c.CP_NAME;
                CiDesig.Text = c.CP_DESIGNATION;
                CiCnic.Text = c.CP_CNIC;
                CiCnicEDate.Text = c.CP_CNIC_EXPIRY;
                CiPNtn.Text = c.CP_NTN;

                CiSubmitButton.Visible = false;

                if (CiListCountry.SelectedItem.Value == "231")
                {
                    PiListAddCountUsa.ClearSelection();
                    PiListAddCountUsa.SelectedIndex = 1;
                }


            }
        }

        private void SetHeadContactInfoOpen(int id)
        {
            HeadContactInfo h = new HeadContactInfo();

            if (h.GetHeadContactInfo(id))
            {
                //HoOfficeMailingAddr.Text =  ListExtensions.RemoveNull(h.ADDRESS);
             //   ListExtensions.SetDropdownValue(h.DISTRICT.ID, HoListDistrict);
                ListExtensions.SetDropdownValue(h.COUNTRY.ID, HoListCountryCode);
                ListExtensions.SetDropdownValue(h.PROVINCE.ID, HoListProvince);
                ListExtensions.SetDropdownValue(h.CITY.ID, HoListCity);

                HoTxtPostBox.Text = ListExtensions.RemoveNull(h.POBOX);             
                HoTxtPotalCode.Text = ListExtensions.RemoveNull(h.POSTAL_CODE);               
                HoOfficeNo.Text =  ListExtensions.RemoveNull(h.PHONE_OFFICE);
                HoMobileNo.Text =  ListExtensions.RemoveNull(h.MOBILE_NO);
                HoFaxNo.Text =  ListExtensions.RemoveNull(h.FAX_NO);
                HoEmail.Text =  ListExtensions.RemoveNull(h.EMAIL);

                HoTxtStreet.Text = h.STREET;
                HoTxtBuilding.Text = h.BIULDING_SUITE;
                HoTxtFloor.Text = h.FLOOR;
                HoTxtDistrict.Text = h.DISTRICT_HEAD;
                ListExtensions.SetDropdownValue(h.COUNTRY_CODE_REG.ID, HoListCountryCodeReg);
                ListExtensions.SetDropdownValue(h.CITY_REG.ID, HoListCityReg);
                ListExtensions.SetDropdownValue(h.PROVINCE_REG.ID, HoListProvinceReg);
                HoTxtStreetReg.Text = h.STREET_REG;
                HoTxtBuildingReg.Text = h.BIULDING_SUITE_REG;
                HoTxtFloorReg.Text = h.FLOOR_REG;
                HoTxtDistrictReg.Text = h.DISTRICT_REG;
                HoTxtPostBoxReg.Text = h.POST_OFFICE_REG;
                HoTxtPotalCodeReg.Text = h.POSTAL_CODE_REG;
               
               

                HoSubmitButton.Visible = false;
            }
        }

        private void SetBusinessDetailOpen(int id)
        {
            BusinessDetail b = new BusinessDetail();

            if (b.GetBusinessDetail(id))
            {
                ListExtensions.SetDropdownValue(b.InfoType.ID, BdListInformationType);
                BdInfoDescr.Text = ListExtensions.RemoveNull(b.INFO_DESC);

                BdTextMS.Text = b.MAJOR_SUPPLIERS;
                 BdTextMC.Text = b.MAIN_CUSTOMERS;
                 BdTextMBLA.Text = b.MAIN_BUSINESS_ACTIVITY;

                 if (b.BusinessCities.Count > 0)
                 {
                     foreach (ListItem item in BdListBusinessInCities.Items)
                     {
                         foreach (var a in b.BusinessCities)
                         {
                             if (item.Value == a.ID.ToString())
                             {
                                 item.Selected = true;
                             }
                         }

                     }
                 }

                //BdListBusinessInCities.Items
                //    .Cast<ListItem>()
                //    .Where(c => b.BusinessCities.Where(i => i.ID == Convert.ToInt32(c.Value)).Any())
                //    .Select(c => c.Selected = true);

                 if (b.BusinessCountries.Count > 0)
                 {
                     foreach (ListItem item in BdListBusinessInCountry.Items)
                     {
                         foreach (var abc in b.BusinessCountries)
                         {
                             if (item.Value == abc.ID.ToString())
                             {
                                 item.Selected = true;
                             }
                         }
                     }
                 }
                //BdListBusinessInCountry.Items
                //    .Cast<ListItem>()
                //    .Where(c => b.BusinessCountries.Where(i => i.ID == Convert.ToInt32(c.Value)).Any())
                //    .Select(c => c.Selected = true);

                BdSubmitButton.Visible = false;

                if (BdListBusinessInCountry.Items.Cast<ListItem>().Where(i => i.Selected == true && i.Value == "231").Any())
                {
                    PiListCBO.ClearSelection();
                    PiListCBO.SelectedIndex = 1;
                }
            }
        }

        private void SetBankingRelationshipOpen(int id)
        {
            BankingRelatationship b = new BankingRelatationship();

            if (b.GetBanikingRelationship(id))
            {
                ListExtensions.SetDropdownValue(b.NBP_BRANCH_INFORMATION.ID, BrListBranchInfo);
                ListExtensions.SetDropdownValue(b.NBP_ACCOUNT_TYPE.ID, BrListAccountType);
                BrAccountNumber.Text = ListExtensions.RemoveNull(b.NBP_ACCOUNT_NUMBER);
                BrAccountTitle.Text = b.NBP_ACCOUNT_TITLE;
                BrRelationShipSince.Text = b.NBP_RELATIONSHIP_SINCE;
                ListExtensions.SetDropdownValue(b.OTHER_BANK_CODE.ID, BrListOtherBranchCode);
                BrOtherBranchName.Text = b.OTHER_BRANCH_NAME;
                BrOtherAccountNumber.Text = b.OTHER_ACCOUNT_NUMBER;
                BrOtherAccountTitle.Text = b.OTHER_ACCOUNT_TITLE;
                BrOtherRelationshipSince.Text = b.OTHER_RELATIONSHIP_SINCE;

                BrSubmitButton.Visible = false;
            }
        }

        private void SetFinancialInfoOpen(int id)
        {
            MiscellaneousInfo m = new MiscellaneousInfo();

            if (m.GetBusinessMiscellaneousInfo(id))
            {
                FiTotalAssetValue.Text = m.TOTAL_ASSET_VALUE;
                FiLiabilities.Text = m.LIABILITIES;
                FiNetWorth.Text = m.NET_WORTH;
                if (m.SOURCE_OF_FUND != null)
                    ListExtensions.SetDropdownValue(m.SOURCE_OF_FUND,MiListSOF);
               // ListExtensions.SetDropdownValue(m.MONTHLY_TURNOVER_DEBIT.ID, FiListMonthTurnOverDebit);
              //  ListExtensions.SetDropdownValue(m.MONTHLY_TURNOVER_CREDIT.ID, FiListMonthTurnOverCredit);
              //  ListExtensions.SetDropdownValue(m.AVERAGE_CASH_DEPOSIT.ID, FiListAvgNoOfCashDeposits);
             //   ListExtensions.SetDropdownValue(m.AVERAGE_CASH_NON_DEPOSIT.ID, FiListAvgNoOfNonCashDeposits);
                FiGrossSale.Text = m.GROSS_SALE;
                //ListExtensions.SetDropdownValue(m.AVERAGE_CASH_NON_DEPOSIT.ID, FiListAvgNoOfNonCashDeposits);

                if (m.FREQUENCY_GROSS_SALE != null)
                    ListExtensions.SetDropdownValue(m.FREQUENCY_GROSS_SALE.ID, FiListFrequencyOfSale);

                FiSubmitButton.Visible = false;
            }
        }

        private void SetShareHolderInfoOpen(int id)
        {
            ShareHolderInformation SH = new ShareHolderInformation();
            SH.BID = id;
            if (SH.GET())
            {
                GridViewSH.DataSource = SH.SHARE_HOLDERS;
                GridViewSH.DataBind();
                btnGridAddSH.Visible = false;

                Session["SH"] = SH.SHARE_HOLDERS;
                ShSubmitButton.Visible = false;
            }

            
            
        }
 

        public int ReadQueryStringID()
        {
            if (Request.QueryString["ID"] != null)
            {
                return Convert.ToInt32(Request.QueryString["ID"]);
            }
            else
                return -1;
        }

        public string ReadQueryStringAction()
        {
            if (Request.QueryString["Action"] != null)
            {
                return Request.QueryString["Action"];
            }
            else
                return "";
        }
        private void SetData()
        {
            SetBasicInfo();
            SetAddrContactInfo();
            SetHeadContactInfo();
            SetBusinessDetail();
            SetBankingRelationship();
            SetFinancialInfo();
            SetShareHolderInfo();
            SetFatca();



        }

        private void SetFatca()
        {
            var arr = new List<string>
            {
                "No",
                "Yes"
                
            };

            PiListCI.DataSource = arr;
            PiListCI.DataBind();

            PiListCBO.DataSource = arr;
            PiListCBO.DataBind();

            PiListAddCountUsa.DataSource = arr;
            PiListAddCountUsa.DataBind();

            PiListPhoneNoUsa.DataSource = arr;
            PiListPhoneNoUsa.DataBind();

            PiListTransferOfFundsUSA.DataSource = arr;
            PiListTransferOfFundsUSA.DataBind();

            FatcaClassification fc = new FatcaClassification();
            PiListFatcaClass.DataSource = fc.GetFatcaClassification();
            PiListFatcaClass.DataValueField = "ID";
            PiListFatcaClass.DataTextField = "Name";
            PiListFatcaClass.DataBind();
            PiListFatcaClass.Items.Insert(0, new ListItem("Select", "0"));

            FatcaDocumentation fd = new FatcaDocumentation();
            PiListFatcaDocumentation.DataSource = fd.GetFatcaDocumentation();
            PiListFatcaDocumentation.DataValueField = "ID";
            PiListFatcaDocumentation.DataTextField = "Name";
            PiListFatcaDocumentation.DataBind();

            UsaTaxType t = new UsaTaxType();
            PiListUsTaxIdType.DataSource = t.GetUsaTaxType();
            PiListUsTaxIdType.DataValueField = "ID";
            PiListUsTaxIdType.DataTextField = "Name";
            PiListUsTaxIdType.DataBind();
            PiListUsTaxIdType.Items.Insert(0, new ListItem("Select", "0"));


        }

        private void SetShareHolderInfo()
        {
            PrimaryDocumentType i = new PrimaryDocumentType();
            ShIdentityType.DataSource = i.GetPrimaryDocumentTypes();
            ShIdentityType.DataValueField = "ID";
            ShIdentityType.DataTextField = "NAME";
            ShIdentityType.DataBind();
            ShIdentityType.Items.Insert(0, new ListItem("Select", "0"));

            DirectorStatus d = new DirectorStatus();
            ShDirectorStatus.DataSource = d.GetDirectorStatus();
            ShDirectorStatus.DataValueField = "ID";
            ShDirectorStatus.DataTextField = "NAME";
            ShDirectorStatus.DataBind();
            ShDirectorStatus.Items.Insert(0, new ListItem("Select", "0"));

            GridViewSH.DataSource = new List<ShareHolderInformation>();
            GridViewSH.DataBind();

        

        }
        private void SetFinancialInfo()
        {
            MonthlyTurnOverDebit md = new MonthlyTurnOverDebit();
            MonthlyTurnOverCredit mc = new MonthlyTurnOverCredit();
            AverageCashDeposit ac = new AverageCashDeposit();
            AverageNonCashDeposit ad = new AverageNonCashDeposit();
            FrequencyGrossSale fc = new FrequencyGrossSale();
            SourceOfFunds sof = new SourceOfFunds();

            FiListMonthTurnOverDebit.DataSource = md.GetMonthlyTurnOverDebit();
            FiListMonthTurnOverDebit.DataValueField = "ID";
            FiListMonthTurnOverDebit.DataTextField = "Name";
            FiListMonthTurnOverDebit.DataBind();
            FiListMonthTurnOverDebit.Items.Insert(0, new ListItem("Select", "0"));

            FiListMonthTurnOverCredit.DataSource = mc.GetMonthlyTurnOverCredit();
            FiListMonthTurnOverCredit.DataValueField = "ID";
            FiListMonthTurnOverCredit.DataTextField = "Name";
            FiListMonthTurnOverCredit.DataBind();
            FiListMonthTurnOverCredit.Items.Insert(0, new ListItem("Select", "0"));

            FiListAvgNoOfCashDeposits.DataSource = ac.GetAverageCashDeposits();
            FiListAvgNoOfCashDeposits.DataValueField = "ID";
            FiListAvgNoOfCashDeposits.DataTextField = "Name";
            FiListAvgNoOfCashDeposits.DataBind();
            FiListAvgNoOfCashDeposits.Items.Insert(0, new ListItem("Select", "0"));

            FiListAvgNoOfNonCashDeposits.DataSource = ad.GetAverageNonCashDeposit();
            FiListAvgNoOfNonCashDeposits.DataValueField = "ID";
            FiListAvgNoOfNonCashDeposits.DataTextField = "Name";
            FiListAvgNoOfNonCashDeposits.DataBind();
            FiListAvgNoOfNonCashDeposits.Items.Insert(0, new ListItem("Select", "0"));

            FiListFrequencyOfSale.DataSource = fc.GetFrequencyGrossSale();
            FiListFrequencyOfSale.DataValueField = "ID";
            FiListFrequencyOfSale.DataTextField = "Name";
            FiListFrequencyOfSale.DataBind();
            FiListFrequencyOfSale.Items.Insert(0, new ListItem("Select", "0"));

            MiListSOF.DataSource = sof.GetSourceOfFundsBusiness();
            MiListSOF.DataValueField = "ID";
            MiListSOF.DataTextField = "NAME";
            MiListSOF.DataBind();
            MiListSOF.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void SetBankingRelationship()
        {
            NbpBranchInformation nb = new NbpBranchInformation();
            AccountType ac = new AccountType();
            OtherBankCodes ob = new OtherBankCodes();

            BrListBranchInfo.DataSource = nb.GetNbpBranchInformation();
            BrListBranchInfo.DataValueField = "ID";
            BrListBranchInfo.DataTextField = "Name";
            BrListBranchInfo.DataBind();
            BrListBranchInfo.Items.Insert(0, new ListItem("Select", "0"));

            BrListAccountType.DataSource = ac.GetAccountTypes();
            BrListAccountType.DataValueField = "ID";
            BrListAccountType.DataTextField = "Name";
            BrListAccountType.DataBind();
            BrListAccountType.Items.Insert(0, new ListItem("Select", "0"));

            BrListOtherBranchCode.DataSource = ob.GetOtherBankCodes();
            BrListOtherBranchCode.DataValueField = "ID";
            BrListOtherBranchCode.DataTextField = "Name";
            BrListOtherBranchCode.DataBind();
            BrListOtherBranchCode.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void SetBusinessDetail()
        {
            InfoTypeBds i = new InfoTypeBds();
            City ci = new City();
            Country co = new Country();

            BdListInformationType.DataSource = i.GetInfoTypeBds();
            BdListInformationType.DataValueField = "ID";
            BdListInformationType.DataTextField = "Name";
            BdListInformationType.DataBind();

            BdListBusinessInCities.DataSource = ci.GetCifTypes();
            BdListBusinessInCities.DataValueField = "ID";
            BdListBusinessInCities.DataTextField = "Name";
            BdListBusinessInCities.DataBind();

            BdListBusinessInCountry.DataSource = co.GetCountries();
            BdListBusinessInCountry.DataValueField = "ID";
            BdListBusinessInCountry.DataTextField = "Name";
            BdListBusinessInCountry.DataBind();

        }
        private void SetHeadContactInfo()
        {
            District d = new District();
            City ci = new City();
            Country co = new Country();
            Province p = new Province();

            //HoListDistrict.DataSource = d.GetDistrictList();
            HoListCity.DataSource = ci.GetCifTypes();
            HoListCountryCode.DataSource = co.GetCountries();
            HoListCityReg.DataSource = ci.GetCifTypes();
            HoListCountryCodeReg.DataSource = co.GetCountries();
            HoListProvince.DataSource = p.GetProvinces();
            HoListProvinceReg.DataSource = p.GetProvinces();
//            BiListCategoryNBP.Items.Insert(0, new ListItem("Select", "0"));
           

            //HoListDistrict.DataValueField = "ID";
            //HoListDistrict.DataTextField = "Name";
            //HoListDistrict.DataBind();
            //HoListDistrict.Items.Insert(0, new ListItem("Select", "0"));
        
            HoListCity.DataValueField = "ID";
            HoListCity.DataTextField = "Name";
            HoListCity.DataBind();
            HoListCity.Items.Insert(0, new ListItem("Select", "0"));

            HoListCountryCode.DataValueField = "ID";
            HoListCountryCode.DataTextField = "Name";
            HoListCountryCode.DataBind();
            HoListCountryCode.Items.Insert(0, new ListItem("Select", "0"));

            HoListCityReg.DataValueField = "ID";
            HoListCityReg.DataTextField = "Name";
            HoListCityReg.DataBind();
            HoListCityReg.Items.Insert(0, new ListItem("Select", "0"));

            HoListCountryCodeReg.DataValueField = "ID";
            HoListCountryCodeReg.DataTextField = "Name";
            HoListCountryCodeReg.DataBind();
            HoListCountryCodeReg.Items.Insert(0, new ListItem("Select", "0"));

            HoListProvince.DataValueField = "ID";
            HoListProvince.DataTextField = "Name";
            HoListProvince.DataBind();
            HoListProvince.Items.Insert(0, new ListItem("Select", "0"));

            HoListProvinceReg.DataValueField = "ID";
            HoListProvinceReg.DataTextField = "Name";
            HoListProvinceReg.DataBind();
            HoListProvinceReg.Items.Insert(0, new ListItem("Select", "0"));

        }

        private void SetAddrContactInfo()
        {
            District d = new District();
            City ci = new City();
            Country co = new Country();
            Province p = new Province();

            //CiListDistrict.DataSource = d.GetDistrictList();
            CiListCity.DataSource = ci.GetCifTypes();
            CiListCountry.DataSource = co.GetCountries();
            CiListProvince.DataSource = p.GetProvinces();

            //CiListDistrict.DataValueField = "ID";
            //CiListDistrict.DataTextField = "Name";
            //CiListDistrict.DataBind();

            CiListCity.DataValueField = "ID";
            CiListCity.DataTextField = "Name";
            CiListCity.DataBind();
            CiListCity.Items.Insert(0, new ListItem("Select", "0"));

            CiListCountry.DataValueField = "ID";
            CiListCountry.DataTextField = "Name";
            CiListCountry.DataBind();
            CiListCountry.Items.Insert(0, new ListItem("Select", "0"));

            CiListProvince.DataValueField = "ID";
            CiListProvince.DataTextField = "Name";
            CiListProvince.DataBind();
            CiListProvince.Items.Insert(0, new ListItem("Select", "0"));
        }

        private void SetBasicInfo()
        {
            CifTypes c = new CifTypes();
            IssuingAgency i = new IssuingAgency();
            AccountNature n = new AccountNature();
            BusinessCustomerClassification b = new BusinessCustomerClassification();
            NatureBusiness nb = new NatureBusiness();
            NbpCategories cnbp = new NbpCategories();
            SbpCategories csbp = new SbpCategories();
            BaseCategories bc = new BaseCategories();
            CustomerDeal cd = new CustomerDeal();
            Country countries = new Country();
            InstitutionType inst = new InstitutionType();
            SicCode scode = new SicCode();

            BiListCifType.DataSource = c.GetCifTypes();
            BiListIssueAgency.DataSource = i.GetIssuingAgency();
            BiListNatureOfAccount.DataSource = n.GetAccountNature();
            BiListCustomerClassification.DataSource = b.GetBusinessCustomerClassifications();
            BiListNatureOfbusiness.DataSource = nb.GetAccountTypes();
            BiListCategoryNBP.DataSource = cnbp.GetNbpCategories();
            BiListCategorySBP.DataSource = csbp.GetSbpCategories();
            BiListCategoryBasel.DataSource = bc.GetBaseCategories();
            BiListCustomerDealIn.DataSource =cd.GetCustomerDeals();
            BiListCountryIncorporation.DataSource = countries.GetCountries();
            BiListInstitutionType.DataSource = inst.GetInstitutionTypes();
            BiListSicCode.DataSource = scode.GetSicCodes();

            BiListCifType.DataValueField = "ID";
            BiListCifType.DataTextField = "Name";
            BiListCifType.DataBind();

            BiListIssueAgency.DataValueField = "ID";
            BiListIssueAgency.DataTextField = "Name";
            BiListIssueAgency.DataBind();
            BiListIssueAgency.Items.Insert(0, new ListItem("Select", "0"));

            BiListNatureOfAccount.DataValueField = "ID";
            BiListNatureOfAccount.DataTextField = "Name";
            BiListNatureOfAccount.DataBind();
            BiListNatureOfAccount.Items.Insert(0, new ListItem("Select", "0"));

            BiListCustomerClassification.DataValueField = "ID";
            BiListCustomerClassification.DataTextField = "Name";
            BiListCustomerClassification.DataBind();
            BiListCustomerClassification.Items.Insert(0, new ListItem("Select", "0"));

            BiListNatureOfbusiness.DataValueField = "ID";
            BiListNatureOfbusiness.DataTextField = "Name";
            BiListNatureOfbusiness.DataBind();
            BiListNatureOfbusiness.Items.Insert(0, new ListItem("Select", "0"));

            BiListCategoryNBP.DataValueField = "ID";
            BiListCategoryNBP.DataTextField = "Name";
            BiListCategoryNBP.DataBind();
            BiListCategoryNBP.Items.Insert(0, new ListItem("Select", "0"));

            BiListCategorySBP.DataValueField = "ID";
            BiListCategorySBP.DataTextField = "Name";
            BiListCategorySBP.DataBind();
            BiListCategorySBP.Items.Insert(0, new ListItem("Select", "0"));

            BiListCategoryBasel.DataValueField = "ID";
            BiListCategoryBasel.DataTextField = "Name";
            BiListCategoryBasel.DataBind();

            BiListCategoryBasel.Items.Insert(0, new ListItem("Select", "0"));


            BiListCustomerDealIn.DataValueField = "ID";
            BiListCustomerDealIn.DataTextField = "Name";
            BiListCustomerDealIn.DataBind();
            BiListCustomerDealIn.Items.Insert(0, new ListItem("Select", "0"));

            BiListCountryIncorporation.DataValueField = "ID";
            BiListCountryIncorporation.DataTextField = "Name";
            BiListCountryIncorporation.DataBind();
            BiListCountryIncorporation.Items.Insert(0, new ListItem("Select", "0"));

            BiListInstitutionType.DataValueField = "ID";
            BiListInstitutionType.DataTextField = "Name";
            BiListInstitutionType.DataBind();
            BiListInstitutionType.Items.Insert(0, new ListItem("Select", "0"));

            BiListSicCode.DataValueField = "ID";
            BiListSicCode.DataTextField = "Name";
            BiListSicCode.DataBind();
            BiListSicCode.Items.Insert(0, new ListItem("Select", "0"));

            BiListBusinessType.Items.Insert(0, new ListItem("Select", "0"));
            BiListSubIndustry.Items.Insert(0, new ListItem("Select", "0"));

            

          //  BiDateOfRegistration.Text = DateTime.Now.ToString("yyyy-MM-dd");
         //   BiDateOfCommence.Text = DateTime.Now.ToString("yyyy-MM-dd");
            BiListCifType.Items.FindByText("BUSINESS / OTHER").Selected = true;

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
        protected void BiListCifType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BiListCifType.SelectedItem.Text == "NEXT_OF_KIN")
            {
                Response.Redirect("NextOfKin.aspx");
            }

            else if (BiListCifType.SelectedItem.Text == "INDIVIDUAL")
            {
                Response.Redirect("Individual.aspx");
            }

        }

        protected void BibtnSubmitBaisc_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                BasicInformations b = new BasicInformations();
                User LogedUser = Session["User"] as User;
                b.UserId = LogedUser.USER_ID;
                b.BRANCH_CODE = LogedUser.Branch.BRANCH_CODE;
                b.COUNTRY_INCORPORATION = new Country() { ID = Convert.ToInt32(b.COUNTRY_INCORPORATION) };
                b.CIF_TYPE = new CifTypes() { ID = Convert.ToInt32(BiListCifType.SelectedItem.Value), Name = BiListCifType.SelectedItem.Text };
                b.NAME_OFFICE = BiCompany.Text;
                b.NTN = BiNtn.Text;
                b.SALES_TAX_NO = BiSalesTax.Text;
                if (BiListIssueAgency.SelectedItem.Text == "Select")
                {
                    b.Issuing_Agency = new IssuingAgency() { ID = null, Name = BiListIssueAgency.SelectedItem.Text };

                }
                else
                {
                    b.Issuing_Agency = new IssuingAgency() { ID = Convert.ToInt32(BiListIssueAgency.SelectedItem.Value), Name = BiListIssueAgency.SelectedItem.Text };
                }
                b.ISSUING_AGENCY_OTHER = BiIssueOther.Text;

                b.COUNTRY_INCORPORATION = new Country() { ID = Convert.ToInt32(BiListCountryIncorporation.SelectedValue) };

                b.REG_NO = BiRegistrationNo.Text;
                b.REG_DATE = BiDateOfRegistration.Text;
                b.COMMENCEMENT_DATE = BiDateOfCommence.Text;
                b.PAST_BUSS_EXP = BiPastBusiness.Text;


                b.ACCOUNT_NATURE = new AccountNature() { ID = Convert.ToInt32(BiListNatureOfAccount.SelectedItem.Value), Name = BiListNatureOfAccount.SelectedItem.Text };



                b.CUSTOMER_CLASSIFICATION = new BusinessCustomerClassification() { ID = Convert.ToInt32(BiListCustomerClassification.SelectedItem.Value), Name = BiListCustomerClassification.SelectedItem.Text };



                b.CIF_GROUP = BiCifNo.Text;

                b.NATURE_BUSINESS = new NatureBusiness() { ID = Convert.ToInt32(BiListNatureOfbusiness.SelectedItem.Value), Name = BiListNatureOfbusiness.SelectedItem.Text };



                b.NATURE_BUSINESS_DESCRP = BiNatureOfBusinessDescr.Text;

                if (BiListCategoryNBP.SelectedItem.Text == "Select")
                {
                    b.CATERGORY_NBP = new NbpCategories() { ID = null, Name = BiListCategoryNBP.SelectedItem.Text };
                }
                else
                {

                    b.CATERGORY_NBP = new NbpCategories() { ID = Convert.ToInt32(BiListCategoryNBP.SelectedItem.Value), Name = BiListCategoryNBP.SelectedItem.Text };
                }

                if (BiListCategorySBP.SelectedItem.Text == "Select")
                {
                    b.CATERGORY_SBP = new SbpCategories() { ID = null, Name = BiListCategorySBP.SelectedItem.Text };
                }
                else
                {

                    b.CATERGORY_SBP = new SbpCategories() { ID = Convert.ToInt32(BiListCategorySBP.SelectedItem.Value), Name = BiListCategorySBP.SelectedItem.Text };
                }

                if (BiListCategoryBasel.SelectedItem.Text == "Select")
                {
                    b.CATERGORY_BASE = new BaseCategories() { ID = null, Name = BiListCategoryBasel.SelectedItem.Text };
                }
                else
                {

                    b.CATERGORY_BASE = new BaseCategories() { ID = Convert.ToInt32(BiListCategoryBasel.SelectedItem.Value), Name = BiListCategoryBasel.SelectedItem.Text };
                }

                //if(BiListCustomerDealIn.SelectedItem.Text == "Select")
                //{
                //    b.CUSTOMER_DEAL = new CustomerDeal() { ID = null, Name = BiListCustomerDealIn.SelectedItem.Text };
                //}
                //else
                //{

                //    b.CUSTOMER_DEAL = new CustomerDeal() { ID = Convert.ToInt32(BiListCustomerDealIn.SelectedItem.Value), Name = BiListCustomerDealIn.SelectedItem.Text };
                //}

                b.BUSINESS_TYPE = new BusinessType() { ID = (int) ListExtensions.getSelectedValue(BiListBusinessType) };
                b.INSTITUTION_TYPE = new InstitutionType() { ID = (int)ListExtensions.getSelectedValue(BiListInstitutionType) };
                b.SIC_CODES = new SicCode() { ID = (int)ListExtensions.getSelectedValue(BiListSicCode) };
                b.SUB_INDUSTRY = new SubIndustry() { ID = (int)ListExtensions.getSelectedValue(BiListSubIndustry) };
                b.CUSTOMER_DEAL = new CustomerDeal() { ID = ListExtensions.getSelectedValue(BiListCustomerDealIn), Name = BiListCustomerDealIn.SelectedItem.Text };
                b.DOCUMENT_VERIFIED = chkDocument.Checked;
                Session["BID"] = b.SaveBusiness();
                String mesg = "Company Information has been saved";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showAlertBasicInfoBus('" + mesg + "');", true);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showallBusiness()", true);

                BibtnSubmitBaisc.Visible = false;

                if (BiListCountryIncorporation.SelectedItem.Value == "231")
                {
                    PiListCI.ClearSelection();
                    PiListCI.SelectedIndex = 1;
                }
                       
            }
            

        }
        
        protected void CiSubmitButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ContactInfo c = new ContactInfo();
                c.BI_ID = Convert.ToInt32(Session["BID"]);
                // c.ADDRESS_PERMANENT = CiOffcMailingAddr.Text;
                //  c.DISTRICT_PERMANENT = new District() { ID = Convert.ToInt32(CiListDistrict.SelectedItem.Value), Name = CiListDistrict.SelectedItem.Text };

                c.COUNTRY_CODE = new Country() { ID = Convert.ToInt32(CiListCountry.SelectedItem.Value), Name = CiListCountry.SelectedItem.Text };
                if (CiListCountry.SelectedItem.Text.Trim() != "PAKISTAN")
                {
                    c.PROVINCE = new Province() { ID = null };
                    c.CITY_PERMANENT = new City() { ID = null, };
                }
                else
                {
                    c.PROVINCE = new Province() { ID = Convert.ToInt32(CiListProvince.SelectedItem.Value) };
                    c.CITY_PERMANENT = new City() { ID = Convert.ToInt32(CiListCity.SelectedItem.Value), Name = CiListCity.SelectedItem.Text };
                }
                
                

                c.POST_OFFICE = CiPoBox.Text;             
                c.POSTAL_CODE = CiPostalCode.Text;
               
                c.STREET = CiTxtStreet.Text;
                c.FLOOR = CiTxtFloor.Text;
                c.BIULDING_SUITE = CiTxtBuilding.Text;
                c.DISTRICT = CiTxtDistrict.Text;
               
                c.OFFICE_CONTACT = CiOfficeNo.Text;
                c.MOBILE_NO = CiMobileNo.Text;
                c.FAX_NO = CiFaxNo.Text;
                c.EMAIL = CiEmail.Text;
                c.WEB = CiWeb.Text;
                c.CP_NAME = CiPName.Text;
                c.CP_DESIGNATION = CiDesig.Text;
                c.CP_CNIC = CiCnic.Text;
                c.CP_CNIC_EXPIRY = CiCnicEDate.Text;
                c.CP_NTN = CiPNtn.Text;
                c.SaveContactInfoBusiness();

                CIF cf = new CIF(Convert.ToInt32(Session["BID"]), CifType.BUSINESS);

                if (cf.CheckCifCompleted())
                {
                    User LoggedUser = Session["User"] as User;
                    cf.ChangeStatus(Status.SUBMITTED, LoggedUser);
                    CalculateRisk();
                    Response.Redirect("CifAccount.aspx");
                }
                int id = Convert.ToInt32(Session["BID"]);
                String mesg = "Address/Contact Information has been saved";
                checkBusinessTabCompleted(id, mesg);


                CiSubmitButton.Visible = false;

                if (CiListCountry.SelectedItem.Value == "231")
                {
                    PiListAddCountUsa.ClearSelection();
                    PiListAddCountUsa.SelectedIndex = 1;
                }
            }

          

        }

        private void checkBusinessTabCompleted(int id, String mesg)
        {

            ContactInfo c = new ContactInfo();
            BusinessDetail b = new BusinessDetail();
            HeadContactInfo h = new HeadContactInfo();
            BankingRelatationship br = new BankingRelatationship();
            MiscellaneousInfo m = new MiscellaneousInfo();
            BasicInformations bi = new BasicInformations();
            ShareHolderInformation sh = new ShareHolderInformation();
            Fatca f = new Fatca();
            String contact = null;
            String bus = null;
            String head = null;
            String bank = null;
            String misc = null;
            String basic = null;
            string fatca = null;

            if (c.CheckIndividualContactInfo(id))
            {
                contact = "1";
            }
            if (b.CheckBusDetailCompleted(id))
            {
                bus = "1";
            }
            if (h.CheckHeadContactCompleted(id))
            {
                head = "1";
            }
            if (br.CheckBankingRelationship(id))
            {
                bank = "1";
            }
            if (m.CheckIndividualMiscInfo(id))
            {
                misc = "1";
            }
            if (sh.CheckSHComp(id))
            {
                basic = "1";
            }
            if (f.CheckIndividualFatca(id))
            {
                fatca = "1";
            }

            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "BusinessAfterPendingAlert('" + contact + "','" + head + "','" + bus + "','" + bank + "','" + misc + "','" + basic + "','" + fatca + "','" + mesg + "');", true);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", "showallBusiness()", true);


        }

        protected void BdSubmitButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BusinessDetail b = new BusinessDetail();
                b.BI_ID = Convert.ToInt32(Session["BID"]);
                b.InfoType = new InfoTypeBds() { ID = Convert.ToInt32(BdListInformationType.SelectedItem.Value), Name = BdListInformationType.SelectedItem.Text };
                b.INFO_DESC = BdInfoDescr.Text;
                // BI.NATIONALITIES = lstNationality.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => new Nationality { CountryID = Convert.ToInt32(i.Value), Country = i.Text }).ToList();
                b.BusinessCities = BdListBusinessInCities.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => new City { ID = Convert.ToInt32(i.Value), Name = i.Text }).ToList();
                b.BusinessCountries = BdListBusinessInCountry.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => new Country { ID = Convert.ToInt16(i.Value), Name = i.Text }).ToList();
                b.MAJOR_SUPPLIERS = BdTextMS.Text;
                b.MAIN_CUSTOMERS = BdTextMC.Text;
                b.MAIN_BUSINESS_ACTIVITY = BdTextMBLA.Text;

                b.SaveBusinesDetail();
                CIF cf = new CIF(Convert.ToInt32(Session["BID"]), CifType.BUSINESS);

                if (cf.CheckCifCompleted())
                {
                    User LoggedUser = Session["User"] as User;
                    cf.ChangeStatus(Status.SUBMITTED,LoggedUser);
                    CalculateRisk();
                    Response.Redirect("CifAccount.aspx");
                }
                int id = Convert.ToInt32(Session["BID"]);
                String mesg = "Business Detail Information has been saved";
                checkBusinessTabCompleted(id, mesg);

                BdSubmitButton.Visible = false;

                if (BdListBusinessInCountry.Items.Cast<ListItem>().Where(i => i.Selected == true && i.Value == "231").Any())
                {
                    PiListCBO.ClearSelection();
                    PiListCBO.SelectedIndex = 1;
                }
            }

          

        }

        protected void HoSubmitButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                HeadContactInfo h = new HeadContactInfo();
                h.BI_ID = Convert.ToInt32(Session["BID"]);
                //    h.ADDRESS = HoOfficeMailingAddr.Text;
                //    h.DISTRICT = new District() { ID =ListExtensions.getSelectedValue(HoListDistrict), Name = HoListDistrict.SelectedItem.Text };
                
                h.COUNTRY = new Country() { ID = ListExtensions.getSelectedValue(HoListCountryCode), Name = HoListCountryCode.SelectedItem.Text };
                if (HoListCountryCode.SelectedItem.Text.Trim() != "PAKISTAN")
                {
                    h.PROVINCE = new Province() { ID = null };
                    h.CITY = new City() { ID = null };
                }
                else
                {
                    h.PROVINCE = new Province() { ID = Convert.ToInt32(HoListProvince.SelectedItem.Value) };
                    h.CITY = new City() { ID = ListExtensions.getSelectedValue(HoListCity), Name = HoListCity.SelectedItem.Text };
                }
              
                h.BIULDING_SUITE = HoTxtBuilding.Text;
                h.STREET = HoTxtStreet.Text;
                h.FLOOR = HoTxtFloor.Text;
                h.DISTRICT_HEAD = HoTxtDistrict.Text;
                h.POBOX = HoTxtPostBox.Text;
                h.POSTAL_CODE = HoTxtPotalCode.Text;
              

                h.PHONE_OFFICE = HoOfficeNo.Text;
                h.MOBILE_NO = HoMobileNo.Text;
                h.FAX_NO = HoFaxNo.Text;
                h.EMAIL = HoEmail.Text;
                

                // New Fields
                h.COUNTRY_CODE_REG = new Country() { ID = Convert.ToInt32(HoListCountryCodeReg.SelectedValue) };
                if (HoListCountryCodeReg.SelectedItem.Text.Trim() != "PAKISTAN")
                {
                    h.CITY_REG = new City() { ID = null };
                    h.PROVINCE_REG = new Province() { ID = null }; 
                }
                else
                {
                    h.CITY_REG = new City() { ID = ListExtensions.getSelectedValue(HoListCityReg), Name = HoListCityReg.SelectedItem.Text };
                    h.PROVINCE_REG = new Province() { ID = Convert.ToInt32(HoListProvinceReg.SelectedItem.Value) }; 
                }

               
                               
                h.BIULDING_SUITE_REG = HoTxtBuildingReg.Text;
                h.FLOOR_REG = HoTxtFloorReg.Text;
                h.STREET_REG = HoTxtStreetReg.Text;
                h.DISTRICT_REG = HoTxtDistrictReg.Text;
                h.POST_OFFICE_REG = HoTxtPostBoxReg.Text;
                h.POSTAL_CODE_REG = HoTxtPotalCodeReg.Text;
            
             

                h.SaveHeadContactInfo();
                CIF cf = new CIF(Convert.ToInt32(Session["BID"]), CifType.BUSINESS);

                if (cf.CheckCifCompleted())
                {
                    User LoggedUser = Session["User"] as User;
                    cf.ChangeStatus(Status.SUBMITTED,LoggedUser);
                    CalculateRisk();
                    Response.Redirect("CifAccount.aspx");
                }
                int id = Convert.ToInt32(Session["BID"]);
                String mesg = "Head Office Address/Contact information has been saved";
                checkBusinessTabCompleted(id, mesg);


                HoSubmitButton.Visible = false;
            }

          

        }

        protected void BrSubmitButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BankingRelatationship b = new BankingRelatationship();
                b.BI_ID = Convert.ToInt32(Session["BID"]);
                b.NBP_BRANCH_INFORMATION = new NbpBranchInformation() { ID = Convert.ToInt32(BrListBranchInfo.SelectedItem.Value), Name = BrListBranchInfo.SelectedItem.Text };
                b.NBP_ACCOUNT_TYPE = new AccountType() { ID = Convert.ToInt32(BrListAccountType.SelectedItem.Value), Name = BrListAccountType.SelectedItem.Text };
                b.NBP_ACCOUNT_NUMBER = BrAccountNumber.Text;
                b.NBP_ACCOUNT_TITLE = BrAccountTitle.Text;
                b.NBP_RELATIONSHIP_SINCE = BrRelationShipSince.Text;

                b.OTHER_BANK_CODE = new OtherBankCodes() { ID = Convert.ToInt32(BrListOtherBranchCode.SelectedItem.Value), Name = BrListOtherBranchCode.SelectedItem.Text };
                b.OTHER_BRANCH_NAME = BrOtherBranchName.Text;
                b.OTHER_ACCOUNT_NUMBER = BrOtherAccountNumber.Text;
                b.OTHER_ACCOUNT_TITLE = BrOtherAccountTitle.Text;
                b.OTHER_RELATIONSHIP_SINCE = BrOtherRelationshipSince.Text;

                b.SaveBankingRelatationship();
                CIF cf = new CIF(Convert.ToInt32(Session["BID"]), CifType.BUSINESS);

                if (cf.CheckCifCompleted())
                {
                    User LoggedUser = Session["User"] as User;
                    cf.ChangeStatus(Status.SUBMITTED,LoggedUser);
                    CalculateRisk();
                    Response.Redirect("CifAccount.aspx");
                }
                int id = Convert.ToInt32(Session["BID"]);
                String mesg = "Banking Relationship has been saved";
                checkBusinessTabCompleted(id, mesg);

                BrSubmitButton.Visible = false;
            }

           
        }

        protected void FiSubmitButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                MiscellaneousInfo m = new MiscellaneousInfo();
                m.BI_ID = Convert.ToInt32(Session["BID"]);
                m.TOTAL_ASSET_VALUE = FiTotalAssetValue.Text;
                m.LIABILITIES = FiLiabilities.Text;
                m.NET_WORTH = FiNetWorth.Text;
                m.SOURCE_OF_FUND = Convert.ToInt32(MiListSOF.SelectedItem.Value);

             //   m.MONTHLY_TURNOVER_DEBIT = new MonthlyTurnOverDebit() { ID = Convert.ToInt32(FiListMonthTurnOverDebit.SelectedItem.Value), Name = FiListMonthTurnOverDebit.SelectedItem.Text };
            //    m.MONTHLY_TURNOVER_CREDIT = new MonthlyTurnOverCredit() { ID = Convert.ToInt32(FiListMonthTurnOverCredit.SelectedItem.Value), Name = FiListMonthTurnOverCredit.SelectedItem.Text };
            //    m.AVERAGE_CASH_DEPOSIT = new AverageCashDeposit() { ID = Convert.ToInt32(FiListAvgNoOfCashDeposits.SelectedItem.Value), Name = FiListAvgNoOfCashDeposits.SelectedItem.Text };
            //    m.AVERAGE_CASH_NON_DEPOSIT = new AverageNonCashDeposit() { ID = Convert.ToInt32(FiListAvgNoOfNonCashDeposits.SelectedItem.Value), Name = FiListAvgNoOfNonCashDeposits.SelectedItem.Text };
                m.GROSS_SALE = FiGrossSale.Text;
                m.FREQUENCY_GROSS_SALE = new FrequencyGrossSale() { ID = Convert.ToInt32(FiListFrequencyOfSale.SelectedItem.Value), Name = FiListFrequencyOfSale.SelectedItem.Text };
                m.SaveBusinessMiscellaneousInfo();


                CIF cf = new CIF(Convert.ToInt32(Session["BID"]), CifType.BUSINESS);

                if (cf.CheckCifCompleted())
                {
                    User LoggedUser = Session["User"] as User;
                    cf.ChangeStatus(Status.SUBMITTED,LoggedUser);
                    CalculateRisk();
                    Response.Redirect("CifAccount.aspx");
                }
                int id = Convert.ToInt32(Session["BID"]);
                String mesg = "Financial Information has been saved";
                checkBusinessTabCompleted(id, mesg);

                FiSubmitButton.Visible = false;
            }

          
        }

        protected void ShSubmitButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var DATA = Session["SH"] as List<ShareHolderInformation>;

                ShareHolderInformation SH = new ShareHolderInformation();
                SH.BID = Convert.ToInt32(Session["BID"]);
                SH.SHARE_HOLDERS = DATA;
                SH.SAVE();


                CIF cf = new CIF(Convert.ToInt32(Session["BID"]), CifType.BUSINESS);

                if (cf.CheckCifCompleted())
                {
                    User LoggedUser = Session["User"] as User;
                    cf.ChangeStatus(Status.SUBMITTED, LoggedUser);
                    CalculateRisk();
                    Response.Redirect("CifAccount.aspx");
                }
                int id = Convert.ToInt32(Session["BID"]);
                String mesg = "Directors Information has been saved";
                checkBusinessTabCompleted(id, mesg);
                ShSubmitButton.Visible = false;

            }

           



        }


     

        #region Update Code

        private void SetUpdateBtnVisible()
        {
            btnUpdateBasic.Visible = true;
            btnUpdateBd.Visible = true;
            btnUpdateBr.Visible = true;
            btnUpdateCi.Visible = true;
            btnUpdateFi.Visible = true;
            btnUpdateHo.Visible = true;
            btnUpdateSh.Visible = true;
        }

        private void SetSibmitCifVisible()
        {
            btnSubmitCifa.Visible = true;
            btnSubmitCifb.Visible = true;
            btnSubmitCifc.Visible = true;
            btnSubmitCifd.Visible = true;
            btnSubmitCife.Visible = true;
            btnSubmitCifg.Visible = true;
            btnSubmitCifg.Visible = true;

        }
        protected void btnSubmitCif_Click(object sender, EventArgs e)
        {
            int BID = (int)Session["BID"];
            CIF cif = new CIF(BID, CifType.BUSINESS);
            User LoggedUser = Session["User"] as User;
            cif.ChangeStatus(Status.SUBMITTED,LoggedUser);
            CalculateRisk();
            Response.Redirect("CifAccount.aspx");
        }

        protected void btnUpdateBasic_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BasicInformations BI = new BasicInformations();
                int BID = (int)Session["BID"];
                UpdateBasicInfo(BI, BID);
                String mesg = "Company Information has been Updated";
                checkBusinessTabCompleted(BID, mesg);
            }

            
        }

        private void UpdateBasicInfo(BasicInformations b, int BID)
        {           
            b.ID = BID;
            b.NAME_OFFICE = BiCompany.Text;
            b.NTN = BiNtn.Text;
            b.SALES_TAX_NO = BiSalesTax.Text;
            if (BiListIssueAgency.SelectedItem.Text == "Select")
            {
                b.Issuing_Agency = new IssuingAgency() { ID = null, Name = BiListIssueAgency.SelectedItem.Text };

            }
            else
            {
                b.Issuing_Agency = new IssuingAgency() { ID = Convert.ToInt32(BiListIssueAgency.SelectedItem.Value), Name = BiListIssueAgency.SelectedItem.Text };
            }
            b.ISSUING_AGENCY_OTHER = BiIssueOther.Text;

            b.REG_NO = BiRegistrationNo.Text;
           // b.REG_DATE = BiDateOfRegistration.Text;
            b.REG_DATE = DateTime.ParseExact(BiDateOfRegistration.Text,
                                  "dd-MM-yyyy",
                                   CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
          //  b.COMMENCEMENT_DATE = BiDateOfCommence.Text;
            b.COMMENCEMENT_DATE = DateTime.ParseExact(BiDateOfCommence.Text,
                                   "dd-MM-yyyy",
                                    CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            b.PAST_BUSS_EXP = BiPastBusiness.Text;
            b.ACCOUNT_NATURE = new AccountNature() { ID = Convert.ToInt32(BiListNatureOfAccount.SelectedItem.Value), Name = BiListNatureOfAccount.SelectedItem.Text };
            b.CUSTOMER_CLASSIFICATION = new BusinessCustomerClassification() { ID = Convert.ToInt32(BiListCustomerClassification.SelectedItem.Value), Name = BiListCustomerClassification.SelectedItem.Text };
            b.CIF_GROUP = BiCifNo.Text;
            b.NATURE_BUSINESS = new NatureBusiness() { ID = Convert.ToInt32(BiListNatureOfbusiness.SelectedItem.Value), Name = BiListNatureOfbusiness.SelectedItem.Text };
            b.NATURE_BUSINESS_DESCRP = BiNatureOfBusinessDescr.Text;
            if (BiListCategoryNBP.SelectedItem.Text == "Select")
            {
                b.CATERGORY_NBP = new NbpCategories() { ID = null, Name = BiListCategoryNBP.SelectedItem.Text };
            }
            else
            {

                b.CATERGORY_NBP = new NbpCategories() { ID = Convert.ToInt32(BiListCategoryNBP.SelectedItem.Value), Name = BiListCategoryNBP.SelectedItem.Text };
            }

            if (BiListCategorySBP.SelectedItem.Text == "Select")
            {
                b.CATERGORY_SBP = new SbpCategories() { ID = null, Name = BiListCategorySBP.SelectedItem.Text };
            }
            else
            {

                b.CATERGORY_SBP = new SbpCategories() { ID = Convert.ToInt32(BiListCategorySBP.SelectedItem.Value), Name = BiListCategorySBP.SelectedItem.Text };
            }

            if (BiListCategoryBasel.SelectedItem.Text == "Select")
            {
                b.CATERGORY_BASE = new BaseCategories() { ID = null, Name = BiListCategoryBasel.SelectedItem.Text };
            }
            else
            {

                b.CATERGORY_BASE = new BaseCategories() { ID = Convert.ToInt32(BiListCategoryBasel.SelectedItem.Value), Name = BiListCategoryBasel.SelectedItem.Text };
            }         
            b.CUSTOMER_DEAL = new CustomerDeal() { ID = ListExtensions.getSelectedValue(BiListCustomerDealIn), Name = BiListCustomerDealIn.SelectedItem.Text };
            b.COUNTRY_INCORPORATION = new Country() { ID = Convert.ToInt32(BiListCountryIncorporation.SelectedValue) };
            b.BUSINESS_TYPE = new BusinessType() { ID = (int)ListExtensions.getSelectedValue(BiListBusinessType) };
            b.INSTITUTION_TYPE = new InstitutionType() { ID = (int)ListExtensions.getSelectedValue(BiListInstitutionType) };
            b.SIC_CODES = new SicCode() { ID = (int)ListExtensions.getSelectedValue(BiListSicCode) };
            b.SUB_INDUSTRY = new SubIndustry() { ID = (int)ListExtensions.getSelectedValue(BiListSubIndustry) };
            b.DOCUMENT_VERIFIED = chkDocument.Checked;
            b.UpdateBusiness();          
        }
       

        protected void btnUpdateCi_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ContactInfo c = new ContactInfo();
                int BID = (int)Session["BID"];
                UpdateContactInfo(c, BID);
                String mesg = "Address / Contact Information  has been Updated";
                checkBusinessTabCompleted(BID, mesg);
            }

          
        }

        private void UpdateContactInfo(ContactInfo c, int BID)
        {
            c.BI_ID = BID;
            c.COUNTRY_CODE = new Country() { ID = Convert.ToInt32(CiListCountry.SelectedItem.Value), Name = CiListCountry.SelectedItem.Text };
            if (CiListCountry.SelectedItem.Text.Trim() != "PAKISTAN")
            {
                c.PROVINCE = new Province() { ID = null };
                c.CITY_PERMANENT = new City() { ID = null, };
            }
            else
            {
                c.PROVINCE = new Province() { ID = Convert.ToInt32(CiListProvince.SelectedItem.Value) };
                c.CITY_PERMANENT = new City() { ID = Convert.ToInt32(CiListCity.SelectedItem.Value), Name = CiListCity.SelectedItem.Text };
            }



            c.POST_OFFICE = CiPoBox.Text;
            c.POSTAL_CODE = CiPostalCode.Text;

            c.STREET = CiTxtStreet.Text;
            c.FLOOR = CiTxtFloor.Text;
            c.BIULDING_SUITE = CiTxtBuilding.Text;
            c.DISTRICT = CiTxtDistrict.Text;

            c.OFFICE_CONTACT = CiOfficeNo.Text;
            c.MOBILE_NO = CiMobileNo.Text;
            c.FAX_NO = CiFaxNo.Text;
            c.EMAIL = CiEmail.Text;
            c.WEB = CiWeb.Text;
            c.CP_NAME = CiPName.Text;
            c.CP_DESIGNATION = CiDesig.Text;
            c.CP_CNIC = CiCnic.Text;
            c.CP_CNIC_EXPIRY = CiCnicEDate.Text;
            c.CP_NTN = CiPNtn.Text;
            c.UpdateContactInfoBusiness();
        }
        
        protected void btnUpdateHo_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                HeadContactInfo c = new HeadContactInfo();
                int BID = (int)Session["BID"];
                UpdateHeadInfo(c, BID);
                String mesg = "Head Office Address / Contact Information  has been Updated";
                checkBusinessTabCompleted(BID, mesg);
            }

          
        }

        private void UpdateHeadInfo(HeadContactInfo h, int BID)
        {
            h.BI_ID = BID;
            h.COUNTRY = new Country() { ID = ListExtensions.getSelectedValue(HoListCountryCode), Name = HoListCountryCode.SelectedItem.Text };
            if (HoListCountryCode.SelectedItem.Text.Trim() != "PAKISTAN")
            {
                h.PROVINCE = new Province() { ID = null };
                h.CITY = new City() { ID = null };
            }
            else
            {
                h.PROVINCE = new Province() { ID = Convert.ToInt32(HoListProvince.SelectedItem.Value) };
                h.CITY = new City() { ID = ListExtensions.getSelectedValue(HoListCity), Name = HoListCity.SelectedItem.Text };
            }

            h.BIULDING_SUITE = HoTxtBuilding.Text;
            h.STREET = HoTxtStreet.Text;
            h.FLOOR = HoTxtFloor.Text;
            h.DISTRICT_HEAD = HoTxtDistrict.Text;
            h.POBOX = HoTxtPostBox.Text;
            h.POSTAL_CODE = HoTxtPotalCode.Text;


            h.PHONE_OFFICE = HoOfficeNo.Text;
            h.MOBILE_NO = HoMobileNo.Text;
            h.FAX_NO = HoFaxNo.Text;
            h.EMAIL = HoEmail.Text;


            // New Fields
            h.COUNTRY_CODE_REG = new Country() { ID = Convert.ToInt32(HoListCountryCodeReg.SelectedValue) };
            if (HoListCountryCodeReg.SelectedItem.Text.Trim() != "PAKISTAN")
            {
                h.CITY_REG = new City() { ID = null };
                h.PROVINCE_REG = new Province() { ID = null };
            }
            else
            {
                h.CITY_REG = new City() { ID = ListExtensions.getSelectedValue(HoListCityReg), Name = HoListCityReg.SelectedItem.Text };
                h.PROVINCE_REG = new Province() { ID = Convert.ToInt32(HoListProvinceReg.SelectedItem.Value) };
            }



            h.BIULDING_SUITE_REG = HoTxtBuildingReg.Text;
            h.FLOOR_REG = HoTxtFloorReg.Text;
            h.STREET_REG = HoTxtStreetReg.Text;
            h.DISTRICT_REG = HoTxtDistrictReg.Text;
            h.POST_OFFICE_REG = HoTxtPostBoxReg.Text;
            h.POSTAL_CODE_REG = HoTxtPotalCodeReg.Text;
            
            h.UpdateHeadContactInfo();
           
        }

        protected void btnUpdateBd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BusinessDetail bd = new BusinessDetail();
                int BID = (int)Session["BID"];
                UpdateBusinessDetail(bd, BID);
                String mesg = "Business Detail  has been Updated";
                checkBusinessTabCompleted(BID, mesg);
            }

     
        }

        private void UpdateBusinessDetail(BusinessDetail b, int BID)
        {
            b.BI_ID = BID;
            b.InfoType = new InfoTypeBds() { ID = Convert.ToInt32(BdListInformationType.SelectedItem.Value), Name = BdListInformationType.SelectedItem.Text };
            b.INFO_DESC = BdInfoDescr.Text;
            b.MAJOR_SUPPLIERS = BdTextMS.Text;
            b.MAIN_CUSTOMERS = BdTextMC.Text;
            b.MAIN_BUSINESS_ACTIVITY = BdTextMBLA.Text;
            b.BusinessCities = BdListBusinessInCities.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => new City { ID = Convert.ToInt32(i.Value), Name = i.Text }).ToList();
            b.BusinessCountries = BdListBusinessInCountry.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => new Country { ID = Convert.ToInt16(i.Value), Name = i.Text }).ToList();
            
            b.UpdateBusinesDetail();          
        }

        protected void btnUpdateBr_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BankingRelatationship br = new BankingRelatationship();
                int BID = (int)Session["BID"];
                UpdateBankRela(br, BID);
                String mesg = "Banking Relatationship  has been Updated";
                checkBusinessTabCompleted(BID, mesg);
            }

           
        }

        private void UpdateBankRela(BankingRelatationship b, int BID)
        {
            b.BI_ID = BID;
            b.NBP_BRANCH_INFORMATION = new NbpBranchInformation() { ID = Convert.ToInt32(BrListBranchInfo.SelectedItem.Value), Name = BrListBranchInfo.SelectedItem.Text };
            b.NBP_ACCOUNT_TYPE = new AccountType() { ID = Convert.ToInt32(BrListAccountType.SelectedItem.Value), Name = BrListAccountType.SelectedItem.Text };
            b.NBP_ACCOUNT_NUMBER = BrAccountNumber.Text;
            b.NBP_ACCOUNT_TITLE = BrAccountTitle.Text;
            b.NBP_RELATIONSHIP_SINCE = BrRelationShipSince.Text;
            b.OTHER_BANK_CODE = new OtherBankCodes() { ID = Convert.ToInt32(BrListOtherBranchCode.SelectedItem.Value), Name = BrListOtherBranchCode.SelectedItem.Text };
            b.OTHER_BRANCH_NAME = BrOtherBranchName.Text;
            b.OTHER_ACCOUNT_NUMBER = BrOtherAccountNumber.Text;
            b.OTHER_ACCOUNT_TITLE = BrOtherAccountTitle.Text;
            b.OTHER_RELATIONSHIP_SINCE = BrOtherRelationshipSince.Text;
            b.UpdateBankingRelationship();
           
        }

        protected void btnUpdateFi_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                MiscellaneousInfo m = new MiscellaneousInfo();
                int BID = (int)Session["BID"];
                UpdateFinanInfo(m, BID);
                String mesg = "Financial Information  has been Updated";
                checkBusinessTabCompleted(BID, mesg);
            }

         
        }

        private void UpdateFinanInfo(MiscellaneousInfo m, int BID)
        {
            m.BI_ID = BID;
            m.TOTAL_ASSET_VALUE = FiTotalAssetValue.Text;
            m.LIABILITIES = FiLiabilities.Text;
            m.NET_WORTH = FiNetWorth.Text;
         //   m.MONTHLY_TURNOVER_DEBIT = new MonthlyTurnOverDebit() { ID = Convert.ToInt32(FiListMonthTurnOverDebit.SelectedItem.Value), Name = FiListMonthTurnOverDebit.SelectedItem.Text };
         //   m.MONTHLY_TURNOVER_CREDIT = new MonthlyTurnOverCredit() { ID = Convert.ToInt32(FiListMonthTurnOverCredit.SelectedItem.Value), Name = FiListMonthTurnOverCredit.SelectedItem.Text };
        //    m.AVERAGE_CASH_DEPOSIT = new AverageCashDeposit() { ID = Convert.ToInt32(FiListAvgNoOfCashDeposits.SelectedItem.Value), Name = FiListAvgNoOfCashDeposits.SelectedItem.Text };
         //   m.AVERAGE_CASH_NON_DEPOSIT = new AverageNonCashDeposit() { ID = Convert.ToInt32(FiListAvgNoOfNonCashDeposits.SelectedItem.Value), Name = FiListAvgNoOfNonCashDeposits.SelectedItem.Text };
            m.GROSS_SALE = FiGrossSale.Text;
            m.SOURCE_OF_FUND = Convert.ToInt32(MiListSOF.SelectedItem.Value);
            m.FREQUENCY_GROSS_SALE = new FrequencyGrossSale() { ID = Convert.ToInt32(FiListFrequencyOfSale.SelectedItem.Value), Name = FiListFrequencyOfSale.SelectedItem.Text };
            m.UpdateBusinessMiscellaneousInfo();
        }

        protected void btnUpdateSh_Click(object sender, EventArgs e)
        {
            ShareHolderInformation sh = new ShareHolderInformation();
            int BID = (int)Session["BID"];
            UpdateShareInfo(sh, BID);
            String mesg = "Directors Information has been Updated";
            checkBusinessTabCompleted(BID, mesg);
        }

        private void UpdateShareInfo(ShareHolderInformation b, int BID)
        {
            var DATA = Session["SH"] as List<ShareHolderInformation>;
            b.BID = BID;
            b.SHARE_HOLDERS = DATA;
            b.UPDATE();
        }

        #endregion

        protected void CustomValidatorNTN_ServerValidate(object source, ServerValidateEventArgs args)
        {
            BasicInformations b = new BasicInformations();
            int CIF_TYPE = Convert.ToInt32(BiListCifType.SelectedItem.Value);
            if (b.IsNtnExists(BiNtn.Text, CIF_TYPE))
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        protected void CustomValidatorSaleTax_ServerValidate(object source, ServerValidateEventArgs args)
        {
            BasicInformations b = new BasicInformations();
            if (b.IsSaleTaxNoExixts(BiSalesTax.Text))
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        protected void CustomValidatorRegNo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            BasicInformations b = new BasicInformations();
            if (b.IsRegNoExixts(BiRegistrationNo.Text))
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        protected void BiListIssueAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BiListIssueAgency.SelectedItem.Text != "Select")
                RequiredFieldValidatorReg.Enabled = true;
            else
                RequiredFieldValidatorReg.Enabled = false;

            if (BiListIssueAgency.SelectedItem.Text == "OTHER")
                RequiredFieldValidatorOtherIssue.Enabled = true;
            else
                RequiredFieldValidatorOtherIssue.Enabled = false;
        }

        protected void FiTotalAssetValue_TextChanged(object sender, EventArgs e)
        {
            decimal TotalAsset = 0M;
            decimal TotalLiab = 0M;
            decimal Calc = 0M;
            if (FiTotalAssetValue.Text.Length > 0)
                TotalAsset = Convert.ToDecimal(FiTotalAssetValue.Text);

            if (FiLiabilities.Text.Length > 0)
                TotalLiab = Convert.ToDecimal(FiLiabilities.Text);
            Calc = TotalAsset - TotalLiab;
            FiNetWorth.Text = Calc.ToString();
        }

        protected void FiLiabilities_TextChanged(object sender, EventArgs e)
        {
            decimal TotalAsset = 0M;
            decimal TotalLiab = 0M;
            decimal Calc = 0M;
            if (FiTotalAssetValue.Text.Length > 0)
                TotalAsset = Convert.ToDecimal(FiTotalAssetValue.Text);

            if (FiLiabilities.Text.Length > 0)
                TotalLiab = Convert.ToDecimal(FiLiabilities.Text);
            Calc = TotalAsset - TotalLiab;
            FiNetWorth.Text = Calc.ToString();
        }

        protected void BiRegistrationNo_TextChanged(object sender, EventArgs e)
        {
            if (BiRegistrationNo.Text.Length > 0)
                RequiredFieldValidatorIssuingAgency.Enabled = true;
            else
                RequiredFieldValidatorIssuingAgency.Enabled = false;
        }

        protected void BiListCustomerClassification_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BiListCustomerClassification.SelectedItem.Value != "0")
            {
                int val = Convert.ToInt32(BiListCustomerClassification.SelectedItem.Value);

                BusinessType bt = new BusinessType();

                BiListBusinessType.DataSource = bt.GetBusinessTypes(val);
                BiListBusinessType.DataValueField = "ID";
                BiListBusinessType.DataTextField = "Name";
                BiListBusinessType.DataBind();
                BiListBusinessType.Items.Insert(0, new ListItem("Select", "0"));
            }

           


        }

        protected void BiListSicCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BiListSicCode.SelectedItem.Value != "0")
            {
                int val = Convert.ToInt32(BiListSicCode.SelectedItem.Value);

                SubIndustry si = new SubIndustry();
                BiListSubIndustry.DataSource = si.GetSubIndustrys(val);
                BiListSubIndustry.DataValueField = "ID";
                BiListSubIndustry.DataTextField = "Name";
                BiListSubIndustry.DataBind();
                BiListSubIndustry.Items.Insert(0, new ListItem("Select", "0"));

            }
        }

        public void CalculateRisk()
        {
            try
            {
                User LoggedUser = Session["User"] as User;
                Country c = new Country();
                string CustomerIdNumber = Convert.ToInt32(Session["BID"]).ToString(); // "2280829";
                string BranchCode = LoggedUser.Branch.BRANCH_CODE;
                string CNIC = "";
                string CustomerType = "1";
                string CustomerTitle = "";
                string CustomerFirstName = "";
                string CustomerMiddleName = "";
                string CustomerLastName = BiCompany.Text;
                string LegalName = "";
                string InstituteName = "NBP";
                string InstituteStartDate = "1949-11-09";
                string Gender = "";
                string DateOfBirth = "";


                string Industry = BiListSicCode.SelectedItem.Value;
                string TaxIdentifierFormat = "";
                string TaxIdentificationNumber = "";
                if (BiNtn.Text.Length > 0)
                    TaxIdentificationNumber = BiNtn.Text;
                else
                    TaxIdentificationNumber = BiRegistrationNo.Text;
                string Occupation = "";
                string CustomerCreationDate = DateTime.Now.ToString("yyyy-MM-dd");
                string ResidenceCountry = c.GetCountryProfileCode(BiListCountryIncorporation.SelectedItem.Text);
                string PrimaryCitznCountry = "";
                string SecondryCitznCountry = "";
                string DocumetsVerifiedFlag = "";
                if (chkDocument.Checked)
                    DocumetsVerifiedFlag = "Y";
                else
                    DocumetsVerifiedFlag = "N";
                string Jurisdiction = "PAK";
                string CountryOfRelationship = "";
                string RelationType = "";
                string AddressType = "P";
                string AddressLine1 = CiTxtBuilding.Text ;
                string AddressLine2 = CiTxtFloor.Text;
                string AddressLine3 = CiTxtStreet.Text;
                string AddressLine4 = CiTxtDistrict.Text;
                string AddressLine5 = "";
                string AddressLine6 = "";
                string City = CiListCity.SelectedItem.Text;
                string State = CiListProvince.SelectedItem.Value;
                string Region = LoggedUser.Region.NAME;
                string PostalCode = CiPostalCode.Text;
                string AddressCountry = c.GetCountryProfileCode(CiListCountry.SelectedItem.Text);
                string Zmobile = "";
                string HPH = "";
                string BPH = "";
                string PhoneType = "";
                if (CiMobileNo.Text.Length > 0)
                {
                    PhoneType = "M";
                    Zmobile = CiMobileNo.Text;
                }
                else if (CiOfficeNo.Text.Length > 0)
                {
                    PhoneType = "M";
                    Zmobile = CiOfficeNo.Text;

                    //PhoneType = "H";
                    //HPH = CiOfficeNo.Text;
                }
                else
                {
                    PhoneType = "F";
                    HPH = CiFaxNo.Text;
                }

                string PhoneExtension = "";
                SourceOfFunds sof = new SourceOfFunds();
                string SourceType = sof.GetProfileCodeBusiness(Convert.ToInt32(MiListSOF.SelectedItem.Value));
                string Currency = "PKR";
                string Role = "OWNER";
                string RelationDefn = "";
                string ParentCustomerNumber = "";
                string ServiceCall = "AO";

                /////
                string RiskCategory = "";
                string RiskScore = "";



                CAOP.RISK.GetRisk obj = new RISK.GetRisk();
                //string reader = obj.GetRiskScore("2280829", "0300", "35201-1676703-5", "", "0", "", "RASHWAN", "Hamza",
                //                                                     "", "", "National Bank of Pakistan", "1949-01-01", "M", "1982-01-05", "", "", "", "7", "2015-01-24", "PK",
                //                                                    "", "", "N", "PAK", "", "1", "P", "Test1 Lahore", "Test2 Lahore", "Test3 Lahore", "Test4 Lahore", "",
                //                                                    "", "LAHORE", "2", "", "", "PK", "M", "03217878439", "", "", "", "101",
                //                                                    "PKR", "OWNER", "", "", "", "", "", "AO");
                string reader = obj.GetRiskScore(BranchCode, CustomerIdNumber, CNIC,
                                       CustomerType, CustomerTitle, CustomerFirstName, CustomerMiddleName,
                                       CustomerLastName, LegalName, InstituteName, InstituteStartDate,
                                       Gender, DateOfBirth, Industry, TaxIdentifierFormat,
                                       TaxIdentificationNumber, Occupation, CustomerCreationDate, ResidenceCountry,
                                       PrimaryCitznCountry, SecondryCitznCountry, DocumetsVerifiedFlag, Jurisdiction,
                                       CountryOfRelationship, RelationType, AddressType, AddressLine1,
                                       AddressLine2, AddressLine3, AddressLine4, AddressLine5,
                                       AddressLine6, City, State, Region,
                                       PostalCode, AddressCountry, PhoneType, Zmobile,
                                       HPH, BPH, PhoneExtension, SourceType,
                                       Currency, Role, RelationDefn, ParentCustomerNumber,
                                       ServiceCall);


                StringReader sr = new StringReader(reader);
                DataSet ds = new DataSet();
                ds.ReadXml(sr);
                if (ds.Tables.Count > 0)
                {
                    DataTable dtResponse = ds.Tables[1];

                    string RiskScoredt = dtResponse.Rows[0]["doRealTimeRiskRatingReturn"].ToString(); //RiskScoreRecorder //"2";//
                    StringReader srscor = new StringReader(RiskScoredt);
                    ds = new DataSet();
                    ds.ReadXml(srscor);
                    dtResponse = ds.Tables[0];

                    // string ServiceReturnMessage;

                    if (dtResponse.Columns.Contains("Error"))
                    {
                        //error

                        //ServiceReturnMessage = "Sorry ! RAOR Service return Error for This Account While Calculating Risk.....";
                    }
                    else
                    {



                        RiskCategory = dtResponse.Rows[0]["RAORiskCategory"].ToString(); //"Statndard";//
                        RiskScore = dtResponse.Rows[0]["RAORiskScore"].ToString(); //RiskScoreRecorder //"2";//

                        // ServiceReturnMessage = "Risk Score for the Requested Customer is Calculated Successfully....";
                    }
                }
                int BID = Convert.ToInt32(Session["BID"]);
                BasicInformations Bi = new BasicInformations();
                Bi.InsertRisk(RiskCategory, RiskScore, BID);
            }
            catch (Exception e)
            {
 
            }

           


        }

        protected void CustomValidatorBusinessCountries_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (BdListBusinessInCountry.Items.Cast<ListItem>().Where(i => i.Selected == true).Any())
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void CustomValidatorBusinessCities_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (BdListBusinessInCities.Items.Cast<ListItem>().Where(i => i.Selected == true).Any())
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void BdListBusinessInCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BdListBusinessInCountry.Items.FindByText("PAKISTAN").Selected == true)
                CustomValidatorBusinessCities.Enabled = true;
            else
                CustomValidatorBusinessCities.Enabled = false;
        }

        protected void btnDeleteSH_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            Label IDENTITY_NO = (Label)gvr.FindControl("IDENTITY_NO");

            var Data = Session["SH"] as List<ShareHolderInformation>;

            Data.Remove(Data.FirstOrDefault(s => s.IDENTITY_NO == IDENTITY_NO.Text));

            GridViewSH.DataSource = Data;
            GridViewSH.DataBind();
        }

        protected void btnGridAddSH_Click(object sender, EventArgs e)
        {
            if (Session["SH"] == null)
                Session["SH"] = new List<ShareHolderInformation>();

            var Data = Session["SH"] as List<ShareHolderInformation>;
            if (!Data.Where(s => s.IDENTITY_NO == ShIdentityNo.Text).Any())
            {
                ShareHolderInformation NSH = new ShareHolderInformation();
                NSH.NAME = ShName.Text;
                NSH.ADDRESS = ShAddress.Text;
                NSH.IDENTITY_TYPE = ShIdentityType.SelectedItem.Text;
                NSH.IDENTITY_TYPE_VALUE = Convert.ToInt32(ShIdentityType.SelectedItem.Value);
                NSH.IDENTITY_NO = ShIdentityNo.Text;
                NSH.IDENTITY_EXPIRY_DATE = ShExpDate.Text;
                NSH.RESIDENCE_PHONE = ShResPhone.Text;
                NSH.OFFICE_PHONE = ShOfPhone.Text;
                NSH.MOBILE_NO = ShMobNo.Text;
                NSH.FAX_NO = ShFaxNo.Text;
                NSH.EMAIL = ShEmail.Text;
                NSH.NO_SHARES = ShNoSharesHeld.Text;
                NSH.AMOUNT_SHARES = ShAmountShareHeld.Text;
                NSH.SHAREHOLDER_PERCENTAGE = ShPercentage.Text;
                NSH.NET_WORTH = ShNetWorth.Text;
                NSH.DIRECTOR_STATUS = ShDirectorStatus.SelectedItem.Text;
                NSH.DIRECTOR_STATUS_VALUE = Convert.ToInt32(ShDirectorStatus.SelectedItem.Value);

                Data.Add(NSH);
                GridViewSH.DataSource = Data;
                GridViewSH.DataBind();
            }

      

            

        }

        protected void CustomValidatorMoreShareHolder_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = GridViewSH.Rows.Count > 0;
             
        }

        protected void ShIdentityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ShIdentityType.SelectedItem.Text == "CNIC")
                RegularExpressionValidatorCnic.Enabled = true;
            else
                RegularExpressionValidatorCnic.Enabled = false;
        }



        protected void PiListPhoneNoUsa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PiListPhoneNoUsa.SelectedIndex == 1)
            {
                CustomValidatorOneNumberFAtca.Enabled = true;
            }
            else
            {
                CustomValidatorOneNumberFAtca.Enabled = false;
            }
        }

        protected void PiListFatcaClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PiListFatcaClass.SelectedIndex == 1)
            {
                RequiredFieldValidatorFDDate.Enabled = true;
                RequiredFieldValidatorFTax.Enabled = true;
                RequiredFieldValidatorFTaxNo.Enabled = true;
                CustomValidatorFatcaDoc.Enabled = true;

            }
            else
            {
                RequiredFieldValidatorFDDate.Enabled = false;
                RequiredFieldValidatorFTax.Enabled = false;
                RequiredFieldValidatorFTaxNo.Enabled = false;
                CustomValidatorFatcaDoc.Enabled = false;
                PiListFatcaDocumentation.Enabled = false;
            }
        }

        protected void PiSubmitButton_Click(object sender, EventArgs e)
        {
            Fatca f = new Fatca();
            f.BI_ID = Convert.ToInt32(Session["BID"]);

            if (PiListAddCountUsa.SelectedItem.Text == "Yes")
            {
                f.ADDRESS_USA = true;
            }
            else
            {
                f.ADDRESS_USA = false;
            }

            f.USA_PHONE = new UsaPhone() { ID = PiListPhoneNoUsa.SelectedIndex, Name = PiListPhoneNoUsa.SelectedItem.Text };
            f.CONTACT_OFFICE = PiContactOffice.Text;
            f.CONTACT_RESIDENCE = PiContactResidence.Text;
            f.MOBNO = PiMobileNo.Text;
            f.FAXNO = PiFaxNo.Text;
            if (PiTxtFatcaDocumentDate.Text.Length > 0)
                f.FATCA_DOCUMENTATION_DATE = Convert.ToDateTime(PiTxtFatcaDocumentDate.Text);
          
            f.FUND_TRANSFER = new UsaFund() { ID = PiListTransferOfFundsUSA.SelectedIndex, Name = PiListTransferOfFundsUSA.SelectedItem.Text };
            f.FTCA_CLASSIFICATION = new FatcaClassification() { ID = ListExtensions.getSelectedValue(PiListFatcaClass), Name = PiListFatcaClass.SelectedItem.Text };
            f.US_TAXID = new UsaTaxType() { ID = Convert.ToInt32(PiListUsTaxIdType.SelectedItem.Value), Name = PiListUsTaxIdType.SelectedItem.Text };
            f.TAXNO = PiTaxNo.Text;
            f.COUNTRY_INCORP = PiListCI.SelectedIndex;
            f.COUNTRY_BUSINESS = PiListCBO.SelectedIndex;


            f.FATCA_DOCUMENTS = PiListFatcaDocumentation.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => new FatcaDocumentation { ID = Convert.ToInt32(i.Value), Name = i.Text }).ToList();
            f.SaveFatca();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

            CIF cf = new CIF(Convert.ToInt32(Session["BID"]), CifType.BUSINESS);


            if (cf.CheckCifCompleted() == true)
            {
                User LoggedUser = Session["User"] as User;

                cf.ChangeStatus(Status.SUBMITTED, LoggedUser);
                CalculateRisk();
                Response.Redirect("CifAccount.aspx");

            }



            String mesg = "FATCA(U.S Person Identification) has been saved";
            int id = Convert.ToInt32(Session["BID"]);

             checkBusinessTabCompleted(id, mesg);

            PiSubmitButton.Visible = false;
        }

        protected void btnPiUpdate_Click(object sender, EventArgs e)
        {
            Fatca f = new Fatca();
            f.BI_ID = Convert.ToInt32(Session["BID"]);

            if (PiListAddCountUsa.SelectedItem.Text == "Yes")
            {
                f.ADDRESS_USA = true;
            }
            else
            {
                f.ADDRESS_USA = false;
            }

            f.USA_PHONE = new UsaPhone() { ID = ListExtensions.getSelectedValue(PiListPhoneNoUsa), Name = PiListPhoneNoUsa.SelectedItem.Text };
            f.CONTACT_OFFICE = PiContactOffice.Text;
            f.CONTACT_RESIDENCE = PiContactResidence.Text;
            f.MOBNO = PiMobileNo.Text;
            f.FAXNO = PiFaxNo.Text;
            if (PiTxtFatcaDocumentDate.Text.Length > 0)
                f.FATCA_DOCUMENTATION_DATE = Convert.ToDateTime(PiTxtFatcaDocumentDate.Text);

            f.FUND_TRANSFER = new UsaFund() { ID = ListExtensions.getSelectedValue(PiListTransferOfFundsUSA), Name = PiListTransferOfFundsUSA.SelectedItem.Text };
            f.FTCA_CLASSIFICATION = new FatcaClassification() { ID = ListExtensions.getSelectedValue(PiListFatcaClass), Name = PiListFatcaClass.SelectedItem.Text };
            f.US_TAXID = new UsaTaxType() { ID = Convert.ToInt32(PiListUsTaxIdType.SelectedItem.Value), Name = PiListUsTaxIdType.SelectedItem.Text };
            f.TAXNO = PiTaxNo.Text;
            f.COUNTRY_INCORP = PiListCI.SelectedIndex;
            f.COUNTRY_BUSINESS = PiListCBO.SelectedIndex;


            f.FATCA_DOCUMENTS = PiListFatcaDocumentation.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => new FatcaDocumentation { ID = Convert.ToInt32(i.Value), Name = i.Text }).ToList();
            f.UpdateFatca();

            int BID = (int)Session["BID"];
            String mesg = "FATCA(U.S Person Identification) has been Updated";
            checkBusinessTabCompleted(BID, mesg);

        }

        private void SetPersonIdentificationOpen(int id)
        {
            Fatca f = new Fatca();
            if (f.GetFatcaIndividual(id))
            {

                

                if (f.ADDRESS_USA == true)
                {
                    PiListAddCountUsa.Items.FindByText("Yes").Selected = true;

                }
                else
                {
                    PiListAddCountUsa.Items.FindByText("No").Selected = true;

                }


             
                PiTxtFatcaDocumentDate.Text = f.FATCA_DOCUMENTATION_DATE.ToString(); ;
               // ListExtensions.SetDropdownValue(f.USA_PHONE.ID, PiListPhoneNoUsa);
                PiListPhoneNoUsa.SelectedIndex = (int) f.USA_PHONE.ID ;
                PiContactOffice.Text = f.CONTACT_OFFICE;
                PiContactResidence.Text = f.CONTACT_RESIDENCE;
                PiMobileNo.Text = f.MOBNO;
                PiFaxNo.Text = f.FAXNO;            
             //   ListExtensions.SetDropdownValue(f.FUND_TRANSFER.ID, PiListTransferOfFundsUSA);
                PiListTransferOfFundsUSA.SelectedIndex = (int)f.FUND_TRANSFER.ID;
                ListExtensions.SetDropdownValue(f.FTCA_CLASSIFICATION.ID, PiListFatcaClass);
                ListExtensions.SetDropdownValue(f.US_TAXID.ID, PiListUsTaxIdType);
                PiTaxNo.Text = f.TAXNO;
                PiListCI.SelectedIndex = (int) f.COUNTRY_INCORP;
                PiListCBO.SelectedIndex = (int)f.COUNTRY_BUSINESS;

                foreach (var i in f.FATCA_DOCUMENTS)
                {
                  // if (i.Name.Trim() != "N/A")
                    PiListFatcaDocumentation.Items.FindByValue(i.ID.ToString()).Selected = true;
                }

                PiSubmitButton.Visible = false;

                //PiResetButton.Visible = false;

            }

        }

        protected void CiListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CiListCountry.SelectedItem.Text.Trim() != "PAKISTAN")
            {
                RequiredFieldValidatorPermanentProvice.Enabled = false;
                RequiredFieldValidatorPermanentCity.Enabled = false;
            }
            else
            {
                RequiredFieldValidatorPermanentProvice.Enabled = true;
                RequiredFieldValidatorPermanentCity.Enabled = true;
            }
        }


        

        
    }
}