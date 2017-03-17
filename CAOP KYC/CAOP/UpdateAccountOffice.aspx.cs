using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using ExtensionMethods;

namespace CAOP
{
    public partial class UpdateAccountOffice : System.Web.UI.Page
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
                        loaddata();
                        loadBusinessCif();

                        documentRequiresLoad();
                    }

                }
                else
                {
                    if (!IsPostBack)
                    {
                        loaddata();
                        loadBusinessCif();
                        documentRequiresLoad();

                        Session["BID"] = queryid;
                        SetData();
                        SetDataOpen(queryid);
                        //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ShowAllAccountOpenIndividual()", true);

                        // CIF cif = new CIF(LoggedUser.USER_ID);
                        AccOpen ac = new AccOpen(LoggedUser.USER_ID);


                        if (ac.CheckStatus(queryid, Status.REJECTEBY_COMPLIANCE_MANAGER.ToString()))
                        {
                            rev.Visible = true;
                            rev.Reviewer = false;
                            SetUpdateBtnVisible();
                            SetCifSubmitVisible();
                            btnGridAddCif.Visible = true;
                            WhoBtnAddGrid.Visible = true;
                            BtnAddBGrid.Visible = true;

                        }
                    }
                    // else
                    //{
                    //   // ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showall()", true);
                    //    String mesg = "null";
                    //     CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);

                    //}
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
                        loaddata();
                        loadBusinessCif();
                        documentRequiresLoad();
                        SetData();
                        SetDataOpen(queryid);
                        rev.Visible = true;
                        rev.Reviewer = false;
                        AccOpen ac = new AccOpen(LoggedUser.USER_ID);
                        //CIF cif = new CIF(LoggedUser.USER_ID);


                        if (ac.CheckStatus(queryid, Status.APPROVED_BY_COMPLIANCE_MANAGER.ToString()))
                            rev.Visible = false;

                        if (ac.CheckStatus(queryid, new string[] { Status.SUBMITTED.ToString(), Status.REJECTED_BY_BRANCH_MANAGER.ToString() }))
                        {
                            rev.Reviewer = true;
                        }


                    }
                    else
                    {
                        String mesg = "null";
                        CheckAccountIndividualTab(queryid, mesg);
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
                        loaddata();
                        loadBusinessCif();
                        documentRequiresLoad();
                        SetData();
                        SetDataOpen(queryid);
                        rev.Visible = true;
                        rev.Reviewer = false;
                        AccOpen ac = new AccOpen(LoggedUser.USER_ID);
                        //CIF cif = new CIF(LoggedUser.USER_ID);


                        if (ac.CheckStatus(queryid, Status.APPROVED_BY_COMPLIANCE_MANAGER.ToString()))
                            rev.Reviewer = true;




                    }
                    else
                    {
                        String mesg = "null";
                        CheckAccountIndividualTab(queryid, mesg);
                    }
                }

            }
        }

        private void SetUpdateBtnVisible()
        {
            btnUpdateAc.Visible = true;
            btnUpdateCi.Visible = true;
            btnUpdateAuth.Visible = true;
            btnUpdateCd.Visible = true;
            btnUpdateDr.Visible = true;
            btnUpdateAu.Visible = true;
            btnUpdateKn.Visible = true;
            WhoBtnUpdate.Visible = true;


        }

        private void SetCifSubmitVisible()
        {
            btnSubmitACa.Visible = true;
            btnSubmitACb.Visible = true;
            btnSubmitACc.Visible = true;
            btnSubmitACe.Visible = true;
            btnSubmitACf.Visible = true;
            btnSubmitACg.Visible = true;
            btnSubmitACh.Visible = true;
            WhoBtnSubmit.Visible = true;
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


        private void documentRequiresLoad()
        {
            User LoggedUser = Session["User"] as User;
            DescriptionDocuments d = new DescriptionDocuments();
            DcGrid.DataSource = d.GetDescpDocBuss();
            DcGrid.DataBind();


        }

        private void loadBusinessCif()
        {

            User LoggedUser = Session["User"] as User;
            //   SearchCIDModal.Focus();

            CIF cf = new CIF(LoggedUser.USER_ID);
            SearchBusinessCif.DataSource = cf.GetCifsForAccounts(Status.APPROVED_BY_BRANCH_MANAGER);
            SearchBusinessCif.DataBind();

        }
        private void loaddata()
        {
            User LoggedUser = Session["User"] as User;
            //   SearchCIDModal.Focus();

            CIF cf = new CIF(LoggedUser.USER_ID);

            //dt.Columns.Add("Id", typeof(int));
            //dt.Columns.Add("CiF Type", typeof(string));
            //dt.Columns.Add("Name", typeof(string));
            //dt.Columns.Add("CNIC", typeof(string));

            //List<BasicInformations> list = cf.GetCifsForAccounts("SUBMITTED");

            //foreach (var i in list)
            //{
            //    dt.Rows.Add(i.ID, i.CIF_TYPE, i.NAME, i.CNIC);
            //}
            searchCIF.DataSource = cf.GetCifsForAccounts(Status.APPROVED_BY_BRANCH_MANAGER);
            searchCIF.DataBind();
        }

        private void SetDataOpen(int id)
        {
            SetAccountNatureOpen(id);
            SetContactInfoOpen(id);
            SetAuthorizedPersonsOpen(id);
            SetOperatingInstructionOpen(id);
            SetKnowYourCustomerOpen(id);
            SetCertDepositOpen(id);
            SetDocumentRequiredOpen(id);
            SetWhoAuthorized(id);



            String mesg = "null";
            CheckAccountIndividualTab(id, mesg);


        }

        private void SetWhoAuthorized(int id)
        {
            WhoAuthorized wa = new WhoAuthorized();
            if (wa.GetWhoAuthorized(id))
            {


                Session["WhoGrid"] = wa.Cifs;
                GridWhoCifs.DataSource = wa.Cifs;
                GridWhoCifs.DataBind();

                WhoBtnSave.Visible = false;
                WhoBtnAddGrid.Visible = false;
            }
        }

        private void SetDocumentRequiredOpen(int id)
        {
            DocumentsRequired d = new DocumentsRequired();
            if (d.Get(id))
            {
                DocumentsList dl = new DocumentsList();

                List<DocumentsList> list = dl.GetDocumentList(id);
                GridView dd = DcGrid;
                //   GridViewRow r = DcGrid.Rows[0];


                if (d.DESCRIPTION == true)
                {
                    int count1 = 0;
                    foreach (GridViewRow row in DcGrid.Rows)
                    {
                        RadioButton rb1 = (RadioButton)row.FindControl("DcRadio1");
                        RadioButton rb2 = (RadioButton)row.FindControl("DcRadio2");
                        RadioButton rb3 = (RadioButton)row.FindControl("DcRadio3");

                        rb1.Checked = false;
                        rb2.Checked = false;
                        rb3.Checked = false;


                        if (list[count1].value == "Yes")
                        {
                            rb1.Checked = true;
                        }
                        else if (list[count1].value == "No")
                        {
                            rb2.Checked = true;
                        }
                        else
                        {
                            rb3.Checked = true;
                        }
                        count1++;


                    }
                    //for (int j = 0; j < list.Count; j++)
                    //{
                    //    RadioButton rb1 = (RadioButton)DcGrid.Rows[j].FindControl("DcRadio1");
                    //    RadioButton rb2 = (RadioButton)DcGrid.Rows[j].FindControl("DcRadio2");
                    //    RadioButton rb3 = (RadioButton)DcGrid.Rows[j].FindControl("DcRadio3");
                    //    if (list[j].value == "Yes")
                    //    {
                    //        rb1.Checked = true;
                    //    }
                    //    else if (list[j].value == "No")
                    //    {
                    //        rb2.Checked = true;
                    //    }
                    //    else
                    //    {
                    //        rb3.Checked = true;
                    //    }

                    //}
                }
                //if (d.DESCRIPTION == true)
                //{
                //    GridView dd = DcGrid;
                //    RadioButton rb1 = (RadioButton)dd.FindControl("DcRadio1");

                //   c
                //    RadioButton rb3 = (RadioButton)dd.FindControl("DcRadio3");
                //    foreach (var i in dl.GetDocumentList(id))
                //    {

                //        //if (i.value == "Yes")
                //        //{
                //        //    rb1.Checked = true;
                //        //}
                //        //else
                //        //    rb1.Checked = false;
                //        //if (i.value == "No")
                //        //{
                //        //    rb2.Checked = true;
                //        //}
                //        //if (i.value == "N/A")
                //        //{
                //        //    rb3.Checked = true;
                //        //}


                //    }


                //}

                DrSubmitButton.Visible = false;



                //if (d.DESCRIPTION == true)
                //{
                //    DocumentsList dl = new DocumentsList();
                //    GridView dd = DcGrid;
                //    RadioButton rb1 = (RadioButton)dd.FindControl("DcRadio1");

                //    RadioButton rb2 = (RadioButton)dd.FindControl("DcRadio2");

                //    RadioButton rb3 = (RadioButton)dd.FindControl("DcRadio3");
                //    foreach (var i in dl.GetDocumentList(id))
                //    {

                //        //if (i.value == "Yes")
                //        //{
                //        //    rb1.Checked = true;
                //        //}
                //        //else
                //        //    rb1.Checked = false;
                //        //if (i.value == "No")
                //        //{
                //        //    rb2.Checked = true;
                //        //}
                //        //if (i.value == "N/A")
                //        //{
                //        //    rb3.Checked = true;
                //        //}


                //    }


                //}

                // DrSubmitButton.Visible = false;
            }

        }


        private void SetCertDepositOpen(int id)
        {
            AccountCertDepositInfo a = new AccountCertDepositInfo();
            if (a.Get(id))
            {
                CdExpDate.Text = a.EXPIRY_DATE;
                CdCertPeriod.Text = a.CERTIFICATE_PERIOD;
                SetRadioButton(a.AUTO_ROLL_ON_EXPIRY, AutoRollExpiryRadio1, AutoRollExpiryRadio2);
                CdSpecialInstr.Text = a.SPECIAL_INSTR_ANY;
                CdListProfitAccount.ClearSelection();
                ListExtensions.SetDropdownValue(a.PROFIT_ACCOUNT_TYPE.ID, CdListProfitAccount);
                CdProfitAccountNumber.Text = a.PROFIT_ACCOUNT_NUMBER;
                CdListTransactionType.ClearSelection();
                ListExtensions.SetDropdownValue(a.TRANSACTION_TYPE.ID, CdListTransactionType);
                CdChequePrefix.Text = a.CHEQUE_PREFIX;
                ChChequeNumber.Text = a.CHEQUE_NUMBER;
                CdCertNumber.Text = a.CERTIFICATE_NUMBER;
                CdCertAmount.Text = a.CERTIFCATE_AMOUNT;
                CdMarkupRate.Text = a.MARK_UP_RATE;
                CdLstPrincipalRenewal.ClearSelection();
                ListExtensions.SetDropdownValue(a.PRINCIPAL_RENEWAL_OPTION.ID, CdLstPrincipalRenewal);

                CdSubmitButton.Visible = false;

            }
        }

        private void SetKnowYourCustomerOpen(int id)
        {
            AccountKnowYourCustomer a = new AccountKnowYourCustomer();
            if (a.GetKnowYourCustomer(id))
            {
                KnListCustomerType.ClearSelection();
                ListExtensions.SetDropdownValue(a.CUSTOMER_TYPE.ID, KnListCustomerType);
                KnListRAC.ClearSelection();
                ListExtensions.SetDropdownValue(a.RAC.ID, KnListRAC);
                knTextRACDetail.Text = a.RAC_DETAIL;
                KnDescrIfRefered.Text = a.DESCRIPTION_IF_REFFERED;

                KnListEducation.ClearSelection();
                ListExtensions.SetDropdownValue(a.EDUCATION.ID, KnListEducation);
                //  ListExtensions.SetDropdownValue(a.PURPOSE_OF_ACCOUNT.ID, KnListPurposeOfAccount);
                foreach (var p in a.PURPOSE_OF_ACCOUNT)
                {
                    KnListPurposeOfAccount.Items.FindByValue(p.ToString()).Selected = true;
                }
                KnDescrOther.Text = a.DESCRIPTION_IF_OTHER;
                //   ListExtensions.SetDropdownValue(a.SOURCE_OF_FUNDS.ID, KnListSourceOfFunds);
                foreach (var s in a.SOURCE_OF_FUNDS)
                {
                    KnListSourceOfFunds.Items.FindByValue(s.ToString()).Selected = true;
                }
                KnDescrOfSource.Text = a.DESCRIPTION_OF_SOURCE;
                SetRadioButton(a.SERVICE_CHARGES_EXEMPTED, ServiceExemptedRadio1, ServiceExemptedRadio2);
                KnListSerExemptCode.ClearSelection();
                ListExtensions.SetDropdownValue(a.SERVICE_CHARGES_EXEMPTED_CODE.ID, KnListSerExemptCode);
                KnReasonExempted.Text = a.REASON_IF_EXEMPTED;
                KnExpectedMonthlyIncome.Text = a.EXPECTED_MONTHLY_INCOME;

                //   BdListBusinessInCities.Items
                //     .Cast<ListItem>()
                //   .Where(c => b.BusinessCities.Where(i => i.ID == Convert.ToInt32(c.Value)).Any())
                // .Select(c => c.Selected = true);

                if (a.MODE_OF_TRANSACTIONS == true)
                {
                    Know_Customer_Transaction_mode k = new Know_Customer_Transaction_mode();
                    foreach (var i in k.GetDocumentList(id))
                    {
                        KnListModeOfTransaction.Items.FindByValue(i.MODE_OF_TRANSACTIONS.ID.ToString()).Selected = true;

                    }
                    //KnListModeOfTransaction.Items.Cast<ListItem>().Where(c => a.MODE_OF_TRANSACTIONS)

                }
                KnOtherModeTrans.Text = a.OTHER_MODE_OF_TRANSACTIONS;
                KnMaxAmountDR.Text = a.MAX_TRANS_AMOUNT_DR;
                KnMaxAmountCR.Text = a.MAX_TRANS_AMOUNT_CR;
                // KnRelationshipManager.Text = a.RELATIONSHIP_MANAGER;
                ListExtensions.SetDropdownValue(a.RELATIONSHIP_MANAGER.ID, KnddlManager);

                SetRadioButton(a.OCCUPATION_VERIFIED, OccupyVerifyRadio1, OccupyVerifyRadio2);
                // ListExtensions.SetDropdownValue(a.ADDRESS_VERIFIED.ID, KnListAddresVerified);
                if (RadioButtonAddressVerifiedYes.Checked)
                    a.ADDRESS_VERIFIED = 1;
                else
                    a.ADDRESS_VERIFIED = 0;
                KnListMeansOfVerification.ClearSelection();
                ListExtensions.SetDropdownValue(a.MEANS_OF_VERIFICATION.ID, KnListMeansOfVerification);
                KnMeanVerifyOther.Text = a.MEANS_OF_VERI_OTHER;
                SetRadioButton(a.IS_VERI_SATISFACTORY, IsVeriSatiRadio1, IsVeriSatiRadio2);
                KnDetailNotSatis.Text = a.DETAIL_IF_NOT_SATISFACTORY;
                KnListCountHomeRemit.ClearSelection();
                ListExtensions.SetDropdownValue(a.COUNTRY_HOME_REMITTANCE.ID, KnListCountHomeRemit);
                KnListRealBenef.ClearSelection();
                ListExtensions.SetDropdownValue(a.REAL_BENEFICIARY_ACCOUNT.ID, KnListRealBenef);
                KnNameOther.Text = a.NAME_OTHER;
                KnCnicOther.Text = a.CNIC_OTHER;
                //  ListExtensions.SetDropdownValue(a.RELATIONSHIP_WITH_ACCOUNTHOLDER.ID, KnListRelationAccountHolder);
                //   KnRelationDetailOther.Text = a.RELATIONSHIP_DETAIL_OTHER;

                foreach (var p in a.KYC_EXPECTED_COUNTER_PARTIES)
                {
                    KnListECP.Items.FindByValue(p.ToString()).Selected = true;
                }

                foreach (var p in a.KYC_GEOGRAPHIES_COUNTER_PARTIES)
                {
                    KnListGCP.Items.FindByValue(p.ToString()).Selected = true;
                }
                KnNODT.Text = a.NODT;
                KnPEDT.Text = a.PEDT;
                KnNOCT.Text = a.NOCT;
                KnPECT.Text = a.PECT;
                KntxtDescETCP.Text = a.ETCP_OTHER;
                KntxtDescGCP.Text = a.GICP_OTHER;

                KnSubmitButton.Visible = false;

                GrdBeneficial.DataSource = a.KycBeneficial;
                GrdBeneficial.DataBind();
                Session["KycBeneficialEntity"] = a.KycBeneficial;

                BtnAddBGrid.Visible = false;

                if (KnListRealBenef.SelectedItem.Text == "OTHER")
                {
                    CustomValidatorBeneficial.Enabled = true;
                }
                else
                {
                    CustomValidatorBeneficial.Enabled = false;
                }

                bool Other = KnListModeOfTransaction.Items.FindByValue("6").Selected == true ? true : false;
                if (Other)
                    ReqValidatorMotOther.Enabled = true;
                else
                    ReqValidatorMotOther.Enabled = false;

                Other = KnListSourceOfFunds.Items.FindByText("Others (specify)").Selected == true ? true : false;
                if (Other)
                    ReqValidatorSourceDesc.Enabled = true;
                else
                    ReqValidatorSourceDesc.Enabled = false;

                Other = KnListPurposeOfAccount.Items.FindByText("Others (specify)").Selected == true ? true : false;
                if (Other)
                    ReqValidatorPurposeAccountOther.Enabled = true;
                else
                    ReqValidatorPurposeAccountOther.Enabled = false;

            }
        }


        private void SetAuthorizedPersonsOpen(int id)
        {
            AccountAuthorizedPerson a = new AccountAuthorizedPerson();
            if (a.Get(id))
            {
                //   AuthCustomerCif.Text = a.CIF_NO;
                //   AuthCustomerName.Text = a.NAME;
                //    AuthCustomerCNIC.Text = a.CNIC;
                //    SetRadioButton(a.APPLICANT_IN_NEGATIVE_LIST, AuthApplicantNegativeRadio1, AuthApplicantNegativeRadio2);
                //    SetRadioButton(a.POWER_OF_ATTORNY, AuthPowerAttornyRadio1, AuthPowerAttornyRadio2);
                //    SetRadioButton(a.SIGNATURE_AUTHORITY, AuthSignatureRadio1, AuthSignatureRadio2);
                //     ListExtensions.SetDropdownValue(a.APPLICANT_STATUS.ID, AuthListApplicantStatus);

                Session["GridCif"] = null;
                Session["GridCif"] = a.Cifs;
                GridViewAccountCifs.DataSource = a.Cifs;
                GridViewAccountCifs.DataBind();
                btnGridAddCif.Visible = false;

                AuthSubmitButton.Visible = false;

            }
        }

        private void SetContactInfoOpen(int id)
        {
            AccountContactInfo a = new AccountContactInfo();
            if (a.Get(id))
            {
                CiCustomerCif.Text = a.CIF_NO;
                CiName.Text = a.NAME;
                CiNationTaxNo.Text = a.NATIONAL_TAXNO;
                CiSalesTaxNo.Text = a.SALES_TAXNO;
                CiRegistrationNo.Text = a.REGISTRATION_NO;

                CiListRegistrationIssueAgency.ClearSelection();
                ListExtensions.SetDropdownValue(a.REGISTRATION_ISSUING_AGENCY.ID, CiListRegistrationIssueAgency);

                CiListNatureOfBusiness.ClearSelection();
                ListExtensions.SetDropdownValue(a.NATURE_OF_BUSINESS.ID, CiListNatureOfBusiness);

                CiListCountry.ClearSelection();
                ListExtensions.SetDropdownValue(a.COUNTRY.ID, CiListCountry);

                CiListCity.ClearSelection();
                ListExtensions.SetDropdownValue(a.CITY.ID, CiListCity);


                CiTxtBuilding.Text = a.BUILDING;
                CiTxtFloor.Text = a.FLOOR;
                CiTxtDistrict.Text = a.DISTRICT;
                CiTxtStreet.Text = a.STREET;
                CiPoBox.Text = a.PO_BOX;
                CiPostalCode.Text = a.POSTAL_CODE;

                CiTelResidence.Text = a.TEL_RESIDENCE;
                CiTelOffice.Text = a.TEL_OFFICE;
                CiMobileNo.Text = a.MOBILE_NO;
                CiFaxNo.Text = a.FAX_NO;

                CiListSmsAlert.ClearSelection();
                ListExtensions.SetDropdownValue((int?)a.SMS_ALERT_REQUIRED.ID, CiListSmsAlert);

                CiListProvince.ClearSelection();
                ListExtensions.SetDropdownValue(a.PROVINCE.ID, CiListProvince);
                CiWebAddress.Text = a.WEB_ADDRESS_URL;

                if (a.EMAIL == true)
                {
                    Emails e = new Emails();
                    if (e.GetEmail(id))
                    {
                        CiEmail.Text = e.EMAIL;
                        if (e.REQUIRED_ESTATEMEN == true)
                        {
                            CiRequiredEstateCheckbox.Checked = true;
                        }
                        else
                        {

                            CiRequiredEstateCheckbox.Checked = false;
                        }
                    }
                }
                else
                {
                    CiEmail.Text = "";
                    CiRequiredEstateCheckbox.Checked = false;
                }

                CiGroupCif.Text = a.GROUP_CIF_NO;
                CiGroupName.Text = a.GROUP_NAME;

                CiSubmitButton.Visible = false;

            }
        }

        private void SetOperatingInstructionOpen(int id)
        {
            OperatingInstructions o = new OperatingInstructions();
            if (o.GetOperatingInstruction(id))
            {
                AuListAuthority.ClearSelection();
                ListExtensions.SetDropdownValue(o.AUTHORITY_TO_OPERATE.ID, AuListAuthority);


                AuDescriptionOther.Text = o.DESCRIPTION_IF_OTHER;
                SetRadioButton(o.ZAKAT_DEDUCTION, ZakatDeductionRadio1, ZakatDeductionRadio2);

                AuListZakatExemption.ClearSelection();
                ListExtensions.SetDropdownValue(o.ZAKAT_EXEMPTION_TYPE.ID, AuListZakatExemption);
                AuExempReasonDetail.Text = o.EXEMPTION_REASON_DETAIL;
                AuListAccountFrequenct.Items.FindByValue(o.ACCOUNT_STATEMENT_FREQUENCY.ID.ToString()).Selected = true;
                AuDescrHoldMail.Text = o.DESCRIPTION_IF_HOLD_MAIL;
                SetRadioButton(o.ATM_CARD_REQUIRED, AtmRequiredRadio1, AtmRequiredRadio2);
                AuCustomerNameAtm.Text = o.CUSTOMER_NAME_ON_ATMCARD;

                AuListEstatement.ClearSelection();
                ListExtensions.SetDropdownValue(o.E_STATEMENT_REQUIRED.ID, AuListEstatement);
                SetRadioButton(o.MOBILE_BANKING_REQUIRED, MobileBankRequirRadio1, MobileBankRequirRadio2);
                AuMobileNo.Text = o.MOBILE_NO;
                SetRadioButton(o.IBT_ALLOWED, IBTAllowRadio1, IBTAllowRadio2);
                SetRadioButton(o.IS_PROFIT_APPLICABLE, IsProfitAppRadio1, IsProfitAppRadio2);
                SetRadioButton(o.IS_FED_EXEMPTED, IsFedRadio1, IsFedRadio2);
                AuExpDateExempted.Text = o.EXPIRY_DATE_EXEMPTED;
                if (o.APPLICABLE_PROFIT_RATE == "Bank Rate")
                {

                    AppProfitRateRadio1.Checked = true;
                }
                else
                {
                    AppProfitRateRadio2.Checked = true;
                }

                AuSpecicalProfitValue.Text = o.SPECIAL_PROFIT_VALUE;

                AuListProfitPayment.ClearSelection();
                ListExtensions.SetDropdownValue(o.PROFIT_PAYMENT.ID, AuListProfitPayment);
                SetRadioButton(o.WHT_DEDUCTED_ON_PROFIT, WhtProfitRadio1, WhtProfitRadio2);
                AuExpDateProfit.Text = o.EXPIRY_DATE_PROFIT;
                SetRadioButton(o.WHT_DEDUCTED_ON_TRANSACTION, WhtTransactionRadio1, WhtTransactionRadio2);
                AuExpDateTrans.Text = o.EXPIRY_DATE_TRANSACTION;

                if (WhtProfitRadio1.Checked)
                    RequiredFieldValidatorExpProfit.Enabled = false;

                if (WhtTransactionRadio1.Checked)
                    RequiredFieldValidatorExpTrans.Enabled = false;

                AuSubmitButton.Visible = false;

            }
        }

        private void SetRadioButton(bool? b, RadioButton r1, RadioButton r2)
        {
            r1.Checked = false;
            r2.Checked = false;

            if (b == true)
            {
                r1.Checked = true;
            }
            else
            {
                r2.Checked = true;
            }
        }


        private void SetAccountNatureOpen(int id)
        {
            AccountNatureCurrency a = new AccountNatureCurrency();
            if (a.GetAccountNature(id))
            {
                if (a.CNIC_VERIFIED == true)
                {
                    AcCnicVerifiedCheck.Checked = true;
                }
                else
                    AcCnicVerifiedCheck.Checked = false;

                AcDdlProducts.ClearSelection();
                ListExtensions.SetDropdownValue(a.PRODUCT.ID, AcDdlProducts);
            //    ListExtensions.SetDropdownValue(a.ACCOUNT_OPEN_TYPE.ID, AcListAccountOpen);
                AcEntryDate.Text = a.ACCOUNT_ENTRY_DATE.ToString("yyyy-MM-dd");
                // ListExtensions.SetDropdownValue(a.GL_CODE.ID, AcListGlCode);
                // ListExtensions.SetDropdownValue(a.SL_CODE.ID, AcListSlCode);

                //AccountType at = new AccountType();
                //AcListAccountType.DataSource = at.GetAccountTypesByVal(Convert.ToInt32(a.ACCOUNT_TYPE.ID));
                //AcListAccountType.DataValueField = "ID";
                //AcListAccountType.DataTextField = "NAME";
                //AcListAccountType.DataBind();
                //AcListAccountType.Items.Insert(0, new ListItem("Select", "0"));

                //string clsgrp = at.GetAccountTypeClsGrp(a.ACCOUNT_TYPE.ID);
                //AccountGroup ag = new AccountGroup();
                //AcListAccountGroup.DataSource = ag.GetAccountGroupTypes(clsgrp.Split(',')[0], clsgrp.Split(',')[1]);
                //AcListAccountGroup.DataValueField = "ID";
                //AcListAccountGroup.DataTextField = "NAME";
                //AcListAccountGroup.DataBind();

                //clsgrp = ag.GetCls(Convert.ToInt32(AcListAccountGroup.SelectedItem.Value));
                //AccountClass ac = new AccountClass();
                //AcListAccountClass.DataSource = ac.GetAccountClassTypes(clsgrp);
                //AcListAccountClass.DataValueField = "ID";
                //AcListAccountClass.DataTextField = "NAME";
                //AcListAccountClass.DataBind();


                //AcListAccountClass.Enabled = false;
                // AcListAccountGroup.Enabled = false;
                AcListAccountType.ClearSelection();
                ListExtensions.SetDropdownValue(a.ACCOUNT_TYPE.ID, AcListAccountType);

                AccountModes am = new AccountModes();
                AcListAccountMode.DataSource = am.GetAccountModesBusiness(Convert.ToInt32(AcListAccountType.SelectedItem.Value));
                AcListAccountMode.DataValueField = "ID";
                AcListAccountMode.DataTextField = "NAME";
                AcListAccountMode.DataBind();
                AcListAccountMode.Items.Insert(0, new ListItem("Select", "0"));
                AcListAccountMode.ClearSelection();
                ListExtensions.SetDropdownValue(a.ACCOUNT_MODE_DETAIL, AcListAccountMode);

                AcListCurrency.ClearSelection();
                ListExtensions.SetDropdownValue(a.CURRENCY.ID, AcListCurrency);
                AcAccountNumber.Text = a.ACCOUNT_NUMBER;
                AcAccountTitle.Text = a.ACCOUNT_TITLE;
                AcInitialDeposit.Text = a.INITIAL_DEPOSIT;
                if (a.ACCOUNT_PURPOSE != null)
                    ListExtensions.SetDropdownValue(a.ACCOUNT_PURPOSE, AcListPOA);
                
                if (a.ACCOUNT_MODE == true)
                {
                    AcAccountModeRadio1.Checked = true;
                }
                else
                {
                    AcAccountModeRadio2.Checked = true;

                }

                //if (a.MINOR_ACCOUNT == true)
                //{
                //    AcMinorAccountRadio1.Checked = true;
                //}
                //else
                //{
                //    AcMinorAccountRadio2.Checked = true;
                //}

                AcSubmitButton.Visible = false;
            }
        }


        private void CheckAccountIndividualTab(int id, String mesg)
        {
            AccountContactInfo ac = new AccountContactInfo();
            AccountAuthorizedPerson ap = new AccountAuthorizedPerson();

            OperatingInstructions o = new OperatingInstructions();
            AccountKnowYourCustomer ak = new AccountKnowYourCustomer();
            AccountCertDepositInfo ad = new AccountCertDepositInfo();
            DocumentsRequired d = new DocumentsRequired();
            WhoAuthorized wa = new WhoAuthorized();
            String contact = null;
            String auth = null;

            String op = null;

            String know = null;
            String cert = null;
            String doc = null;
            string who = null;


            if (ac.CheckAccountContactInfo(id))
            {
                contact = "1";
            }
            if (ap.CheckIndividualAuthorizedPerson(id))
            {
                auth = "1";
            }
            if (o.CheckIndividualOperatinInstruction(id))
            {
                op = "1";
            }
            if (ak.CheckIndividualKnowYourCustomer(id))
            {
                know = "1";
            }
            if (ad.CheckCertDeposit(id))
            {
                cert = "1";
            }
            if (d.CheckDocumentRequired(id))
            {
                doc = "1";
            }
            if (wa.CheckWhoAuthorized(id))
            {
                who = "1";
            }

            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "BusinessAccountOpenPendingAlert('" + contact + "','" + auth + "','" + op + "','" + know + "','" + cert + "','" + doc + "','" + who + "','" + mesg + "');", true);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ShowAllAccountOpenBusiness()", true);

        }

        private void SetData()
        {
            SetAccountNature();
            SetContactInfo();
            SetAuthorizedPersons();
            SetOperatingInstruction();
            SetKnowYourCustomer();
            SetCertDeposit();
            SetWHoAuthorized();

        }


        private void SetWHoAuthorized()
        {
            Session["WhoGrid"] = null;
            GridWhoCifs.DataSource = new List<WhoAuthorized>();
            GridWhoCifs.DataBind();
        }
        private void SetCertDeposit()
        {
            AccountType a = new AccountType();
            TransactionType t = new TransactionType();
            PrinciparRenewalOption p = new PrinciparRenewalOption();

            CdListProfitAccount.DataSource = a.GetAccountTypes();
            CdListProfitAccount.DataValueField = "ID";
            CdListProfitAccount.DataTextField = "NAME";
            CdListProfitAccount.DataBind();
            CdListProfitAccount.Items.Insert(0, new ListItem("Select", "0"));

            CdListTransactionType.DataSource = t.GetAccountTypes();
            CdListTransactionType.DataValueField = "ID";
            CdListTransactionType.DataTextField = "NAME";
            CdListTransactionType.DataBind();
            CdListTransactionType.Items.Insert(0, new ListItem("Select", "0"));

            CdLstPrincipalRenewal.DataSource = p.GetPrincipalRenewalOtions();
            CdLstPrincipalRenewal.DataValueField = "ID";
            CdLstPrincipalRenewal.DataTextField = "NAME";
            CdLstPrincipalRenewal.DataBind();
            CdLstPrincipalRenewal.Items.Insert(0, new ListItem("Select", "0"));

            // CdExpDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void SetKnowYourCustomer()
        {
            CustomerType c = new CustomerType();
            Education e = new Education();
            PurposeOfAccount p = new PurposeOfAccount();
            SourceOfFunds s = new SourceOfFunds();
            ServiceChargesExemptedCode sc = new ServiceChargesExemptedCode();
            ModeOfTransactions m = new ModeOfTransactions();
            AddressVerified a = new AddressVerified();
            MeansOfVerification v = new MeansOfVerification();
            Country co = new Country();
            RealBeneficiaryAccount r = new RealBeneficiaryAccount();
            Relationship re = new Relationship();
            Know_Your_Customer_Relationship rela = new Know_Your_Customer_Relationship();
            PrimaryDocumentType pDoc = new PrimaryDocumentType();
            ExpectedCounterParties ECP = new ExpectedCounterParties();
            GeographiesCounterParties GCP = new GeographiesCounterParties();
            Reason_Account_Opening rac = new Reason_Account_Opening();

            KnListRAC.DataSource = rac.GetReason_Account_OpeningTypes();
            KnListRAC.DataValueField = "ID";
            KnListRAC.DataTextField = "Name";
            KnListRAC.DataBind();
            KnListRAC.Items.Insert(0, new ListItem("Select", "0"));


            KnListCustomerType.DataSource = c.GetCustomerTypeBusiness();
            KnListCustomerType.DataValueField = "ID";
            KnListCustomerType.DataTextField = "NAME";
            KnListCustomerType.DataBind();
            KnListCustomerType.Items.Insert(0, new ListItem("Select", "0"));

            KnListEducation.DataSource = e.GetEducation();
            KnListEducation.DataValueField = "ID";
            KnListEducation.DataTextField = "NAME";
            KnListEducation.DataBind();
            KnListEducation.Items.Insert(0, new ListItem("Select", "0"));

            KnListPurposeOfAccount.DataSource = p.GetPAccountTypesBusiness();
            KnListPurposeOfAccount.DataValueField = "ID";
            KnListPurposeOfAccount.DataTextField = "NAME";
            KnListPurposeOfAccount.DataBind();


            KnListSourceOfFunds.DataSource = s.GetSourceOfFundsBusiness();
            KnListSourceOfFunds.DataValueField = "ID";
            KnListSourceOfFunds.DataTextField = "NAME";
            KnListSourceOfFunds.DataBind();


            KnListSerExemptCode.DataSource = sc.GetAccountTypes();
            KnListSerExemptCode.DataValueField = "ID";
            KnListSerExemptCode.DataTextField = "NAME";
            KnListSerExemptCode.DataBind();
            KnListSerExemptCode.Items.Insert(0, new ListItem("Select", "0"));

            KnListModeOfTransaction.DataSource = m.GetAccountTypes();
            KnListModeOfTransaction.DataValueField = "ID";
            KnListModeOfTransaction.DataTextField = "NAME";
            KnListModeOfTransaction.DataBind();

            KnListAddresVerified.DataSource = a.GetAccountTypes();
            KnListAddresVerified.DataValueField = "ID";
            KnListAddresVerified.DataTextField = "NAME";
            KnListAddresVerified.DataBind();

            KnListMeansOfVerification.DataSource = v.GetAccountTypes();
            KnListMeansOfVerification.DataValueField = "ID";
            KnListMeansOfVerification.DataTextField = "NAME";
            KnListMeansOfVerification.DataBind();
            KnListMeansOfVerification.Items.Insert(0, new ListItem("Select", "0"));

            KnListCountHomeRemit.DataSource = co.GetCountries();
            KnListCountHomeRemit.DataValueField = "ID";
            KnListCountHomeRemit.DataTextField = "NAME";
            KnListCountHomeRemit.DataBind();
            KnListCountHomeRemit.Items.Insert(0, new ListItem("Select", "0"));

            KnListRealBenef.DataSource = r.GetAccountTypes();
            KnListRealBenef.DataValueField = "ID";
            KnListRealBenef.DataTextField = "NAME";
            KnListRealBenef.DataBind();
            KnListRealBenef.Items.Insert(0, new ListItem("Select", "0"));

            //  KnListRelationAccountHolder.DataSource = re.GetRelationship();
            //  KnListRelationAccountHolder.DataValueField = "ID";
            //   KnListRelationAccountHolder.DataTextField = "NAME";
            //   KnListRelationAccountHolder.DataBind();
            //   KnListRelationAccountHolder.Items.Insert(0, new ListItem("Select", "0"));

            KnddlManager.DataSource = rela.GetRelationships();
            KnddlManager.DataValueField = "ID";
            KnddlManager.DataTextField = "NAME";
            KnddlManager.DataBind();
            KnddlManager.Items.Insert(0, new ListItem("Select", "0"));

            knListDocType.DataSource = pDoc.GetPrimaryDocumentTypes();
            knListDocType.DataValueField = "ID";
            knListDocType.DataTextField = "NAME";
            knListDocType.DataBind();
            knListDocType.Items.Insert(0, new ListItem("Select", "0"));

            knListUs.Items.Insert(0, new ListItem("NO", "2"));
            knListUs.Items.Insert(0, new ListItem("YES", "1"));
            knListUs.Items.Insert(0, new ListItem("Select", "0"));

            KnListECP.DataSource = ECP.GetExpectedCounterParties();
            KnListECP.DataValueField = "ID";
            KnListECP.DataTextField = "NAME";
            KnListECP.DataBind();

            KnListGCP.DataSource = GCP.GetGeographiesCounterParties();
            KnListGCP.DataValueField = "ID";
            KnListGCP.DataTextField = "NAME";
            KnListGCP.DataBind();


            GrdBeneficial.DataSource = new List<int>();
            GrdBeneficial.DataBind();

            BtnAddBGrid.Visible = true;



        }

        private void SetAuthorizedPersons()
        {
            Session["GridCif"] = null;
            ApplicantStatuses a = new ApplicantStatuses();
            AuthListApplicantStatus.DataSource = a.GetApplicantStatusesBusiness();
            AuthListApplicantStatus.DataValueField = "ID";
            AuthListApplicantStatus.DataTextField = "NAME";
            AuthListApplicantStatus.DataBind();
            AuthListApplicantStatus.Items.Insert(0, new ListItem("Select", "0"));

            GridViewAccountCifs.DataSource = new List<ApplicantInformationCifs>();
            GridViewAccountCifs.DataBind();
        }

        private void SetContactInfo()
        {
            IssuingAgency i = new IssuingAgency();
            NatureBusiness n = new NatureBusiness();
            District d = new District();
            City c = new City();
            Country co = new Country();
            Sms_Alert_Required s = new Sms_Alert_Required();
            Province p = new Province();

            CiListRegistrationIssueAgency.DataSource = i.GetIssuingAgency();
            CiListRegistrationIssueAgency.DataValueField = "ID";
            CiListRegistrationIssueAgency.DataTextField = "NAME";
            CiListRegistrationIssueAgency.DataBind();

            CiListNatureOfBusiness.DataSource = n.GetAccountTypes();
            CiListNatureOfBusiness.DataValueField = "ID";
            CiListNatureOfBusiness.DataTextField = "NAME";
            CiListNatureOfBusiness.DataBind();


            CiListCity.DataSource = c.GetCifTypes();
            CiListCity.DataValueField = "ID";
            CiListCity.DataTextField = "NAME";
            CiListCity.DataBind();
            CiListCity.Items.Insert(0, new ListItem("Select", "0"));

            CiListCountry.DataSource = co.GetCountries();
            CiListCountry.DataValueField = "ID";
            CiListCountry.DataTextField = "NAME";
            CiListCountry.DataBind();
            CiListCountry.Items.Insert(0, new ListItem("Select", "0"));

            CiListSmsAlert.DataSource = s.GetAccountTypes();
            CiListSmsAlert.DataValueField = "ID";
            CiListSmsAlert.DataTextField = "NAME";
            CiListSmsAlert.DataBind();

            CiListProvince.DataSource = p.GetProvinces();
            CiListProvince.DataValueField = "ID";
            CiListProvince.DataTextField = "NAME";
            CiListProvince.DataBind();
            CiListProvince.Items.Insert(0, new ListItem("Select", "0"));


            CiListRegistrationIssueAgency.Items.Insert(0, new ListItem("Select", "0"));
            CiListNatureOfBusiness.Items.Insert(0, new ListItem("Select", "0"));
            //            BiListCustomerDealIn.Items.Insert(0, new ListItem("Select", "0"));

        }
        private void SetOperatingInstruction()
        {
            AuthorityToOperate a = new AuthorityToOperate();
            ZakatExemptionType z = new ZakatExemptionType();
            AccountStatementFrequency f = new AccountStatementFrequency();
            E_Statement_Required e = new E_Statement_Required();
            ProfitPayment p = new ProfitPayment();

            AuListAuthority.DataSource = a.GetAccountTypes();
            AuListAuthority.DataValueField = "ID";
            AuListAuthority.DataTextField = "NAME";
            AuListAuthority.DataBind();
            AuListAuthority.Items.Insert(0, new ListItem("Select", "0"));

            AuListZakatExemption.DataSource = z.GetAccountTypes();
            AuListZakatExemption.DataValueField = "ID";
            AuListZakatExemption.DataTextField = "NAME";
            AuListZakatExemption.DataBind();
            AuListZakatExemption.Items.Insert(0, new ListItem("Select", "0"));

            AuListAccountFrequenct.DataSource = f.GetAccountTypes();
            AuListAccountFrequenct.DataValueField = "ID";
            AuListAccountFrequenct.DataTextField = "NAME";
            AuListAccountFrequenct.DataBind();
            AuListAccountFrequenct.Items[0].Selected = true;

            AuListEstatement.DataSource = e.GetAccountTypes();
            AuListEstatement.DataValueField = "ID";
            AuListEstatement.DataTextField = "NAME";
            AuListEstatement.DataBind();

            AuListProfitPayment.DataSource = p.GetAccountTypes();
            AuListProfitPayment.DataValueField = "ID";
            AuListProfitPayment.DataTextField = "NAME";
            AuListProfitPayment.DataBind();
            AuListProfitPayment.Items.Insert(0, new ListItem("Select", "0"));

            RequiredFieldValidatorExpProfit.Enabled = true;
            RequiredFieldValidatorExpTrans.Enabled = true;

            //  AuExpDateExempted.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //  AuExpDateProfit.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //   AuExpDateTrans.Text = DateTime.Now.ToString("yyyy-MM-dd");

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
        public int ReadQueryStringID()
        {
            if (Request.QueryString["ID"] != null)
            {
                return Convert.ToInt32(Request.QueryString["ID"]);
            }
            else
                return -1;
        }


        private void SetAccountNature()
        {
            Gl_code g = new Gl_code();
            Sl_code s = new Sl_code();
            AccountType a = new AccountType();
            AccountClass Ac = new AccountClass();
            Currency c = new Currency();
            AccountOpenType an = new AccountOpenType();
            Products p = new Products();
            AccountPurpose ap = new AccountPurpose();

            AcListGlCode.DataSource = g.GetGlCodeTypes();
            AcListGlCode.DataValueField = "ID";
            AcListGlCode.DataTextField = "NAME";
            AcListGlCode.DataBind();
            AcListGlCode.Items.Insert(0, new ListItem("Select", "0"));

            AcListSlCode.DataSource = s.GetGlCodeTypes();
            AcListSlCode.DataValueField = "ID";
            AcListSlCode.DataTextField = "NAME";
            AcListSlCode.DataBind();
            AcListSlCode.Items.Insert(0, new ListItem("Select", "0"));

            AcListAccountOpen.DataSource = an.GetAccountTypes();
            AcListAccountOpen.DataValueField = "ID";
            AcListAccountOpen.DataTextField = "NAME";
            AcListAccountOpen.DataBind();
            AcListAccountOpen.Enabled = false;


            AcListCurrency.DataSource = c.GetGlCodeTypes();
            AcListCurrency.DataValueField = "ID";
            AcListCurrency.DataTextField = "NAME";
            AcListCurrency.DataBind();
            AcListCurrency.Items.Insert(0, new ListItem("Select", "0"));

            //   AcListAccountType.DataSource = a.GetAccountTypes();
            //   AcListAccountType.DataValueField = "ID";
            //    AcListAccountType.DataTextField = "NAME";
            //    AcListAccountType.DataBind();
            AcListAccountType.Items.Insert(0, new ListItem("Select", "0"));
            AcListAccountGroup.Items.Insert(0, new ListItem("Select", "0"));
            AcListAccountMode.Items.Insert(0, new ListItem("Select", "0"));

            AcDdlProducts.DataSource = p.GetProducts();
            AcDdlProducts.DataValueField = "ID";
            AcDdlProducts.DataTextField = "NAME";
            AcDdlProducts.DataBind();
            AcDdlProducts.Items.Insert(0, new ListItem("Select", "0"));

            AcListAccountClass.DataSource = Ac.GetAccountClassTypes();
            AcListAccountClass.DataValueField = "ID";
            AcListAccountClass.DataTextField = "NAME";
            AcListAccountClass.DataBind();
            AcListAccountClass.Items.Insert(0, new ListItem("Select", "0"));

            AcListPOA.DataSource = ap.GetAccountPurposes();
            AcListPOA.DataValueField = "ID";
            AcListPOA.DataTextField = "Name";
            AcListPOA.DataBind();
            AcListPOA.Items.Insert(0, new ListItem("Select", "0"));


            AcEntryDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            //            BiListCifType.Items.FindByText("BUSINESS / OTHER").Selected = true;
            AcListAccountOpen.Items.FindByText("OFFICE").Selected = true;

            AcListAccountClass.Items.FindByText("Deposit").Selected = true;
            AcListAccountClass_SelectedIndexChanged(null, null);

            AcListAccountGroup.Items.FindByText("Demand Deposits").Selected = true;
            AcListAccountGroup_SelectedIndexChanged(null, null);

            AcListAccountType.Items.FindByText("4599 -- Branch Office Account").Selected = true;
            AcListAccountType_SelectedIndexChanged(null, null);

        }
        protected void CiSearchCifButton_Click(object sender, EventArgs e)
        {

        }

        protected void CiSubmitButton_Click(object sender, EventArgs e)
        {
            AccountContactInfo a = new AccountContactInfo();
            a.BI_ID = Convert.ToInt32(Session["BID"]);
            a.CIF_NO = CiCustomerCif.Text;
            a.NAME = CiName.Text;
            a.NATIONAL_TAXNO = CiNationTaxNo.Text;
            a.SALES_TAXNO = CiSalesTaxNo.Text;
            a.REGISTRATION_NO = CiRegistrationNo.Text;

            //a.REGISTRATION_ISSUING_AGENCY = new IssuingAgency() { ID = Convert.ToInt32(CiListRegistrationIssueAgency.SelectedItem.Value), Name = CiListRegistrationIssueAgency.SelectedItem.Text };
            // ListExtensions.getSelectedValue(CiListRegistrationIssueAgency);
            // ListExtensions.getSelectedValue(CiListNatureOfBusiness);
            a.REGISTRATION_ISSUING_AGENCY = new IssuingAgency() { ID = ListExtensions.getSelectedValue(CiListRegistrationIssueAgency), Name = CiListRegistrationIssueAgency.SelectedItem.Text };
            a.NATURE_OF_BUSINESS = new NatureBusiness() { ID = ListExtensions.getSelectedValue(CiListNatureOfBusiness), Name = CiListNatureOfBusiness.SelectedItem.Text };
            a.BUILDING = CiTxtBuilding.Text;
            a.FLOOR = CiTxtFloor.Text;
            a.STREET = CiTxtStreet.Text;
            a.DISTRICT = CiTxtDistrict.Text;
            a.PO_BOX = CiPoBox.Text;
            a.COUNTRY = new Country() { ID = Convert.ToInt32(CiListCountry.SelectedItem.Value), Name = CiListCountry.SelectedItem.Text };
            if (CiListCountry.SelectedItem.Text.Trim() != "PAKISTAN")
            {
                a.PROVINCE = new Province() { ID = null };
                a.CITY = new City() { ID = null };
            }
            else
            {
                a.PROVINCE = new Province() { ID = Convert.ToInt32(CiListProvince.SelectedItem.Value) };
                a.CITY = new City() { ID = Convert.ToInt32(CiListCity.SelectedItem.Value), Name = CiListCity.SelectedItem.Text };
            }

            a.POSTAL_CODE = CiPostalCode.Text;

            a.TEL_RESIDENCE = CiTelResidence.Text;
            a.TEL_OFFICE = CiTelOffice.Text;
            a.MOBILE_NO = CiMobileNo.Text;
            a.FAX_NO = CiFaxNo.Text;
            a.SMS_ALERT_REQUIRED = new Sms_Alert_Required() { ID = Convert.ToInt32(CiListSmsAlert.SelectedItem.Value), NAME = CiListSmsAlert.SelectedItem.Text };
            a.WEB_ADDRESS_URL = CiWebAddress.Text;
            if (CiEmail.Text == "")
            {
                a.EMAIL = false;
            }
            else
            {
                a.EMAIL = true;
                Emails em = new Emails();
                em.BI_ID = Convert.ToInt32(Session["BID"]);
                em.EMAIL = CiEmail.Text;
                if (CiRequiredEstateCheckbox.Checked == true)
                {
                    em.REQUIRED_ESTATEMEN = true;
                }
                else
                {
                    em.REQUIRED_ESTATEMEN = false;
                }

                em.SaveEmail();
            }

            a.GROUP_CIF_NO = CiGroupCif.Text;
            a.GROUP_NAME = CiGroupName.Text;
            a.Save();

            AccOpen ac = new AccOpen(Convert.ToInt32(Session["BID"]), AccountOpenTypes.BUSINESS);

            if (ac.CheckIfCompleted())
            {
                User LoggedUser = Session["User"] as User;
                ac.ChangeStatus(Status.SUBMITTED, LoggedUser);
                Response.Redirect("AccountList.aspx");
            }



            String mesg = "Contact Information has been saved";
            CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);
            CiSubmitButton.Visible = false;

        }

        protected void AuthSubmitButton_Click(object sender, EventArgs e)
        {

            AccountAuthorizedPerson a = new AccountAuthorizedPerson();
            a.BI_ID = Convert.ToInt32(Session["BID"]);
            List<ApplicantInformationCifs> CifsData = Session["GridCif"] as List<ApplicantInformationCifs>;
            a.Cifs = CifsData;
            //   a.CIF_NO = AuthCustomerCif.Text;
            //   a.NAME = AuthCustomerName.Text;
            //   a.CNIC = AuthCustomerCNIC.Text;
            //if (AuthApplicantNegativeRadio1.Checked == true)
            //{
            //    a.APPLICANT_IN_NEGATIVE_LIST = true;
            //}
            //else
            //{
            //    a.APPLICANT_IN_NEGATIVE_LIST = false;
            //}

            //    if (AuthPowerAttornyRadio1.Checked == true)
            //{
            //    a.POWER_OF_ATTORNY = true;
            //}
            //    else
            //{
            //    a.POWER_OF_ATTORNY = false;
            //}


            //    if (AuthSignatureRadio1.Checked==true)
            //{
            //    a.SIGNATURE_AUTHORITY = true;
            //}
            //else
            //{
            //    a.SIGNATURE_AUTHORITY = false;
            //}

            //a.APPLICANT_STATUS = new ApplicantStatuses() { ID = Convert.ToInt32(AuthListApplicantStatus.SelectedItem.Value), Name = AuthListApplicantStatus.SelectedItem.Text };
            a.Save();
            AccOpen ac = new AccOpen(Convert.ToInt32(Session["BID"]), AccountOpenTypes.BUSINESS);

            if (ac.CheckIfCompleted())
            {
                User LoggedUser = Session["User"] as User;
                ac.ChangeStatus(Status.SUBMITTED, LoggedUser);
                Response.Redirect("AccountList.aspx");
            }

            String mesg = "Authorized Persons has been saved";
            CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);
            AuthSubmitButton.Visible = false;
        }

        protected void AuthSearchCifButton_Click(object sender, EventArgs e)
        {

        }

        protected void AcListAccountOpen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AcListAccountOpen.SelectedItem.Text == "INDIVIDUAL")
            {
                Response.Redirect("Account_Individual.aspx");
            }
        }

        protected void AcSubmitButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                AccountNatureCurrency a = new AccountNatureCurrency();
                User LoggedUser = Session["User"] as User;
                a.USERID = LoggedUser.USER_ID;
                a.BRANCH_CODE = LoggedUser.Branch.BRANCH_CODE;

                if (AcCnicVerifiedCheck.Checked == true)
                {
                    a.CNIC_VERIFIED = true;
                }
                else
                {
                    a.CNIC_VERIFIED = false;
                }
                a.PRODUCT = new Products() { ID = Convert.ToInt32(AcDdlProducts.SelectedItem.Value) };
                a.ACCOUNT_ENTRY_DATE = Convert.ToDateTime(AcEntryDate.Text);
                // a.GL_CODE = new Gl_code() { ID = Convert.ToInt32(AcListGlCode.SelectedItem.Value), NAME = AcListGlCode.SelectedItem.Text };
                //  a.SL_CODE = new Sl_code() { ID = Convert.ToInt32(AcListSlCode.SelectedItem.Value), NAME = AcListSlCode.SelectedItem.Text };
                a.ACCOUNT_TYPE = new AccountType() { ID = Convert.ToInt32(AcListAccountType.SelectedItem.Value), Name = AcListAccountType.SelectedItem.Text };
                a.CURRENCY = new Currency() { ID = Convert.ToInt32(AcListCurrency.SelectedItem.Value), NAME = AcListCurrency.SelectedItem.Text };
                a.ACCOUNT_NUMBER = AcAccountNumber.Text;
                a.ACCOUNT_TITLE = AcAccountTitle.Text;
                a.INITIAL_DEPOSIT = AcInitialDeposit.Text;
                a.ACCOUNT_OPEN_TYPE = new AccountOpenType() { ID = Convert.ToInt32(AcListAccountOpen.SelectedItem.Value), NAME = AcListAccountOpen.SelectedItem.Text };
                a.ACCOUNT_MODE_DETAIL = Convert.ToInt32(AcListAccountMode.SelectedItem.Value);
                a.ACCOUNT_PURPOSE = Convert.ToInt32(AcListPOA.SelectedItem.Value);
                if (AcAccountModeRadio1.Checked == true)
                {
                    a.ACCOUNT_MODE = true;
                }
                else
                {
                    a.ACCOUNT_MODE = false;
                }

                //if (AcMinorAccountRadio1.Checked == true)
                //{
                //    a.MINOR_ACCOUNT = true;

                //}
                //else
                //{
                //    a.MINOR_ACCOUNT = false;

                //}

                Session["BID"] = a.SetAccountNature();

                String mesg = "Account Nature and currency has been saved";
                int id = Convert.ToInt32(Session["BID"]);

                CheckAccountIndividualTab(id, mesg);
                AcSubmitButton.Visible = false;

                AcListAccountClass.Enabled = false;
                AcListAccountGroup.Enabled = false;
            }



        }

        protected void NkSubmitButton_Click(object sender, EventArgs e)
        {

        }

        protected void KnSubmitButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                AccountKnowYourCustomer a = new AccountKnowYourCustomer();
                a.BI_ID = Convert.ToInt32(Session["BID"]);
                a.CUSTOMER_TYPE = new CustomerType() { ID = Convert.ToInt32(KnListCustomerType.SelectedItem.Value), NAME = KnListCustomerType.SelectedItem.Text };
                a.RAC = new Reason_Account_Opening() { ID = Convert.ToInt32(KnListRAC.SelectedItem.Value), Name = KnListRAC.SelectedItem.Text };
                a.RAC_DETAIL = knTextRACDetail.Text;
                a.DESCRIPTION_IF_REFFERED = KnDescrIfRefered.Text;
                a.EDUCATION = new Education() { ID = Convert.ToInt32(KnListEducation.SelectedItem.Value), Name = KnListEducation.SelectedItem.Text };
                //   a.PURPOSE_OF_ACCOUNT = new PurposeOfAccount() { ID = Convert.ToInt32(KnListPurposeOfAccount.SelectedItem.Value), NAME = KnListPurposeOfAccount.SelectedItem.Text };
                a.PURPOSE_OF_ACCOUNT = KnListPurposeOfAccount.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => Convert.ToInt32(i.Value)).ToList();
                a.DESCRIPTION_IF_OTHER = KnDescrOther.Text;
                //   a.SOURCE_OF_FUNDS = new SourceOfFunds() { ID = Convert.ToInt32(KnListSourceOfFunds.SelectedItem.Value), NAME = KnListSourceOfFunds.SelectedItem.Text };
                a.SOURCE_OF_FUNDS = KnListSourceOfFunds.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => Convert.ToInt32(i.Value)).ToList();
                a.DESCRIPTION_OF_SOURCE = KnDescrOfSource.Text;
                if (ServiceExemptedRadio1.Checked == true)
                {
                    a.SERVICE_CHARGES_EXEMPTED = true;
                }
                else
                {
                    a.SERVICE_CHARGES_EXEMPTED = false;
                }

                a.SERVICE_CHARGES_EXEMPTED_CODE = new ServiceChargesExemptedCode() { ID = Convert.ToInt32(KnListSerExemptCode.SelectedItem.Value), NAME = KnListSerExemptCode.SelectedItem.Text };
                a.REASON_IF_EXEMPTED = KnReasonExempted.Text;
                a.EXPECTED_MONTHLY_INCOME = KnExpectedMonthlyIncome.Text;
                // BI.NATIONALITIES = lstNationality.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => new Nationality { CountryID = Convert.ToInt32(i.Value), Country = i.Text }).ToList();
                //a.MODE_OF_TRANSACTIONS = KnListModeOfTransaction.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => new Know_Customer_Transaction_mode { BI_ID = Convert.ToInt32(Session["BID"]), MODE_OF_TRANSACTIONS = i.Value }).ToList();

                List<ListItem> selected = new List<ListItem>();
                int count = 0;
                foreach (ListItem item in KnListModeOfTransaction.Items)
                {
                    if (item.Selected)
                    {
                        //    a.MODE_OF_TRANSACTIONS = true;
                        count = 1;
                        Know_Customer_Transaction_mode k = new Know_Customer_Transaction_mode();
                        k.BI_ID = Convert.ToInt32(Session["BID"]);
                        k.MODE_OF_TRANSACTIONS = new ModeOfTransactions() { ID = Convert.ToInt32(item.Value), NAME = item.Text };
                        k.Save();
                    }
                    else
                    {
                        //a.MODE_OF_TRANSACTIONS = false;
                    }

                }

                if (count == 1)
                {
                    a.MODE_OF_TRANSACTIONS = true;
                }
                else
                {
                    a.MODE_OF_TRANSACTIONS = false;
                }

                a.OTHER_MODE_OF_TRANSACTIONS = KnOtherModeTrans.Text;
                a.MAX_TRANS_AMOUNT_DR = KnMaxAmountDR.Text;
                a.MAX_TRANS_AMOUNT_CR = KnMaxAmountCR.Text;
                a.RELATIONSHIP_MANAGER = new Know_Your_Customer_Relationship() { ID = Convert.ToInt32(KnddlManager.SelectedItem.Value) };
                if (OccupyVerifyRadio1.Checked == true)
                {
                    a.OCCUPATION_VERIFIED = true;
                }
                else
                {
                    a.OCCUPATION_VERIFIED = false;
                }

                // a.ADDRESS_VERIFIED = new AddressVerified() { ID = Convert.ToInt32(KnListAddresVerified.SelectedItem.Value), NAME = KnListAddresVerified.SelectedItem.Text };
                if (RadioButtonAddressVerifiedYes.Checked)
                    a.ADDRESS_VERIFIED = 1;
                else
                    a.ADDRESS_VERIFIED = 0;
                a.MEANS_OF_VERIFICATION = new MeansOfVerification() { ID = Convert.ToInt32(KnListMeansOfVerification.SelectedItem.Value), NAME = KnListMeansOfVerification.SelectedItem.Text };
                a.MEANS_OF_VERI_OTHER = KnMeanVerifyOther.Text;
                if (IsVeriSatiRadio1.Checked == true)
                {
                    a.IS_VERI_SATISFACTORY = true;
                }
                else
                {
                    a.IS_VERI_SATISFACTORY = false;
                }
                a.NODT = KnNODT.Text;
                a.PEDT = KnPEDT.Text;
                a.NOCT = KnNOCT.Text;
                a.PECT = KnPECT.Text;
                a.ETCP_OTHER = KntxtDescETCP.Text;
                a.GICP_OTHER = KntxtDescGCP.Text;

                a.KYC_EXPECTED_COUNTER_PARTIES = KnListECP.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => Convert.ToInt32(i.Value)).ToList();
                a.KYC_GEOGRAPHIES_COUNTER_PARTIES = KnListGCP.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => Convert.ToInt32(i.Value)).ToList();

                a.DETAIL_IF_NOT_SATISFACTORY = KnDetailNotSatis.Text;
                a.COUNTRY_HOME_REMITTANCE = new Country() { ID = Convert.ToInt32(KnListCountHomeRemit.SelectedItem.Value), Name = KnListCountHomeRemit.SelectedItem.Text };
                a.REAL_BENEFICIARY_ACCOUNT = new RealBeneficiaryAccount() { ID = Convert.ToInt32(KnListRealBenef.SelectedItem.Value), NAME = KnListRealBenef.SelectedItem.Text };
                a.NAME_OTHER = KnNameOther.Text;
                a.CNIC_OTHER = KnCnicOther.Text;
                //   a.RELATIONSHIP_WITH_ACCOUNTHOLDER = new Relationship() { ID = Convert.ToInt32(KnListRelationAccountHolder.SelectedItem.Value), NAME = KnListRelationAccountHolder.SelectedItem.Text };
                //   a.RELATIONSHIP_DETAIL_OTHER = KnRelationDetailOther.Text;
                if (Session["KycBeneficialEntity"] == null)
                    Session["KycBeneficialEntity"] = new List<KycBeneficialEntity>();

                var data = Session["KycBeneficialEntity"] as List<KycBeneficialEntity>;
                a.KycBeneficial = data;

                a.SaveKnowYourCustomerBusiness();

                AccOpen ac = new AccOpen(Convert.ToInt32(Session["BID"]), AccountOpenTypes.BUSINESS);

                if (ac.CheckIfCompleted())
                {
                    User LoggedUser = Session["User"] as User;
                    ac.ChangeStatus(Status.SUBMITTED, LoggedUser);
                    Response.Redirect("AccountList.aspx");
                }

                String mesg = "Know Your Customer has been saved";
                CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);
                KnSubmitButton.Visible = false;
                BtnAddBGrid.Visible = false;
            }




        }

        protected void CdSubmitButton_Click(object sender, EventArgs e)
        {
            AccountCertDepositInfo a = new AccountCertDepositInfo();
            a.BI_ID = Convert.ToInt32(Session["BID"]);
            a.EXPIRY_DATE = CdExpDate.Text;
            a.CERTIFICATE_PERIOD = CdCertPeriod.Text;
            if (AutoRollExpiryRadio1.Checked == true)
            {
                a.AUTO_ROLL_ON_EXPIRY = true;
            }
            else
            {
                a.AUTO_ROLL_ON_EXPIRY = false;
            }
            a.SPECIAL_INSTR_ANY = CdSpecialInstr.Text;
            a.PROFIT_ACCOUNT_TYPE = new AccountType() { ID = Convert.ToInt32(CdListProfitAccount.SelectedItem.Value), Name = CdListProfitAccount.SelectedItem.Text };
            a.PROFIT_ACCOUNT_NUMBER = CdProfitAccountNumber.Text;
            a.TRANSACTION_TYPE = new TransactionType() { ID = Convert.ToInt32(CdListTransactionType.SelectedItem.Value), NAME = CdListTransactionType.SelectedItem.Text };
            a.CHEQUE_PREFIX = CdChequePrefix.Text;
            a.CHEQUE_NUMBER = ChChequeNumber.Text;
            a.CERTIFICATE_NUMBER = CdCertNumber.Text;
            a.CERTIFCATE_AMOUNT = CdCertAmount.Text;
            a.MARK_UP_RATE = CdMarkupRate.Text;
            a.PRINCIPAL_RENEWAL_OPTION = new PrinciparRenewalOption() { ID = Convert.ToInt32(CdLstPrincipalRenewal.SelectedItem.Value), Name = CdLstPrincipalRenewal.SelectedItem.Text };

            a.Sava();

            AccOpen ac = new AccOpen(Convert.ToInt32(Session["BID"]), AccountOpenTypes.BUSINESS);

            if (ac.CheckIfCompleted())
            {
                User LoggedUser = Session["User"] as User;
                ac.ChangeStatus(Status.SUBMITTED, LoggedUser);
                Response.Redirect("AccountList.aspx");
            }
            String mesg = "Certificate Deposit Info has been saved";
            CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);
            CdSubmitButton.Visible = false;

        }

        protected void DcGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void DcGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void AuSubmitButton_Click(object sender, EventArgs e)
        {

            OperatingInstructions o = new OperatingInstructions();
            o.BI_ID = Convert.ToInt32(Session["BID"]);
            o.AUTHORITY_TO_OPERATE = new AuthorityToOperate() { ID = Convert.ToInt32(AuListAuthority.SelectedItem.Value), NAME = AuListAuthority.SelectedItem.Text };
            o.DESCRIPTION_IF_OTHER = AuDescriptionOther.Text;
            if (ZakatDeductionRadio1.Checked == true)
            {
                o.ZAKAT_DEDUCTION = true;
            }
            else
            {
                o.ZAKAT_DEDUCTION = false;
            }

            o.ZAKAT_EXEMPTION_TYPE = new ZakatExemptionType() { ID = Convert.ToInt32(AuListZakatExemption.SelectedItem.Value), NAME = AuListZakatExemption.SelectedItem.Text };
            o.EXEMPTION_REASON_DETAIL = AuExempReasonDetail.Text;
            o.ACCOUNT_STATEMENT_FREQUENCY = new AccountStatementFrequency() { ID = Convert.ToInt32(AuListAccountFrequenct.SelectedItem.Value), NAME = AuListAccountFrequenct.SelectedItem.Text };
            o.DESCRIPTION_IF_HOLD_MAIL = AuDescrHoldMail.Text;
            if (AtmRequiredRadio1.Checked == true)
            {
                o.ATM_CARD_REQUIRED = true;
            }
            else
            {

                o.ATM_CARD_REQUIRED = false;
            }

            o.CUSTOMER_NAME_ON_ATMCARD = AuCustomerNameAtm.Text;
            o.E_STATEMENT_REQUIRED = new E_Statement_Required() { ID = Convert.ToInt32(AuListEstatement.SelectedItem.Value), NAME = AuListEstatement.SelectedItem.Text };
            if (MobileBankRequirRadio1.Checked == true)
            {
                o.MOBILE_BANKING_REQUIRED = true;
            }
            else
            {
                o.MOBILE_BANKING_REQUIRED = false;
            }

            o.MOBILE_NO = AuMobileNo.Text;
            if (IBTAllowRadio1.Checked == true)
            {
                o.IBT_ALLOWED = true;
            }
            else
            {
                o.IBT_ALLOWED = false;
            }
            if (IsProfitAppRadio1.Checked == true)
            {
                o.IS_PROFIT_APPLICABLE = true;
            }
            else
            {
                o.IS_PROFIT_APPLICABLE = false;
            }
            if (IsFedRadio1.Checked == true)
            {
                o.IS_FED_EXEMPTED = true;
            }
            else
            {
                o.IS_FED_EXEMPTED = false;
            }

            o.EXPIRY_DATE_EXEMPTED = AuExpDateExempted.Text;
            if (AppProfitRateRadio1.Checked == true)
            {
                o.APPLICABLE_PROFIT_RATE = "Bank Rate";
            }
            else
            {
                o.APPLICABLE_PROFIT_RATE = "Special Rate";
            }

            o.SPECIAL_PROFIT_VALUE = AuSpecicalProfitValue.Text;
            o.PROFIT_PAYMENT = new ProfitPayment() { ID = Convert.ToInt32(AuListProfitPayment.SelectedItem.Value), NAME = AuListProfitPayment.SelectedItem.Text };
            if (WhtProfitRadio1.Checked == true)
            {
                o.WHT_DEDUCTED_ON_PROFIT = true;
            }
            else
                o.WHT_DEDUCTED_ON_PROFIT = false;

            o.EXPIRY_DATE_PROFIT = AuExpDateProfit.Text;
            if (WhtTransactionRadio1.Checked == true)
            {
                o.WHT_DEDUCTED_ON_TRANSACTION = true;
            }
            else
            {
                o.WHT_DEDUCTED_ON_TRANSACTION = false;
            }

            o.EXPIRY_DATE_TRANSACTION = AuExpDateTrans.Text;

            o.SaveOperatingInstructions();
            AccOpen ac = new AccOpen(Convert.ToInt32(Session["BID"]), AccountOpenTypes.BUSINESS);

            if (ac.CheckIfCompleted())
            {
                User LoggedUser = Session["User"] as User;
                ac.ChangeStatus(Status.SUBMITTED, LoggedUser);
                Response.Redirect("AccountList.aspx");
            }
            String mesg = "Operating Instructions has been saved";
            CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);
            AuSubmitButton.Visible = false;

        }

        protected void DrSubmitButton_Click(object sender, EventArgs e)
        {
            DocumentsRequired d = new DocumentsRequired();
            //RadioButton b = sender as RadioButton;

            //GridViewRow g = (GridViewRow)b.NamingContainer;

            foreach (GridViewRow r in DcGrid.Rows)
            {
                RadioButton Yes = r.FindControl("DcRadio1") as RadioButton;
                RadioButton No = r.FindControl("DcRadio2") as RadioButton;
                RadioButton NA = r.FindControl("DcRadio3") as RadioButton;

                DocumentsList d1 = new DocumentsList();
                d1.BI_ID = Convert.ToInt32(Session["BID"]);
                int n = 0;
                int.TryParse(((Label)r.FindControl("lblDcId")).Text, out n);
                d1.Documents = n;
                if (Yes.Checked)
                    d1.value = Yes.Text;
                else if (No.Checked)
                    d1.value = No.Text;
                else if (NA.Checked)
                    d1.value = NA.Text;

                //  d1.value = ((RadioButton)r.FindControl("DcRadio2")).Text;
                d1.Save();
            }


            d.BI_ID = Convert.ToInt32(Session["BID"]);
            d.DESCRIPTION = true;


            d.Save();
            AccOpen ac = new AccOpen(Convert.ToInt32(Session["BID"]), AccountOpenTypes.BUSINESS);

            if (ac.CheckIfCompleted())
            {
                User LoggedUser = Session["User"] as User;
                ac.ChangeStatus(Status.SUBMITTED, LoggedUser);
                Response.Redirect("AccountList.aspx");
            }

            String mesg = "Description Documents has been saved";
            CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);

            DrSubmitButton.Visible = false;

            //            ApCustomerCif.Text = ((Label)g.FindControl("lblId")).Text;
            //          ApCustomerCNIC.Text = ((Label)g.FindControl("lblCnic")).Text;
            //        ApCustomerName.Text = ((Label)g.FindControl("lblName")).Text;
            //string rowIndex = (sender as LinkButton).CommandArgument;


        }
        //public int num = 0;
        //protected void DcRadio1_CheckedChanged(object sender, EventArgs e)
        //{
        //    RadioButton r = sender as RadioButton;

        //    if (r.Checked == true)
        //    {
        //        num = 1;

        //        DocumentsList d1 = new DocumentsList();
        //        d1.BI_ID = Convert.ToInt32(Session["BID"]);
        //        int n = 0;
        //        int.TryParse(((Label)r.FindControl("lblDcId")).Text, out n);
        //        d1.Documents = n;
        //        d1.value = ((RadioButton)r.FindControl("DcRadio1")).Text;
        //        d1.Save();

        //    }
        //    GridViewRow g = (GridViewRow)b.NamingContainer;

        //}

        //protected void DcRadio2_CheckedChanged(object sender, EventArgs e)
        //{
        //    RadioButton r = sender as RadioButton;

        //    if (r.Checked == true)
        //    {
        //        num = 1;
        //        DocumentsList d1 = new DocumentsList();
        //        d1.BI_ID = Convert.ToInt32(Session["BID"]);
        //        int n = 0;
        //        int.TryParse(((Label)r.FindControl("lblDcId")).Text, out n);
        //        d1.Documents = n;
        //        d1.value = ((RadioButton)r.FindControl("DcRadio2")).Text;
        //        d1.Save();
        //    }
        //}

        //protected void DcRadio3_CheckedChanged(object sender, EventArgs e)
        //{
        //    RadioButton r = sender as RadioButton;

        //    if (r.Checked == true)
        //    {
        //        num = 1;
        //        DocumentsList d1 = new DocumentsList();
        //        d1.BI_ID = Convert.ToInt32(Session["BID"]);
        //        int n = 0;
        //        int.TryParse(((Label)r.FindControl("lblDcId")).Text, out n);
        //        d1.Documents = n;
        //        d1.value = ((RadioButton)r.FindControl("DcRadio3")).Text;
        //        d1.Save();
        //    }
        //}

        protected void Search_Click(object sender, EventArgs e)
        {
            User LoggedUser = Session["User"] as User;

            CIF cf = new CIF(LoggedUser.USER_ID);
            searchCIF.DataSource = cf.GetCifsForAccounts2(SearchCIDModal.Text, Status.APPROVED_BY_BRANCH_MANAGER);
            searchCIF.DataBind();
        }

        protected void Select_Click(object sender, EventArgs e)
        {

        }

        protected void searchCIF_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void searchCIF_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void searchCIF_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void searchCIF_DataBound(object sender, EventArgs e)
        {

        }

        protected void btnUpdateAc_Click(object sender, EventArgs e)
        {
            AccountNatureCurrency a = new AccountNatureCurrency();

            int BID = (int)Session["BID"];

            a.ID = BID;

            if (AcCnicVerifiedCheck.Checked == true)
            {
                a.CNIC_VERIFIED = true;
            }
            else
            {
                a.CNIC_VERIFIED = false;
            }

            a.PRODUCT = new Products() { ID = Convert.ToInt32(AcDdlProducts.SelectedItem.Value) };
            a.ACCOUNT_ENTRY_DATE = Convert.ToDateTime(AcEntryDate.Text);
            //  a.GL_CODE = new Gl_code() { ID = Convert.ToInt32(AcListGlCode.SelectedItem.Value), NAME = AcListGlCode.SelectedItem.Text };
            //  a.SL_CODE = new Sl_code() { ID = Convert.ToInt32(AcListSlCode.SelectedItem.Value), NAME = AcListSlCode.SelectedItem.Text };
            a.ACCOUNT_TYPE = new AccountType() { ID = Convert.ToInt32(AcListAccountType.SelectedItem.Value), Name = AcListAccountType.SelectedItem.Text };
            a.CURRENCY = new Currency() { ID = Convert.ToInt32(AcListCurrency.SelectedItem.Value), NAME = AcListCurrency.SelectedItem.Text };
            a.ACCOUNT_NUMBER = AcAccountNumber.Text;
            a.ACCOUNT_TITLE = AcAccountTitle.Text;
            a.INITIAL_DEPOSIT = AcInitialDeposit.Text;
            a.ACCOUNT_OPEN_TYPE = new AccountOpenType() { ID = Convert.ToInt32(AcListAccountOpen.SelectedItem.Value), NAME = AcListAccountOpen.SelectedItem.Text };
            a.ACCOUNT_PURPOSE = Convert.ToInt32(AcListPOA.SelectedItem.Value);
            if (AcAccountModeRadio1.Checked == true)
            {
                a.ACCOUNT_MODE = true;
            }
            else
            {
                a.ACCOUNT_MODE = false;
            }

            //if (AcMinorAccountRadio1.Checked == true)
            //{
            //    a.MINOR_ACCOUNT = true;

            //}
            //else
            //{
            //    a.MINOR_ACCOUNT = false;

            //}

            a.Update();

            String mesg = "Account Nature and currency has been updated";
            int id = Convert.ToInt32(Session["BID"]);

            CheckAccountIndividualTab(id, mesg);
            AcSubmitButton.Visible = false;

        }

        protected void btnSubmitACa_Click(object sender, EventArgs e)
        {

            int BID = (int)Session["BID"];
            AccOpen ac = new AccOpen(BID, AccountOpenTypes.BUSINESS);
            //CIF cif = new CIF(BID, CifType.INDIVIDUAL);
            User LoggedUser = Session["User"] as User;
            ac.ChangeStatus(Status.SUBMITTED, LoggedUser);
            //cif.ChangeStatus(Status.SUBMITTED);
            Response.Redirect("AccountList.aspx");
        }

        protected void btnUpdateCi_Click(object sender, EventArgs e)
        {

            AccountContactInfo a = new AccountContactInfo();
            a.BI_ID = Convert.ToInt32(Session["BID"]);
            a.CIF_NO = CiCustomerCif.Text;
            a.NAME = CiName.Text;
            a.NATIONAL_TAXNO = CiNationTaxNo.Text;
            a.SALES_TAXNO = CiSalesTaxNo.Text;
            a.REGISTRATION_NO = CiRegistrationNo.Text;
            //a.REGISTRATION_ISSUING_AGENCY = new IssuingAgency() { ID = Convert.ToInt32(CiListRegistrationIssueAgency.SelectedItem.Value), Name = CiListRegistrationIssueAgency.SelectedItem.Text };
            // ListExtensions.getSelectedValue(CiListRegistrationIssueAgency);
            // ListExtensions.getSelectedValue(CiListNatureOfBusiness);
            a.COUNTRY = new Country() { ID = Convert.ToInt32(CiListCountry.SelectedItem.Value), Name = CiListCountry.SelectedItem.Text };
            if (CiListCountry.SelectedItem.Text.Trim() != "PAKISTAN")
            {
                a.PROVINCE = new Province() { ID = null };
                a.CITY = new City() { ID = null };
            }
            else
            {
                a.PROVINCE = new Province() { ID = Convert.ToInt32(CiListProvince.SelectedItem.Value) };
                a.CITY = new City() { ID = Convert.ToInt32(CiListCity.SelectedItem.Value), Name = CiListCity.SelectedItem.Text };
            }

            a.REGISTRATION_ISSUING_AGENCY = new IssuingAgency() { ID = ListExtensions.getSelectedValue(CiListRegistrationIssueAgency), Name = CiListRegistrationIssueAgency.SelectedItem.Text };
            a.NATURE_OF_BUSINESS = new NatureBusiness() { ID = ListExtensions.getSelectedValue(CiListNatureOfBusiness), Name = CiListNatureOfBusiness.SelectedItem.Text };
            a.BUILDING = CiTxtBuilding.Text;
            a.FLOOR = CiTxtFloor.Text;
            a.STREET = CiTxtStreet.Text;
            a.DISTRICT = CiTxtDistrict.Text;
            a.PO_BOX = CiPoBox.Text;
            a.POSTAL_CODE = CiPostalCode.Text;
            a.TEL_RESIDENCE = CiTelResidence.Text;
            a.TEL_OFFICE = CiTelOffice.Text;
            a.MOBILE_NO = CiMobileNo.Text;
            a.FAX_NO = CiFaxNo.Text;
            a.SMS_ALERT_REQUIRED = new Sms_Alert_Required() { ID = Convert.ToInt32(CiListSmsAlert.SelectedItem.Value), NAME = CiListSmsAlert.SelectedItem.Text };
            a.WEB_ADDRESS_URL = CiWebAddress.Text;
            if (CiEmail.Text == "")
            {
                a.EMAIL = false;
            }
            else
            {
                a.EMAIL = true;
                Emails em = new Emails();
                em.BI_ID = Convert.ToInt32(Session["BID"]);
                em.EMAIL = CiEmail.Text;
                if (CiRequiredEstateCheckbox.Checked == true)
                {
                    em.REQUIRED_ESTATEMEN = true;
                }
                else
                {
                    em.REQUIRED_ESTATEMEN = false;
                }

                em.SaveEmail();
            }

            a.GROUP_CIF_NO = CiGroupCif.Text;
            a.GROUP_NAME = CiGroupName.Text;
            a.Update();





            String mesg = "Contact Information has been updated";
            CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);
            CiSubmitButton.Visible = false;


        }

        protected void btnUpdateAu_Click(object sender, EventArgs e)
        {
            OperatingInstructions o = new OperatingInstructions();
            o.BI_ID = Convert.ToInt32(Session["BID"]);
            o.AUTHORITY_TO_OPERATE = new AuthorityToOperate() { ID = Convert.ToInt32(AuListAuthority.SelectedItem.Value), NAME = AuListAuthority.SelectedItem.Text };
            o.DESCRIPTION_IF_OTHER = AuDescriptionOther.Text;
            if (ZakatDeductionRadio1.Checked == true)
            {
                o.ZAKAT_DEDUCTION = true;
            }
            else
            {
                o.ZAKAT_DEDUCTION = false;
            }

            o.ZAKAT_EXEMPTION_TYPE = new ZakatExemptionType() { ID = Convert.ToInt32(AuListZakatExemption.SelectedItem.Value), NAME = AuListZakatExemption.SelectedItem.Text };
            o.EXEMPTION_REASON_DETAIL = AuExempReasonDetail.Text;
            o.ACCOUNT_STATEMENT_FREQUENCY = new AccountStatementFrequency() { ID = Convert.ToInt32(AuListAccountFrequenct.SelectedItem.Value), NAME = AuListAccountFrequenct.SelectedItem.Text };
            o.DESCRIPTION_IF_HOLD_MAIL = AuDescrHoldMail.Text;
            if (AtmRequiredRadio1.Checked == true)
            {
                o.ATM_CARD_REQUIRED = true;
            }
            else
            {

                o.ATM_CARD_REQUIRED = false;
            }

            o.CUSTOMER_NAME_ON_ATMCARD = AuCustomerNameAtm.Text;
            o.E_STATEMENT_REQUIRED = new E_Statement_Required() { ID = Convert.ToInt32(AuListEstatement.SelectedItem.Value), NAME = AuListEstatement.SelectedItem.Text };
            if (MobileBankRequirRadio1.Checked == true)
            {
                o.MOBILE_BANKING_REQUIRED = true;
            }
            else
            {
                o.MOBILE_BANKING_REQUIRED = false;
            }

            o.MOBILE_NO = AuMobileNo.Text;
            if (IBTAllowRadio1.Checked == true)
            {
                o.IBT_ALLOWED = true;
            }
            else
            {
                o.IBT_ALLOWED = false;
            }
            if (IsProfitAppRadio1.Checked == true)
            {
                o.IS_PROFIT_APPLICABLE = true;
            }
            else
            {
                o.IS_PROFIT_APPLICABLE = false;
            }
            if (IsFedRadio1.Checked == true)
            {
                o.IS_FED_EXEMPTED = true;
            }
            else
            {
                o.IS_FED_EXEMPTED = false;
            }

            o.EXPIRY_DATE_EXEMPTED = AuExpDateExempted.Text;
            if (AppProfitRateRadio1.Checked == true)
            {
                o.APPLICABLE_PROFIT_RATE = "Bank Rate";
            }
            else
            {
                o.APPLICABLE_PROFIT_RATE = "Special Rate";
            }

            o.SPECIAL_PROFIT_VALUE = AuSpecicalProfitValue.Text;
            o.PROFIT_PAYMENT = new ProfitPayment() { ID = Convert.ToInt32(AuListProfitPayment.SelectedItem.Value), NAME = AuListProfitPayment.SelectedItem.Text };
            if (WhtProfitRadio1.Checked == true)
            {
                o.WHT_DEDUCTED_ON_PROFIT = true;
            }
            else
                o.WHT_DEDUCTED_ON_PROFIT = false;

            o.EXPIRY_DATE_PROFIT = AuExpDateProfit.Text;
            if (WhtTransactionRadio1.Checked == true)
            {
                o.WHT_DEDUCTED_ON_TRANSACTION = true;
            }
            else
            {
                o.WHT_DEDUCTED_ON_TRANSACTION = false;
            }

            o.EXPIRY_DATE_TRANSACTION = AuExpDateTrans.Text;

            o.Update();

            String mesg = "Operating Instructions has been updated";
            CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);
            AuSubmitButton.Visible = false;


        }

        protected void btnUpdateAuth_Click(object sender, EventArgs e)
        {


            AccountAuthorizedPerson a = new AccountAuthorizedPerson();
            a.BI_ID = Convert.ToInt32(Session["BID"]);
            List<ApplicantInformationCifs> CifsData = Session["GridCif"] as List<ApplicantInformationCifs>;
            a.Cifs = CifsData;
            //   a.CIF_NO = AuthCustomerCif.Text;
            //   a.NAME = AuthCustomerName.Text;
            //   a.CNIC = AuthCustomerCNIC.Text;
            //if (AuthApplicantNegativeRadio1.Checked == true)
            //{
            //    a.APPLICANT_IN_NEGATIVE_LIST = true;
            //}
            //else
            //{
            //    a.APPLICANT_IN_NEGATIVE_LIST = false;
            //}

            //if (AuthPowerAttornyRadio1.Checked == true)
            //{
            //    a.POWER_OF_ATTORNY = true;
            //}
            //else
            //{
            //    a.POWER_OF_ATTORNY = false;
            //}


            //if (AuthSignatureRadio1.Checked == true)
            //{
            //    a.SIGNATURE_AUTHORITY = true;
            //}
            //else
            //{
            //    a.SIGNATURE_AUTHORITY = false;
            //}

            //a.APPLICANT_STATUS = new ApplicantStatuses() { ID = Convert.ToInt32(AuthListApplicantStatus.SelectedItem.Value), Name = AuthListApplicantStatus.SelectedItem.Text };


            a.Update();



            String mesg = "Authorized Persons has been updated";
            CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);
            AuthSubmitButton.Visible = false;


        }

        protected void btnUpdateKn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                AccountKnowYourCustomer a = new AccountKnowYourCustomer();
                a.BI_ID = Convert.ToInt32(Session["BID"]);
                a.CUSTOMER_TYPE = new CustomerType() { ID = Convert.ToInt32(KnListCustomerType.SelectedItem.Value), NAME = KnListCustomerType.SelectedItem.Text };
                a.RAC = new Reason_Account_Opening() { ID = Convert.ToInt32(KnListRAC.SelectedItem.Value), Name = KnListRAC.SelectedItem.Text };
                a.RAC_DETAIL = knTextRACDetail.Text;
                a.DESCRIPTION_IF_REFFERED = KnDescrIfRefered.Text;
                a.EDUCATION = new Education() { ID = Convert.ToInt32(KnListEducation.SelectedItem.Value), Name = KnListEducation.SelectedItem.Text };
                //    a.PURPOSE_OF_ACCOUNT = new PurposeOfAccount() { ID = Convert.ToInt32(KnListPurposeOfAccount.SelectedItem.Value), NAME = KnListPurposeOfAccount.SelectedItem.Text };
                a.PURPOSE_OF_ACCOUNT = KnListPurposeOfAccount.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => Convert.ToInt32(i.Value)).ToList();
                a.DESCRIPTION_IF_OTHER = KnDescrOther.Text;
                //    a.SOURCE_OF_FUNDS = new SourceOfFunds() { ID = Convert.ToInt32(KnListSourceOfFunds.SelectedItem.Value), NAME = KnListSourceOfFunds.SelectedItem.Text };
                a.SOURCE_OF_FUNDS = KnListSourceOfFunds.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => Convert.ToInt32(i.Value)).ToList();
                a.DESCRIPTION_OF_SOURCE = KnDescrOfSource.Text;
                if (ServiceExemptedRadio1.Checked == true)
                {
                    a.SERVICE_CHARGES_EXEMPTED = true;
                }
                else
                {
                    a.SERVICE_CHARGES_EXEMPTED = false;
                }

                a.SERVICE_CHARGES_EXEMPTED_CODE = new ServiceChargesExemptedCode() { ID = Convert.ToInt32(KnListSerExemptCode.SelectedItem.Value), NAME = KnListSerExemptCode.SelectedItem.Text };
                a.REASON_IF_EXEMPTED = KnReasonExempted.Text;
                a.EXPECTED_MONTHLY_INCOME = KnExpectedMonthlyIncome.Text;
                // BI.NATIONALITIES = lstNationality.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => new Nationality { CountryID = Convert.ToInt32(i.Value), Country = i.Text }).ToList();
                //a.MODE_OF_TRANSACTIONS = KnListModeOfTransaction.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => new Know_Customer_Transaction_mode { BI_ID = Convert.ToInt32(Session["BID"]), MODE_OF_TRANSACTIONS = i.Value }).ToList();
                Know_Customer_Transaction_mode kt = new Know_Customer_Transaction_mode();
                kt.Clean(Convert.ToInt32(Session["BID"]));

                List<ListItem> selected = new List<ListItem>();
                int count = 0;
                foreach (ListItem item in KnListModeOfTransaction.Items)
                {
                    if (item.Selected)
                    {
                        //    a.MODE_OF_TRANSACTIONS = true;
                        count = 1;
                        Know_Customer_Transaction_mode k = new Know_Customer_Transaction_mode();
                        k.BI_ID = Convert.ToInt32(Session["BID"]);
                        k.MODE_OF_TRANSACTIONS = new ModeOfTransactions() { ID = Convert.ToInt32(item.Value), NAME = item.Text };
                        k.Save();
                    }
                    else
                    {
                        //a.MODE_OF_TRANSACTIONS = false;
                    }

                }

                if (count == 1)
                {
                    a.MODE_OF_TRANSACTIONS = true;
                }
                else
                {
                    a.MODE_OF_TRANSACTIONS = false;
                }

                a.OTHER_MODE_OF_TRANSACTIONS = KnOtherModeTrans.Text;
                a.MAX_TRANS_AMOUNT_DR = KnMaxAmountDR.Text;
                a.MAX_TRANS_AMOUNT_CR = KnMaxAmountCR.Text;
                //  a.RELATIONSHIP_MANAGER = KnRelationshipManager.Text;
                a.RELATIONSHIP_MANAGER = new Know_Your_Customer_Relationship() { ID = Convert.ToInt32(KnddlManager.SelectedItem.Value) };
                if (OccupyVerifyRadio1.Checked == true)
                {
                    a.OCCUPATION_VERIFIED = true;
                }
                else
                {
                    a.OCCUPATION_VERIFIED = false;
                }

                // a.ADDRESS_VERIFIED = new AddressVerified() { ID = Convert.ToInt32(KnListAddresVerified.SelectedItem.Value), NAME = KnListAddresVerified.SelectedItem.Text };
                if (RadioButtonAddressVerifiedYes.Checked)
                    a.ADDRESS_VERIFIED = 1;
                else
                    a.ADDRESS_VERIFIED = 0;
                a.MEANS_OF_VERIFICATION = new MeansOfVerification() { ID = Convert.ToInt32(KnListMeansOfVerification.SelectedItem.Value), NAME = KnListMeansOfVerification.SelectedItem.Text };
                a.MEANS_OF_VERI_OTHER = KnMeanVerifyOther.Text;
                if (IsVeriSatiRadio1.Checked == true)
                {
                    a.IS_VERI_SATISFACTORY = true;
                }
                else
                {
                    a.IS_VERI_SATISFACTORY = false;
                }

                a.NODT = KnNODT.Text;
                a.PEDT = KnPEDT.Text;
                a.NOCT = KnNOCT.Text;
                a.PECT = KnPECT.Text;
                a.ETCP_OTHER = KntxtDescETCP.Text;
                a.GICP_OTHER = KntxtDescGCP.Text;

                a.KYC_EXPECTED_COUNTER_PARTIES = KnListECP.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => Convert.ToInt32(i.Value)).ToList();
                a.KYC_GEOGRAPHIES_COUNTER_PARTIES = KnListGCP.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => Convert.ToInt32(i.Value)).ToList();

                a.DETAIL_IF_NOT_SATISFACTORY = KnDetailNotSatis.Text;
                a.COUNTRY_HOME_REMITTANCE = new Country() { ID = Convert.ToInt32(KnListCountHomeRemit.SelectedItem.Value), Name = KnListCountHomeRemit.SelectedItem.Text };
                a.REAL_BENEFICIARY_ACCOUNT = new RealBeneficiaryAccount() { ID = Convert.ToInt32(KnListRealBenef.SelectedItem.Value), NAME = KnListRealBenef.SelectedItem.Text };
                a.NAME_OTHER = KnNameOther.Text;
                a.CNIC_OTHER = KnCnicOther.Text;
                var data = Session["KycBeneficialEntity"] as List<KycBeneficialEntity>;
                a.KycBeneficial = data;

                //   a.RELATIONSHIP_WITH_ACCOUNTHOLDER = new Relationship() { ID = Convert.ToInt32(KnListRelationAccountHolder.SelectedItem.Value), NAME = KnListRelationAccountHolder.SelectedItem.Text };
                //  a.RELATIONSHIP_DETAIL_OTHER = KnRelationDetailOther.Text;
                a.UpdateBusiness();



                String mesg = "Know Your Customer has been updated";
                CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);
                KnSubmitButton.Visible = false;

                BtnAddBGrid.Visible = false;
            }



        }

        protected void btnUpdateCd_Click(object sender, EventArgs e)
        {

            AccountCertDepositInfo a = new AccountCertDepositInfo();
            a.BI_ID = Convert.ToInt32(Session["BID"]);
            a.EXPIRY_DATE = CdExpDate.Text;
            a.CERTIFICATE_PERIOD = CdCertPeriod.Text;
            if (AutoRollExpiryRadio1.Checked == true)
            {
                a.AUTO_ROLL_ON_EXPIRY = true;
            }
            else
            {
                a.AUTO_ROLL_ON_EXPIRY = false;
            }
            a.SPECIAL_INSTR_ANY = CdSpecialInstr.Text;
            a.PROFIT_ACCOUNT_TYPE = new AccountType() { ID = Convert.ToInt32(CdListProfitAccount.SelectedItem.Value), Name = CdListProfitAccount.SelectedItem.Text };
            a.PROFIT_ACCOUNT_NUMBER = CdProfitAccountNumber.Text;
            a.TRANSACTION_TYPE = new TransactionType() { ID = Convert.ToInt32(CdListTransactionType.SelectedItem.Value), NAME = CdListTransactionType.SelectedItem.Text };
            a.CHEQUE_PREFIX = CdChequePrefix.Text;
            a.CHEQUE_NUMBER = ChChequeNumber.Text;
            a.CERTIFICATE_NUMBER = CdCertNumber.Text;
            a.CERTIFCATE_AMOUNT = CdCertAmount.Text;
            a.MARK_UP_RATE = CdMarkupRate.Text;
            a.PRINCIPAL_RENEWAL_OPTION = new PrinciparRenewalOption() { ID = Convert.ToInt32(CdLstPrincipalRenewal.SelectedItem.Value), Name = CdLstPrincipalRenewal.SelectedItem.Text };
            a.Update();


            String mesg = "Certificate Deposit Info has been updated";
            CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);
            CdSubmitButton.Visible = false;


        }

        protected void btnUpdateDr_Click(object sender, EventArgs e)
        {


            List<DocumentsList> UpdatedDocuments = new List<DocumentsList>();
            foreach (GridViewRow r in DcGrid.Rows)
            {
                RadioButton Yes = r.FindControl("DcRadio1") as RadioButton;
                RadioButton No = r.FindControl("DcRadio2") as RadioButton;
                RadioButton NA = r.FindControl("DcRadio3") as RadioButton;

                DocumentsList d1 = new DocumentsList();
                d1.BI_ID = Convert.ToInt32(Session["BID"]);
                int n = 0;
                int.TryParse(((Label)r.FindControl("lblDcId")).Text, out n);
                d1.Documents = n;
                if (Yes.Checked)
                    d1.value = Yes.Text;
                else if (No.Checked)
                    d1.value = No.Text;
                else if (NA.Checked)
                    d1.value = NA.Text;

                UpdatedDocuments.Add(d1);

            }

            DocumentsList dbDoc = new DocumentsList();
            dbDoc.UpdatedDocuments = UpdatedDocuments;
            dbDoc.Update();

            DocumentsRequired d = new DocumentsRequired();
            //RadioButton b = sender as RadioButton;

            //GridViewRow g = (GridViewRow)b.NamingContainer;
            d.BI_ID = Convert.ToInt32(Session["BID"]);
            d.DESCRIPTION = true;




            d.Update();


            String mesg = "Description Documents has been updated";
            CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);

            DrSubmitButton.Visible = false;
        }

        protected void Search_BusinessCIF_Click(object sender, EventArgs e)
        {
            if (SearchBusinessCifText.Text.Length > 0)
            {
                User LoggedUser = Session["User"] as User;
                int checked_id = 0;
                if (RadioButton1.Checked)
                {
                    checked_id = 1;
                }
                //else if (RadioButton2.Checked)
                //{
                //    checked_id = 2;
                //}
                //else if (RadioButton3.Checked)
                //{
                //    checked_id = 3;
                //}
                //else if (RadioButton4.Checked)
                //{
                //    checked_id = 4;
                //}


                CIF cf = new CIF(LoggedUser.USER_ID);
                SearchBusinessCif.DataSource = cf.GetOfficeCifsForAccounts(SearchBusinessCifText.Text, Status.APPROVED_BY_BRANCH_MANAGER, checked_id);
                SearchBusinessCif.DataBind();
            }


        }

        protected void SearchBusinessCif_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void SearchBusinessCif_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SearchBusinessCif.PageIndex = e.NewPageIndex;
            loadBusinessCif();
        }

        protected void SearchBusinessCif_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void SearchBusinessCif_DataBound(object sender, EventArgs e)
        {

        }

        protected void SelectBusinessCif_Click(object sender, EventArgs e)
        {

        }

        protected void CustomValidatorContact_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (CiTelOffice.Text.Length > 0 || CiTelResidence.Text.Length > 0 || CiMobileNo.Text.Length > 0)
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void CiRequiredEstateCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (CiRequiredEstateCheckbox.Checked)
                RequiredFieldValidatorEmail.Enabled = true;
            else
                RequiredFieldValidatorEmail.Enabled = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            Label CUSTOMER_CIF_NO = (Label)gvr.FindControl("CUSTOMER_CIF_NO");
            List<ApplicantInformationCifs> CifsData = Session["GridCif"] as List<ApplicantInformationCifs>;

            if (CifsData != null)
            {
                CifsData
               .Remove(
                       CifsData
                       .FirstOrDefault
                       (
                       c => c.CUSTOMER_CIF_NO == CUSTOMER_CIF_NO.Text
                       )
                   );
                GridViewAccountCifs.DataSource = CifsData;
                GridViewAccountCifs.DataBind();

                Session["GridCif"] = CifsData;
            }
        }

        protected void btnGridAddCif_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Debug.WriteLine("Cif text " + AuthCustomerCif.Text);

            if (Session["GridCif"] == null)
                Session["GridCif"] = new List<ApplicantInformationCifs>();

            string CIFID = AuthCustomerCif.Text;
            bool IsSignatry = AuthSignatureRadio1.Checked;
            bool IsPower = AuthPowerAttornyRadio1.Checked;
            string App_Status = AuthListApplicantStatus.SelectedItem.Text;
            string name = AuthCustomerName.Text;
            string cnic = AuthCustomerCNIC.Text;
            List<ApplicantInformationCifs> CifsData = Session["GridCif"] as List<ApplicantInformationCifs>;

            if (!CifsData.Where(c => c.CUSTOMER_CIF_NO == AuthCustomerCif.Text).Any())
            {
                ApplicantInformationCifs newCif = new ApplicantInformationCifs();

                newCif.CUSTOMER_CIF_NO = AuthCustomerCif.Text;
                newCif.POWER_OF_ATTORNY = Convert.ToInt32(AuthPowerAttornyRadio1.Checked);
                newCif.SIGNATURE_AUTHORITY = Convert.ToInt32(AuthSignatureRadio1.Checked);
                newCif.APPLICANT_STATUS = AuthListApplicantStatus.SelectedItem.Text;
                newCif.CUSTOMER_NAME = AuthCustomerName.Text;
                newCif.CUSTOMER_IDENTITY = AuthCustomerCNIC.Text;


                CifsData.Add(newCif);

                GridViewAccountCifs.DataSource = CifsData;
                GridViewAccountCifs.DataBind();
            }



            Session["GridCif"] = CifsData;
        }

        protected void CustomValidatorOneCustomers_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = GridViewAccountCifs.Rows.Count > 0;
        }

        //Operating Isntructions

        protected void ZakatDeductionRadio1_CheckedChanged(object sender, EventArgs e)
        {
            if (ZakatDeductionRadio1.Checked)
                RequiredFieldValidatorAuZakatExemptionType.Enabled = true;
            else
                RequiredFieldValidatorAuZakatExemptionType.Enabled = false;
        }

        protected void AtmRequiredRadio1_CheckedChanged(object sender, EventArgs e)
        {
            if (AtmRequiredRadio1.Checked)
                RequiredFieldValidatorAtmName.Enabled = true;
            else
                RequiredFieldValidatorAtmName.Enabled = false;
        }

        protected void MobileBankRequirRadio1_CheckedChanged(object sender, EventArgs e)
        {
            if (MobileBankRequirRadio1.Checked)
                RequiredFieldValidatorAuMobile.Enabled = true;
            else
                RequiredFieldValidatorAuMobile.Enabled = false;
        }

        protected void IsFedRadio1_CheckedChanged(object sender, EventArgs e)
        {
            if (IsFedRadio1.Checked)
                RequiredFieldValidatorExpiryFed.Enabled = true;
            else
                RequiredFieldValidatorExpiryFed.Enabled = false;
        }

        protected void WhtProfitRadio1_CheckedChanged(object sender, EventArgs e)
        {
            if (WhtProfitRadio2.Checked)
                RequiredFieldValidatorExpProfit.Enabled = true;
            else
                RequiredFieldValidatorExpProfit.Enabled = false;

        }

        protected void WhtTransactionRadio1_CheckedChanged(object sender, EventArgs e)
        {
            if (WhtTransactionRadio2.Checked)
                RequiredFieldValidatorExpTrans.Enabled = true;
            else
                RequiredFieldValidatorExpTrans.Enabled = false;

        }

        // Know Your Customer

        protected void ServiceExemptedRadio1_CheckedChanged(object sender, EventArgs e)
        {
            if (ServiceExemptedRadio1.Checked)
                RequiredFieldValidatorExemptCode.Enabled = true;
            else
                RequiredFieldValidatorExemptCode.Enabled = false;
        }


        // Who Authorized
        protected void WhoBtnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                List<WhoAuthorized> CifsData = Session["WhoGrid"] as List<WhoAuthorized>;
                WhoAuthorized wa = new WhoAuthorized();

                wa.Cifs = CifsData;
                wa.SAVE();

                AccOpen ac = new AccOpen(Convert.ToInt32(Session["BID"]), AccountOpenTypes.BUSINESS);

                if (ac.CheckIfCompleted())
                {
                    User LoggedUser = Session["User"] as User;
                    ac.ChangeStatus(Status.SUBMITTED, LoggedUser);
                    Response.Redirect("AccountList.aspx");
                }

                String mesg = "Who Authorized has been updated";
                CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);

                WhoBtnSubmit.Visible = false;
            }
        }

        protected void WhoBtnUpdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                List<WhoAuthorized> CifsData = Session["WhoGrid"] as List<WhoAuthorized>;
                WhoAuthorized wa = new WhoAuthorized();

                wa.BI_ID = Convert.ToInt32(Session["BID"]);
                wa.Cifs = CifsData;
                wa.Update();

                String mesg = "Who Authorized has been updated";
                CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);

                //  WhoBtnSubmit.Visible = false;
            }
        }

        protected void WhoBtnSubmit_Click(object sender, EventArgs e)
        {
            int BID = (int)Session["BID"];
            AccOpen ac = new AccOpen(BID, AccountOpenTypes.BUSINESS);
            //CIF cif = new CIF(BID, CifType.INDIVIDUAL);
            User LoggedUser = Session["User"] as User;
            ac.ChangeStatus(Status.SUBMITTED, LoggedUser);
            //cif.ChangeStatus(Status.SUBMITTED);
            Response.Redirect("AccountList.aspx");
        }

        protected void BtnWhoSearch_Click(object sender, EventArgs e)
        {

            User LoggedUser = Session["User"] as User;

            CIF cf = new CIF(LoggedUser.USER_ID);
            GridWhoCif.DataSource = cf.GetCifsForAccountsWho(txtWhoIdentityNo.Text, Status.APPROVED_BY_BRANCH_MANAGER);
            GridWhoCif.DataBind();
        }

        protected void btnDeleteCifWho_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            Label CIF_NO = (Label)gvr.FindControl("CIF_NO");
            List<WhoAuthorized> CifsData = Session["WhoGrid"] as List<WhoAuthorized>;

            if (CifsData != null)
            {
                CifsData
               .Remove(
                       CifsData
                       .FirstOrDefault
                       (
                       c => c.CIF_NO == Convert.ToInt32(CIF_NO.Text)
                       )
                   );
                GridWhoCifs.DataSource = CifsData;
                GridWhoCifs.DataBind();

                Session["WhoGrid"] = CifsData;
            }
        }

        protected void WhoBtnAddGrid_Click(object sender, EventArgs e)
        {
            if (Session["WhoGrid"] == null)
                Session["WhoGrid"] = new List<WhoAuthorized>();

            List<WhoAuthorized> WhoGridData = Session["WhoGrid"] as List<WhoAuthorized>;
            var newWhoCIf = new WhoAuthorized();
            newWhoCIf.BI_ID = Convert.ToInt32(Session["BID"]);
            newWhoCIf.CIF_NO = Convert.ToInt32(WhoTxtCifNo.Text);

            if (!WhoGridData.Where(c => c.CIF_NO == newWhoCIf.CIF_NO).Any())
            {
                newWhoCIf.REFERENCE_DOCUMENT_DATE = WhoTxtRefDate.Text;
                newWhoCIf.REFERENCE_DOCUMENT_NO = WhoTxtRefNo.Text;
                newWhoCIf.NAME = WhoTxtNAme.Text;
                newWhoCIf.IDENTITY_NO = WhoTxtIdentityNo.Text;
                WhoGridData.Add(newWhoCIf);

                GridWhoCifs.DataSource = WhoGridData;
                GridWhoCifs.DataBind();
            }

        }

        protected void CustomValidatorWHoOne_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = GridWhoCifs.Rows.Count > 0;
        }

        // Account Type hanlding
        protected void AcListAccountClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AcListAccountClass.SelectedItem.Text != "Select")
            {
                AccountGroup ag = new AccountGroup();
                AccountClass ac = new AccountClass();

                AcListAccountGroup.DataSource = ag.GetAccountGroupTypes(ac.GetAccountTypeCls(Convert.ToInt32(AcListAccountClass.SelectedItem.Value)));
                AcListAccountGroup.DataValueField = "ID";
                AcListAccountGroup.DataTextField = "NAME";
                AcListAccountGroup.DataBind();
                AcListAccountGroup.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        protected void AcListAccountGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AcListAccountGroup.SelectedItem.Text != "Select")
            {
                AccountGroup ag = new AccountGroup();
                AccountType at = new AccountType();
                AcListAccountType.DataSource = at.GetAccountTypes(ag.GetAccountClassClsGrp(Convert.ToInt32(AcListAccountGroup.SelectedItem.Value)));
                AcListAccountType.DataValueField = "ID";
                AcListAccountType.DataTextField = "NAME";
                AcListAccountType.DataBind();
                AcListAccountType.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        protected void AcListAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AcListAccountType.SelectedItem.Text != "Select")
            {
                AccountModes am = new AccountModes();


                AcListAccountMode.DataSource = am.GetAccountModesBusiness(Convert.ToInt32(AcListAccountType.SelectedItem.Value));
                AcListAccountMode.DataValueField = "ID";
                AcListAccountMode.DataTextField = "NAME";
                AcListAccountMode.DataBind();
                AcListAccountMode.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        protected void AcListAccountMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            AccountModes am = new AccountModes();
            string amTag = am.GetAccountTag(Convert.ToInt32(AcListAccountMode.SelectedItem.Value));
            if (amTag.ToLower() == "s")
            {

                AcAccountModeRadio1.Checked = true;
                AcAccountModeRadio2.Checked = false;
            }
            else
            {
                AcAccountModeRadio1.Checked = false;
                AcAccountModeRadio2.Checked = true;
            }
        }

        protected void CustomValidatorCurrency_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (AcListAccountType.SelectedItem.Text.Contains("EUR") || AcListAccountType.SelectedItem.Text.Contains("GBP") || AcListAccountType.SelectedItem.Text.Contains("JPY") || AcListAccountType.SelectedItem.Text.Contains("USD"))
            {
                if (AcListCurrency.SelectedItem.Text.Trim() == "Pakistani Rupee")
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            else
                args.IsValid = true;
        }

        protected void KnListPurposeOfAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool Other = KnListPurposeOfAccount.Items.FindByText("Others (specify)").Selected == true ? true : false;
            if (Other)
                ReqValidatorPurposeAccountOther.Enabled = true;
            else
                ReqValidatorPurposeAccountOther.Enabled = false;
        }

        protected void KnListSourceOfFunds_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool Other = KnListSourceOfFunds.Items.FindByText("Others (specify)").Selected == true ? true : false;
            if (Other)
                ReqValidatorSourceDesc.Enabled = true;
            else
                ReqValidatorSourceDesc.Enabled = false;
        }

        protected void KnListModeOfTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool Other = KnListModeOfTransaction.Items.FindByText("Other").Selected == true ? true : false;
            if (Other)
                ReqValidatorMotOther.Enabled = true;
            else
                ReqValidatorMotOther.Enabled = false;
        }

        protected void btnDelete_Click1(object sender, EventArgs e)
        {
            Button del = sender as Button;
            GridViewRow grow = (GridViewRow)del.NamingContainer;

            Label name = (Label)grow.FindControl("NAME");
            var Data = Session["KycBeneficialEntity"] as List<KycBeneficialEntity>;

            Data.Remove(Data.FirstOrDefault(b => b.NAME == name.Text));
            Session["KycBeneficialEntity"] = Data;

            GrdBeneficial.DataSource = Data;
            GrdBeneficial.DataBind();


        }

        protected void BtnAddBGrid_Click(object sender, EventArgs e)
        {
            if (Session["KycBeneficialEntity"] == null)
                Session["KycBeneficialEntity"] = new List<KycBeneficialEntity>();

            var Data = Session["KycBeneficialEntity"] as List<KycBeneficialEntity>;

            KycBeneficialEntity nCust = new KycBeneficialEntity();
            nCust.NAME = KnNameOther.Text;
            nCust.IDENTITY_DOCUMENT = knListDocType.SelectedItem.Text;
            nCust.IDENTITY_NUMBER = KnCnicOther.Text;
            nCust.EXPIRY_DATE = KntxtExpiry.Text;
            nCust.POB = KnTxtPOB.Text;
            nCust.US = knListUs.Text;
            nCust.OWNERSHIP = KnTxtPercentageOwn.Text;
            Data.Add(nCust);

            Session["KycBeneficialEntity"] = Data;

            GrdBeneficial.DataSource = Data;
            GrdBeneficial.DataBind();
        }

        protected void KnListRealBenef_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KnListRealBenef.SelectedItem.Text == "OTHER")
            {
                CustomValidatorBeneficial.Enabled = true;
            }
            else
            {
                CustomValidatorBeneficial.Enabled = false;
            }
        }

        protected void KnListRAC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KnListRAC.SelectedItem.Value == "9")
                RequiredFieldValidatorRACDetail.Enabled = true;
            else
                RequiredFieldValidatorRACDetail.Enabled = false;
        }

        protected void KnListECP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KnListECP.Items.Cast<ListItem>().Where(i => i.Text.Trim() == "Others (specify)" && i.Selected == true).Any())
            {
                RequiredFieldValidatorDescETCP.Enabled = true;
            }
            else
            {
                RequiredFieldValidatorDescETCP.Enabled = false;
            }
        }

        protected void KnListGCP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KnListGCP.Items.Cast<ListItem>().Where(i => i.Text.Trim() == "Outside Pakistan" && i.Selected == true).Any())
            {
                RequiredFieldValidatorDescGCP.Enabled = true;
            }
            else
            {
                RequiredFieldValidatorDescGCP.Enabled = false;
            }
        }

        protected void CiListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CiListCountry.SelectedItem.Text.Trim() != "PAKISTAN")
            {
                RequiredFieldValidatorProvince.Enabled = false;
                RequiredFieldValidatorCity.Enabled = false;
            }
            else
            {
                RequiredFieldValidatorProvince.Enabled = true;
                RequiredFieldValidatorCity.Enabled = true;
            }
        }
    }
}