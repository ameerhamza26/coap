using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using ExtensionMethods;
using CAOP.UserControls;
using System.IO;
using System.Data;
using BioMetricClasses;
using System.Globalization;

namespace CAOP
{
    public partial class Individual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
                        SetProfileCif();
                        SetBioMetric();
                        
                    }

                }
                else
                {
                    if (!IsPostBack)
                    {
                        Session["BID"] = queryid;
                        SetData();
                        SetDataOpen(queryid);
                        // temperory risk check
                      //  CalculateRisk();

                        CIF cif = new CIF(LoggedUser.USER_ID);

                        if (cif.CheckStatus(queryid, Status.REJECTEBY_COMPLIANCE_MANAGER.ToString()))
                        {
                            rev.Visible = true;
                            rev.Reviewer = false;
                            SetUpdateBtnVisible();
                            SetCifSubmitVisible();
                            CustomValidatorCNIC.Enabled = false;
                            txtCnic.Enabled = false;
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
                        checkIndividualTabCompleted(queryid, mesg);
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
                        checkIndividualTabCompleted(queryid, mesg);
                    }
                }
            }
        }


        private void SetDataOpen(int id)
        {
            SetBiDataOpen(id);
            SetOtherIdentityDataOpen(id);
            SetContactInfoDataOpen(id);
            SetEmployementInformationOpen(id);
            SetMiscellaneousInfoOpen(id);
            SetBankingRelationShipOpen(id);
            SetPersonIdentificationOpen(id);
            String mesg = "null";
            checkIndividualTabCompleted(id, mesg);



        }

        private void checkIndividualTabCompleted(int id, String mesg)
        {
            Identity i = new Identity();
            ContactInfo c = new ContactInfo();
            EmploymentInfo e = new EmploymentInfo();
            MiscellaneousInfo m = new MiscellaneousInfo();
            BankingRelatationship b = new BankingRelatationship();
            Fatca f = new Fatca();

            String iden = null;
            String contact = null;
            String employ = null;
            String misc = null;
            String bank = null;
            String fatca = null;

            if (i.CheckIdentity(id))
            {
                iden = "1";
            }

            if (c.CheckIndividualContactInfo(id))
            {
                contact = "1";
            }

            if (e.CheckIndividualEmploymentInfo(id))
            {
                employ = "1";
            }
            if (m.CheckIndividualMiscInfo(id))
            {
                misc = "1";
            }
            if (b.CheckBankingRelationship(id))
            {
                bank = "1";
            }

            if (f.CheckIndividualFatca(id))
            {
                fatca = "1";
            }


            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "IndividualAfterPendingAlert('" + iden + "','" + contact + "','" + employ + "','" + misc + "','" + bank + "','" + fatca + "','" + mesg + "');", true);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", "showall()", true);

        }

        private void SetBankingRelationShipOpen(int id)
        {
            BankingRelatationship b = new BankingRelatationship();
            if (b.GetBanikingRelationship(id))
            {
                ListExtensions.SetDropdownValue(b.NBP_BRANCH_INFORMATION.ID, BrListBranchInfo);
                BrListAccountType.Items.FindByValue(b.NBP_ACCOUNT_TYPE.ID.ToString()).Selected = true;
                BrAccountNumber.Text = b.NBP_ACCOUNT_NUMBER;
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

        private void SetPersonIdentificationOpen(int id)
        {
            Fatca f = new Fatca();
            if (f.GetFatcaIndividual(id))
            {
                try
                {
                    //if (f.RESIDENT == true)
                    //{
                    //    PiListResident.Items.FindByText("Yes").Selected = true;
                    //}
                    //else
                    //{
                    //    PiListResident.Items.FindByText("No").Selected = true;

                    //}

                    //if (f.CITIZEN == true)
                    //{
                    //    PiListCitizen.Items.FindByText("Yes").Selected = true;
                    //}
                    //else
                    //{

                    //    PiListCitizen.Items.FindByText("No").Selected = true;
                    //}


                    //if (f.BIRTH_USA == true)
                    //{
                    //    PiListCountBirthUsa.Items.FindByText("Yes").Selected = true;

                    //}
                    //else
                    //{
                    //    PiListCountBirthUsa.Items.FindByText("No").Selected = true;

                    //}

                    //if (f.ADDRESS_USA == true)
                    //{
                    //    PiListAddCountUsa.Items.FindByText("Yes").Selected = true;

                    //}
                    //else
                    //{
                    //    PiListAddCountUsa.Items.FindByText("No").Selected = true;

                    //}


                    ListExtensions.SetDropdownValue(f.TYPE_TIN.ID, PiListTinType);
                    PiTxtTin.Text = f.TIN;
                    PiTxtFatcaDocumentDate.Text = f.FATCA_DOCUMENTATION_DATE.ToString(); ;
                    ListExtensions.SetDropdownValue(f.USA_PHONE.ID, PiListPhoneNoUsa);
                    PiContactOffice.Text = f.CONTACT_OFFICE;
                    PiContactResidence.Text = f.CONTACT_RESIDENCE;
                    PiMobileNo.Text = f.MOBNO;
                    PiFaxNo.Text = f.FAXNO;

                    ListExtensions.SetDropdownValue(f.RESIDENCE_CARD.ID, PiListResidenceCard);
                    ListExtensions.SetDropdownValue(f.FUND_TRANSFER.ID, PiListTransferOfFundsUSA);
                    ListExtensions.SetDropdownValue(f.FTCA_CLASSIFICATION.ID, PiListFatcaClass);
                    ListExtensions.SetDropdownValue(f.US_TAXID.ID, PiListUsTaxIdType);
                    PiTaxNo.Text = f.TAXNO;

                    foreach (var i in f.FATCA_DOCUMENTS)
                    {
                       // if (i.Name.Trim() != "N/A")
                            PiListFatcaDocumentation.Items.FindByValue(i.ID.ToString()).Selected = true;
                    }

                    PiSubmitButton.Visible = false;
                }
                catch(Exception err)
                {

                }

                //PiResetButton.Visible = false;

            }

        }
        private void SetMiscellaneousInfoOpen(int id)
        {
            MiscellaneousInfo m = new MiscellaneousInfo();
            if (m.GetIndividualMiscellaneousInfo(id))
            {
                MiListEducation.Items.FindByValue(m.EDUCATION.ID.ToString()).Selected = true;
                MiSocialStatus.Text = m.SOCIAL_STATUS;
                MiListAccomType.Items.FindByValue(m.ACCOMODATION_TYPE.ID.ToString()).Selected = true;
                MiAccomTypeDescr.Text = m.ACCOMODATION_TYPE_DESCRIPTION;
                ListExtensions.SetDropdownValue(m.TRANSPORTATION_TYPE.ID, MiListTransportType);
                if (m.SOURCE_OF_FUND != null)
                ListExtensions.SetDropdownValue(m.SOURCE_OF_FUND, MiListSOF);

                MiBlindVisualRadio1.Checked = false;
                MiBlindVisualRadio2.Checked = false;

                if (m.BLIND_VISUALLY_IMPARIED == false)
                {
                    MiBlindVisualRadio1.Checked = true;
                }
                else
                {
                    MiBlindVisualRadio2.Checked = true;

                }

                ListExtensions.SetDropdownValue(m.PEP.ID, MiListPoliticExposed);

                if (m.PEP_NATURE_SINGLE != null)
                {
                    if ((bool)m.PEP_NATURE_SINGLE)
                    {
                        MiRadPepSingle.Checked = (bool)m.PEP_NATURE_SINGLE;
                    }
                    else
                    {
                        MiRadPepLinked.Checked = true;
                    }

                    MiRadPepSingle.Enabled = true;
                    MiRadPepLinked.Enabled = true;

                    MiPepRelation.Text = m.PEP_RELATIONSHIP;
                    MiPepRelation.Enabled = true;
                    RequiredFieldValidatorMiPepRelation.Enabled = true;

                    MiTxtPED.Text = m.PEP_DESC;
                    MiTxtPED.Enabled = true;
                    RequiredFieldValidatorMiPepDesc.Enabled = true;
                }

                if (m.PARDA_NASHEEN == false)
                {
                    MiPardaRadio1.Checked = true;
                }
                else
                {
                    MiPardaRadio2.Checked = true;
                }

             //   MiListMonthTurnOverDebit.Items.FindByValue(m.MONTHLY_TURNOVER_DEBIT.ID.ToString()).Selected = true;
            //    MiListMonthTurnOverCredit.Items.FindByValue(m.MONTHLY_TURNOVER_CREDIT.ID.ToString()).Selected = true;
            //    MiListAvgNoOfCashDeposits.Items.FindByValue(m.AVERAGE_CASH_DEPOSIT.ID.ToString()).Selected = true;
            //    MiListAvgNoOfNonCashDeposits.Items.FindByValue(m.AVERAGE_CASH_NON_DEPOSIT.ID.ToString()).Selected = true;
                MiTotalAsset.Text = m.TOTAL_ASSET_VALUE;
                MiLiabilities.Text = m.LIABILITIES;
                MiNetWorth.Text = m.NET_WORTH;

                foreach (var i in m.MiscellaneousInfoCountryTax)
                {
                    MiListCountryOfTax.Items.FindByValue(i.ID.ToString()).Selected = true;
                }


                MiSubmitButton.Visible = false;
                //MiResetButton.Visible = false;

            }

        }



        private void SetContactInfoDataOpen(int id)
        {
            ContactInfo c = new ContactInfo();
            if (c.GetIndividualContactInfo(id))
            {
                try
                {
                    CiListCountryCode.Items.FindByValue(c.COUNTRY_CODE.ID.ToString()).Selected = true;
                    CiTxtStreet.Text = c.STREET;
                    CiTxtBuilding.Text = c.BIULDING_SUITE;
                    CiTxtFloor.Text = c.FLOOR;
                    CiTxtDistrict.Text = c.DISTRICT;
                    CiTxtPostBox.Text = c.POST_OFFICE;
                    CiTxtPotalCode.Text = c.POSTAL_CODE;
                    if (c.CITY_PERMANENT.ID != null)
                    {
                        CiListCity.Items.FindByValue(c.CITY_PERMANENT.ID.ToString()).Selected = true;
                        CiListProvince.Items.FindByValue(c.PROVINCE.ID.ToString()).Selected = true;
                    }

                    if (c.CITY_PRESENT.ID != null)
                    {
                        CiListCityPre.Items.FindByValue(c.CITY_PRESENT.ID.ToString()).Selected = true;
                        CiListProvincePre.Items.FindByValue(c.PROVINCE_PRE.ID.ToString()).Selected = true;
                    }
                   

                    CiListCountryCodePre.Items.FindByValue(c.COUNTRY_CODE_PRE.ID.ToString()).Selected = true;
                    CiTxtStreetPre.Text = c.STREET_PRE;
                    CiTxtBuildingPre.Text = c.BIULDING_SUITE_PRE;
                    CiTxtFloorPre.Text = c.FLOOR_PRE;
                    CiTxtDistrictPre.Text = c.DISTRICT_PRE;
                    CiTxtPostBoxPre.Text = c.POST_OFFICE_PRE;
                    CiTxtPotalCodePre.Text = c.POSTAL_CODE_PRE;
                    

                    //  CiPermResdAddInfo.Text = c.ADDRESS_PERMANENT;
                    //  CiListDistrict.Items.FindByValue(c.DISTRICT_PERMANENT.ID.ToString()).Selected = true;
                    //  CiTxtPoBox.Text = ListExtensions.RemoveNull(c.POBOX_PERMANENT);
                    //  CiListCity.Items.FindByValue(c.CITY_PERMANENT.ID.ToString()).Selected = true;
                    //  CiTxtPostalCode.Text = ListExtensions.RemoveNull(c.POSTAL_CODE_PERMANENT);
                    //  CiListCountry.Items.FindByValue(c.COUNTRY_PERMANENT.ID.ToString()).Selected = true;

                    //  CiTxtPresentAddrInfo.Text = c.ADDRESS_PRESENT;
                    //  CiListDistrictPresent.Items.FindByValue(c.DISTRICT_PRESENT.ID.ToString()).Selected = true;
                    //  CiTxtPOBoxPresent.Text = ListExtensions.RemoveNull(c.POBOX_PRESENT);
                    //  CiListCityPresent.Items.FindByValue(c.CITY_PRESENT.ID.ToString()).Selected = true;

                    //  CiTxtPostalCodePresent.Text = ListExtensions.RemoveNull(c.POSTAL_CODE_PRESENT);
                    //  CiListCountryPresent.Items.FindByValue(c.COUNTRY_PRESENT.ID.ToString()).Selected = true;

                    CiContactNoOffice.Text = ListExtensions.RemoveNull(c.OFFICE_CONTACT);
                    CiContactNoResidence.Text = ListExtensions.RemoveNull(c.RESIDENCE_CONTACT);
                    CiMobileNo.Text = ListExtensions.RemoveNull(c.MOBILE_NO);
                    CiFaxNo.Text = ListExtensions.RemoveNull(c.FAX_NO);
                    CiEmail.Text = ListExtensions.RemoveNull(c.EMAIL);

                    CiSubmitButton.Visible = false;
                    //CiResetButton.Visible = false;

               if (CiListCountryCode.SelectedItem.Text == "UNITED STATES" || CiListCountryCodePre.SelectedItem.Text == "UNITED STATES")
               {
                   PiListAddCountUsa.ClearSelection();
                   PiListAddCountUsa.Items.FindByText("Yes").Selected = true;
               }
               else
               {
                      PiListAddCountUsa.ClearSelection();
                      PiListAddCountUsa.Items.FindByText("No").Selected = true;
               }
                }
                catch (Exception e)
                {
 
                }

             

            }
        }
        private void SetEmployementInformationOpen(int id)
        {

            EmploymentInfo e = new EmploymentInfo();
            if (e.GetIndividualEmploymentInfo(id))
            {
                EiListEmployDetail.Items.FindByValue(e.EMPLOYMENT_DETAIL.ID.ToString()).Selected = true;
                txtDescEmpDetail.Text = e.EMPLOYMENT_DETAIL_OTHER_DESCRIPTION;
                EiListConsumer.Items.FindByValue(e.CONSUMER_SEGMENT.ID.ToString()).Selected = true;
                EiDepartment.Text = e.DEPARTMENT;

                EiRadio1.Checked = false;
                EiRadioButton2.Checked = false;
                if (e.RETIRED == false)
                {
                    EiRadio1.Checked = true;
                }
                else
                {
                    EiRadioButton2.Checked = true;
                }
                EiDesignation.Text = e.DESIGNATION;
                EiPFNo.Text = e.PF_NO;
                EiPPONo.Text = e.PPQ_NO;
                ListExtensions.SetDropdownValue(e.EMPLOYER_CODE.ID, EiListEmployerCode);
                EiTxtEmployer.Text = e.EMPLOYER_DESC;
                EiEmpBusAddr.Text = e.EMPLOYER_BUSINESS_ADDRESS;
                ListExtensions.SetDropdownValue(e.COUNTRY_EMPLOYMENT.ID, EiListCountryEmpBus);
                ListExtensions.SetDropdownValue(e.ARMY_RANK_CODE.ID, EiListPakArmy);

                ListExtensions.SetDropdownValue(e.EMPLOYER_GROUP, EiListEmpGrp);
                try
                {
                    if (e.EMPLOYER_GROUP != null && e.EMPLOYER_GROUP != 0)
                    {
                        EmployerSubGroup ESG = new EmployerSubGroup();
                        EmployerGroup EG = new EmployerGroup();

                        EiListEmpSubGrp.DataSource = ESG.GetEmpoyerSubGroup(EG.GetZEMPGRP(Convert.ToInt32(EiListEmpGrp.SelectedItem.Value)));
                        EiListEmpSubGrp.DataValueField = "ID";
                        EiListEmpSubGrp.DataTextField = "Name";
                        EiListEmpSubGrp.DataBind();
                        EiListEmpSubGrp.Items.Insert(0, new ListItem("Select", "0"));
                        ListExtensions.SetDropdownValue(e.EMPLOYER_SUB_GROUP, EiListEmpSubGrp);


                        EmployerCode EC = new EmployerCode();

                        EiListEmpNum.DataSource = EC.GetEmployerCode(ESG.GetZEMPGRP(Convert.ToInt32(EiListEmpSubGrp.SelectedItem.Value)), ESG.GetZEMPSUBG(Convert.ToInt32(EiListEmpSubGrp.SelectedItem.Value)));
                        EiListEmpNum.DataValueField = "ID";
                        EiListEmpNum.DataTextField = "Name";
                        EiListEmpNum.DataBind();
                        EiListEmpNum.Items.Insert(0, new ListItem("Select", "0"));
                        ListExtensions.SetDropdownValue(e.EMPLOYER_NUMBER, EiListEmpNum);
                    }
                  
                }
                catch(Exception excep)
                {

                }
              

                // EiListPakArmy.Items.FindByValue(e.ARMY_RANK_CODE.ID.ToString()).Selected = true;

                EiSubmitButton.Visible = false;
                //EiResetButton.Visible = false;
            }
        }




        private void SetOtherIdentityDataOpen(int id)
        {
            Identity i = new Identity();

            if (i.GetIndividualIdentity(id))
            {
                OitxtCnic.Text = txtCnic.Text;
                OiDateIssue.Text = i.CNIC_DATE_ISSUE;
                OiExpDate.Text = i.EXPIRY_DATE;
                OiTxtIdentMark.Text = ListExtensions.RemoveNull(i.IDENTIFICATION_MARK);
                OiTxtFamilyNo.Text = ListExtensions.RemoveNull(i.FAMILY_NO);
                OiTxtTokenNo.Text = ListExtensions.RemoveNull(i.TOKEN_NO);
                OiTxtTokenIssueDate.Text = i.TOKEN_ISSUE_DATE.ToString("yyyy-MM-dd");
                OiTxtNTN.Text = ListExtensions.RemoveNull(i.NTN);
                OiTxtOldNic.Text = ListExtensions.RemoveNull(i.NIC_OLD);
                ListExtensions.SetDropdownValue(i.IDENTITY_TYPE.ID, OiListIdentType);
                OiTxtIdentNo.Text = ListExtensions.RemoveNull(i.IDENTITY_NO);
                ListExtensions.SetDropdownValue(i.COUNTRY_ISSUE.ID, OiListCountryOfIssue);
                OiDateIssue2.Text = i.OTHER_IDENTITY_ISSUE_DATE;
                OiPlaceOfIssue.Text = ListExtensions.RemoveNull(i.PLACE_ISSUE);
                OiExpDate2.Text = i.OTHER_IDENTITY_EXPIRY_DATE;
                OiTxtPlaceIssueCnic.Text = i.PLACE_ISSUE_CNIC;
                ListExtensions.SetDropdownValue(i.COUNTRY_ISSUE_CNIC.ID, OiListICIssue);
                OiButtonSubmit.Visible = false;
                //OiButtonReset.Visible = false;

            }



        }

        private void SetBiDataOpen(int id)
        {
            BmData.Visible = false;
            BasicInformations b1 = new BasicInformations();
            if (b1.GetIndividual(id))
            {
                lstCifType.Items.FindByValue(b1.CIF_TYPE.ID.ToString()).Selected = true;

                ListExtensions.SetDropdownValue(b1.PRIMARY_DOCUMENT_TYPE.ID, lstPrimaryDocumentType);
                lstPrimaryDocumentType.Enabled = false;

                txtCnic.Text = b1.CNIC;

                lstTitle.Items.FindByValue(b1.TITLE.ID.ToString()).Selected = true;
                //txtName.Text = b1.NAME;
                txtFirstName.Text = b1.FIRST_NAME;
                txtMiddleName.Text = b1.MIDDLE_NAME;
                txtLastName.Text = b1.LAST_NAME;
                lstTitleFather.Items.FindByValue(b1.TITLE_FH.ID.ToString()).Selected = true;
                txtFatherName.Text = b1.NAME_FH;
                txtFatherCnic.Text = ListExtensions.RemoveNull(b1.CNIC_FH);
                txtFatherCif.Text = ListExtensions.RemoveNull(b1.CIF_FH);
                txtMotherName.Text = b1.NAME_MOTHER;
                txtMotherCnic.Text = ListExtensions.RemoveNull(b1.CNIC_MOTHER);
                txtMotherCnicOld.Text = ListExtensions.RemoveNull(b1.CNIC_MOTHER_OLD);

             
                try
                {
                    //  txtDOB.Text = b1.DATE_BIRTH;
                    txtDOB.Text = DateTime.ParseExact(b1.DATE_BIRTH,
                                       "yyyy-MM-dd",
                                        CultureInfo.InvariantCulture).ToString("dd-MM-yyyy");
                }
                catch (Exception abc)
                {
                    txtDOB.Text = b1.DATE_BIRTH;
                }
                txtBithPlace.Text = b1.PLACE_BIRTH;
                lstCOB.ClearSelection();
                lstCOB.Items.FindByValue(b1.Country_Birth.ID.ToString()).Selected = true;
                lstMartialStatus.Items.FindByValue(b1.MARTIAL_STATUS.ID.ToString()).Selected = true;
                lstGender.Items.FindByValue(b1.GENDER.ID.ToString()).Selected = true;
                lstReligion.Items.FindByValue(b1.RELIGION.ID.ToString()).Selected = true;
                ListExtensions.SetDropdownValue(b1.RESIDENT_TYPE.ID, lstResident);
                OitxtCnic.Text = b1.CNIC;
                chkDocument.Checked = b1.DOCUMENT_VERIFIED;

                ListExtensions.SetDropdownValue(b1.CUSTOMER_TYPE.ID, LstCustomerType);

                if (b1.CIF_OFFICER_CODE != null)
                    ListExtensions.SetDropdownValue(b1.CIF_OFFICER_CODE, lstOfficerCode);

                foreach (var n in b1.NATIONALITIES)
                {
                    lstNationality.Items.FindByValue(n.CountryID.ToString()).Selected = true;
                }


                txtIncome.Text = b1.MONTHLY_INCOME;

                lstCOR.Items.FindByText("PAKISTAN").Selected = false;
                ListExtensions.SetDropdownValue(b1.COUNTRY_RESIDENCE.ID, lstCOR);
                ListExtensions.SetDropdownValue(b1.CUSTOMER_DEAL.ID, lstCustomerDeals);
                ///   lstCustomerDeals.Items.FindByValue(b1.CUSTOMER_DEAL.ID.ToString()).Selected = true;
                ///   
                if (lstCOR.SelectedItem.Text.Trim() != "UNITED STATES")
                {
                    PiListResident.ClearSelection();
                    PiListResident.Items.FindByText("No").Selected = true;
                    //   PiListCitizen.ClearSelection();
                    //    PiListCitizen.Items.FindByText("No").Selected = true;
                    //    PiListAddCountUsa.ClearSelection();
                    //    PiListAddCountUsa.Items.FindByText("No").Selected = true;
                }
                else
                {
                    PiListResident.ClearSelection();
                    PiListResident.Items.FindByText("Yes").Selected = true;
                    //     PiListCitizen.ClearSelection();
                    //     PiListCitizen.Items.FindByText("Yes").Selected = true;
                    //     PiListAddCountUsa.ClearSelection();
                    //     PiListAddCountUsa.Items.FindByText("Yes").Selected = true;
                }
                if (lstNationality.Items.Cast<ListItem>().Where(n => n.Selected == true && n.Text.Trim() == "UNITED STATES").Any())
                {
                    PiListCitizen.ClearSelection();
                    PiListCitizen.Items.FindByText("Yes").Selected = true;
                }
                else
                {
                    PiListCitizen.ClearSelection();
                    PiListCitizen.Items.FindByText("No").Selected = true;
                }

                if (lstCOB.SelectedItem.Text.Trim() != "UNITED STATES")
                {
                    PiListCountBirthUsa.ClearSelection();
                    PiListCountBirthUsa.Items.FindByText("No").Selected = true;
                }
                else
                {
                    PiListCountBirthUsa.ClearSelection();
                    PiListCountBirthUsa.Items.FindByText("Yes").Selected = true;
                }

                //NadraInfo nadra = new NadraInfo();                
                //nadra = nadra.GetNadraInfo(txtCnic.Text.Replace("-",String.Empty));

                //if (nadra != null)
                //{
                //    OiExpDate.Text = nadra.EXPIRYDATE;
                //    CiTxtBuilding.Text = nadra.PERMANENT_ADDRESS;
                //    CiTxtBuildingPre.Text = nadra.PRESENT_ADDRESS;

                //}

                btnSubmitBaisc.Visible = false;


            }


            //btnResetBasic.Visible = false;

            //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "hideAll();", true);





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

        public void SetProfileCif()
        {
            string PCIF = Request.QueryString["PROFILECIF"];
            {
                if (PCIF != null)
                {
                    String[] ProfileCif = GetProfileCif(PCIF);

                    // Basic Information
                    lstPrimaryDocumentType.Items.FindByText("CNIC/SNIC").Selected = true;
                    txtCnic.Text = ProfileCif[0];

                    if (lstTitle.Items.FindByText(ProfileCif[7]) != null)
                        lstTitle.Items.FindByText(ProfileCif[7]).Selected = true;

                    txtFirstName.Text = ProfileCif[8];
                    txtMiddleName.Text = ProfileCif[10];
                    txtLastName.Text = ProfileCif[9];
                    txtFatherName.Text = ProfileCif[32];
                    txtMotherName.Text = ProfileCif[33];
                    txtDOB.Text = ProfileCif[12];

                    if (lstMartialStatus.Items.FindByValue(ProfileCif[16].TrimStart('0')) != null)
                        lstMartialStatus.Items.FindByValue(ProfileCif[16].TrimStart('0')).Selected = true;

                    Gender g = new Gender();
                    if (lstGender.Items.FindByText(g.GetTextGender(ProfileCif[15])) != null)
                        lstGender.Items.FindByText(g.GetTextGender(ProfileCif[15])).Selected = true;
                    if (lstReligion.Items.FindByValue(ProfileCif[17].TrimStart('0')) != null)
                        lstReligion.Items.FindByValue(ProfileCif[17].TrimStart('0')).Selected = true;

                    Country c = new Country();
                    if (lstNationality.Items.FindByText(c.GetCountyName(ProfileCif[20])) != null)
                        lstNationality.Items.FindByText(c.GetCountyName(ProfileCif[20])).Selected = true;

                    if (lstCOR.Items.FindByText(c.GetCountyName(ProfileCif[23])) != null)
                        lstCOR.Items.FindByText(c.GetCountyName(ProfileCif[23])).Selected = true;

                    if (lstResident.Items.FindByText(ProfileCif[100]) != null)
                        lstResident.Items.FindByText(ProfileCif[100]).Selected = true;

                    txtIncome.Text = ProfileCif[81];


                    // CNIC Other Identity
                    if (ProfileCif[25].Length > 0)
                    {
                        OiDateIssue.Text = ProfileCif[25];
                    }

                    if (ProfileCif[26].Length > 0)
                    {
                        OiExpDate.Text = ProfileCif[26];
                    }
                    OiTxtFamilyNo.Text = ProfileCif[27];

                    OiTxtNTN.Text = ProfileCif[28];

                    if (OiListIdentType.Items.FindByText(ProfileCif[1]) != null)
                        OiListIdentType.Items.FindByText(ProfileCif[1]).Selected = true;

                    OiTxtIdentNo.Text = ProfileCif[5];

                    // Address / Contact information

                    if (CiListCountryCode.Items.FindByText(c.GetCountyName(ProfileCif[22])) != null)
                        CiListCountryCode.Items.FindByText(c.GetCountyName(ProfileCif[22])).Selected = true;

                    if (CiListProvince.Items.FindByValue(ProfileCif[62].TrimStart('0')) != null)
                        CiListProvince.Items.FindByValue(ProfileCif[62].TrimStart('0')).Selected = true;

                    if (CiListCity.Items.FindByText(ProfileCif[60]) != null)
                        CiListCity.Items.FindByText(ProfileCif[60]).Selected = true;

                    CiTxtBuilding.Text = ProfileCif[56];
                    CiTxtFloor.Text = ProfileCif[57];
                    CiTxtStreet.Text = ProfileCif[58];

                    if (CiListCountryCodePre.Items.FindByText(c.GetCountyName(ProfileCif[55])) != null)
                        CiListCountryCodePre.Items.FindByText(c.GetCountyName(ProfileCif[55])).Selected = true;

                    if (CiListProvincePre.Items.FindByValue(ProfileCif[54]) != null)
                        CiListProvincePre.Items.FindByValue(ProfileCif[54]).Selected = true;

                    if (CiListCityPre.Items.FindByText(ProfileCif[52]) != null)
                        CiListCityPre.Items.FindByText(ProfileCif[52]).Selected = true;

                    CiTxtBuildingPre.Text = ProfileCif[48];
                    CiTxtFloorPre.Text = ProfileCif[49];
                    CiTxtStreetPre.Text = ProfileCif[50];

                    CiContactNoOffice.Text = ProfileCif[64];
                    CiContactNoResidence.Text = ProfileCif[63];
                    CiMobileNo.Text = ProfileCif[66];
                    CiFaxNo.Text = ProfileCif[67];
                    CiEmail.Text = ProfileCif[68];

                    // Employment Information

                    EiEmpBusAddr.Text = ProfileCif[85] + " " + ProfileCif[86] + " " + ProfileCif[87];
                    if (EiListCountryEmpBus.Items.FindByText(ProfileCif[91]) != null)
                        EiListCountryEmpBus.Items.FindByText(ProfileCif[91]).Selected = true;

                    // Miscellaneous
                    if (MiListEducation.Items.FindByValue(ProfileCif[19].TrimStart('0')) != null)
                        MiListEducation.Items.FindByValue(ProfileCif[19].TrimStart('0')).Selected = true;

                    if (ProfileCif[29].Length > 0)
                    {
                        if (ProfileCif[29] == "1")
                            MiPardaRadio2.Checked = true;

                    }

                }
            }
        }

        public string[] GetProfileCif(string PCIF)
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
                XMLDataToString = connector.CIFEnquiryCIFNo("CIF.ACN=" + PCIF);
                sr = new StringReader(XMLDataToString.Replace("&", " and ").Replace("<2", ""));
                ds = new DataSet();
                ds.ReadXml(sr);
            }
            catch (Exception et)
            {
                //WriteActions("Error." + DateTime.Now.ToString() + e.Message);
            }

            var Cif = ds.Tables[0].Rows[0][0].ToString().Split('|');
            return Cif;


        }

        public void SetBioMetric()
        {
          // string NAME = Request.QueryString["NAME"];
           string CNIC = Request.QueryString["CNIC"];
         //  string FNAME = Request.QueryString["FNAME"];
        //   string DOB = Request.QueryString["DOB"];
         //  string PERMANENTADDRESS = Request.QueryString["PERMANENTADDRESS"];
        //   string PRESENTADDRESS = Request.QueryString["PRESENTADDRESS"];
           if (CNIC != null )
           {
               NadraInfo nadra = new NadraInfo();
               nadra = nadra.GetNadraInfo(CNIC);
               string success = "0";
               string Msg = "";

                if (nadra != null)
                {
                    // Succes
                    if (nadra.STATUSCODE == ((int) ResponseCodes.successful).ToString())
                    {
                        success = "1";
                        Msg = "Nadra Information Successfully Integrated";
                        lstPrimaryDocumentType.ClearSelection();
                        lstPrimaryDocumentType.Items.FindByText("CNIC/SNIC").Selected = true;
                        lstPrimaryDocumentType.Enabled = false;

                        txtCnic.Text = nadra.CNIC;
                        txtCnic.Enabled = false;

                        //txtFirstName.Text = nadra.NAME;
                        //txtFatherName.Text = nadra.FH_NAME;
                        //CiTxtBuilding.Text = nadra.PERMANENT_ADDRESS;
                        //CiTxtBuildingPre.Text = nadra.PRESENT_ADDRESS;
                        //txtDOB.Text = nadra.DOB;
                        //txtBithPlace.Text = nadra.BIRTH_PLACE;
                        //OiExpDate.Text = nadra.EXPIRYDATE;

                        BmData.Visible = false;

                        
                    }
                        // Nadra Error
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.citizen_number_is_not_verified).ToString())
                        Msg = "Nadra Error, Citizen Number is not Verified ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.finger_print_doesnot_exist_in_citizen_database).ToString())
                        Msg = "Nadra Error, Finger Print Doesnot Exist in Citizen Database ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.error_generating_session_id).ToString())
                        Msg = "Nadra Error, Error Generating Session id ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.invalid_verification_reference_number).ToString())
                        Msg = "Nadra Error, Invalid Verification Reference Number ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.invalid_service_provide_transaction_id).ToString())
                        Msg = "Nadra Error, Invalid Service Provide Transaction id ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.finger_verification_has_been_exhausted_for_current_finger).ToString())
                        Msg = "Nadra Error, Finger Verification Has Been Exhausted For Current Finger ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.verification_limit_for_current_citizen_number_has_been_exhausted).ToString())
                        Msg = "Nadra Error, Verification Limit For Current Citizen Number Has Been Exhausted ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.invalid_input_finger_template).ToString())
                        Msg = "Nadra Error, Invalid Input Finger Template ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.invalid_finger_index).ToString())
                        Msg = "Nadra Error, Invalid Finger Index ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.finger_print_doesnot_match).ToString())
                        Msg = "Nadra Error, Finger Print Doesnot Match ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.invalid_finger_template_type).ToString())
                        Msg = "Nadra Error, Invalid Finger Template Type ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.this_operation_will_only_be_enabled_if_biometric_verification_of_all_available_finger_is_failed).ToString())
                        Msg = "Nadra Error, Biometric Verification of all available fingers failed ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.contact_number_is_not_valid).ToString())
                        Msg = "Nadra Error, Contact Number is not Valid ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.transaction_id_already_exist).ToString())
                        Msg = "Nadra Error, Transaction id Already Exist ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.invalid_area_name).ToString())
                        Msg = "Nadra Error, Invalid Area Name ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.invalid_account_type).ToString())
                        Msg = "Nadra Error, Invalid Account Type ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.no_request_found_against_citizen_number_or_transaction_id).ToString())
                        Msg = "Nadra Error, No Request Found Against Citizen Number or Transaction_id ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.last_verification_was_not_successful).ToString())
                        Msg = "Nadra Error, Last Verification Was Not Successful ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.invalid_xml).ToString())
                        Msg = "Nadra Error, Invalid xml ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.invalid_username_or_password).ToString())
                        Msg = "Nadra Error, Invalid Username or Password ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.invalid_session_id).ToString())
                        Msg = "Nadra Error, Invalid Session id ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.sesssion_has_been_expired).ToString())
                        Msg = "Nadra Error, Sesssion Has Been Expired ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.verification_was_successful_therefore_session_has_been_expired).ToString())
                        Msg = "Nadra Error, Verification Was Successful Therefore Session Has Been Expired ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.user_do_not_have_access_to_this_functionality).ToString())
                        Msg = "Nadra Error, User Do Not Have Access to this_Functionality ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.invalid_franchise_id).ToString())
                        Msg = "Nadra Error, Invalid Franchise id ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.invalid_citizen_number).ToString())
                        Msg = "Nadra Error, Invalid Citizen Number ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.citizen_verification_service_is_down_Please_try_again_later).ToString())
                        Msg = "Nadra Error, Citizen Verification Service is Down Please Try Again Later ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.Exception_system_has_encounter_an_unexpected_error_Administrator_has_been_informed_please_try_again_later).ToString())
                        Msg = "Nadra Error, System Exception has Encountered ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.daily_verification_limit_has_been_exhausted_for_testing).ToString())
                        Msg = "Nadra Error, Daily Verification limit has been Exhausted for testing ";

                      //  RBTS_NADRA Service Error
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.CNIC_not_valid).ToString())
                        Msg = "RBTS NADRA Service Error, CNIC Not Valid ";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.invalid_data).ToString())
                        Msg = "RBTS NADRA Service Error, Invalid Data";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.invalidDevice).ToString())
                        Msg = "RBTS NADRA Service Error, Invalid Device";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.invalidDate).ToString())
                        Msg = "RBTS NADRA Service Error, Invalid Date";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.ErrorConnectingNadra).ToString())
                        Msg = "RBTS NADRA Service Error, Error Connecting Nadra";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.Exception_system_has_encounter_an_unexpected_error).ToString())
                        Msg = "RBTS NADRA Service Error, Exception System has encounter an unexpected error";

                    //BioMetric Device
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.Branch_Code_doesnot_exists).ToString())
                        Msg = "BioMetric Device Error, Branch Code Does not Exists";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.Invalid_Input_parameters).ToString())
                        Msg = "BioMetric Device Error, Invalid Input Parameters";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.Device_Serial_doesn0t_exists).ToString())
                        Msg = "BioMetric Device Error, Device Serial Does n't Exists";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.Timed_out).ToString())
                        Msg = "BioMetric Device Error, TimeOut";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.Device_is_Offline).ToString())
                        Msg = "BioMetric Device Error, Device is Offline";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.Invalid_Transaction_ID).ToString())
                        Msg = "BioMetric Device Error, Invalid Transaction ID";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.Operation_in_process).ToString())
                        Msg = "BioMetric Device Error, Operation in Process";


                        //	PORTAL Connecting Device AND NADRA
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.BioMetricDeviceNotConnecting).ToString())
                        Msg = "Portal Error, Bio Metric Device Not Connecting";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.SystemError).ToString())
                        Msg = "Portal Error, System Error";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.BioMetricDeviceResponseIsInvalid).ToString())
                        Msg = "Portal Error, Bio Metric Device Is Invalid";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.NADRA_RBTS_Service_Not_Connecting).ToString())
                        Msg = "Portal Error, Nadra RBTS Service Not Connecting";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.InvalidDataAos).ToString())
                        Msg = "Portal Error, Invalid Data";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.AosDbError).ToString())
                        Msg = "Portal Error, Database Error";
                    else if (nadra.STATUSCODE == ((int)ResponseCodes.NoDeviceRegisteredInBranch).ToString())
                        Msg = "Portal Error, No Device Registered in Branch";
                    else
                        Msg = "Nadra Intergration Failed, Error Code " + nadra.STATUSCODE;

                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "BioMetric('" + success + "','" + Msg + "');", true);

                }




               

           }
        }
       

        private void SetData()
        {


            SetBIData();
            SetOtherIdentityData();
            SetContactInfoData();
            SetEmployementInformation();
            SetMiscellaneousInfo();
            SetBankingRelationShip();
            SetPersonIdentification();

        }


        private void SetPersonIdentification()
        {
            var arr = new List<string>
            {
                "No",
                "Yes"
                
            };

          //  PiTxtFatcaDocumentDate.Text = DateTime.Now.ToString("yyyy-MM-dd"); ;
            PiListResident.DataSource = arr;
            PiListResident.DataBind();

            PiListCitizen.DataSource = arr;
            PiListCitizen.DataBind();

            PiListCountBirthUsa.DataSource = arr;
            PiListCountBirthUsa.DataBind();
            PiListAddCountUsa.DataSource = arr;
            PiListAddCountUsa.DataBind();

            UsaPhone up = new UsaPhone();
            PiListPhoneNoUsa.DataSource = up.GetUsaPhone();
            PiListPhoneNoUsa.DataValueField = "ID";
            PiListPhoneNoUsa.DataTextField = "Name";
            PiListPhoneNoUsa.DataBind();
            PiListPhoneNoUsa.Items.Insert(0, new ListItem("Select", "0"));

            UsaResidenceCard rc = new UsaResidenceCard();

            PiListResidenceCard.DataSource = rc.GetUsaResidenceCards();
            PiListResidenceCard.DataValueField = "ID";
            PiListResidenceCard.DataTextField = "Name";
            PiListResidenceCard.DataBind();
            PiListResidenceCard.Items.Insert(0, new ListItem("Select", "0"));

            UsaFund f = new UsaFund();
            PiListTransferOfFundsUSA.DataSource = f.GetUsaFund();
            PiListTransferOfFundsUSA.DataValueField = "ID";
            PiListTransferOfFundsUSA.DataTextField = "Name";
            PiListTransferOfFundsUSA.DataBind();
            PiListTransferOfFundsUSA.Items.Insert(0, new ListItem("Select", "0"));

            FatcaClassification fc = new FatcaClassification();
            PiListFatcaClass.DataSource = fc.GetFatcaClassification();
            PiListFatcaClass.DataValueField = "ID";
            PiListFatcaClass.DataTextField = "Name";
            PiListFatcaClass.DataBind();
            PiListFatcaClass.Items.Insert(0, new ListItem("Select", "0"));

            UsaTaxType t = new UsaTaxType();
            PiListUsTaxIdType.DataSource = t.GetUsaTaxType();
            PiListUsTaxIdType.DataValueField = "ID";
            PiListUsTaxIdType.DataTextField = "Name";
            PiListUsTaxIdType.DataBind();
            PiListUsTaxIdType.Items.Insert(0, new ListItem("Select", "0"));

            FatcaDocumentation fd = new FatcaDocumentation();

            PiListFatcaDocumentation.DataSource = fd.GetFatcaDocumentation();
            PiListFatcaDocumentation.DataValueField = "ID";
            PiListFatcaDocumentation.DataTextField = "Name";
            PiListFatcaDocumentation.DataBind();

            FatcasTin FT = new FatcasTin();
            PiListTinType.DataSource = FT.GetFatcasTin();
            PiListTinType.DataValueField = "ID";
            PiListTinType.DataTextField = "Name";
            PiListTinType.DataBind();
            PiListTinType.Items.Insert(0, new ListItem("Select", "0"));

        }

        private void SetBankingRelationShip()
        {
            NbpBranchInformation br = new NbpBranchInformation();
            AccountType ac = new AccountType();
            OtherBankCodes bc = new OtherBankCodes();

            BrListBranchInfo.DataSource = br.GetNbpBranchInformation();
            BrListBranchInfo.DataValueField = "ID";
            BrListBranchInfo.DataTextField = "Name";
            BrListBranchInfo.DataBind();
            BrListBranchInfo.Items.Insert(0, new ListItem("Select", "0"));

            BrListAccountType.DataSource = ac.GetAccountTypes();
            BrListAccountType.DataValueField = "ID";
            BrListAccountType.DataTextField = "Name";
            BrListAccountType.DataBind();
            BrListAccountType.Items.Insert(0, new ListItem("Select", "0"));

            BrListOtherBranchCode.DataSource = bc.GetOtherBankCodes();
            BrListOtherBranchCode.DataValueField = "ID";
            BrListOtherBranchCode.DataTextField = "Name";
            BrListOtherBranchCode.DataBind();
            BrListOtherBranchCode.Items.Insert(0, new ListItem("Select", "0"));

        }
        private void SetMiscellaneousInfo()
        {
            Education edu = new Education();
            AccomodationTypes ac = new AccomodationTypes();
            TransportationType tr = new TransportationType();
            Pep pp = new Pep();

            MonthlyTurnOverDebit md = new MonthlyTurnOverDebit();
            MonthlyTurnOverCredit mc = new MonthlyTurnOverCredit();
            AverageCashDeposit ad = new AverageCashDeposit();
            AverageNonCashDeposit an = new AverageNonCashDeposit();
            SourceOfFunds sof = new SourceOfFunds();

            Country c1 = new Country();

            MiListEducation.DataSource = edu.GetEducation();
            MiListEducation.DataValueField = "ID";
            MiListEducation.DataTextField = "Name";
            MiListEducation.DataBind();
            MiListEducation.Items.Insert(0, new ListItem("Select", "0"));

            MiListAccomType.DataSource = ac.GetAccomodationTypes();
            MiListAccomType.DataValueField = "ID";
            MiListAccomType.DataTextField = "Name";
            MiListAccomType.DataBind();
            MiListAccomType.Items.Insert(0, new ListItem("Select", "0"));

            MiListTransportType.DataSource = tr.GetTransportationTypes();
            MiListTransportType.DataValueField = "ID";
            MiListTransportType.DataTextField = "Name";
            MiListTransportType.DataBind();
            MiListTransportType.Items.Insert(0, new ListItem("Select", "0"));

            MiListPoliticExposed.DataSource = pp.GetPeps();
            MiListPoliticExposed.DataValueField = "ID";
            MiListPoliticExposed.DataTextField = "Name";
            MiListPoliticExposed.DataBind();
            MiListPoliticExposed.Items.Insert(0, new ListItem("Select", "0"));

            MiListMonthTurnOverDebit.DataSource = md.GetMonthlyTurnOverDebit();
            MiListMonthTurnOverDebit.DataValueField = "ID";
            MiListMonthTurnOverDebit.DataTextField = "Name";
            MiListMonthTurnOverDebit.DataBind();
            MiListMonthTurnOverDebit.Items.Insert(0, new ListItem("Select", "0"));

            MiListMonthTurnOverCredit.DataSource = mc.GetMonthlyTurnOverCredit();
            MiListMonthTurnOverCredit.DataValueField = "ID";
            MiListMonthTurnOverCredit.DataTextField = "Name";
            MiListMonthTurnOverCredit.DataBind();
            MiListMonthTurnOverCredit.Items.Insert(0, new ListItem("Select", "0"));

            MiListAvgNoOfCashDeposits.DataSource = ad.GetAverageCashDeposits();
            MiListAvgNoOfCashDeposits.DataValueField = "ID";
            MiListAvgNoOfCashDeposits.DataTextField = "Name";
            MiListAvgNoOfCashDeposits.DataBind();
            MiListAvgNoOfCashDeposits.Items.Insert(0, new ListItem("Select", "0"));

            MiListAvgNoOfNonCashDeposits.DataSource = an.GetAverageNonCashDeposit();
            MiListAvgNoOfNonCashDeposits.DataValueField = "ID";
            MiListAvgNoOfNonCashDeposits.DataTextField = "Name";
            MiListAvgNoOfNonCashDeposits.DataBind();
            MiListAvgNoOfNonCashDeposits.Items.Insert(0, new ListItem("Select", "0"));

            MiListCountryOfTax.DataSource = c1.GetCountries();
            MiListCountryOfTax.DataValueField = "ID";
            MiListCountryOfTax.DataTextField = "Name";
            MiListCountryOfTax.DataBind();

            MiListSOF.DataSource = sof.GetSouceOfFund();
            MiListSOF.DataValueField = "ID";
            MiListSOF.DataTextField = "NAME";
            MiListSOF.DataBind();
            MiListSOF.Items.Insert(0, new ListItem("Select", "0"));

        }

        private void SetEmployementInformation()
        {
            EmploymentDetail ed = new EmploymentDetail();
            EmployerCodes ec = new EmployerCodes();
            Country c1 = new Country();
            ArmyRankCodes ar = new ArmyRankCodes();
            ConsumerSegment cg = new ConsumerSegment();
            EmployerGroup EG = new EmployerGroup();


            EiListEmployDetail.DataSource = ed.GetEmploymentDetail();
            EiListEmployDetail.DataValueField = "ID";
            EiListEmployDetail.DataTextField = "Name";
            EiListEmployDetail.DataBind();
            EiListEmployDetail.Items.Insert(0, new ListItem("Select", "0"));

            EiListEmployerCode.DataSource = ec.GetEmployerCode();
            EiListEmployerCode.DataValueField = "ID";
            EiListEmployerCode.DataTextField = "Code";
            EiListEmployerCode.DataBind();
            EiListEmployerCode.Items.Insert(0, new ListItem("Select", "0"));

            EiListCountryEmpBus.DataSource = c1.GetCountries();
            EiListCountryEmpBus.DataValueField = "ID";
            EiListCountryEmpBus.DataTextField = "Name";
            EiListCountryEmpBus.DataBind();
            EiListCountryEmpBus.Items.Insert(0, new ListItem("Select", "0"));

            EiListPakArmy.DataSource = ar.GetArmyCodes();
            EiListPakArmy.DataValueField = "ID";
            EiListPakArmy.DataTextField = "Code";
            EiListPakArmy.DataBind();
            EiListPakArmy.Items.Insert(0, new ListItem("Select", "0"));

            EiListConsumer.DataSource = cg.GetConsumerSegmentTypes();
            EiListConsumer.DataValueField = "ID";
            EiListConsumer.DataTextField = "Name";
            EiListConsumer.DataBind();
            EiListConsumer.Items.Insert(0, new ListItem("Select", "0"));

            EiListEmpGrp.DataSource = EG.GetEmpoyerGroup();
            EiListEmpGrp.DataValueField = "ID";
            EiListEmpGrp.DataTextField = "Name";
            EiListEmpGrp.DataBind();
            EiListEmpGrp.Items.Insert(0, new ListItem("Select", "0"));

            EiListEmpSubGrp.Items.Insert(0, new ListItem("Select", "0"));
            EiListEmpNum.Items.Insert(0, new ListItem("Select", "0"));
        }

        private void SetContactInfoData()
        {
            District d1 = new District();
            City c1 = new City();
            Country country = new Country();
            CountryCode countryCode = new CountryCode();
            Province p = new Province();

            //CiListDistrict.DataSource = d1.GetDistrictList();
            //CiListDistrict.DataValueField = "ID";
            //CiListDistrict.DataTextField = "Name";
            //CiListDistrict.DataBind();

            //CiListDistrictPresent.DataSource = d1.GetDistrictList();
            //CiListDistrictPresent.DataValueField = "ID";
            //CiListDistrictPresent.DataTextField = "Name";
            //CiListDistrictPresent.DataBind();

            CiListProvince.DataSource = p.GetProvinces();
            CiListProvince.DataValueField = "ID";
            CiListProvince.DataTextField = "Name";
            CiListProvince.DataBind();
            CiListProvince.Items.Insert(0, new ListItem("Select", "0"));

            CiListProvincePre.DataSource = p.GetProvinces();
            CiListProvincePre.DataValueField = "ID";
            CiListProvincePre.DataTextField = "Name";
            CiListProvincePre.DataBind();
            CiListProvincePre.Items.Insert(0, new ListItem("Select", "0"));

            CiListCity.DataSource = c1.GetCifTypes();
            CiListCity.DataValueField = "ID";
            CiListCity.DataTextField = "Name";
            CiListCity.DataBind();
            CiListCity.Items.Insert(0, new ListItem("Select", "0"));

            CiListCountryCode.DataSource = country.GetCountries();
            CiListCountryCode.DataValueField = "ID";
            CiListCountryCode.DataTextField = "Name";
            CiListCountryCode.DataBind();
            CiListCountryCode.Items.Insert(0, new ListItem("Select", "0"));

            CiListCountryCodePre.DataSource = country.GetCountries();
            CiListCountryCodePre.DataValueField = "ID";
            CiListCountryCodePre.DataTextField = "Name";
            CiListCountryCodePre.DataBind();
            CiListCountryCodePre.Items.Insert(0, new ListItem("Select", "0"));

            CiListCityPre.DataSource = c1.GetCifTypes();
            CiListCityPre.DataValueField = "ID";
            CiListCityPre.DataTextField = "Name";
            CiListCityPre.DataBind();
            CiListCityPre.Items.Insert(0, new ListItem("Select", "0"));

            //CiListCountry.DataSource = country.GetCountries();
            //CiListCountry.DataValueField = "ID";
            //CiListCountry.DataTextField = "Name";
            //CiListCountry.DataBind();

            //CiListCountryPresent.DataSource = country.GetCountries();
            //CiListCountryPresent.DataValueField = "ID";
            //CiListCountryPresent.DataTextField = "Name";
            //CiListCountryPresent.DataBind();





        }
        private void SetOtherIdentityData()
        {
            IdentityType identityType = new IdentityType();
            Country country = new Country();
            City city = new City();


            OiListCountryOfIssue.DataSource = country.GetCountries();
            OiListCountryOfIssue.DataValueField = "ID";
            OiListCountryOfIssue.DataTextField = "Name";
            OiListCountryOfIssue.DataBind();
            OiListCountryOfIssue.Items.Insert(0, new ListItem("Select", "0"));

            OiListIdentType.DataSource = identityType.GetIdentityTypes();
            OiListIdentType.DataValueField = "ID";
            OiListIdentType.DataTextField = "Name";
            OiListIdentType.DataBind();
            OiListIdentType.Items.Insert(0, new ListItem("Select", "0"));

            OiListICIssue.DataSource = country.GetCountries();
            OiListICIssue.DataValueField = "ID";
            OiListICIssue.DataTextField = "Name";
            OiListICIssue.DataBind();
            OiListICIssue.Items.Insert(0, new ListItem("Select", "0"));

           // OiDateIssue.Text = DateTime.Now.ToString("yyyy-MM-dd");
           // OiExpDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            OiTxtTokenIssueDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
           // OiDateIssue2.Text = DateTime.Now.ToString("yyyy-MM-dd");
          //  OiExpDate2.Text = DateTime.Now.ToString("yyyy-MM-dd");



        }
        private void SetBIData()
        {
            CifTypes CifType = new CifTypes();
            PrimaryDocumentType PrimaryDocumentTypes = new PrimaryDocumentType();
            Title Title = new Title();
            Country Country = new Country();
            MartialStatus MartialStatus = new MartialStatus();
            Gender Gender = new Gender();
            Religion Religion = new Religion();
            ResidentType ResidentType = new ResidentType();
            CustomerDeal CustomerDeal = new CustomerDeal();
            CifCustomerType cct = new CifCustomerType();
            OfficerCodes OC = new OfficerCodes();
            
            BmData.Visible = true;
          //  txtDOB.Text = DateTime.Now.ToString("yyyy-MM-dd");
           // txtDOB.Text = "";

            lstCifType.DataSource = CifType.GetCifTypes();
            lstCifType.DataValueField = "ID";
            lstCifType.DataTextField = "Name";
            lstCifType.DataBind();

            lstPrimaryDocumentType.DataSource = PrimaryDocumentTypes.GetPrimaryDocumentTypes();
            lstPrimaryDocumentType.DataValueField = "ID";
            lstPrimaryDocumentType.DataTextField = "Name";
            lstPrimaryDocumentType.DataBind();
            lstPrimaryDocumentType.Items.Insert(0, new ListItem("Select", "0"));

            lstTitle.DataSource = Title.GetTitles();
            lstTitle.DataValueField = "ID";
            lstTitle.DataTextField = "Name";
            lstTitle.DataBind();
            lstTitle.Items.Insert(0, new ListItem("Select", "0"));

            lstTitleFather.DataSource = Title.GetTitles();
            lstTitleFather.DataValueField = "ID";
            lstTitleFather.DataTextField = "Name";
            lstTitleFather.DataBind();
            lstTitleFather.Items.Insert(0, new ListItem("Select", "0"));

            lstCOB.DataSource = Country.GetCountries();
            lstCOB.DataValueField = "ID";
            lstCOB.DataTextField = "Name";
            lstCOB.DataBind();
            lstCOB.Items.Insert(0, new ListItem("Select", "0"));
            lstCOB.ClearSelection();
            lstCOB.Items.FindByText("PAKISTAN").Selected = true;

            lstMartialStatus.DataSource = MartialStatus.GetMartialStatus();
            lstMartialStatus.DataValueField = "ID";
            lstMartialStatus.DataTextField = "Name";
            lstMartialStatus.DataBind();

            lstGender.DataSource = Gender.GetGenders();
            lstGender.DataValueField = "ID";
            lstGender.DataTextField = "Name";
            lstGender.DataBind();
            lstGender.Items.Insert(0, new ListItem("Select", "0"));

            lstReligion.DataSource = Religion.GetReligions();
            lstReligion.DataValueField = "ID";
            lstReligion.DataTextField = "Name";
            lstReligion.DataBind();
            lstReligion.Items.Insert(0, new ListItem("Select", "0"));

            lstResident.DataSource = ResidentType.GetResidentTypes();
            lstResident.DataValueField = "ID";
            lstResident.DataTextField = "Name";
            lstResident.DataBind();
            lstResident.Items.Insert(0, new ListItem("Select", "0"));

            lstNationality.DataSource = Country.GetCountries();
            lstNationality.DataValueField = "ID";
            lstNationality.DataTextField = "Name";
            lstNationality.DataBind();

            lstCOR.DataSource = Country.GetCountries();
            lstCOR.DataValueField = "ID";
            lstCOR.DataTextField = "Name";
            lstCOR.DataBind();
            lstCOR.Items.Insert(0, new ListItem("Select", "0"));
            lstCOR.Items.FindByText("PAKISTAN").Selected = true;

            lstCustomerDeals.DataSource = CustomerDeal.GetCustomerDeals();
            lstCustomerDeals.DataValueField = "ID";
            lstCustomerDeals.DataTextField = "Name";
            lstCustomerDeals.DataBind();
            lstCustomerDeals.Items.Insert(0, new ListItem("Select", "0"));

            LstCustomerType.DataSource = cct.GetCustomerType();
            LstCustomerType.DataValueField = "ID";
            LstCustomerType.DataTextField = "Name";
            LstCustomerType.DataBind();
            LstCustomerType.Items.Insert(0, new ListItem("Select", "0"));

            lstOfficerCode.DataSource = OC.GetOfficerCodes();
            lstOfficerCode.DataValueField = "ID";
            lstOfficerCode.DataTextField = "Name";
            lstOfficerCode.DataBind();
            lstOfficerCode.Items.Insert(0, new ListItem("Select", "0"));
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

        protected void btnSubmitBaisc_Click(object sender, EventArgs e)
        {
            if (lstPrimaryDocumentType.SelectedItem.Value == "1" || lstPrimaryDocumentType.SelectedItem.Value == "2")
            {
                if (Request.QueryString["PROFILECIF"] == null)
                {
                    if (IsCustomerExistsInProfile(txtCnic.Text))
                    {
                        CustomValidatorCNIC.ErrorMessage = "CIF Already Exists With this CNIC in Profile. Proceed to Incorporate Cif";
                        return;
                    }
                }
            }

            BasicInformations b = new BasicInformations();

            if (lstPrimaryDocumentType.SelectedItem.Text == "Passport")
            {
                //args.IsValid = true;
            }
            else if (b.IsCnicExists(txtCnic.Text))
            {
                CustomValidatorCNIC.ErrorMessage = "CIF Already Exists With this CNIC. Proceed to Account Creation";

                if (b.IsCnicExistsPArtially(txtCnic.Text))
                    CustomValidatorCNIC.ErrorMessage = "CIF already created partially, Please complete it first";

                return;
            }

            if (Page.IsValid)
            {
                InsertBasicInformation();
               
                String mesg = "Basic Information has been saved";
                int id = Convert.ToInt32(Session["BID"]);
                checkIndividualTabCompleted(id, mesg);
                OitxtCnic.Text = txtCnic.Text;
                btnSubmitBaisc.Visible = false;
                //btnResetBasic.Visible = false;
            }

           

        }

        private void InsertBasicInformation()
        {
            BasicInformations BI = new BasicInformations();
            BI.CIF_ENTRY_DATE = DateTime.Now;
            BI.PRIMARY_DOCUMENT_TYPE = new PrimaryDocumentType { ID = ListExtensions.getSelectedValue(lstPrimaryDocumentType), Name = lstPrimaryDocumentType.SelectedItem.Text };
            BI.CIF_TYPE = new CifTypes() { ID = Convert.ToInt32(lstCifType.SelectedItem.Value), Name = lstCifType.SelectedItem.Text };
            BI.CNIC = txtCnic.Text;
            BI.TITLE = new Title() { ID = Convert.ToInt32(lstTitle.SelectedItem.Value), Name = lstTitle.SelectedItem.Text };
          //  BI.NAME = txtName.Text;
            BI.FIRST_NAME = txtFirstName.Text;
            BI.MIDDLE_NAME = txtMiddleName.Text;
            BI.LAST_NAME = txtLastName.Text;
            BI.TITLE_FH = new Title() { ID = Convert.ToInt32(lstTitleFather.SelectedItem.Value), Name = lstTitleFather.SelectedItem.Text };
            BI.NAME_FH = txtFatherName.Text;
            BI.CNIC_FH = txtFatherCnic.Text;
            BI.CIF_FH = txtFatherCif.Text;
            BI.NAME_MOTHER = txtMotherName.Text;
            BI.CNIC_MOTHER = txtMotherCnic.Text;
            BI.CNIC_MOTHER_OLD = txtMotherCnicOld.Text;
            BI.DATE_BIRTH = DateTime.ParseExact(txtDOB.Text,
                                   "dd-MM-yyyy",
                                    CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            BI.PLACE_BIRTH = txtBithPlace.Text;
            BI.Country_Birth = new Country { ID = Convert.ToInt32(lstCOB.SelectedItem.Value), Name = lstCOB.SelectedItem.Text };
            BI.MARTIAL_STATUS = new MartialStatus { ID = Convert.ToInt32(lstMartialStatus.SelectedItem.Value), Name = lstMartialStatus.SelectedItem.Text };
            BI.GENDER = new Gender { ID = Convert.ToInt32(lstGender.SelectedItem.Value), Name = lstGender.SelectedItem.Text };
            BI.RELIGION = new Religion { ID = Convert.ToInt32(lstReligion.SelectedItem.Value), Name = lstReligion.SelectedItem.Text };
            BI.RESIDENT_TYPE = new ResidentType { ID = Convert.ToInt32(lstResident.SelectedItem.Value), Name = lstResident.SelectedItem.Text };
            BI.NATIONALITIES = lstNationality.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => new Nationality { CountryID = Convert.ToInt32(i.Value), Country = i.Text }).ToList();
            BI.MONTHLY_INCOME = txtIncome.Text;
            BI.COUNTRY_RESIDENCE = new Country { ID = ListExtensions.getSelectedValue(lstCOR), Name = lstCOR.SelectedItem.Text };
            BI.CUSTOMER_DEAL = new CustomerDeal { ID = Convert.ToInt32(lstCustomerDeals.SelectedItem.Value), Name = lstCustomerDeals.SelectedItem.Text };
            BI.DOCUMENT_VERIFIED = chkDocument.Checked;
            if (Request.QueryString["PROFILECIF"] != null)
                BI.PROFILE_CIF_NO = Request.QueryString["PROFILECIF"];
            else
                BI.PROFILE_CIF_NO = "";

            User LogedUser = Session["User"] as User;
            BI.UserId = LogedUser.USER_ID;
            BI.BRANCH_CODE = LogedUser.Branch.BRANCH_CODE;
            BI.CUSTOMER_TYPE = new CifCustomerType() { ID = Convert.ToInt32(LstCustomerType.SelectedItem.Value) };
            BI.CIF_OFFICER_CODE = Convert.ToInt32(lstOfficerCode.SelectedItem.Value);

            Session["BID"] = BI.SaveIndividual();
            if (lstCOR.SelectedItem.Text.Trim() != "UNITED STATES")
            {
                PiListResident.ClearSelection();
                PiListResident.Items.FindByText("No").Selected = true;
             //   PiListCitizen.ClearSelection();
            //    PiListCitizen.Items.FindByText("No").Selected = true;
            //    PiListAddCountUsa.ClearSelection();
            //    PiListAddCountUsa.Items.FindByText("No").Selected = true;
            }
            else
            {
                PiListResident.ClearSelection();
                PiListResident.Items.FindByText("Yes").Selected = true;
           //     PiListCitizen.ClearSelection();
           //     PiListCitizen.Items.FindByText("Yes").Selected = true;
           //     PiListAddCountUsa.ClearSelection();
           //     PiListAddCountUsa.Items.FindByText("Yes").Selected = true;
            }
            if (lstNationality.Items.Cast<ListItem>().Where(n => n.Selected == true && n.Text.Trim() == "UNITED STATES").Any())
            {
                     PiListCitizen.ClearSelection();
                     PiListCitizen.Items.FindByText("Yes").Selected = true;
            }
            else
            {
                PiListCitizen.ClearSelection();
                PiListCitizen.Items.FindByText("No").Selected = true;
            }

            if (lstCOB.SelectedItem.Text.Trim() != "UNITED STATES")
            {
                PiListCountBirthUsa.ClearSelection();
                PiListCountBirthUsa.Items.FindByText("No").Selected = true;
            }
            else
            {
                PiListCountBirthUsa.ClearSelection();
                PiListCountBirthUsa.Items.FindByText("Yes").Selected = true;
            }

        }

        protected void OiButtonSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertOtherIdentities();


                String mesg = "CNIC/Other Identity has been saved";
                int id = Convert.ToInt32(Session["BID"]);

                checkIndividualTabCompleted(id, mesg);


                OiButtonSubmit.Visible = false;
                //OiButtonReset.Visible = false;
            }

            
        }

        private void InsertOtherIdentities()
        {
            Identity I1 = new Identity();
            I1.BI_ID = Convert.ToInt32(Session["BID"]);
            I1.CNIC_DATE_ISSUE = OiDateIssue.Text;
            I1.EXPIRY_DATE = OiExpDate.Text;
            I1.IDENTIFICATION_MARK = OiTxtIdentMark.Text;
            I1.FAMILY_NO = OiTxtFamilyNo.Text;
            I1.TOKEN_NO = OiTxtTokenNo.Text;
            I1.TOKEN_ISSUE_DATE = Convert.ToDateTime(OiTxtTokenIssueDate.Text);
            I1.NTN = OiTxtNTN.Text;
            I1.NIC_OLD = OiTxtOldNic.Text;
            I1.IDENTITY_TYPE = new IdentityType() { ID = ListExtensions.getSelectedValue(OiListIdentType), Name = OiListIdentType.SelectedItem.Text };
            I1.IDENTITY_NO = OiTxtIdentNo.Text;
            I1.COUNTRY_ISSUE = new Country() { ID = ListExtensions.getSelectedValue(OiListCountryOfIssue), Name = OiListCountryOfIssue.SelectedItem.Value };
            I1.OTHER_IDENTITY_ISSUE_DATE = OiDateIssue2.Text;
            I1.PLACE_ISSUE = OiPlaceOfIssue.Text;
            I1.OTHER_IDENTITY_EXPIRY_DATE = OiExpDate2.Text;
            I1.COUNTRY_ISSUE_CNIC = new Country() { ID = ListExtensions.getSelectedValue(OiListICIssue), Name = OiListICIssue.SelectedItem.Text };

            I1.PLACE_ISSUE_CNIC = OiTxtPlaceIssueCnic.Text;

            I1.SaveIdentity();

            CIF cf = new CIF(Convert.ToInt32(Session["BID"]), CifType.INDIVIDUAL);

            if (cf.CheckCifCompleted() == true)
            {
                User LoggedUser = Session["User"] as User;
                cf.ChangeStatus(Status.SUBMITTED, LoggedUser);
                CalculateRisk();
                Response.Redirect("CifAccount.aspx");

            }


            //BI.CIF_TYPE = new CifTypes() { ID = Convert.ToInt32(lstCifType.SelectedItem.Value), Name = lstCifType.SelectedItem.Text };
        }

        protected void CiSubmitButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ContactInfo ci = new ContactInfo();
                ci.BI_ID = Convert.ToInt32(Session["BID"]);
                ci.COUNTRY_CODE = new Country { ID = Convert.ToInt32(CiListCountryCode.SelectedItem.Value) };
                ci.STREET = CiTxtStreet.Text;
                ci.BIULDING_SUITE = CiTxtBuilding.Text;
                ci.FLOOR = CiTxtFloor.Text;
                ci.DISTRICT = CiTxtDistrict.Text;
                ci.POST_OFFICE = CiTxtPostBox.Text;
                ci.POSTAL_CODE = CiTxtPotalCode.Text;
                if (CiListCountryCode.SelectedItem.Text.Trim() != "PAKISTAN")
                {
                    ci.CITY_PERMANENT = new City() { ID = null, Name = CiListCity.SelectedItem.Text };
                    ci.PROVINCE = new Province() { ID = null , Name = CiListProvince.SelectedItem.Text };
                }
                else
                {
                    ci.CITY_PERMANENT = new City() { ID = Convert.ToInt32(CiListCity.SelectedItem.Value), Name = CiListCity.SelectedItem.Text };
                    ci.PROVINCE = new Province() { ID = Convert.ToInt32(CiListProvince.SelectedItem.Value), Name = CiListProvince.SelectedItem.Text };
                }


                if (CiListCountryCodePre.SelectedItem.Text.Trim() != "PAKISTAN")
                {
                    ci.CITY_PRESENT = new City() { ID = null, Name = CiListCity.SelectedItem.Text };
                    ci.PROVINCE_PRE = new Province() { ID = null, Name = CiListProvincePre.SelectedItem.Text };
                }
                else
                {
                    ci.CITY_PRESENT = new City() { ID = Convert.ToInt32(CiListCityPre.SelectedItem.Value), Name = CiListCity.SelectedItem.Text };
                    ci.PROVINCE_PRE = new Province() { ID = Convert.ToInt32(CiListProvincePre.SelectedItem.Value), Name = CiListProvincePre.SelectedItem.Text };
                }
                ci.COUNTRY_CODE_PRE = new Country { ID = Convert.ToInt32(CiListCountryCodePre.SelectedItem.Value) };
                ci.STREET_PRE = CiTxtStreetPre.Text;
                ci.BIULDING_SUITE_PRE = CiTxtBuildingPre.Text;
                ci.FLOOR_PRE = CiTxtFloorPre.Text;
                ci.DISTRICT_PRE = CiTxtDistrictPre.Text;
                ci.POST_OFFICE_PRE = CiTxtPostBoxPre.Text;
                ci.POSTAL_CODE_PRE = CiTxtPotalCodePre.Text;
               

                ci.OFFICE_CONTACT = CiContactNoOffice.Text;
                ci.RESIDENCE_CONTACT = CiContactNoResidence.Text;
                ci.MOBILE_NO = CiMobileNo.Text;
                ci.FAX_NO = CiFaxNo.Text;
                ci.EMAIL = CiEmail.Text;

                ci.SaveContactInfo();

                CIF cf = new CIF(Convert.ToInt32(Session["BID"]), CifType.INDIVIDUAL);

                if (cf.CheckCifCompleted() == true)
                {
                    User LoggedUser = Session["User"] as User;
                    cf.ChangeStatus(Status.SUBMITTED, LoggedUser);
                    CalculateRisk();
                    Response.Redirect("CifAccount.aspx");

                }


                String mesg = "Address/Contact Information has been saved";
                int id = Convert.ToInt32(Session["BID"]);

                checkIndividualTabCompleted(id, mesg);
                CiSubmitButton.Visible = false;

               if (CiListCountryCode.SelectedItem.Text == "UNITED STATES" || CiListCountryCodePre.SelectedItem.Text == "UNITED STATES")
               {
                   PiListAddCountUsa.ClearSelection();
                   PiListAddCountUsa.Items.FindByText("Yes").Selected = true;
               }
               else
               {
                      PiListAddCountUsa.ClearSelection();
                      PiListAddCountUsa.Items.FindByText("No").Selected = true;
               }
                //CiResetButton.Visible = false;
            }          

        }

        protected void EiSubmitButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                EmploymentInfo e1 = new EmploymentInfo();
                e1.BI_ID = Convert.ToInt32(Session["BID"]);
                e1.CONSUMER_SEGMENT = new ConsumerSegment() { ID = Convert.ToInt32(EiListConsumer.SelectedItem.Value) };
                e1.EMPLOYMENT_DETAIL = new EmploymentDetail() { ID = Convert.ToInt32(EiListEmployDetail.SelectedItem.Value), Name = EiListEmployDetail.SelectedItem.Text };
                e1.EMPLOYMENT_DETAIL_OTHER_DESCRIPTION = txtDescEmpDetail.Text;
                e1.DEPARTMENT = EiDepartment.Text;
                if (EiRadio1.Checked == true)
                {
                    e1.RETIRED = false;
                }
                else
                {
                    e1.RETIRED = true;
                }
                e1.DESIGNATION = EiDesignation.Text;
                e1.PF_NO = EiPFNo.Text;
                e1.PPQ_NO = EiPPONo.Text;
                e1.EMPLOYER_CODE = new EmployerCodes() { ID = ListExtensions.getSelectedValue(EiListEmployerCode), Code = EiListEmployerCode.SelectedItem.Text };
                e1.EMPLOYER_DESC = EiTxtEmployer.Text;
                e1.EMPLOYER_BUSINESS_ADDRESS = EiEmpBusAddr.Text;
                e1.COUNTRY_EMPLOYMENT = new Country() { ID = ListExtensions.getSelectedValue(EiListCountryEmpBus), Name = EiListCountryEmpBus.SelectedItem.Text };
                e1.ARMY_RANK_CODE = new ArmyRankCodes() { ID = ListExtensions.getSelectedValue(EiListPakArmy), Code = EiListPakArmy.SelectedItem.Text };
                
                e1.EMPLOYER_GROUP = Convert.ToInt32(EiListEmpGrp.SelectedItem.Value);
                e1.EMPLOYER_SUB_GROUP = Convert.ToInt32(EiListEmpSubGrp.SelectedItem.Value);
                e1.EMPLOYER_NUMBER = Convert.ToInt32(EiListEmpNum.SelectedItem.Value);
               
                
                e1.SaveEmploymentInfo();

                CIF cf = new CIF(Convert.ToInt32(Session["BID"]), CifType.INDIVIDUAL);

                if (cf.CheckCifCompleted() == true)
                {
                    User LoggedUser = Session["User"] as User;
                    
                    cf.ChangeStatus(Status.SUBMITTED,LoggedUser);
                    CalculateRisk();
                    Response.Redirect("CifAccount.aspx");

                }



                String mesg = "Employment Information has been saved";
                int id = Convert.ToInt32(Session["BID"]);

                checkIndividualTabCompleted(id, mesg);


                EiSubmitButton.Visible = false;
                //EiResetButton.Visible = false;
            }

           
        }

        protected void MiSubmitButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                MiscellaneousInfo m = new MiscellaneousInfo();
                m.BI_ID = Convert.ToInt32(Session["BID"]);
                m.EDUCATION = new Education()
                {
                    ID = Convert.ToInt32(MiListEducation.SelectedItem.Value),
                    Name = MiListEducation.SelectedItem.Text
                };

                m.SOCIAL_STATUS = MiSocialStatus.Text;

                m.ACCOMODATION_TYPE = new AccomodationTypes() { ID = Convert.ToInt32(MiListAccomType.SelectedItem.Value), Name = MiListAccomType.SelectedItem.Text };
                m.ACCOMODATION_TYPE_DESCRIPTION = MiAccomTypeDescr.Text;
                m.TRANSPORTATION_TYPE = new TransportationType() { ID = ListExtensions.getSelectedValue(MiListTransportType), Name = MiListTransportType.SelectedItem.Text };


                if (MiBlindVisualRadio1.Checked == true)
                {
                    m.BLIND_VISUALLY_IMPARIED = false;
                }
                else
                {
                    m.BLIND_VISUALLY_IMPARIED = true;
                }

                m.PEP = new Pep() { ID = ListExtensions.getSelectedValue(MiListPoliticExposed), Name = MiListPoliticExposed.SelectedItem.Text };
                if (MiRadPepSingle.Checked)
                    m.PEP_NATURE_SINGLE = true;
                else if (MiRadPepLinked.Checked)
                    m.PEP_NATURE_SINGLE = false;
                else
                    m.PEP_NATURE_SINGLE = null;
                m.PEP_RELATIONSHIP = MiPepRelation.Text;
                m.PEP_DESC = MiTxtPED.Text;

                m.PEP_RELATIONSHIP = m.PEP_RELATIONSHIP;
                m.PEP_DESC = m.PEP_DESC;

                if (MiPardaRadio1.Checked == true)
                {
                    m.PARDA_NASHEEN = false;

                }
                else
                {
                    m.PARDA_NASHEEN = true;
                }

                //  m.MONTHLY_TURNOVER_DEBIT = new MonthlyTurnOverDebit() { ID = Convert.ToInt32(MiListMonthTurnOverDebit.SelectedItem.Value), Name = MiListMonthTurnOverDebit.SelectedItem.Text };
                //   m.MONTHLY_TURNOVER_CREDIT = new MonthlyTurnOverCredit() { ID = Convert.ToInt32(MiListMonthTurnOverCredit.SelectedItem.Value), Name = MiListMonthTurnOverCredit.SelectedItem.Text };
                //  m.AVERAGE_CASH_DEPOSIT = new AverageCashDeposit() { ID = Convert.ToInt32(MiListAvgNoOfCashDeposits.SelectedItem.Value), Name = MiListAvgNoOfCashDeposits.SelectedItem.Text };
                //   m.AVERAGE_CASH_NON_DEPOSIT = new AverageNonCashDeposit() { ID = Convert.ToInt32(MiListAvgNoOfNonCashDeposits.SelectedItem.Value), Name = MiListAvgNoOfNonCashDeposits.SelectedItem.Text };
                m.TOTAL_ASSET_VALUE = MiTotalAsset.Text;
                m.LIABILITIES = MiLiabilities.Text;
                m.NET_WORTH = MiNetWorth.Text;
                m.SOURCE_OF_FUND = Convert.ToInt32(MiListSOF.SelectedItem.Value);

                m.MiscellaneousInfoCountryTax = MiListCountryOfTax.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => new Country { ID = Convert.ToInt32(i.Value), Name = i.Text }).ToList();
                //            BI.NATIONALITIES = lstNationality.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => new Nationality { CountryID = Convert.ToInt32(i.Value), Country = i.Text }).ToList();
                m.SaveIndividualMiscellaneousInfo();

                CIF cf = new CIF(Convert.ToInt32(Session["BID"]), CifType.INDIVIDUAL);

                if (cf.CheckCifCompleted() == true)
                {
                    User LoggedUser = Session["User"] as User;
                    cf.ChangeStatus(Status.SUBMITTED, LoggedUser);
                    CalculateRisk();
                    Response.Redirect("CifAccount.aspx");

                }


                String mesg = "Miscellaneous Information has been saved";
                int id = Convert.ToInt32(Session["BID"]);

                checkIndividualTabCompleted(id, mesg);

                MiSubmitButton.Visible = false;
                //MiResetButton.Visible = false;
            }

           

        }

        protected void BrSubmitButton_Click(object sender, EventArgs e)
        {
            BankingRelatationship b = new BankingRelatationship();
            b.BI_ID = Convert.ToInt32(Session["BID"]);
            b.NBP_BRANCH_INFORMATION = new NbpBranchInformation() { ID = ListExtensions.getSelectedValue(BrListBranchInfo), Name = BrListBranchInfo.SelectedItem.Text };
            b.NBP_ACCOUNT_TYPE = new AccountType() { ID = Convert.ToInt32(BrListAccountType.SelectedItem.Value), Name = BrListAccountType.SelectedItem.Text };
            b.NBP_ACCOUNT_NUMBER = BrAccountNumber.Text;
            b.NBP_ACCOUNT_TITLE = BrAccountTitle.Text;
            b.NBP_RELATIONSHIP_SINCE = BrRelationShipSince.Text;

            b.OTHER_BANK_CODE = new OtherBankCodes() { ID = ListExtensions.getSelectedValue(BrListOtherBranchCode), Name = BrListOtherBranchCode.SelectedItem.Text };
            b.OTHER_BRANCH_NAME = BrOtherBranchName.Text;
            b.OTHER_ACCOUNT_NUMBER = BrOtherAccountNumber.Text;
            b.OTHER_ACCOUNT_TITLE = BrOtherAccountTitle.Text;
            b.OTHER_RELATIONSHIP_SINCE = BrOtherRelationshipSince.Text;

            b.SaveBankingRelatationship();

            CIF cf = new CIF(Convert.ToInt32(Session["BID"]), CifType.INDIVIDUAL);

            if (cf.CheckCifCompleted() == true)
            {
                User LoggedUser = Session["User"] as User;
                cf.ChangeStatus(Status.SUBMITTED,LoggedUser);
                CalculateRisk();
                Response.Redirect("CifAccount.aspx");

            }


            String mesg = "Banking Relationship has been saved";
            int id = Convert.ToInt32(Session["BID"]);

            checkIndividualTabCompleted(id, mesg);


            BrSubmitButton.Visible = false;
            //BrResetButton.Visible = false;
        }

        protected void PiSubmitButton_Click(object sender, EventArgs e)
        {
            Fatca f = new Fatca();
            f.BI_ID = Convert.ToInt32(Session["BID"]);
            if (PiListResident.SelectedItem.Text == "Yes")
            {
                f.RESIDENT = true;
            }
            else
            {
                f.RESIDENT = false;
            }


            if (PiListCitizen.SelectedItem.Text == "Yes")
            {
                f.CITIZEN = true;

            }
            else
            {
                f.CITIZEN = false;
            }

            if (PiListCountBirthUsa.SelectedItem.Text == "Yes")
            {
                f.BIRTH_USA = true;
            }
            else
            {
                f.BIRTH_USA = false;
            }

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
            f.TYPE_TIN = new FatcasTin() { ID = ListExtensions.getSelectedValue(PiListTinType) };
            f.TIN = PiTxtTin.Text;
            if (PiTxtFatcaDocumentDate.Text.Length > 0)
                 f.FATCA_DOCUMENTATION_DATE = DateTime.ParseExact(PiTxtFatcaDocumentDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            f.RESIDENCE_CARD = new UsaResidenceCard() { ID = ListExtensions.getSelectedValue(PiListResidenceCard), Name = PiListResidenceCard.SelectedItem.Text };
            f.FUND_TRANSFER = new UsaFund() { ID = ListExtensions.getSelectedValue(PiListTransferOfFundsUSA), Name = PiListTransferOfFundsUSA.SelectedItem.Text };
            f.FTCA_CLASSIFICATION = new FatcaClassification() { ID = ListExtensions.getSelectedValue(PiListFatcaClass), Name = PiListFatcaClass.SelectedItem.Text };
            f.US_TAXID = new UsaTaxType() { ID = Convert.ToInt32(PiListUsTaxIdType.SelectedItem.Value), Name = PiListUsTaxIdType.SelectedItem.Text };
            f.TAXNO = PiTaxNo.Text;
           

            f.FATCA_DOCUMENTS = PiListFatcaDocumentation.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => new FatcaDocumentation { ID = Convert.ToInt32(i.Value), Name = i.Text }).ToList();
            f.SaveFatca();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

            CIF cf = new CIF(Convert.ToInt32(Session["BID"]), CifType.INDIVIDUAL);


            if (cf.CheckCifCompleted() == true)
            {
                User LoggedUser = Session["User"] as User;
                
                cf.ChangeStatus(Status.SUBMITTED,LoggedUser);
                CalculateRisk();
                Response.Redirect("CifAccount.aspx");

            }



            String mesg = "FATCA(U.S Person Identification) has been saved";
            int id = Convert.ToInt32(Session["BID"]);

            checkIndividualTabCompleted(id, mesg);

            PiSubmitButton.Visible = false;
            //PiResetButton.Visible = false;
            //            BI.NATIONALITIES = lstNationality.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => new Nationality { CountryID = Convert.ToInt32(i.Value), Country = i.Text }).ToList();


        }

        protected void chkSW_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSW.Checked == true)
            {
                CiListCountryCodePre.SelectedValue = CiListCountryCode.SelectedValue.ToString();
                CiTxtStreetPre.Text = CiTxtStreet.Text;
                CiTxtBuildingPre.Text = CiTxtBuilding.Text;
                CiTxtFloorPre.Text = CiTxtFloor.Text;
                CiTxtDistrictPre.Text = CiTxtDistrict.Text;
                CiTxtPostBoxPre.Text = CiTxtPostBox.Text;
                CiTxtPotalCodePre.Text = CiTxtPotalCode.Text;
                CiListCityPre.SelectedValue = CiListCity.SelectedValue.ToString();
                CiListProvincePre.SelectedValue = CiListProvince.SelectedValue.ToString();
                //CiTxtPresentAddrInfo.Text = CiPermResdAddInfo.Text;
                //CiListDistrictPresent.SelectedValue = CiListDistrict.SelectedValue.ToString();
                //CiTxtPOBoxPresent.Text = CiTxtPoBox.Text;
                //CiListCityPresent.SelectedValue = CiListCity.SelectedValue.ToString();
                //CiTxtPostalCodePresent.Text = CiTxtPostalCode.Text;
                //CiListCountryPresent.SelectedValue = CiListCountry.SelectedValue.ToString();

                CiListCountryCodePre_SelectedIndexChanged(null, null);
            }
            else
            {
                //CiListCountryCodePre.SelectedValue = "";
                //CiTxtStreetPre.Text = "";
                //CiTxtBuildingPre.Text = "";
                //CiTxtFloorPre.Text = "";
                //CiTxtDistrictPre.Text = "";
                //CiTxtPostBoxPre.Text = "";
                //CiTxtPotalCodePre.Text = "";
                //CiListCityPre.SelectedValue = "";
                //CiTxtPresentAddrInfo.Text = "";
                //CiListDistrict.SelectedValue = "";
                //CiTxtPOBoxPresent.Text = "";
                //CiListCityPresent.SelectedValue = "";

                //CiTxtPostalCodePresent.Text = "";

                //CiListCountryPresent.SelectedValue = "";

            }
        }

        #region reset Event HAndlers
        //protected void OiButtonReset_Click(object sender, EventArgs e)
        //{
        //    SetOtherIdentityData();
        //    OiTxtIdentMark.Text = "";
        //    OiTxtFamilyNo.Text = "";
        //    OiTxtTokenNo.Text = "";
        //    OiTxtNTN.Text = "";
        //    OiTxtOldNic.Text = "";
        //    OiTxtIdentNo.Text = "";
        //    OiPlaceOfIssue.Text = "";
        //}

        //protected void EiResetButton_Click(object sender, EventArgs e)
        //{
        //    SetEmployementInformation();
        //    EiDepartment.Text = "";
        //    EiDesignation.Text = "";
        //    EiEmployeeID.Text = "";
        //    EiPFNo.Text = "";
        //    EiPPONo.Text = "";
        //    EiEmpBusAddr.Text = "";

        //}

        //protected void CiResetButton_Click(object sender, EventArgs e)
        //{
        //    SetContactInfoData();
        //    CiPermResdAddInfo.Text = "";
        //    CiTxtPoBox.Text = "";
        //    CiTxtPostalCode.Text = "";
        //    CiTxtPresentAddrInfo.Text = "";
        //    CiTxtPOBoxPresent.Text = "";
        //    CiTxtPostalCodePresent.Text = "";
        //    CiContactNoOffice.Text = "";
        //    CiContactNoResidence.Text = "";
        //    CiMobileNo.Text = "";
        //    CiFaxNo.Text = "";
        //    CiEmail.Text = "";

        //}

        //protected void MiResetButton_Click(object sender, EventArgs e)
        //{
        //    SetMiscellaneousInfo();
        //    MiSocialStatus.Text = "";
        //    MiAccomTypeDescr.Text = "";
        //    MiTotalAsset.Text = "";
        //    MiLiabilities.Text = "";
        //    MiNetWorth.Text = "";

        //}

        //protected void BrResetButton_Click(object sender, EventArgs e)
        //{
        //    SetBankingRelationShip();
        //    BrAccountNumber.Text = "";
        //    BrAccountTitle.Text = "";
        //    BrRelationShipSince.Text = "";
        //    BrOtherBranchName.Text = "";
        //    BrOtherAccountNumber.Text = "";
        //    BrOtherAccountTitle.Text = "";
        //    BrOtherRelationshipSince.Text = "";

        //}

        //protected void PiResetButton_Click(object sender, EventArgs e)
        //{
        //    SetPersonIdentification();
        //    PiContactOffice.Text = "";
        //    PiContactResidence.Text = "";
        //    PiMobileNo.Text = "";
        //    PiFaxNo.Text = "";
        //    PiTaxNo.Text = "";
        //}

        #endregion
        protected void lstCifType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (lstCifType.SelectedItem.Text == "NEXT_OF_KIN")
            {
                Response.Redirect("NextOfKin.aspx");
            }

            else if (lstCifType.SelectedItem.Text == "BUSINESS / OTHER")
            {
                Response.Redirect("Business.aspx");
            }


        }


        #region Update Code
        private void SetUpdateBtnVisible()
        {
            btnUpdateBasic.Visible = true;
            btnUpdateBr.Visible = true;
            btnUpdateContactInfo.Visible = true;
            btnUpdateEi.Visible = true;
            btnUpdateIdentity.Visible = true;
            btnUpdateMi.Visible = true;
            btnPiUpdate.Visible = true;
        }

        private void SetCifSubmitVisible()
        {
            btnSubmitCifa.Visible = true;
            btnSubmitCifb.Visible = true;
            btnSubmitCifc.Visible = true;
            btnSubmitCifd.Visible = true;
            btnSubmitCife.Visible = true;
            btnSubmitCiff.Visible = true;
            btnSubmitCifg.Visible = true;
        }

        protected void btnUpdateBasic_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BasicInformations BI = new BasicInformations();
                int BID = (int)Session["BID"];
                UpdateBI(BI, BID);
                String mesg = "Basic Information has been Updated";
                checkIndividualTabCompleted(BID, mesg);
            }

          

        }

        private void UpdateBI(BasicInformations BI, int BID)
        {
            BI.PRIMARY_DOCUMENT_TYPE = new PrimaryDocumentType { ID = ListExtensions.getSelectedValue(lstPrimaryDocumentType), Name = lstPrimaryDocumentType.SelectedItem.Text };
            BI.CNIC = txtCnic.Text;
            BI.TITLE = new Title() { ID = Convert.ToInt32(lstTitle.SelectedItem.Value), Name = lstTitle.SelectedItem.Text };
          //  BI.NAME = txtName.Text;
            BI.FIRST_NAME = txtFirstName.Text;
            BI.MIDDLE_NAME = txtMiddleName.Text;
            BI.LAST_NAME = txtLastName.Text;
            BI.TITLE_FH = new Title() { ID = Convert.ToInt32(lstTitleFather.SelectedItem.Value), Name = lstTitleFather.SelectedItem.Text };
            BI.NAME_FH = txtFatherName.Text;
            BI.CNIC_FH = txtFatherCnic.Text;
            BI.CIF_FH = txtFatherCif.Text;
            BI.NAME_MOTHER = txtMotherName.Text;
            BI.CNIC_MOTHER = txtMotherCnic.Text;
            BI.CNIC_MOTHER_OLD = txtMotherCnicOld.Text;
            BI.DATE_BIRTH = DateTime.ParseExact(txtDOB.Text,
                                   "dd-MM-yyyy",
                                    CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            BI.PLACE_BIRTH = txtBithPlace.Text;
            BI.Country_Birth = new Country { ID = Convert.ToInt32(lstCOB.SelectedItem.Value), Name = lstCOB.SelectedItem.Text };
            BI.MARTIAL_STATUS = new MartialStatus { ID = Convert.ToInt32(lstMartialStatus.SelectedItem.Value), Name = lstMartialStatus.SelectedItem.Text };
            BI.GENDER = new Gender { ID = Convert.ToInt32(lstGender.SelectedItem.Value), Name = lstGender.SelectedItem.Text };
            BI.RELIGION = new Religion { ID = Convert.ToInt32(lstReligion.SelectedItem.Value), Name = lstReligion.SelectedItem.Text };
            BI.RESIDENT_TYPE = new ResidentType { ID = Convert.ToInt32(lstResident.SelectedItem.Value), Name = lstResident.SelectedItem.Text };
            BI.NATIONALITIES = lstNationality.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => new Nationality { CountryID = Convert.ToInt32(i.Value), Country = i.Text }).ToList();
            BI.MONTHLY_INCOME = txtIncome.Text;
            BI.COUNTRY_RESIDENCE = new Country { ID = ListExtensions.getSelectedValue(lstCOR), Name = lstCOR.SelectedItem.Text };
            BI.CUSTOMER_DEAL = new CustomerDeal { ID = ListExtensions.getSelectedValue(lstCustomerDeals), Name = lstCustomerDeals.SelectedItem.Text };
            BI.DOCUMENT_VERIFIED = chkDocument.Checked;
            BI.CUSTOMER_TYPE = new CifCustomerType() { ID = Convert.ToInt32(LstCustomerType.SelectedItem.Value) };
            BI.CIF_OFFICER_CODE = Convert.ToInt32(lstOfficerCode.SelectedItem.Value);
            BI.ID = BID;
            BI.UpdateIndividual();

              if (lstCOR.SelectedItem.Text.Trim() != "UNITED STATES")
              {
                    PiListResident.ClearSelection();
                    PiListResident.Items.FindByText("No").Selected = true;
                    //   PiListCitizen.ClearSelection();
                    //    PiListCitizen.Items.FindByText("No").Selected = true;
                    //    PiListAddCountUsa.ClearSelection();
                    //    PiListAddCountUsa.Items.FindByText("No").Selected = true;
              }
              else
              {
                    PiListResident.ClearSelection();
                    PiListResident.Items.FindByText("Yes").Selected = true;
                    //     PiListCitizen.ClearSelection();
                    //     PiListCitizen.Items.FindByText("Yes").Selected = true;
                    //     PiListAddCountUsa.ClearSelection();
                    //     PiListAddCountUsa.Items.FindByText("Yes").Selected = true;
              }

              if (lstNationality.Items.Cast<ListItem>().Where(n => n.Selected == true && n.Text.Trim() == "UNITED STATES").Any())
              {
                  PiListCitizen.ClearSelection();
                  PiListCitizen.Items.FindByText("Yes").Selected = true;
              }
              else
              {
                  PiListCitizen.ClearSelection();
                  PiListCitizen.Items.FindByText("No").Selected = true;
              }
        }
        

        protected void btnUpdateIdentity_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Identity I1 = new Identity();
                int BID = (int)Session["BID"];
                UpdateIdentity(I1, BID);
                String mesg = "CNIC/Other Identity has been Updated";
                checkIndividualTabCompleted(BID, mesg);
            }

          
        }

        private void UpdateIdentity(Identity I1, int BID)
        {
            I1.BI_ID = BID;
            I1.CNIC_DATE_ISSUE = OiDateIssue.Text;
            I1.EXPIRY_DATE = OiExpDate.Text;
            I1.IDENTIFICATION_MARK = OiTxtIdentMark.Text;
            I1.FAMILY_NO = OiTxtFamilyNo.Text;
            I1.TOKEN_NO = OiTxtTokenNo.Text;
            I1.TOKEN_ISSUE_DATE = Convert.ToDateTime(OiTxtTokenIssueDate.Text);
            I1.NTN = OiTxtNTN.Text;
            I1.NIC_OLD = OiTxtOldNic.Text;
            I1.IDENTITY_TYPE = new IdentityType() { ID = ListExtensions.getSelectedValue(OiListIdentType), Name = OiListIdentType.SelectedItem.Text };
            I1.IDENTITY_NO = OiTxtIdentNo.Text;
            I1.COUNTRY_ISSUE = new Country() { ID = ListExtensions.getSelectedValue(OiListCountryOfIssue), Name = OiListCountryOfIssue.SelectedItem.Value };
            I1.OTHER_IDENTITY_ISSUE_DATE = OiDateIssue2.Text;
            I1.PLACE_ISSUE = OiPlaceOfIssue.Text;
            I1.OTHER_IDENTITY_EXPIRY_DATE = OiExpDate2.Text;
            I1.COUNTRY_ISSUE_CNIC = new Country() { ID = ListExtensions.getSelectedValue(OiListICIssue), Name = OiListICIssue.SelectedItem.Text };
            I1.PLACE_ISSUE_CNIC = OiTxtPlaceIssueCnic.Text;
            I1.UpdateIdentity();
        }

        protected void btnUpdateContactInfo_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ContactInfo CInfo = new ContactInfo();
                int BID = (int)Session["BID"];
                UpdateContactInfo(CInfo, BID);
                String mesg = "Address/ Contact Iformation has been Updated";
                checkIndividualTabCompleted(BID, mesg);
            }

           
        }

        private void UpdateContactInfo(ContactInfo ci, int BID)
        {
            ci.BI_ID = BID;
            ci.COUNTRY_CODE = new Country { ID = Convert.ToInt32(CiListCountryCode.SelectedItem.Value) };
            ci.STREET = CiTxtStreet.Text;
            ci.BIULDING_SUITE = CiTxtBuilding.Text;
            ci.FLOOR = CiTxtFloor.Text;
            ci.DISTRICT = CiTxtDistrict.Text;
            ci.POST_OFFICE = CiTxtPostBox.Text;
            ci.POSTAL_CODE = CiTxtPotalCode.Text;
            if (CiListCountryCode.SelectedItem.Text.Trim() != "PAKISTAN")
            {
                ci.CITY_PERMANENT = new City() { ID = null, Name = CiListCity.SelectedItem.Text };
                ci.PROVINCE = new Province() { ID = null, Name = CiListProvince.SelectedItem.Text };
            }
            else
            {
                ci.CITY_PERMANENT = new City() { ID = Convert.ToInt32(CiListCity.SelectedItem.Value), Name = CiListCity.SelectedItem.Text };
                ci.PROVINCE = new Province() { ID = Convert.ToInt32(CiListProvince.SelectedItem.Value), Name = CiListProvince.SelectedItem.Text };
            }


            if (CiListCountryCodePre.SelectedItem.Text.Trim() != "PAKISTAN")
            {
                ci.CITY_PRESENT = new City() { ID = null, Name = CiListCity.SelectedItem.Text };
                ci.PROVINCE_PRE = new Province() { ID = null, Name = CiListProvincePre.SelectedItem.Text };
            }
            else
            {
                ci.CITY_PRESENT = new City() { ID = Convert.ToInt32(CiListCityPre.SelectedItem.Value), Name = CiListCity.SelectedItem.Text };
                ci.PROVINCE_PRE = new Province() { ID = Convert.ToInt32(CiListProvincePre.SelectedItem.Value), Name = CiListProvincePre.SelectedItem.Text };
            }
            ci.COUNTRY_CODE_PRE = new Country { ID = Convert.ToInt32(CiListCountryCodePre.SelectedItem.Value) };
            ci.STREET_PRE = CiTxtStreetPre.Text;
            ci.BIULDING_SUITE_PRE = CiTxtBuildingPre.Text;
            ci.FLOOR_PRE = CiTxtFloorPre.Text;
            ci.DISTRICT_PRE = CiTxtDistrictPre.Text;
            ci.POST_OFFICE_PRE = CiTxtPostBoxPre.Text;
            ci.POSTAL_CODE_PRE = CiTxtPotalCodePre.Text;


            ci.OFFICE_CONTACT = CiContactNoOffice.Text;
            ci.RESIDENCE_CONTACT = CiContactNoResidence.Text;
            ci.MOBILE_NO = CiMobileNo.Text;
            ci.FAX_NO = CiFaxNo.Text;
            ci.EMAIL = CiEmail.Text;
            ci.UpdateContactInfo();

             if (CiListCountryCode.SelectedItem.Text == "UNITED STATES" || CiListCountryCodePre.SelectedItem.Text == "UNITED STATES")
               {
                   PiListAddCountUsa.ClearSelection();
                   PiListAddCountUsa.Items.FindByText("Yes").Selected = true;
               }
               else
               {
                      PiListAddCountUsa.ClearSelection();
                      PiListAddCountUsa.Items.FindByText("No").Selected = true;
               }

        }

        protected void btnUpdateEi_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                EmploymentInfo EInfo = new EmploymentInfo();
                int BID = (int)Session["BID"];
                UpdateEmploymentInfo(EInfo, BID);
                String mesg = "Employment Iformation has been Updated";
                checkIndividualTabCompleted(BID, mesg);
            }

         
        }

        private void UpdateEmploymentInfo(EmploymentInfo e1, int BID)
        {
            e1.BI_ID = BID;
            e1.CONSUMER_SEGMENT = new ConsumerSegment() { ID = Convert.ToInt32(EiListConsumer.SelectedItem.Value) };
            e1.EMPLOYMENT_DETAIL_OTHER_DESCRIPTION = txtDescEmpDetail.Text;
            e1.EMPLOYMENT_DETAIL = new EmploymentDetail() { ID = Convert.ToInt32(EiListEmployDetail.SelectedItem.Value), Name = EiListEmployDetail.SelectedItem.Text };
            e1.DEPARTMENT = EiDepartment.Text;
            if (EiRadio1.Checked == true)
            {
                e1.RETIRED = false;
            }
            else
            {
                e1.RETIRED = true;
            }
            e1.DESIGNATION = EiDesignation.Text;
            e1.PF_NO = EiPFNo.Text;
            e1.PPQ_NO = EiPPONo.Text;
            e1.EMPLOYER_DESC = EiTxtEmployer.Text;
            e1.EMPLOYER_CODE = new EmployerCodes() { ID = ListExtensions.getSelectedValue(EiListEmployerCode), Code = EiListEmployerCode.SelectedItem.Text };
            e1.EMPLOYER_BUSINESS_ADDRESS = EiEmpBusAddr.Text;
            e1.COUNTRY_EMPLOYMENT = new Country() { ID = ListExtensions.getSelectedValue(EiListCountryEmpBus), Name = EiListCountryEmpBus.SelectedItem.Text };
            e1.ARMY_RANK_CODE = new ArmyRankCodes() { ID = ListExtensions.getSelectedValue(EiListPakArmy), Code = EiListPakArmy.SelectedItem.Text };
            e1.EMPLOYER_GROUP = Convert.ToInt32(EiListEmpGrp.SelectedItem.Value);
            e1.EMPLOYER_SUB_GROUP = Convert.ToInt32(EiListEmpSubGrp.SelectedItem.Value);
            e1.EMPLOYER_NUMBER = Convert.ToInt32(EiListEmpNum.SelectedItem.Value);
            
            
            e1.UpdateEmploymentInfo();
        }

        protected void btnUpdateMi_Click(object sender, EventArgs e)
        {
            MiscellaneousInfo MInfo = new MiscellaneousInfo();
            int BID = (int)Session["BID"];
            UpdateMiscInfo(MInfo, BID);
            String mesg = "Miscellaneous Iformation has been Updated";
            checkIndividualTabCompleted(BID, mesg);
        }

        private void UpdateMiscInfo(MiscellaneousInfo m, int BID)
        {
            m.BI_ID = BID;
            m.EDUCATION = new Education()
            {
                ID = Convert.ToInt32(MiListEducation.SelectedItem.Value),
                Name = MiListEducation.SelectedItem.Text
            };
            m.SOCIAL_STATUS = MiSocialStatus.Text;
            m.ACCOMODATION_TYPE = new AccomodationTypes() { ID = Convert.ToInt32(MiListAccomType.SelectedItem.Value), Name = MiListAccomType.SelectedItem.Text };
            m.ACCOMODATION_TYPE_DESCRIPTION = MiAccomTypeDescr.Text;
            m.TRANSPORTATION_TYPE = new TransportationType() { ID = ListExtensions.getSelectedValue(MiListTransportType), Name = MiListTransportType.SelectedItem.Text };
            if (MiBlindVisualRadio1.Checked == true)
            {
                m.BLIND_VISUALLY_IMPARIED = false;
            }
            else
            {
                m.BLIND_VISUALLY_IMPARIED = true;
            }

            m.PEP = new Pep() { ID = ListExtensions.getSelectedValue(MiListPoliticExposed), Name = MiListPoliticExposed.SelectedItem.Text };
            
            if (MiRadPepSingle.Checked)
                m.PEP_NATURE_SINGLE = true;
            else if (MiRadPepLinked.Checked)
                m.PEP_NATURE_SINGLE = false;
            else
                m.PEP_NATURE_SINGLE = null;

            if (m.PEP_NATURE_SINGLE == null)
            {

            }
            else
            {
                m.PEP_RELATIONSHIP = MiPepRelation.Text;
                m.PEP_DESC = MiTxtPED.Text;
            }
           

            if (MiPardaRadio1.Checked == true)
            {
                m.PARDA_NASHEEN = false;

            }
            else
            {
                m.PARDA_NASHEEN = true;
            }

        //    m.MONTHLY_TURNOVER_DEBIT = new MonthlyTurnOverDebit() { ID = Convert.ToInt32(MiListMonthTurnOverDebit.SelectedItem.Value), Name = MiListMonthTurnOverDebit.SelectedItem.Text };
        //    m.MONTHLY_TURNOVER_CREDIT = new MonthlyTurnOverCredit() { ID = Convert.ToInt32(MiListMonthTurnOverCredit.SelectedItem.Value), Name = MiListMonthTurnOverCredit.SelectedItem.Text };
        //    m.AVERAGE_CASH_DEPOSIT = new AverageCashDeposit() { ID = Convert.ToInt32(MiListAvgNoOfCashDeposits.SelectedItem.Value), Name = MiListAvgNoOfCashDeposits.SelectedItem.Text };
        //    m.AVERAGE_CASH_NON_DEPOSIT = new AverageNonCashDeposit() { ID = Convert.ToInt32(MiListAvgNoOfNonCashDeposits.SelectedItem.Value), Name = MiListAvgNoOfNonCashDeposits.SelectedItem.Text };
            m.TOTAL_ASSET_VALUE = MiTotalAsset.Text;
            m.LIABILITIES = MiLiabilities.Text;
            m.NET_WORTH = MiNetWorth.Text;
            m.SOURCE_OF_FUND = Convert.ToInt32(MiListSOF.SelectedItem.Value);
            m.MiscellaneousInfoCountryTax = MiListCountryOfTax.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => new Country { ID = Convert.ToInt32(i.Value), Name = i.Text }).ToList();

            m.UpdateIndividualMiscellaneousInfo();
        }

        protected void btnUpdateBr_Click(object sender, EventArgs e)
        {
            BankingRelatationship BR = new BankingRelatationship();
            int BID = (int)Session["BID"];
            UpdateBankingRelatationship(BR, BID);
            String mesg = "Banking Relationship has been Updated";
            checkIndividualTabCompleted(BID, mesg);
        }

        private void UpdateBankingRelatationship(BankingRelatationship b, int BID)
        {
            b.BI_ID = BID;
            b.NBP_BRANCH_INFORMATION = new NbpBranchInformation() { ID = ListExtensions.getSelectedValue(BrListBranchInfo), Name = BrListBranchInfo.SelectedItem.Text };
            b.NBP_ACCOUNT_TYPE = new AccountType() { ID = Convert.ToInt32(BrListAccountType.SelectedItem.Value), Name = BrListAccountType.SelectedItem.Text };
            b.NBP_ACCOUNT_NUMBER = BrAccountNumber.Text;
            b.NBP_ACCOUNT_TITLE = BrAccountTitle.Text;
            b.NBP_RELATIONSHIP_SINCE = BrRelationShipSince.Text;
            b.OTHER_BANK_CODE = new OtherBankCodes() { ID = ListExtensions.getSelectedValue(BrListOtherBranchCode), Name = BrListOtherBranchCode.SelectedItem.Text };
            b.OTHER_BRANCH_NAME = BrOtherBranchName.Text;
            b.OTHER_ACCOUNT_NUMBER = BrOtherAccountNumber.Text;
            b.OTHER_ACCOUNT_TITLE = BrOtherAccountTitle.Text;
            b.OTHER_RELATIONSHIP_SINCE = BrOtherRelationshipSince.Text;
            b.UpdateBankingRelationship();

        }

        protected void btnPiUpdate_Click(object sender, EventArgs e)
        {
            Fatca f = new Fatca();
            int BID = (int)Session["BID"];
            UpdateFatca(f, BID);
            String mesg = "FATCA(U.S Person Identification) has been Updated";
            checkIndividualTabCompleted(BID, mesg);
        }

        private void UpdateFatca(Fatca f, int BID)
        {
            f.BI_ID = BID;
            if (PiListResident.SelectedItem.Text == "Yes")
            {
                f.RESIDENT = true;
            }
            else
            {
                f.RESIDENT = false;
            }


            if (PiListCitizen.SelectedItem.Text == "Yes")
            {
                f.CITIZEN = true;

            }
            else
            {
                f.CITIZEN = false;
            }

            if (PiListCountBirthUsa.SelectedItem.Text == "Yes")
            {
                f.BIRTH_USA = true;
            }
            else
            {
                f.BIRTH_USA = false;
            }

            if (PiListAddCountUsa.SelectedItem.Text == "Yes")
            {
                f.ADDRESS_USA = true;
            }
            else
            {
                f.ADDRESS_USA = false;
            }
            f.TYPE_TIN = new FatcasTin() { ID = ListExtensions.getSelectedValue(PiListTinType) };
            f.TIN = PiTxtTin.Text;
            if (PiTxtFatcaDocumentDate.Text.Length > 0)
                f.FATCA_DOCUMENTATION_DATE = DateTime.ParseExact(PiTxtFatcaDocumentDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            else
                f.FATCA_DOCUMENTATION_DATE = null;
            f.USA_PHONE = new UsaPhone() { ID = ListExtensions.getSelectedValue(PiListPhoneNoUsa), Name = PiListPhoneNoUsa.SelectedItem.Text };
            f.CONTACT_OFFICE = PiContactOffice.Text;
            f.CONTACT_RESIDENCE = PiContactResidence.Text;
            f.MOBNO = PiMobileNo.Text;
            f.FAXNO = PiFaxNo.Text;
            f.RESIDENCE_CARD = new UsaResidenceCard() { ID = ListExtensions.getSelectedValue(PiListResidenceCard), Name = PiListResidenceCard.SelectedItem.Text };
            f.FUND_TRANSFER = new UsaFund() { ID = ListExtensions.getSelectedValue(PiListTransferOfFundsUSA), Name = PiListTransferOfFundsUSA.SelectedItem.Text };
            f.FTCA_CLASSIFICATION = new FatcaClassification() { ID = ListExtensions.getSelectedValue(PiListFatcaClass), Name = PiListFatcaClass.SelectedItem.Text };
            f.US_TAXID = new UsaTaxType() { ID = Convert.ToInt32(PiListUsTaxIdType.SelectedItem.Value), Name = PiListUsTaxIdType.SelectedItem.Text };
            f.TAXNO = PiTaxNo.Text;
            f.FATCA_DOCUMENTS = PiListFatcaDocumentation.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => new FatcaDocumentation { ID = Convert.ToInt32(i.Value), Name = i.Text }).ToList();
            f.UpdateFatca();
        }

        protected void btnSubmitCif_Click(object sender, EventArgs e)
        {
            int BID = (int)Session["BID"];
            CIF cif = new CIF(BID, CifType.INDIVIDUAL);
            User LoggedUser = Session["User"] as User;
            cif.ChangeStatus(Status.SUBMITTED,LoggedUser);
            CalculateRisk();
            Response.Redirect("CifAccount.aspx");
        }

        #endregion

        protected void CustomValidatorCNIC_ServerValidate(object source, ServerValidateEventArgs args)
        {
            BasicInformations b = new BasicInformations();
            if (lstPrimaryDocumentType.SelectedItem.Text == "Passport")
            {
                args.IsValid = true;
            }           
            else if (b.IsCnicExists(txtCnic.Text))
            {
                CustomValidatorCNIC.ErrorMessage = "CIF Already Exists With this CNIC. Proceed to Account Creation";

                if (b.IsCnicExistsPArtially(txtCnic.Text))
                    CustomValidatorCNIC.ErrorMessage = "CIF already created partially, Please complete it first";

                args.IsValid = false;
            }                
            else
            {
                if (lstPrimaryDocumentType.SelectedItem.Value == "1" || lstPrimaryDocumentType.SelectedItem.Value == "2")
                {
                    if (Request.QueryString["PROFILECIF"] == null)
                    {
                        if (IsCustomerExistsInProfile(txtCnic.Text))
                        {
                            CustomValidatorCNIC.ErrorMessage = "CIF Already Exists With this CNIC in Profile. Proceed to Incorporate Cif";
                            args.IsValid = false;
                        }
                        else
                            args.IsValid = true;
                    }
                    else
                        args.IsValid = true;
                    
                   
                }
                else
                    args.IsValid = true;
            } 
        }

        private bool IsCustomerExistsInProfile(string CNIC)
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
                return true;
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
                return true;
            }

            if (IsEmpty(ds))
            {
                return false;
            }
            else
                return true;
        }

        bool IsEmpty(DataSet dataSet)
        {
            return !dataSet.Tables.Cast<DataTable>().Any(x => x.DefaultView.Count > 0);
        }



        protected void lstPrimaryDocumentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPrimaryDocumentType.SelectedItem.Text.Trim() == "CNIC/SNIC" || lstPrimaryDocumentType.SelectedItem.Text.Trim() == "NICOP" || lstPrimaryDocumentType.SelectedItem.Text.Trim() == "POC")
            {
                RegularExpressionValidatorCnic.Enabled = true;
                
            }
            else
            {
                RegularExpressionValidatorCnic.Enabled = false;
                
            }

            if (lstPrimaryDocumentType.SelectedItem.Text.Trim() == "CNIC/SNIC" || lstPrimaryDocumentType.SelectedItem.Text.Trim() == "NICOP")
            {
                btnBioMetricVerify.Visible = true;
            }
            else
            {
                btnBioMetricVerify.Visible = false;
            }

            //if (lstPrimaryDocumentType.SelectedItem.Text != "Select" || lstPrimaryDocumentType.SelectedItem.Text != "Passport")
            //{
            //    CustomValidatorCNIC.Enabled = true;
            //}
            //else
            //{
            //    CustomValidatorCNIC.Enabled = false;
            //}
        }

        protected void MiNetWorth_TextChanged(object sender, EventArgs e)
        {

        }

        protected void MiTotalAsset_TextChanged(object sender, EventArgs e)
        {
            decimal TotalAsset = 0M;
            decimal TotalLiab = 0M;
            decimal Calc = 0M;
            if (MiTotalAsset.Text.Length > 0)
                TotalAsset = Convert.ToDecimal(MiTotalAsset.Text);

            if (MiLiabilities.Text.Length > 0)
                TotalLiab = Convert.ToDecimal(MiLiabilities.Text);
            Calc = TotalAsset - TotalLiab;
            MiNetWorth.Text = Calc.ToString();

        }

        protected void MiLiabilities_TextChanged(object sender, EventArgs e)
        {
            decimal TotalAsset = 0M;
            decimal TotalLiab = 0M;
            decimal Calc = 0M;
            if (MiTotalAsset.Text.Length > 0)
                TotalAsset = Convert.ToDecimal(MiTotalAsset.Text);

            if (MiLiabilities.Text.Length > 0)
                TotalLiab = Convert.ToDecimal(MiLiabilities.Text);
            Calc = TotalAsset - TotalLiab;
            MiNetWorth.Text = Calc.ToString();
        }

        public void CalculateRisk()
        {

            try
            {
                User LoggedUser = Session["User"] as User;
                Gender g = new Gender();
                Country c = new Country();
                string CustomerIdNumber = Convert.ToInt32(Session["BID"]).ToString(); // "2280829";
                string BranchCode = LoggedUser.Branch.BRANCH_CODE;
                string CNIC = "";
                if (lstPrimaryDocumentType.SelectedItem.Text == "CNIC/SNIC")
                    CNIC = txtCnic.Text;
                string CustomerType = "0";
                string CustomerTitle = lstTitle.SelectedItem.Text;
                string CustomerFirstName = txtFirstName.Text;
                string CustomerMiddleName = txtMiddleName.Text;
                string CustomerLastName = txtLastName.Text;
                string LegalName = "";
                string InstituteName = "NBP";
                string InstituteStartDate = "1949-11-09";
                string Gender = g.GetProfileCodeOfGender(lstGender.SelectedItem.Text);
                string DateOfBirth = "";
                try
                {
                    DateOfBirth = DateTime.ParseExact(txtDOB.Text, "dd-mm-yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-mm-dd");
                }
                catch (Exception e)
                {
                    DateOfBirth = "1992-03-01";
                }

                string Industry = "";
                string TaxIdentifierFormat = "";
                string TaxIdentificationNumber = OiTxtNTN.Text;
                string Occupation = (EiListEmployDetail.SelectedItem.Text.Length > 17) ? EiListEmployDetail.SelectedItem.Text.Substring(0, 17) : EiListEmployDetail.SelectedItem.Text;
                string CustomerCreationDate = DateTime.Now.ToString("yyyy-MM-dd");
                string ResidenceCountry = c.GetCountryProfileCode(lstCOR.SelectedItem.Text);
                string PrimaryCitznCountry = c.GetCountryProfileCode(lstCOR.SelectedItem.Text);
                var SecList = lstNationality.Items.Cast<ListItem>().Where(i => i.Selected == true && i.Text != lstCOR.SelectedItem.Text).Select(i => new Nationality { CountryID = Convert.ToInt32(i.Value), Country = i.Text }).ToList();

                string SecondryCitznCountry = "";
                if (SecList.Count > 0)
                    SecondryCitznCountry = c.GetCountryProfileCode(SecList[0].Country);
                string DocumetsVerifiedFlag = "";
                if (chkDocument.Checked)
                    DocumetsVerifiedFlag = "Y";
                else
                    DocumetsVerifiedFlag = "N";
                string Jurisdiction = "PAK";
                string CountryOfRelationship = "";
                string RelationType = lstResident.SelectedItem.Value;
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
                string PostalCode = CiTxtPotalCode.Text;
                string AddressCountry = c.GetCountryProfileCode(CiListCountryCode.SelectedItem.Text);
                string Zmobile = "";
                string HPH = "";
                string BPH = "";
                string PhoneType = "";
                if (CiMobileNo.Text.Length > 0)
                {
                    PhoneType = "M";
                    Zmobile = CiMobileNo.Text;
                }
                else if (CiContactNoResidence.Text.Length > 0)
                {
                    PhoneType = "H";
                    HPH = CiContactNoResidence.Text;
                }
                else
                {
                    PhoneType = "B";
                    BPH = CiContactNoOffice.Text;
                }

                string PhoneExtension = "";
                SourceOfFunds sof = new SourceOfFunds();
                string SourceType = sof.GetProfileCodeIndividual(Convert.ToInt32(MiListSOF.SelectedItem.Value));
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

        protected void btnBioMetricVerify_Click(object sender, EventArgs e)
        {
            if (txtCnic.Text.Length > 0)
            {
                clsSkillOrbitObject obj = new clsSkillOrbitObject();
                User LoggedUser = Session["User"] as User;
                obj.CNIC = txtCnic.Text.Replace("-",string.Empty);
                obj.TOTAccount = "Saving";
                obj.ContactNumber = BmTxtContactNo.Text;
                obj.UserId = LoggedUser.USER_ID.ToString();
                obj.BranchCode = LoggedUser.Branch.BRANCH_CODE;
                obj.NameOfArea = LoggedUser.Branch.AREA;

                Session["clsSkillOrbitObject"] = obj;
                Response.Redirect("BioMetric.aspx");
            }
            else
                CnicBiometric.Visible = true;

           

        }

        protected void txtCnic_TextChanged(object sender, EventArgs e)
        {
            CnicBiometric.Visible = false;
        }

        protected void MiListPoliticExposed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MiListPoliticExposed.SelectedItem.Value != "0" && MiListPoliticExposed.SelectedItem.Value != "1")
            {
                MiRadPepSingle.Enabled = true;
                MiRadPepSingle.Checked = true;
                MiRadPepLinked.Enabled = true;

                MiPepRelation.Enabled = true;
                RequiredFieldValidatorMiPepRelation.Enabled = true;

                MiTxtPED.Enabled = true;
                RequiredFieldValidatorMiPepDesc.Enabled = true;

            }
            else
            {
                MiRadPepSingle.Enabled = true;
                MiRadPepSingle.Checked = false;

                MiRadPepLinked.Enabled = true;
                MiRadPepLinked.Enabled = false;

                MiPepRelation.Enabled = false;
                RequiredFieldValidatorMiPepRelation.Enabled = false;

                MiTxtPED.Enabled = false;
                RequiredFieldValidatorMiPepDesc.Enabled = false;
            }
        }

        protected void CustomValidatorDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                var newDate = DateTime.ParseExact(txtDOB.Text,
                                   "dd-MM-yyyy",
                                    CultureInfo.InvariantCulture);
                args.IsValid = true;
            }
            catch 
            {
                args.IsValid = false;
            }
            
        }

        protected void PiListPhoneNoUsa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PiListPhoneNoUsa.SelectedItem.Text.Trim() == "YES")
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

        protected void CustomValidatorNonResident_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (lstResident.SelectedItem.Text.Trim() == "RESIDENT")
                args.IsValid = true;
            else
            {
                if (lstPrimaryDocumentType.SelectedItem.Text.Trim() == "Passport")
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
        }

        protected void CiListCountryCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CiListCountryCode.SelectedItem.Text.Trim() != "PAKISTAN")
            {
                RequiredFieldValidatorPermanentProvice.Enabled = false;
                RequiredFieldValidatorPermenentCity.Enabled = false;
            }
            else
            {
                RequiredFieldValidatorPermanentProvice.Enabled = true;
                RequiredFieldValidatorPermenentCity.Enabled = true;
            }
        }

        protected void CiListCountryCodePre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CiListCountryCode.SelectedItem.Text.Trim() != "PAKISTAN")
            {
                RequiredFieldValidatorProvicePreset.Enabled = false;
                RequiredFieldValidatorCityPresent.Enabled = false;
            }
            else
            {
                RequiredFieldValidatorProvicePreset.Enabled = true;
                RequiredFieldValidatorCityPresent.Enabled = true;
            }
        }

        protected void EiListEmpGrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EiListEmpGrp.SelectedIndex != 0)
            {
                EmployerSubGroup ESG = new EmployerSubGroup();
                EmployerGroup EG = new EmployerGroup();

                EiListEmpSubGrp.DataSource = ESG.GetEmpoyerSubGroup(EG.GetZEMPGRP(Convert.ToInt32(EiListEmpGrp.SelectedItem.Value)));
                EiListEmpSubGrp.DataValueField = "ID";
                EiListEmpSubGrp.DataTextField = "Name";
                EiListEmpSubGrp.DataBind();
                EiListEmpSubGrp.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {

                EiListEmpSubGrp.DataSource = new List<EmployerSubGroup>();
                EiListEmpSubGrp.DataValueField = "ID";
                EiListEmpSubGrp.DataTextField = "Name";
                EiListEmpSubGrp.DataBind();
                EiListEmpSubGrp.Items.Insert(0, new ListItem("Select", "0"));

                EiListEmpNum.DataSource = new List<EmployerCode>();
                EiListEmpNum.DataValueField = "ID";
                EiListEmpNum.DataTextField = "Name";
                EiListEmpNum.DataBind();
                EiListEmpNum.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        protected void EiListEmpSubGrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EiListEmpSubGrp.SelectedIndex != 0)
            {
                EmployerSubGroup ESG = new EmployerSubGroup();
                EmployerCode EC = new EmployerCode();

                EiListEmpNum.DataSource = EC.GetEmployerCode(ESG.GetZEMPGRP(Convert.ToInt32(EiListEmpSubGrp.SelectedItem.Value)), ESG.GetZEMPSUBG(Convert.ToInt32(EiListEmpSubGrp.SelectedItem.Value)));
                EiListEmpNum.DataValueField = "ID";
                EiListEmpNum.DataTextField = "Name";
                EiListEmpNum.DataBind();
                EiListEmpNum.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        protected void EiListEmployDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmploymentDetail ED = new EmploymentDetail();

            if (ED.GetEmpSubgMand(Convert.ToInt32(EiListEmployDetail.SelectedItem.Value)))
            {
                EiReqValidatorEGrp.Enabled = true;
                EiReqValidatorESubGrp.Enabled = true;
                EiReqValidatorENum.Enabled = true;
            }
            else
            {
                EiReqValidatorEGrp.Enabled = false;
                EiReqValidatorESubGrp.Enabled = false;
                EiReqValidatorENum.Enabled = false;
            }
        }

        




        // Changing ne requirments Feburary 11 2016
        //protected void lstPrimaryDocumentType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ListItem PDType = lstPrimaryDocumentType.SelectedItem;
        //    if (PDType.Text == "CNIC")
        //    {
        //        txtCnic.MaxLength = 15;
        //        RegularExpressionValidatorCnic.Enabled = true;
        //        CustomValidatorCNIC.Enabled = true;
        //    }
        //}
    }
}