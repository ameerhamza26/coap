using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using ExtensionMethods;
using System.Data;
using BioMetricClasses;

namespace CAOP
{
    public partial class Account_Individual : System.Web.UI.Page
    {
        bool BioMetricPopUp = false;
        string successs = "";
        string mesgg = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            SearchCIDModal.Focus();
            //if (ReadQueryStringID() == -1)
            //{
            //    if (!IsPostBack)
            //    {
            //        User LoggedUser = Session["User"] as User;
            //        CheckPermissions(LoggedUser);
            //        SetData();

            //        loaddata();

            //        documentRequiresLoad();
            //    }
            //}
            //else
            //{
            //    if (!IsPostBack)
            //    {

            //        int queryid = ReadQueryStringID();
            //        User LoggedUser = Session["User"] as User;
            //        documentRequiresLoad();

            //        CheckPermissions(LoggedUser);
            //        Session["BID"] = queryid;
            //        SetData();
            //        SetDataOpen(queryid);
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ShowAllAccountOpenIndividual()", true);
            //    }
            //}

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
                        

                        documentRequiresLoad();
                    }

                }
                else
                {
                    if (!IsPostBack)
                    {
                        SetBioMetric();
                        loaddata();

                        documentRequiresLoad();

                        Session["BID"] = queryid;
                        SetData();
                        SetDataOpen(queryid);
                       // SetBioMetric();
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
                        }
                    }
                    //else
                    //{
                    //   // ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showall()", true);
                    //    String mesg = "null";
                    //    CheckAccountIndividualTab(queryid, mesg);

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
            BtnUpdateAp.Visible = true;
            BtnUpdateAu.Visible = true;
            BtnUpdateCd.Visible = true;
            BtnUpdateDr.Visible = true;
            BtnUpdateAd.Visible = true;
            BtnUpdateKn.Visible = true;

            BtnUpdateNK.Visible = true;
        }

        private void SetCifSubmitVisible()
        {
            btnSubmitACa.Visible = true;
            btnSubmitACb.Visible = true;
            btnSubmitACc.Visible = true;
            BtnSubmitACd.Visible = true;
            BtnSubmitACe.Visible = true;
            BtnSubmitACf.Visible = true;
            BtnSubmitACg.Visible = true;

            BtnSubmitACh.Visible = true;
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



        DataTable dt = new DataTable();

        private void documentRequiresLoad()
        {
            User LoggedUser = Session["User"] as User;
            DescriptionDocuments d = new DescriptionDocuments();
            DcGrid.DataSource = d.GetAccountTypes();
            DcGrid.DataBind();

            foreach (GridViewRow r in DcGrid.Rows)
            {
                (r.FindControl("DcRadio2") as RadioButton).Checked = true;
            }


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

        private void SetData()
        {
            SetAccountNature();
            SetApplicantInformation();
            SetAddressInformation();
            SetOperatingInstruction();
            SetNextOfKinInfo();
            SetKnowYourCustomer();
            SetCertDeposit();
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
            CdListProfitAccount.Items.Insert(0, new ListItem( "Select","0"));

            CdListTransactionType.DataSource = t.GetAccountTypes();
            CdListTransactionType.DataValueField = "ID";
            CdListTransactionType.DataTextField = "NAME";
            CdListTransactionType.DataBind();

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
            Country countries = new Country();
            ResidentType rType = new ResidentType();
            PrimaryDocumentType pDoc = new PrimaryDocumentType();
            ExpectedCounterParties ECP = new ExpectedCounterParties();
            GeographiesCounterParties GCP = new GeographiesCounterParties();
            Reason_Account_Opening rac = new Reason_Account_Opening();

            KnListRAC.DataSource = rac.GetReason_Account_OpeningTypes();
            KnListRAC.DataValueField = "ID";
            KnListRAC.DataTextField = "Name";
            KnListRAC.DataBind();
            KnListRAC.Items.Insert(0, new ListItem("Select", "0"));

            KnListCustomerType.DataSource = c.GetCustomerType();
            KnListCustomerType.DataValueField = "ID";
            KnListCustomerType.DataTextField = "NAME";
            KnListCustomerType.DataBind();
            KnListCustomerType.Items.Insert(0, new ListItem("Select", "0"));

            KnListEducation.DataSource = e.GetEducation();
            KnListEducation.DataValueField = "ID";
            KnListEducation.DataTextField = "NAME";
            KnListEducation.DataBind();
            KnListEducation.Items.Insert(0, new ListItem("Select", "0"));

            KnListPurposeOfAccount.DataSource = p.GetAccountTypes();
            KnListPurposeOfAccount.DataValueField = "ID";
            KnListPurposeOfAccount.DataTextField = "NAME";
            KnListPurposeOfAccount.DataBind();
         //   KnListPurposeOfAccount.Items.Insert(0, new ListItem("Select", "0"));

            KnListSourceOfFunds.DataSource = s.GetSouceOfFund();
            KnListSourceOfFunds.DataValueField = "ID";
            KnListSourceOfFunds.DataTextField = "NAME";
            KnListSourceOfFunds.DataBind();
          //  KnListSourceOfFunds.Items.Insert(0, new ListItem("Select", "0"));

            KnListSerExemptCode.DataSource = sc.GetAccountTypes();
            KnListSerExemptCode.DataValueField = "ID";
            KnListSerExemptCode.DataTextField = "NAME";
            KnListSerExemptCode.DataBind();
            KnListSerExemptCode.Items.Insert(0, new ListItem("Select", "0"));

            KnListModeOfTransaction.DataSource = m.GetAccountTypes();
            KnListModeOfTransaction.DataValueField = "ID";
            KnListModeOfTransaction.DataTextField = "NAME";
            KnListModeOfTransaction.DataBind();

            //KnListAddresVerified.DataSource = a.GetAccountTypes();
            //KnListAddresVerified.DataValueField = "ID";
            //KnListAddresVerified.DataTextField = "NAME";
            //KnListAddresVerified.DataBind();

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

            KnListRelationAccountHolder.DataSource = re.GetRelationship();
            KnListRelationAccountHolder.DataValueField = "ID";
            KnListRelationAccountHolder.DataTextField = "NAME";
            KnListRelationAccountHolder.DataBind();
            KnListRelationAccountHolder.Items.Insert(0, new ListItem("Select", "0"));

            // New Manger Field
            KnddlManager.DataSource = rela.GetRelationships();
            KnddlManager.DataValueField = "ID";
            KnddlManager.DataTextField = "NAME";
            KnddlManager.DataBind();
            KnddlManager.Items.Insert(0, new ListItem("Select", "0"));

            knNationality.DataSource = countries.GetCountries();
            knNationality.DataValueField = "ID";
            knNationality.DataTextField = "NAME";
            knNationality.DataBind();
            knNationality.Items.Insert(0, new ListItem("Select", "0"));

            knListResidence.DataSource = rType.GetResidentTypes();
            knListResidence.DataValueField = "ID";
            knListResidence.DataTextField = "NAME";
            knListResidence.DataBind();
            knListResidence.Items.Insert(0, new ListItem("Select", "0"));

            knListDocType.DataSource = pDoc.GetPrimaryDocumentTypes();
            knListDocType.DataValueField = "ID";
            knListDocType.DataTextField = "NAME";
            knListDocType.DataBind();
            knListDocType.Items.Insert(0, new ListItem("Select", "0"));

            KnListSourceOfFund.DataSource = s.GetSouceOfFund();
            KnListSourceOfFund.DataValueField = "ID";
            KnListSourceOfFund.DataTextField = "NAME";
            KnListSourceOfFund.DataBind();
            KnListSourceOfFund.Items.Insert(0, new ListItem("Select", "0"));

            KnListECP.DataSource = ECP.GetExpectedCounterParties();
            KnListECP.DataValueField = "ID";
            KnListECP.DataTextField = "NAME";
            KnListECP.DataBind();

            KnListGCP.DataSource = GCP.GetGeographiesCounterParties();
            KnListGCP.DataValueField = "ID";
            KnListGCP.DataTextField = "NAME";
            KnListGCP.DataBind();
            


        }

        private void SetNextOfKinInfo()
        {
            Relationship r = new Relationship();
            Country c = new Country();
            City cities = new City();

            NkListRelationship.DataSource = r.GetRelationship();
            NkListRelationship.DataValueField = "ID";
            NkListRelationship.DataTextField = "NAME";
            NkListRelationship.DataBind();
            NkListRelationship.Items.Insert(0, new ListItem("Select", "0"));


            NkListCountry.DataSource = c.GetCountries();
            NkListCountry.DataValueField = "ID";
            NkListCountry.DataTextField = "NAME";
            NkListCountry.DataBind();
            NkListCountry.Items.Insert(0,new ListItem("Select","0"));

            NkListCity.DataSource = cities.GetCifTypes();
            NkListCity.DataValueField = "ID";
            NkListCity.DataTextField = "NAME";
            NkListCity.DataBind();
            NkListCity.Items.Insert(0, new ListItem("Select", "0"));
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

            RequiredFieldValidatorWhtProfitExpiry.Enabled = true;
            RequiredFieldValidatorWhtTrans.Enabled = true;

          //  AuExpDateExempted.Text = DateTime.Now.ToString("yyyy-MM-dd");
         //   AuExpDateProfit.Text = DateTime.Now.ToString("yyyy-MM-dd");
          //  AuExpDateTrans.Text = DateTime.Now.ToString("yyyy-MM-dd");

        }

        private void SetAddressInformation()
        {
            District d = new District();
            City c = new City();
            Country co = new Country();
            Province p = new Province();
          //  Sms_Alert_Required s = new Sms_Alert_Required();



            AdListCity.DataSource = c.GetCifTypes();
            AdListCity.DataValueField = "ID";
            AdListCity.DataTextField = "NAME";
            AdListCity.DataBind();
            AdListCity.Items.Insert(0, new ListItem("Select", "0"));

            AdListCountry.DataSource = co.GetCountries();
            AdListCountry.DataValueField = "ID";
            AdListCountry.DataTextField = "NAME";
            AdListCountry.DataBind();
            AdListCountry.Items.Insert(0, new ListItem("Select", "0"));

            AdListProvince.DataSource = p.GetProvinces();
            AdListProvince.DataValueField = "ID";
            AdListProvince.DataTextField = "NAME";
            AdListProvince.DataBind();
            AdListProvince.Items.Insert(0, new ListItem("Select", "0"));

            //AdListSmsRequired.DataSource = s.GetAccountTypes();
            //AdListSmsRequired.DataValueField = "ID";
            //AdListSmsRequired.DataTextField = "NAME";
            //AdListSmsRequired.DataBind();

        }

        private void SetApplicantInformation()
        {
            ApplicantStatuses a = new ApplicantStatuses();
            Relationship r = new Relationship();

            ApListApplicantStatus.DataSource = a.GetApplicantStatuses();
            ApListApplicantStatus.DataValueField = "ID";
            ApListApplicantStatus.DataTextField = "NAME";
            ApListApplicantStatus.DataBind();
            ApListApplicantStatus.Items.Insert(0, new ListItem("Select", "0"));

            ApListRelationPrimary.DataSource = r.GetRelationship();
            ApListRelationPrimary.DataValueField = "ID";
            ApListRelationPrimary.DataTextField = "NAME";
            ApListRelationPrimary.DataBind();

            //            lstCOR.Items.Insert(0, new ListItem("Select", "0"));
            ApListRelationPrimary.Items.Insert(0, new ListItem("Select", "0"));

            GridViewAccountCifs.DataSource = new List<ApplicantInformationCifs>();
            GridViewAccountCifs.DataBind();

        }

        private void SetAccountNature()
        {
            Gl_code g = new Gl_code();
            Sl_code s = new Sl_code();
            AccountType a = new AccountType();
            Currency c = new Currency();
            AccountOpenType an = new AccountOpenType();
            AccountClass Ac = new AccountClass();
            LSO lsoList = new LSO();

            //AcListGlCode.DataSource = g.GetGlCodeTypes();
            //AcListGlCode.DataValueField = "ID";
            //AcListGlCode.DataTextField = "NAME";
            //AcListGlCode.DataBind();

            //AcListSlCode.DataSource = s.GetGlCodeTypes();
            //AcListSlCode.DataValueField = "ID";
            //AcListSlCode.DataTextField = "NAME";
            //AcListSlCode.DataBind();

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

          //  AcListAccountType.DataSource = a.GetAccountTypes();
          //  AcListAccountType.DataValueField = "ID";
          //  AcListAccountType.DataTextField = "NAME";
          //  AcListAccountType.DataBind();
            AcListAccountType.Items.Insert(0, new ListItem("Select", "0"));
            AcListAccountGroup.Items.Insert(0, new ListItem("Select", "0"));
            AcListAccountMode.Items.Insert(0, new ListItem("Select", "0"));

            AcListAccountClass.DataSource = Ac.GetAccountClassTypes();
            AcListAccountClass.DataValueField = "ID";
            AcListAccountClass.DataTextField = "NAME";
            AcListAccountClass.DataBind();
            AcListAccountClass.Items.Insert(0, new ListItem("Select", "0"));

            LSOOfficerCode.DataSource = lsoList.GetLSOList();
            LSOOfficerCode.DataValueField = "ID";
            LSOOfficerCode.DataTextField = "NAME";
            LSOOfficerCode.DataBind();
            LSOOfficerCode.Items.Insert(0, new ListItem("Select", "0"));

            AcEntryDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        private void SetDataOpen(int id)
        {
            SetAccountNatureOpen(id);
            SetApplicantInformationOpen(id);
            SetAddressInformationOpen(id);
            SetOperatingInstructionOpen(id);
            SetNextOfKinInfoOpen(id);
            SetKnowYourCustomerOpen(id);
            SetCertDepositOpen(id);
            SetDocumentRequiredOpen(id);
            String mesg = "null";
            CheckAccountIndividualTab(id, mesg);

        }

        private void SetDocumentRequiredOpen(int id)
        {
            try 
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
            catch(Exception e)
            {

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
                ListExtensions.SetDropdownValue(a.PROFIT_ACCOUNT_TYPE.ID, CdListProfitAccount);
                CdProfitAccountNumber.Text = a.PROFIT_ACCOUNT_NUMBER;
                ListExtensions.SetDropdownValue(a.TRANSACTION_TYPE.ID, CdListTransactionType);
                CdChequePrefix.Text = a.CHEQUE_PREFIX;
                CdChequeNumber.Text = a.CHEQUE_NUMBER;
                CdCertNumber.Text = a.CERTIFICATE_NUMBER;
                CdCertAmount.Text = a.CERTIFCATE_AMOUNT;
                CdMarkupRate.Text = a.MARK_UP_RATE;
                ListExtensions.SetDropdownValue(a.PRINCIPAL_RENEWAL_OPTION.ID, CdLstPrincipalRenewal);
                CdSubmitButton.Visible = false;

            }
        }

        private void SetKnowYourCustomerOpen(int id)
        {
            AccountKnowYourCustomer a = new AccountKnowYourCustomer();
            if (a.GetKnowYourCustomer(id))
            {
                ListExtensions.SetDropdownValue(a.CUSTOMER_TYPE.ID, KnListCustomerType);
                ListExtensions.SetDropdownValue(a.RAC.ID, KnListRAC);
                knTextRACDetail.Text = a.RAC_DETAIL;
                KnDescrIfRefered.Text = a.DESCRIPTION_IF_REFFERED;
                ListExtensions.SetDropdownValue(a.EDUCATION.ID, KnListEducation);
              //  ListExtensions.SetDropdownValue(a.PURPOSE_OF_ACCOUNT.ID, KnListPurposeOfAccount);
                foreach(var p in a.PURPOSE_OF_ACCOUNT)
                {
                    KnListPurposeOfAccount.Items.FindByValue(p.ToString()).Selected = true;
                }
                KnDescrOther.Text = a.DESCRIPTION_IF_OTHER;
               // ListExtensions.SetDropdownValue(a.SOURCE_OF_FUNDS.ID, KnListSourceOfFunds);

                foreach (var s in a.SOURCE_OF_FUNDS)
                {
                    KnListSourceOfFunds.Items.FindByValue(s.ToString()).Selected = true;
                }
                KnDescrOfSource.Text = a.DESCRIPTION_OF_SOURCE;
                SetRadioButton(a.SERVICE_CHARGES_EXEMPTED, ServiceExemptedRadio1, ServiceExemptedRadio2);
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
               // ListExtensions.SetDropdownValue(a.ADDRESS_VERIFIED.ID, KnListAddresVerified);
                ListExtensions.SetDropdownValue(a.RELATIONSHIP_MANAGER.ID,KnddlManager);

                SetRadioButton(a.OCCUPATION_VERIFIED, OccupyVerifyRadio1, OccupyVerifyRadio2);
                if (RadioButtonAddressVerifiedYes.Checked)
                    a.ADDRESS_VERIFIED = 1;
                else
                    a.ADDRESS_VERIFIED = 0;
             //   ListExtensions.SetDropdownValue(a.ADDRESS_VERIFIED.ID, KnListAddresVerified);
                ListExtensions.SetDropdownValue(a.MEANS_OF_VERIFICATION.ID, KnListMeansOfVerification);
                KnMeanVerifyOther.Text = a.MEANS_OF_VERI_OTHER;
                SetRadioButton(a.IS_VERI_SATISFACTORY, IsVeriSatiRadio1, IsVeriSatiRadio2);
                KnDetailNotSatis.Text = a.DETAIL_IF_NOT_SATISFACTORY;
                ListExtensions.SetDropdownValue(a.COUNTRY_HOME_REMITTANCE.ID, KnListCountHomeRemit);
                ListExtensions.SetDropdownValue(a.REAL_BENEFICIARY_ACCOUNT.ID, KnListRealBenef);
                if (KnListRealBenef.SelectedItem.Text == "OTHER")
                {
                    RequiredFieldValidatorBName.Enabled = true;
                    RequiredFieldValidatorRelaWithAcc.Enabled = true;
                    RequiredFieldValidatorBAddress.Enabled = true;
                    RequiredFieldValidatorNationality.Enabled = true;
                    RequiredFieldValidatorKnResidence.Enabled = true;
                    RequiredFieldValidatorDocType.Enabled = true;
                    RequiredFieldValidatorIdNumber.Enabled = true;
                    RequiredFieldValidatorExpiry.Enabled = true;
                    RequiredFieldValidatorBSOF.Enabled = true;
                    KnNameOther.Enabled = true;
                    KnListRelationAccountHolder.Enabled = true;
                    KntxtAddress.Enabled = true;
                    knNationality.Enabled = true;
                    knListResidence.Enabled = true;
                    KnTxtResOther.Enabled = true;
                    knListDocType.Enabled = true;
                    KnCnicOther.Enabled = true;
                    KntxtExpiry.Enabled = true;
                    KnListSourceOfFund.Enabled = true;
                }
                else
                {

                    RequiredFieldValidatorBName.Enabled = false;
                    RequiredFieldValidatorRelaWithAcc.Enabled = false;
                    RequiredFieldValidatorBAddress.Enabled = false;
                    RequiredFieldValidatorNationality.Enabled = false;
                    RequiredFieldValidatorKnResidence.Enabled = false;
                    RequiredFieldValidatorDescResi.Enabled = false;
                    RequiredFieldValidatorDocType.Enabled = false;
                    RequiredFieldValidatorIdNumber.Enabled = false;
                    RequiredFieldValidatorExpiry.Enabled = false;
                    RequiredFieldValidatorBSOF.Enabled = false;
                    KnNameOther.Enabled = false;
                    KnListRelationAccountHolder.Enabled = false;
                    KntxtAddress.Enabled = false;
                    knNationality.Enabled = false;
                    knListResidence.Enabled = false;
                    KnTxtResOther.Enabled = false;
                    knListDocType.Enabled = false;
                    KnCnicOther.Enabled = false;
                    KntxtExpiry.Enabled = false;
                    KnListSourceOfFund.Enabled = false;


                }
                KnNameOther.Text = a.NAME_OTHER;
                KnCnicOther.Text = a.CNIC_OTHER;
                ListExtensions.SetDropdownValue(a.RELATIONSHIP_WITH_ACCOUNTHOLDER.ID, KnListRelationAccountHolder);
                KnRelationDetailOther.Text = a.RELATIONSHIP_DETAIL_OTHER;

                KntxtAddress.Text = a.BENEFICIAL_ADDRESS;
                ListExtensions.SetDropdownValue(a.BENEFICIAL_NATIONALITY, knNationality);
                ListExtensions.SetDropdownValue(a.BENEFICIAL_RESIDENCE, knListResidence);
                KnTxtResOther.Text = a.BENEFICIAL_RESIDENCE_DESC;
                ListExtensions.SetDropdownValue(a.BENEFICIAL_IDENTITY, knListDocType);
                KntxtExpiry.Text = a.BENEFICIAL_IDENTITY_EXPIRY;
                ListExtensions.SetDropdownValue(a.BENEFICIAL_SOURCE_OF_FUND, KnListSourceOfFund);

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

                if (KnListRealBenef.SelectedItem.Text == "OTHER")
                {
                    RequiredFieldValidatorBName.Enabled = true;
                    RequiredFieldValidatorRelaWithAcc.Enabled = true;
                    RequiredFieldValidatorBAddress.Enabled = true;
                    RequiredFieldValidatorNationality.Enabled = true;
                    RequiredFieldValidatorKnResidence.Enabled = true;
                    RequiredFieldValidatorDocType.Enabled = true;
                    RequiredFieldValidatorIdNumber.Enabled = true;
                    RequiredFieldValidatorExpiry.Enabled = true;
                    RequiredFieldValidatorBSOF.Enabled = true;
                }

                bool Other = KnListPurposeOfAccount.Items.FindByText("OTHER").Selected == true;
                if (Other)
                    ReqValidatorPurposeAccountOther.Enabled = true;
                else
                    ReqValidatorPurposeAccountOther.Enabled = false;

                Other = KnListSourceOfFunds.Items.FindByText("OTHERS").Selected == true;
                if (Other)
                    ReqValidatorSourceDesc.Enabled = true;
                else
                    ReqValidatorSourceDesc.Enabled = false;

                Other = KnListModeOfTransaction.Items.FindByText("Other").Selected == true;
                if (Other)
                    ReqValidatorMotOther.Enabled = true;
                else
                    ReqValidatorMotOther.Enabled = false;

                if (knListResidence.SelectedItem.Value == "2")
                    RequiredFieldValidatorDescResi.Enabled = true;
                else
                    RequiredFieldValidatorDescResi.Enabled = false;


                KnSubmitButton.Visible = false;

            }
        }

        private void SetNextOfKinInfoOpen(int id)
        {
            AccountNexofKinInfo a = new AccountNexofKinInfo();
            if (a.GetAccountNextofKinInfo(id))
            {
                NkCifNo.Text = a.NEXT_OF_KIN;
                NkCNIC.Text = a.NEXT_OF_KIN_CNIC;
                NkName.Text = a.NEXT_OF_KIN_NAME;
                ListExtensions.SetDropdownValue(a.RELATIONSHIP.ID, NkListRelationship);
                NkRelationDetailOther.Text = a.RELATIONSHIP_DETAIL;

                ListExtensions.SetDropdownValue(a.COUNTRY.ID, NkListCountry);
                ListExtensions.SetDropdownValue(a.CITY.ID, NkListCity);
                NkBuilding.Text = a.BUILDING;
                NkTxtfloor.Text = a.FLOOR;
                NkTxtStreet.Text = a.STREET;
                NkTxtDistrict.Text = a.DISTRICT;
                NkTxtPostOffice.Text = a.POST_OFFICE;
                NkTxtPostalCode.Text = a.POSTAL_CODE;
                NktxtResidenceContact.Text = a.RESIDENCE_CONTACT;
                NkTxtOfficeNo.Text = a.OFFICE_NO;
                NkTxtMobileNo.Text = a.MOB_NO;

                NkSubmitButton.Visible = false;
            }
        }

        private void SetOperatingInstructionOpen(int id)
        {
            OperatingInstructions o = new OperatingInstructions();
            if (o.GetOperatingInstruction(id))
            {
                ListExtensions.SetDropdownValue(o.AUTHORITY_TO_OPERATE.ID, AuListAuthority);
                AuDescriptionOther.Text = o.DESCRIPTION_IF_OTHER;
                SetRadioButton(o.ZAKAT_DEDUCTION, ZakatDeductionRadio1, ZakatDeductionRadio2);
                ListExtensions.SetDropdownValue(o.ZAKAT_EXEMPTION_TYPE.ID, AuListZakatExemption);
                AuExempReasonDetail.Text = o.EXEMPTION_REASON_DETAIL;
                AuListAccountFrequenct.ClearSelection();
                AuListAccountFrequenct.Items.FindByValue(o.ACCOUNT_STATEMENT_FREQUENCY.ID.ToString()).Selected = true;
                AuDescrHoldMail.Text = o.DESCRIPTION_IF_HOLD_MAIL;
                SetRadioButton(o.ATM_CARD_REQUIRED, AtmRequiredRadio1, AtmRequiredRadio2);
                AuCustomerNameAtm.Text = o.CUSTOMER_NAME_ON_ATMCARD;
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
                ListExtensions.SetDropdownValue(o.PROFIT_PAYMENT.ID, AuListProfitPayment);
                SetRadioButton(o.WHT_DEDUCTED_ON_PROFIT, WhtProfitRadio1, WhtProfitRadio2);
                AuExpDateProfit.Text = o.EXPIRY_DATE_PROFIT;
                SetRadioButton(o.WHT_DEDUCTED_ON_TRANSACTION, WhtTransactionRadio1, WhtTransactionRadio2);
                AuExpDateTrans.Text = o.EXPIRY_DATE_TRANSACTION;

                if (WhtProfitRadio1.Checked)
                    RequiredFieldValidatorWhtProfitExpiry.Enabled = false;

                if (WhtTransactionRadio1.Checked)
                    RequiredFieldValidatorWhtTrans.Enabled = false;

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
        private void SetAddressInformationOpen(int id)
        {
            AccountAddressInformation a = new AccountAddressInformation();
            if (a.GetAccountAddress(id))
            {
                ListExtensions.SetDropdownValue(a.COUNTRY.ID, AdListCountry);
                ListExtensions.SetDropdownValue(a.CITY.ID, AdListCity);
                ListExtensions.SetDropdownValue(a.PROVINCE.ID, AdListProvince);
                AdTxtBuilding.Text = a.BUILDING_SUITE;
                AdTxtFloor.Text = a.FLOOR;
                AdTxtStreet.Text = a.STREET;
                AdTxtDistrict.Text = a.DISTRICT;
                AdPoBox.Text = a.PO_BOX;
                AdPostalCode.Text = a.POSTAL_CODE;
                
                AdOffice.Text = a.TEL_OFFICE;
                AdResidence.Text = a.TEL_RESIDENCE;
                AdMobileNo.Text = a.MOBILE_NO;
                AdFaxNo.Text = a.FAX_NO;

                if (a.SMS_ALERT_REQUIRED == 1)
                    RadioButtonSms1.Checked = true;
                else
                    RadioButtonSms2.Checked = false;

                if (a.EMAIL == true)
                {
                    Emails e = new Emails();
                    if (e.GetEmail(id))
                    {
                        AdEmail.Text = e.EMAIL;
                        if (e.REQUIRED_ESTATEMEN == true)
                        {
                            AdEstatementCheckbox.Checked = true;
                        }

                    }
                }
                else
                {
                    AdEmail.Text = "";
                    AdEstatementCheckbox.Checked = false;
                }

                AdSubmitButton.Visible = false;
            }
        }
        private void SetApplicantInformationOpen(int id)
        {
            AccountApplicantInformation a = new AccountApplicantInformation();
            if (a.GetApplicantOpen(id))
            {
                if (a.Cifs == null)
                {
                    ApCustomerCif.Text = a.CUSTOMER_CIF_NO;
                    ApCustomerName.Text = a.CUSTOMER_NAME;
                    ApCustomerCNIC.Text = a.CUSTOMER_CNIC;

                    if (a.IS_PRIMARY_ACCOUNT_HOLDER == 1)
                    {
                        ApIsPrimaryRadio1.Checked = true;
                    }
                    else
                    {
                        ApIsPrimaryRadio2.Checked = true;

                    }

                    if (a.ACCOUNT_IN_NEGATIVE_LIST == 1)
                    {
                        ApApplicantNegativeRadio1.Checked = true;
                    }
                    else
                    {
                        ApApplicantNegativeRadio2.Checked = true;
                    }

                    if (a.POWER_OF_ATTORNY == 1)
                    {
                        ApPowerAttornyRadio1.Checked = true;
                    }
                    else
                    {
                        ApPowerAttornyRadio2.Checked = true;
                    }
                    if (a.SIGNATURE_AUTHORITY == 1)
                    {
                        ApSignatureRadio1.Checked = true;
                    }
                    else
                    {
                        ApSignatureRadio2.Checked = true;
                    }

                    ListExtensions.SetDropdownValue(a.APPLICANT_STATUS.ID, ApListApplicantStatus);
                    ListExtensions.SetDropdownValue(a.RELATIONSHIP_NOT_PRIMARY.ID, ApListRelationPrimary);
                    ApRelationshipDetail.Text = a.RELATIONSHIP_DETAIL;
                    ApInvestmentShare.Text = a.INVESTMENT_SHARE;
                }
                else
                {
                    
                    GridViewAccountCifs.DataSource = a.Cifs;
                    GridViewAccountCifs.DataBind();
                    GridViewAccountCifs.Enabled = true;
                    GridViewAccountCifs.Visible = true;
                    btnGridAddCif.Visible = false;
                    Session["GridCif"] = null;
                    Session["GridCif"] = a.Cifs;
                  
                }


              

                ApSubmitButton.Visible = false;
            }
        }
        private void SetAccountNatureOpen(int id)
        {
            AccountNatureCurrency a = new AccountNatureCurrency();
            if (a.GetAccountNatureIndividual(id))
            {
                if (a.CNIC_VERIFIED == true)
                {
                    AcCnicVerifiedCheck.Checked = true;
                }
                else
                    AcCnicVerifiedCheck.Checked = false;
                
                ListExtensions.SetDropdownValue(a.ACCOUNT_OPEN_TYPE.ID, AcListAccountOpen);
                AcEntryDate.Text = a.ACCOUNT_ENTRY_DATE.ToString("yyyy-MM-dd");
              //  ListExtensions.SetDropdownValue(a.GL_CODE.ID, AcListGlCode);
             //   ListExtensions.SetDropdownValue(a.SL_CODE.ID, AcListSlCode);

                AccountType at = new AccountType();
                AcListAccountType.DataSource = at.GetAccountTypesByVal(Convert.ToInt32(a.ACCOUNT_TYPE.ID));
                AcListAccountType.DataValueField = "ID";
                AcListAccountType.DataTextField = "NAME";
                AcListAccountType.DataBind();
                AcListAccountType.Items.Insert(0, new ListItem("Select", "0"));

                string clsgrp = at.GetAccountTypeClsGrp(a.ACCOUNT_TYPE.ID);
                AccountGroup ag = new AccountGroup();
                AcListAccountGroup.DataSource = ag.GetAccountGroupTypes(clsgrp.Split(',')[0], clsgrp.Split(',')[1]);
                AcListAccountGroup.DataValueField = "ID";
                AcListAccountGroup.DataTextField = "NAME";
                AcListAccountGroup.DataBind();

                clsgrp = ag.GetCls(Convert.ToInt32(AcListAccountGroup.SelectedItem.Value));
                AccountClass ac = new AccountClass();
                AcListAccountClass.DataSource = ac.GetAccountClassTypes(clsgrp);
                AcListAccountClass.DataValueField = "ID";
                AcListAccountClass.DataTextField = "NAME";
                AcListAccountClass.DataBind();


              //  AcListAccountClass.Enabled = false;
              //  AcListAccountGroup.Enabled = false;

                 ListExtensions.SetDropdownValue(a.ACCOUNT_TYPE.ID, AcListAccountType);
                 AccountModes am = new AccountModes();
                 AcListAccountMode.DataSource = am.GetAccountModes(Convert.ToInt32(AcListAccountType.SelectedItem.Value));
                 AcListAccountMode.DataValueField = "ID";
                 AcListAccountMode.DataTextField = "NAME";
                 AcListAccountMode.DataBind();
                 AcListAccountMode.Items.Insert(0, new ListItem("Select", "0"));
                 ListExtensions.SetDropdownValue(a.ACCOUNT_MODE_DETAIL, AcListAccountMode);



                ListExtensions.SetDropdownValue(a.CURRENCY.ID, AcListCurrency);
                AcAccountNumber.Text = a.ACCOUNT_NUMBER;
                AcAccountTitle.Text = a.ACCOUNT_TITLE;
                AcInitialDeposit.Text = a.INITIAL_DEPOSIT;
                if (a.ACCOUNT_MODE == true)
                {
                    AcAccountModeRadio1.Checked = true;

                    // Enabling Primary account Radio button
                    ApIsPrimaryRadio1.Enabled = false;
                    ApIsPrimaryRadio2.Enabled = false;

                    // set primary acc holder to false                  
                    ApIsPrimaryRadio1.Checked = true;
                    ApIsPrimaryRadio2.Checked = false;

                    // Disabling grid
                    GridViewAccountCifs.Visible = false;
                    GridViewAccountCifs.Enabled = false;
                    btnGridAddCif.Visible = false;


                    // signature authority 
                    ApSignatureRadio1.Enabled = false;
                    ApSignatureRadio2.Enabled = false;
                    ApSignatureRadio2.Checked = false;
                    ApSignatureRadio1.Checked = true;

                    // if single then applicant status should be individual and disabled
                    ApListApplicantStatus.Items.FindByText("INDIVIDUAL").Selected = true;
                    ApListApplicantStatus.Enabled = false;

                    // disable relationshil ddl if single
                    ApListRelationPrimary.Enabled = false;

                    // authority to operate shoub be singe and disable if accoutn is single
                    AuListAuthority.Items.FindByText("Single").Selected = true;
                    AuListAuthority.Enabled = false;
                }
                else
                {
                    AcAccountModeRadio2.Checked = true;

                    // disabling Primary account Radio button
                    ApIsPrimaryRadio1.Enabled = true;
                    ApIsPrimaryRadio2.Enabled = true;

                    // set primary acc holder to false
                    ApIsPrimaryRadio1.Checked = false;
                    ApIsPrimaryRadio2.Checked = true;

                    GridViewAccountCifs.DataSource = new List<String>();
                    GridViewAccountCifs.DataBind();
                    // enabling grid
                    GridViewAccountCifs.Visible = true;
                    GridViewAccountCifs.Enabled = true;
                    btnGridAddCif.Visible = true;

                    // Setting validation group to joint
                    ApSubmitButton.ValidationGroup = "JOINTACCOUNT";
                    CustomValidatorAccountType.ValidationGroup = "JOINTACCOUNT";
                }

                //if (a.MINOR_ACCOUNT == true)
                //{
                //    AcMinorAccountRadio1.Checked = true;
                //}
                //else
                //{
                //    AcMinorAccountRadio2.Checked = true;
                //}

                if (AcListAccountGroup.SelectedItem.Text == "Certificates of Deposit")
                {
                    RequiredFieldValidatorCdMarkupRate.Enabled = true;
                    RequiredFieldValidatorCdLstPrincipalRenewal.Enabled = true;
                    RequiredFieldValidatorCdCertNumber.Enabled = true;
                    RequiredFieldValidatorProfitAccNumber.Enabled = true;
                }

                AcSubmitButton.Visible = false;

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
        public int ReadQueryStringID()
        {
            if (Request.QueryString["ID"] != null)
            {
                return Convert.ToInt32(Request.QueryString["ID"]);
            }
            else
                return -1;
        }


        protected void AcListAccountOpen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AcListAccountOpen.SelectedItem.Text == "BUSINESS")
            {
                Response.Redirect("Account_Business.aspx");
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

                a.ACCOUNT_ENTRY_DATE = Convert.ToDateTime(AcEntryDate.Text);
                //  a.GL_CODE = new Gl_code() { ID = Convert.ToInt32(AcListGlCode.SelectedItem.Value), NAME = AcListGlCode.SelectedItem.Text };
                //  a.SL_CODE = new Sl_code() { ID = Convert.ToInt32(AcListSlCode.SelectedItem.Value), NAME = AcListSlCode.SelectedItem.Text };
                a.ACCOUNT_TYPE = new AccountType() { ID = Convert.ToInt32(AcListAccountType.SelectedItem.Value), Name = AcListAccountType.SelectedItem.Text };
                a.CURRENCY = new Currency() { ID = Convert.ToInt32(AcListCurrency.SelectedItem.Value), NAME = AcListCurrency.SelectedItem.Text };
                a.ACCOUNT_NUMBER = AcAccountNumber.Text;
                a.ACCOUNT_TITLE = AcAccountTitle.Text;
                a.INITIAL_DEPOSIT = AcInitialDeposit.Text;
                a.ACCOUNT_OPEN_TYPE = new AccountOpenType() { ID = Convert.ToInt32(AcListAccountOpen.SelectedItem.Value), NAME = AcListAccountOpen.SelectedItem.Text };
                a.ACCOUNT_MODE_DETAIL = Convert.ToInt32(AcListAccountMode.SelectedItem.Value);
                if (AcAccountModeRadio1.Checked == true)
                {
                    a.ACCOUNT_MODE = true;

                    ApIsPrimaryRadio1.Enabled = false;
                    ApIsPrimaryRadio2.Enabled = false;

                    // if account is single disable signature should be disable and yes
                    ApSignatureRadio1.Enabled = false;
                    ApSignatureRadio2.Enabled = false;
                    ApSignatureRadio2.Checked = false;
                    ApSignatureRadio1.Checked = true;

                    // if single then applicant status should be individual and disabled
                    ApListApplicantStatus.ClearSelection();
                    ApListApplicantStatus.Items.FindByText("INDIVIDUAL").Selected = true;
                    ApListApplicantStatus.Enabled = false;

                    // disable relationshil ddl if single
                    ApListRelationPrimary.Enabled = false;

                    // authority to operate shoub be singe and disable if accoutn is single
                    AuListAuthority.ClearSelection();
                    AuListAuthority.Items.FindByText("Single").Selected = true;
                    AuListAuthority.Enabled = false;
                }
                else
                {
                    a.ACCOUNT_MODE = false;

                    // Setting Validation group according to joint account on grid add button
                    ApSubmitButton.ValidationGroup = "JOINTACCOUNT";
                    CustomValidatorAccountType.ValidationGroup = "JOINTACCOUNT";

                    GridViewAccountCifs.Visible = true;
                    GridViewAccountCifs.Enabled = true;
                    btnGridAddCif.Visible = true;
                }

                //if (AcMinorAccountRadio1.Checked == true)
                //{
                //    a.MINOR_ACCOUNT = true;

                //}
                //else
                //{
                //    a.MINOR_ACCOUNT = false;

                //}

                Session["BID"] = a.SetAccountNatureIndividual();

                String mesg = "Account Nature and currency has been saved";
                int id = Convert.ToInt32(Session["BID"]);

                CheckAccountIndividualTab(id, mesg);
                AcSubmitButton.Visible = false;
                AcListAccountClass.Enabled = false;
                AcListAccountGroup.Enabled = false;

                if (AcListAccountGroup.SelectedItem.Text == "Certificates of Deposit")
                {
                    RequiredFieldValidatorCdMarkupRate.Enabled = true;
                    RequiredFieldValidatorCdLstPrincipalRenewal.Enabled = true;
                    RequiredFieldValidatorCdCertNumber.Enabled = true;
                    RequiredFieldValidatorProfitAccNumber.Enabled = true;
                }
                    
            }
           
        }

        private void CheckAccountIndividualTab(int id, String mesg)
        {
            AccountApplicantInformation a = new AccountApplicantInformation();
            AccountAddressInformation ad = new AccountAddressInformation();
            OperatingInstructions o = new OperatingInstructions();
            AccountNexofKinInfo ak = new AccountNexofKinInfo();
            AccountKnowYourCustomer ac = new AccountKnowYourCustomer();
            AccountCertDepositInfo ai = new AccountCertDepositInfo();
            DocumentsRequired d = new DocumentsRequired();
            String app = null;
            String add = null;
            String op = null;
            String nkn = null;
            String know = null;
            String cert = null;
            String doc = null;

            if (a.CheckIndividualApplicantInformation(id))
            {
                app = "1";
            }

            if (ad.CheckIndividualAddressInformation(id))
            {
                add = "1";
            }
            if (o.CheckIndividualOperatinInstruction(id))
            {
                op = "1";
            }
            if (ak.CheckIndividualNextOfKin(id))
            {
                nkn = "1";
            }
            if (ac.CheckIndividualKnowYourCustomer(id))
            {
                know = "1";
            }
            if (ai.CheckCertDeposit(id))
            {
                cert = "1";
            }

            if (d.CheckDocumentRequired(id))
            {
                doc = "1";
            }

            if (BioMetricPopUp)
            {
                string temp = "IndividualAccountOpenPendingAlertBio('" + app + "','" + add + "','" + op + "','" + nkn + "','" + know + "','" + cert + "','" + doc + "','" + mesgg + "','" + successs + "');";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", temp , true);
            }
               


            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "IndividualAccountOpenPendingAlert('" + app + "','" + add + "','" + op + "','" + nkn + "','" + know + "','" + cert + "','" + doc + "','" + mesg + "');", true);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ShowAllAccountOpenIndividual()", true);
            

        }

        protected void ApSearchCifButton_Click(object sender, EventArgs e)
        {

        }

        protected void ApSubmitButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                AccountApplicantInformation a = new AccountApplicantInformation();
                a.BI_ID = Convert.ToInt32(Session["BID"]);
                a.CUSTOMER_CIF_NO = ApCustomerCif.Text;
                a.CUSTOMER_NAME = ApCustomerName.Text;
                a.CUSTOMER_CNIC = ApCustomerCNIC.Text;


                if (!AcAccountModeRadio1.Checked)
                {
                    a.Cifs = Session["GridCif"] as List<ApplicantInformationCifs>;
                }
                else
                {
                    if (ApIsPrimaryRadio1.Checked == true)
                    {
                        a.IS_PRIMARY_ACCOUNT_HOLDER = 1;

                    }
                    else
                    {
                        a.IS_PRIMARY_ACCOUNT_HOLDER = 0;
                    }


                    if (ApApplicantNegativeRadio1.Checked == true)
                    {
                        a.ACCOUNT_IN_NEGATIVE_LIST = 1;
                    }
                    else
                    {
                        a.ACCOUNT_IN_NEGATIVE_LIST = 0;
                    }

                    if (ApPowerAttornyRadio1.Checked == true)
                    {
                        a.POWER_OF_ATTORNY = 1;
                    }
                    else
                    {
                        a.POWER_OF_ATTORNY = 0;
                    }

                    if (ApSignatureRadio1.Checked == true)
                    {
                        a.SIGNATURE_AUTHORITY = 1;
                    }
                    else
                    {
                        a.SIGNATURE_AUTHORITY = 0;
                    }

                    a.APPLICANT_STATUS = new ApplicantStatuses() { ID = Convert.ToInt32(ApListApplicantStatus.SelectedItem.Value), Name = ApListApplicantStatus.SelectedItem.Text };
                    a.RELATIONSHIP_NOT_PRIMARY = new Relationship() { ID = Convert.ToInt32(ApListRelationPrimary.SelectedItem.Value), NAME = ApListRelationPrimary.SelectedItem.Text };
                    a.RELATIONSHIP_DETAIL = ApRelationshipDetail.Text;
                    a.INVESTMENT_SHARE = ApInvestmentShare.Text;
                }

                a.SaveAccountOpen();
                btnGridAddCif.Visible = false;
                Session["GridCif"] = null;
                AccOpen ac = new AccOpen(Convert.ToInt32(Session["BID"]), AccountOpenTypes.INDIVIDUAL);

                if (ac.CheckIfCompleted())
                {
                    User LoggedUser = Session["User"] as User;
                    ac.ChangeStatus(Status.SUBMITTED, LoggedUser);
                    Response.Redirect("AccountList.aspx");
                }


                String msg = "Applicant Information has been saved";
                int id = Convert.ToInt32(Session["BID"]);
                CheckAccountIndividualTab(id, msg);
                ApSubmitButton.Visible = false;
            }


            String meSsg = "null";
            int Iid = Convert.ToInt32(Session["BID"]);
            CheckAccountIndividualTab(Iid, meSsg);

        }

        protected void searchCIF_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            searchCIF.PageIndex = e.NewPageIndex;
            loaddata();
        }

        protected void searchCIF_RowDataBound(object sender, GridViewRowEventArgs e)
        {



        }

        protected void Select_Click(object sender, EventArgs e)
        {

            LinkButton b = sender as LinkButton;

            GridViewRow g = (GridViewRow)b.NamingContainer;

            ApCustomerCif.Text = ((Label)g.FindControl("lblId")).Text;
            ApCustomerCNIC.Text = ((Label)g.FindControl("lblCnic")).Text;
            ApCustomerName.Text = ((Label)g.FindControl("lblName")).Text;
            //string rowIndex = (sender as LinkButton).CommandArgument;


           

            String mesg = "null";
            int id = Convert.ToInt32(Session["BID"]);
            CheckAccountIndividualTab(id, mesg);

            //  ApCustomerCif.Text = rowIndex;
            // ApCustomerCNIC.Text = searchCIF.Rows[Convert.ToInt32(rowIndex)].Cells[3].Text;
            // ApCustomerName.Text = searchCIF.Rows[Convert.ToInt32(rowIndex)].Cells[2].Text;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ShowAllAccountOpenIndividual()", true);

            //    string id = searchCIF.Rows[Convert.ToInt32(rowIndex)].Cells[0].Text;
            //    ApCustomerCif.Text = id;
            //    ApCustomerName.Text = searchCIF.Rows[Convert.ToInt32(rowIndex)].Cells[2].Text;
            //    ApCustomerCNIC.Text = searchCIF.Rows[Convert.ToInt32(rowIndex)].Cells[3].Text;
        }

        protected void searchCIF_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void AdSubmitButton_Click(object sender, EventArgs e)
        {
            AccountAddressInformation a = new AccountAddressInformation();
            a.BI_ID = Convert.ToInt32(Session["BID"]);
            a.COUNTRY = new Country() { ID = Convert.ToInt32(AdListCountry.SelectedItem.Value), Name = AdListCountry.SelectedItem.Text };
            if (AdListCountry.SelectedItem.Text.Trim() != "PAKISTAN")
            {
                a.CITY = new City() { ID = null };
                a.PROVINCE = new Province() { ID = null };
            }
            else
            {
                a.CITY = new City() { ID = Convert.ToInt32(AdListCity.SelectedItem.Value), Name = AdListCity.SelectedItem.Text };
                a.PROVINCE = new Province() { ID = Convert.ToInt32(AdListProvince.SelectedItem.Value) };
            }
           
            a.STREET = AdTxtStreet.Text;
            a.FLOOR = AdTxtFloor.Text;
            a.BUILDING_SUITE = AdTxtBuilding.Text;
            a.DISTRICT = AdTxtDistrict.Text;
            a.PO_BOX = AdPoBox.Text;           
            a.POSTAL_CODE = AdPostalCode.Text;                      
            a.TEL_OFFICE = AdOffice.Text;
            a.TEL_RESIDENCE = AdResidence.Text;
            a.MOBILE_NO = AdMobileNo.Text;
            a.FAX_NO = AdFaxNo.Text;

            /// Sms Change Req
            int SmsReq = 0;
            if (RadioButtonSms1.Checked)
                SmsReq = 1;
            a.SMS_ALERT_REQUIRED = SmsReq;

            if (AdEmail.Text != "")
            {
                a.EMAIL = true;

                Emails email = new Emails();
                email.BI_ID = Convert.ToInt32(Session["BID"]);
                email.EMAIL = AdEmail.Text;
                if (AdEstatementCheckbox.Checked == true)
                {
                    email.REQUIRED_ESTATEMEN = true;
                }
                else
                {
                    email.REQUIRED_ESTATEMEN = false;
                }

                email.SaveEmail();
            }
            else
            {
                a.EMAIL = false;
            }

            a.SaveAccountAddress();

            AccOpen ac = new AccOpen(Convert.ToInt32(Session["BID"]), AccountOpenTypes.INDIVIDUAL);

            if (ac.CheckIfCompleted())
            {
                User LoggedUser = Session["User"] as User;
                ac.ChangeStatus(Status.SUBMITTED,LoggedUser);
                Response.Redirect("AccountList.aspx");
            }

            String mesg = "Address Informaion has been saved";
            CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);

            AdSubmitButton.Visible = false;
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            User LoggedUser = Session["User"] as User;

            CIF cf = new CIF(LoggedUser.USER_ID);
            searchCIF.DataSource = cf.GetCifsForAccounts(Status.APPROVED_BY_BRANCH_MANAGER);
            searchCIF.DataBind();

        }

        protected void searchCIF_DataBound(object sender, EventArgs e)
        {

        }

        protected void SearchCIDModal_TextChanged(object sender, EventArgs e)
        {
            User LoggedUser = Session["User"] as User;

            CIF cf = new CIF(LoggedUser.USER_ID);
            searchCIF.DataSource = cf.GetCifsForAccounts2(SearchCIDModal.Text, Status.APPROVED_BY_BRANCH_MANAGER);
            searchCIF.DataBind();
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

            AccOpen ac = new AccOpen(Convert.ToInt32(Session["BID"]), AccountOpenTypes.INDIVIDUAL);

            if (ac.CheckIfCompleted())
            {
                User LoggedUser = Session["User"] as User;
                ac.ChangeStatus(Status.SUBMITTED,LoggedUser);
                Response.Redirect("AccountList.aspx");
            }

            String mesg = "Operating Instructions has been saved";
            CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);
            AuSubmitButton.Visible = false;


        }

        protected void NkSubmitButton_Click(object sender, EventArgs e)
        {

            AccountNexofKinInfo a = new AccountNexofKinInfo();
            a.BI_ID = Convert.ToInt32(Session["BID"]);
            a.NEXT_OF_KIN = NkCifNo.Text;
            a.NEXT_OF_KIN_NAME = NkName.Text;
            a.NEXT_OF_KIN_CNIC = NkCNIC.Text;
            a.RELATIONSHIP = new Relationship() { ID = Convert.ToInt32(NkListRelationship.SelectedItem.Value), NAME = NkListRelationship.SelectedItem.Text };
            a.RELATIONSHIP_DETAIL = NkRelationDetailOther.Text;

            a.COUNTRY = new Country() { ID = Convert.ToInt32(NkListCountry.SelectedItem.Value) };

            if (NkListCountry.SelectedItem.Text.Trim() != "PAKISTAN")
                a.CITY = new City() { ID = null };
            else
                a.CITY = new City() { ID = Convert.ToInt32(NkListCity.SelectedItem.Value) };
            a.BUILDING = NkBuilding.Text;
            a.FLOOR = NkTxtfloor.Text;
            a.STREET = NkTxtStreet.Text;
            a.DISTRICT = NkTxtDistrict.Text;
            a.POST_OFFICE = NkTxtPostOffice.Text;
            a.POSTAL_CODE = NkTxtPostalCode.Text;
            a.RESIDENCE_CONTACT = NktxtResidenceContact.Text;
            a.OFFICE_NO = NkTxtOfficeNo.Text;
            a.MOB_NO = NkTxtMobileNo.Text;

            a.SaveAccountNextofKinInfo();
            AccOpen ac = new AccOpen(Convert.ToInt32(Session["BID"]), AccountOpenTypes.INDIVIDUAL);

            if (ac.CheckIfCompleted())
            {
                User LoggedUser = Session["User"] as User;
                ac.ChangeStatus(Status.SUBMITTED,LoggedUser);
                Response.Redirect("AccountList.aspx");
            }

            String mesg = "Next of Kin Info has been saved";
            CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);
            NkSubmitButton.Visible = false;

        }

        protected void KnSubmitButton_Click(object sender, EventArgs e)
        {
            AccountKnowYourCustomer a = new AccountKnowYourCustomer();
            a.BI_ID = Convert.ToInt32(Session["BID"]);
            a.CUSTOMER_TYPE = new CustomerType() { ID = Convert.ToInt32(KnListCustomerType.SelectedItem.Value), NAME = KnListCustomerType.SelectedItem.Text };
            a.RAC = new Reason_Account_Opening() { ID = Convert.ToInt32(KnListRAC.SelectedItem.Value), Name = KnListRAC.SelectedItem.Text };
            a.RAC_DETAIL = knTextRACDetail.Text;
            a.DESCRIPTION_IF_REFFERED = KnDescrIfRefered.Text;
            a.EDUCATION = new Education() { ID = Convert.ToInt32(KnListEducation.SelectedItem.Value), Name = KnListEducation.SelectedItem.Text };
           // a.PURPOSE_OF_ACCOUNT = new PurposeOfAccount() { ID = Convert.ToInt32(KnListPurposeOfAccount.SelectedItem.Value), NAME = KnListPurposeOfAccount.SelectedItem.Text };
            a.PURPOSE_OF_ACCOUNT = KnListPurposeOfAccount.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => Convert.ToInt32(i.Value)).ToList();
            a.DESCRIPTION_IF_OTHER = KnDescrOther.Text;
           // a.SOURCE_OF_FUNDS = new SourceOfFunds() { ID = Convert.ToInt32(KnListSourceOfFunds.SelectedItem.Value), NAME = KnListSourceOfFunds.SelectedItem.Text };
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
            a.RELATIONSHIP_MANAGER = new Know_Your_Customer_Relationship { ID = Convert.ToInt32(KnddlManager.SelectedItem.Value) };
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

            a.DETAIL_IF_NOT_SATISFACTORY = KnDetailNotSatis.Text;
            a.COUNTRY_HOME_REMITTANCE = new Country() { ID = Convert.ToInt32(KnListCountHomeRemit.SelectedItem.Value), Name = KnListCountHomeRemit.SelectedItem.Text };
            a.REAL_BENEFICIARY_ACCOUNT = new RealBeneficiaryAccount() { ID = Convert.ToInt32(KnListRealBenef.SelectedItem.Value), NAME = KnListRealBenef.SelectedItem.Text };
            a.NAME_OTHER = KnNameOther.Text;
            a.CNIC_OTHER = KnCnicOther.Text;
            a.RELATIONSHIP_WITH_ACCOUNTHOLDER = new Relationship() { ID = Convert.ToInt32(KnListRelationAccountHolder.SelectedItem.Value), NAME = KnListRelationAccountHolder.SelectedItem.Text };
            a.RELATIONSHIP_DETAIL_OTHER = KnRelationDetailOther.Text;
            //////////////
            a.BENEFICIAL_ADDRESS = KntxtAddress.Text;
            a.BENEFICIAL_NATIONALITY = Convert.ToInt32(knNationality.SelectedItem.Value);
            a.BENEFICIAL_RESIDENCE = Convert.ToInt32(knListResidence.SelectedItem.Value);
            a.BENEFICIAL_RESIDENCE_DESC = KnTxtResOther.Text;
            a.BENEFICIAL_IDENTITY = Convert.ToInt32(knListDocType.SelectedItem.Value);
            a.BENEFICIAL_IDENTITY_EXPIRY = KntxtExpiry.Text;
            a.BENEFICIAL_SOURCE_OF_FUND = Convert.ToInt32(KnListSourceOfFund.SelectedItem.Value);
            a.NODT = KnNODT.Text;
            a.PEDT = KnPEDT.Text;
            a.NOCT = KnNOCT.Text;
            a.PECT = KnPECT.Text;
            a.ETCP_OTHER = KntxtDescETCP.Text;
            a.GICP_OTHER = KntxtDescGCP.Text;

            a.KYC_EXPECTED_COUNTER_PARTIES = KnListECP.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => Convert.ToInt32(i.Value)).ToList();
            a.KYC_GEOGRAPHIES_COUNTER_PARTIES = KnListGCP.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => Convert.ToInt32(i.Value)).ToList();
            a.SaveKnowYourCustomer();
            AccOpen ac = new AccOpen(Convert.ToInt32(Session["BID"]), AccountOpenTypes.INDIVIDUAL);

            if (ac.CheckIfCompleted())
            {
                User LoggedUser = Session["User"] as User;
                ac.ChangeStatus(Status.SUBMITTED,LoggedUser);
                Response.Redirect("AccountList.aspx");
            }

            String mesg = "Know Your Customer has been saved";
            CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);
            KnSubmitButton.Visible = false;



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
            a.CHEQUE_NUMBER = CdChequeNumber.Text;
            a.CERTIFICATE_NUMBER = CdCertNumber.Text;
            a.CERTIFCATE_AMOUNT = CdCertAmount.Text;
            a.MARK_UP_RATE = CdMarkupRate.Text;
            a.PRINCIPAL_RENEWAL_OPTION = new PrinciparRenewalOption() { ID = Convert.ToInt32(CdLstPrincipalRenewal.SelectedItem.Value), Name = CdLstPrincipalRenewal.SelectedItem.Text };
            a.Sava();
            AccOpen ac = new AccOpen(Convert.ToInt32(Session["BID"]), AccountOpenTypes.INDIVIDUAL);

            if (ac.CheckIfCompleted())
            {
                User LoggedUser = Session["User"] as User;
                ac.ChangeStatus(Status.SUBMITTED,LoggedUser);
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

        protected void Search_Click(object sender, EventArgs e)
        {
            User LoggedUser = Session["User"] as User;

            CIF cf = new CIF(LoggedUser.USER_ID);
            searchCIF.DataSource = cf.GetCifsForAccounts2(SearchCIDModal.Text, Status.APPROVED_BY_BRANCH_MANAGER);
            searchCIF.DataBind();
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

            AccOpen ac = new AccOpen(Convert.ToInt32(Session["BID"]), AccountOpenTypes.INDIVIDUAL);

            if (ac.CheckIfCompleted())
            {
                User LoggedUser = Session["User"] as User;
                ac.ChangeStatus(Status.SUBMITTED,LoggedUser);
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
        public int num = 0;
        protected void DcRadio1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton r = sender as RadioButton;

            if (r.Checked == true)
            {
                num = 1;

                DocumentsList d1 = new DocumentsList();
                d1.BI_ID = Convert.ToInt32(Session["BID"]);
                int n = 0;
                int.TryParse(((Label)r.FindControl("lblDcId")).Text, out n);
                d1.Documents = n;
                d1.value = ((RadioButton)r.FindControl("DcRadio1")).Text;
                d1.Save();

            }
            //GridViewRow g = (GridViewRow)b.NamingContainer;

        }

        protected void DcRadio2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton r = sender as RadioButton;

            if (r.Checked == true)
            {
                num = 1;
                DocumentsList d1 = new DocumentsList();
                d1.BI_ID = Convert.ToInt32(Session["BID"]);
                int n = 0;
                int.TryParse(((Label)r.FindControl("lblDcId")).Text, out n);
                d1.Documents = n;
                d1.value = ((RadioButton)r.FindControl("DcRadio2")).Text;
                d1.Save();
            }
        }

        protected void DcRadio3_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton r = sender as RadioButton;

            if (r.Checked == true)
            {
                num = 1;
                DocumentsList d1 = new DocumentsList();
                d1.BI_ID = Convert.ToInt32(Session["BID"]);
                int n = 0;
                int.TryParse(((Label)r.FindControl("lblDcId")).Text, out n);
                d1.Documents = n;
                d1.value = ((RadioButton)r.FindControl("DcRadio3")).Text;
                d1.Save();
            }
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

            a.ACCOUNT_ENTRY_DATE = Convert.ToDateTime(AcEntryDate.Text);
          //  a.GL_CODE = new Gl_code() { ID = Convert.ToInt32(AcListGlCode.SelectedItem.Value), NAME = AcListGlCode.SelectedItem.Text };
           // a.SL_CODE = new Sl_code() { ID = Convert.ToInt32(AcListSlCode.SelectedItem.Value), NAME = AcListSlCode.SelectedItem.Text };
            a.ACCOUNT_TYPE = new AccountType() { ID = Convert.ToInt32(AcListAccountType.SelectedItem.Value), Name = AcListAccountType.SelectedItem.Text };
            a.CURRENCY = new Currency() { ID = Convert.ToInt32(AcListCurrency.SelectedItem.Value), NAME = AcListCurrency.SelectedItem.Text };
            a.ACCOUNT_NUMBER = AcAccountNumber.Text;
            a.ACCOUNT_TITLE = AcAccountTitle.Text;
            a.INITIAL_DEPOSIT = AcInitialDeposit.Text;
            a.ACCOUNT_OPEN_TYPE = new AccountOpenType() { ID = Convert.ToInt32(AcListAccountOpen.SelectedItem.Value), NAME = AcListAccountOpen.SelectedItem.Text };

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

            a.UpdateIndividual();

            String mesg = "Account Nature and currency has been updated";
            int id = Convert.ToInt32(Session["BID"]);

            CheckAccountIndividualTab(id, mesg);
            AcSubmitButton.Visible = false;


        }

        protected void btnSubmitACa_Click(object sender, EventArgs e)
        {

            int BID = (int)Session["BID"];
            AccOpen ac = new AccOpen(BID, AccountOpenTypes.INDIVIDUAL);
            //CIF cif = new CIF(BID, CifType.INDIVIDUAL);
            User LoggedUser = Session["User"] as User;
            ac.ChangeStatus(Status.SUBMITTED,LoggedUser);
            //cif.ChangeStatus(Status.SUBMITTED);
            Response.Redirect("AccountList.aspx");

        }

        protected void BtnUpdateAp_Click(object sender, EventArgs e)
        {

            AccountApplicantInformation a = new AccountApplicantInformation();
            a.BI_ID = Convert.ToInt32(Session["BID"]);
            a.CUSTOMER_CIF_NO = ApCustomerCif.Text;
            a.CUSTOMER_NAME = ApCustomerName.Text;
            a.CUSTOMER_CNIC = ApCustomerCNIC.Text;

            if (ApIsPrimaryRadio1.Checked == true)
            {
                a.IS_PRIMARY_ACCOUNT_HOLDER = 1;
            }
            else
            {
                a.IS_PRIMARY_ACCOUNT_HOLDER = 0;
            }


            if (ApApplicantNegativeRadio1.Checked == true)
            {
                a.ACCOUNT_IN_NEGATIVE_LIST = 1;
            }
            else
            {
                a.ACCOUNT_IN_NEGATIVE_LIST = 0;
            }

            if (ApPowerAttornyRadio1.Checked == true)
            {
                a.POWER_OF_ATTORNY = 1;
            }
            else
            {
                a.POWER_OF_ATTORNY = 0;
            }

            if (ApSignatureRadio1.Checked == true)
            {
                a.SIGNATURE_AUTHORITY = 1;
            }
            else
            {
                a.SIGNATURE_AUTHORITY = 0;
            }

            a.APPLICANT_STATUS = new ApplicantStatuses() { ID = Convert.ToInt32(ApListApplicantStatus.SelectedItem.Value), Name = ApListApplicantStatus.SelectedItem.Text };
            a.RELATIONSHIP_NOT_PRIMARY = new Relationship() { ID = Convert.ToInt32(ApListRelationPrimary.SelectedItem.Value), NAME = ApListRelationPrimary.SelectedItem.Text };
            a.RELATIONSHIP_DETAIL = ApRelationshipDetail.Text;
            a.INVESTMENT_SHARE = ApInvestmentShare.Text;

            a.Update();
            String msg = "Applicant Information has been updated";
            int id = Convert.ToInt32(Session["BID"]);
            CheckAccountIndividualTab(id, msg);
            ApSubmitButton.Visible = false;



        }

        protected void BtnUpdateAd_Click(object sender, EventArgs e)
        {
            AccountAddressInformation a = new AccountAddressInformation();
            a.BI_ID = Convert.ToInt32(Session["BID"]);
            a.COUNTRY = new Country() { ID = Convert.ToInt32(AdListCountry.SelectedItem.Value), Name = AdListCountry.SelectedItem.Text };
            if (AdListCountry.SelectedItem.Text.Trim() != "PAKISTAN")
            {
                a.CITY = new City() { ID = null };
                a.PROVINCE = new Province() { ID = null };
            }
            else
            {
                a.CITY = new City() { ID = Convert.ToInt32(AdListCity.SelectedItem.Value), Name = AdListCity.SelectedItem.Text };
                a.PROVINCE = new Province() { ID = Convert.ToInt32(AdListProvince.SelectedItem.Value) };
            }
           
            a.STREET = AdTxtStreet.Text;
            a.FLOOR = AdTxtFloor.Text;
            a.BUILDING_SUITE = AdTxtBuilding.Text;
            a.DISTRICT = AdTxtDistrict.Text;
            a.PO_BOX = AdPoBox.Text;
          
            a.POSTAL_CODE = AdPostalCode.Text;
            
          
            a.TEL_OFFICE = AdOffice.Text;
            a.TEL_RESIDENCE = AdResidence.Text;
            a.MOBILE_NO = AdMobileNo.Text;
            a.FAX_NO = AdFaxNo.Text;
            /// Sms Change Req
            int SmsReq = 0;
            if (RadioButtonSms1.Checked)
                SmsReq = 1;
            a.SMS_ALERT_REQUIRED = SmsReq;

            //a.SMS_ALERT_REQUIRED = new Sms_Alert_Required() { ID = Convert.ToInt32(AdListSmsRequired.SelectedItem.Value), NAME = AdListSmsRequired.SelectedItem.Text };

            if (AdEmail.Text != "")
            {
                a.EMAIL = true;

                Emails email = new Emails();
                email.BI_ID = Convert.ToInt32(Session["BID"]);
                email.EMAIL = AdEmail.Text;
                if (AdEstatementCheckbox.Checked == true)
                {
                    email.REQUIRED_ESTATEMEN = true;
                }
                else
                {
                    email.REQUIRED_ESTATEMEN = false;
                }

                email.UpdateEmail();
            }
            else
            {
                a.EMAIL = false;
            }

            a.Update();


            String mesg = "Address Informaion has been updated";
            CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);

            AdSubmitButton.Visible = false;


        }

        protected void BtnUpdateAu_Click(object sender, EventArgs e)
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

        protected void BtnUpdateNK_Click(object sender, EventArgs e)
        {


            AccountNexofKinInfo a = new AccountNexofKinInfo();
            a.BI_ID = Convert.ToInt32(Session["BID"]);
            a.NEXT_OF_KIN = NkCifNo.Text;
            a.NEXT_OF_KIN_NAME = NkName.Text;
            a.NEXT_OF_KIN_CNIC = NkCNIC.Text;
            a.RELATIONSHIP = new Relationship() { ID = Convert.ToInt32(NkListRelationship.SelectedItem.Value), NAME = NkListRelationship.SelectedItem.Text };
            a.RELATIONSHIP_DETAIL = NkRelationDetailOther.Text;
            a.COUNTRY = new Country() { ID = Convert.ToInt32(NkListCountry.SelectedItem.Value) };
            if (NkListCountry.SelectedItem.Text.Trim() != "PAKISTAN")
                a.CITY = new City() { ID = null };
            else
                a.CITY = new City() { ID = Convert.ToInt32(NkListCity.SelectedItem.Value) };
            a.BUILDING = NkBuilding.Text;
            a.FLOOR = NkTxtfloor.Text;
            a.STREET = NkTxtStreet.Text;
            a.DISTRICT = NkTxtDistrict.Text;
            a.POST_OFFICE = NkTxtPostOffice.Text;
            a.POSTAL_CODE = NkTxtPostalCode.Text;
            a.RESIDENCE_CONTACT = NktxtResidenceContact.Text;
            a.OFFICE_NO = NkTxtOfficeNo.Text;
            a.MOB_NO = NkTxtMobileNo.Text;

          //  a.SaveAccountNextofKinInfo();
            a.Update();
            String mesg = "Next of Kin Info has been updated";
            CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);
            NkSubmitButton.Visible = false;


        }

        protected void BtnUpdateKn_Click(object sender, EventArgs e)
        {

            AccountKnowYourCustomer a = new AccountKnowYourCustomer();
            a.BI_ID = Convert.ToInt32(Session["BID"]);
            a.CUSTOMER_TYPE = new CustomerType() { ID = Convert.ToInt32(KnListCustomerType.SelectedItem.Value), NAME = KnListCustomerType.SelectedItem.Text };
            a.RAC = new Reason_Account_Opening() { ID = Convert.ToInt32(KnListRAC.SelectedItem.Value), Name = KnListRAC.SelectedItem.Text };
            a.RAC_DETAIL = knTextRACDetail.Text;
            a.DESCRIPTION_IF_REFFERED = KnDescrIfRefered.Text;
            a.EDUCATION = new Education() { ID = Convert.ToInt32(KnListEducation.SelectedItem.Value), Name = KnListEducation.SelectedItem.Text };
            a.PURPOSE_OF_ACCOUNT = KnListPurposeOfAccount.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => Convert.ToInt32(i.Value)).ToList();
            // a.PURPOSE_OF_ACCOUNT = new PurposeOfAccount() { ID = Convert.ToInt32(KnListPurposeOfAccount.SelectedItem.Value), NAME = KnListPurposeOfAccount.SelectedItem.Text };
            a.DESCRIPTION_IF_OTHER = KnDescrOther.Text;
            a.SOURCE_OF_FUNDS = KnListSourceOfFunds.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => Convert.ToInt32(i.Value)).ToList();
           // a.SOURCE_OF_FUNDS = new SourceOfFunds() { ID = Convert.ToInt32(KnListSourceOfFunds.SelectedItem.Value), NAME = KnListSourceOfFunds.SelectedItem.Text };
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
            a.RELATIONSHIP_MANAGER = new Know_Your_Customer_Relationship { ID = Convert.ToInt32(KnddlManager.SelectedItem.Value) };
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

            a.DETAIL_IF_NOT_SATISFACTORY = KnDetailNotSatis.Text;
            a.COUNTRY_HOME_REMITTANCE = new Country() { ID = Convert.ToInt32(KnListCountHomeRemit.SelectedItem.Value), Name = KnListCountHomeRemit.SelectedItem.Text };
            a.REAL_BENEFICIARY_ACCOUNT = new RealBeneficiaryAccount() { ID = Convert.ToInt32(KnListRealBenef.SelectedItem.Value), NAME = KnListRealBenef.SelectedItem.Text };
            a.NAME_OTHER = KnNameOther.Text;
            a.CNIC_OTHER = KnCnicOther.Text;
            a.RELATIONSHIP_WITH_ACCOUNTHOLDER = new Relationship() { ID = Convert.ToInt32(KnListRelationAccountHolder.SelectedItem.Value), NAME = KnListRelationAccountHolder.SelectedItem.Text };
            a.RELATIONSHIP_DETAIL_OTHER = KnRelationDetailOther.Text;
            //////////////
            a.BENEFICIAL_ADDRESS = KntxtAddress.Text;
            a.BENEFICIAL_NATIONALITY = Convert.ToInt32(knNationality.SelectedItem.Value);
            a.BENEFICIAL_RESIDENCE = Convert.ToInt32(knListResidence.SelectedItem.Value);
            a.BENEFICIAL_RESIDENCE_DESC = KnTxtResOther.Text;
            a.BENEFICIAL_IDENTITY = Convert.ToInt32(knListDocType.SelectedItem.Value);
            a.BENEFICIAL_IDENTITY_EXPIRY = KntxtExpiry.Text;
            a.BENEFICIAL_SOURCE_OF_FUND = Convert.ToInt32(KnListSourceOfFund.SelectedItem.Value);

            a.NODT = KnNODT.Text;
            a.PEDT = KnPEDT.Text;
            a.NOCT = KnNOCT.Text;
            a.PECT = KnPECT.Text;
            a.ETCP_OTHER = KntxtDescETCP.Text;
            a.GICP_OTHER = KntxtDescGCP.Text;

            a.KYC_EXPECTED_COUNTER_PARTIES = KnListECP.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => Convert.ToInt32(i.Value)).ToList();
            a.KYC_GEOGRAPHIES_COUNTER_PARTIES = KnListGCP.Items.Cast<ListItem>().Where(i => i.Selected == true).Select(i => Convert.ToInt32(i.Value)).ToList();


            a.Update();
            String mesg = "Know Your Customer has been updated";
            CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);
            KnSubmitButton.Visible = false;



        }

        protected void BtnUpdateCd_Click(object sender, EventArgs e)
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
            a.CHEQUE_NUMBER = CdChequeNumber.Text;
            a.CERTIFICATE_NUMBER = CdCertNumber.Text;
            a.CERTIFCATE_AMOUNT = CdCertAmount.Text;
            a.MARK_UP_RATE = CdMarkupRate.Text;
            a.PRINCIPAL_RENEWAL_OPTION = new PrinciparRenewalOption() { ID = Convert.ToInt32(CdLstPrincipalRenewal.SelectedItem.Value), Name = CdLstPrincipalRenewal.SelectedItem.Text };
            a.Update();
            String mesg = "Certificate Deposit Info has been updated";
            CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);
            CdSubmitButton.Visible = false;

        }

        protected void BtnUpdateDr_Click(object sender, EventArgs e)
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
            //if (num > 0)
            //{
                d.DESCRIPTION = true;

            //}
            //else
            //{
            //    d.DESCRIPTION = false;
            //}

            d.Update();

            String mesg = "Description Documents has been updated";
            CheckAccountIndividualTab(Convert.ToInt32(Session["BID"]), mesg);

            DrSubmitButton.Visible = false;

        }

        protected void btnSearchCifAdress_Click(object sender, EventArgs e)
        {
            if (AcAccountModeRadio2.Checked)
            {
                AccountApplicantInformation a = new AccountApplicantInformation();
                if (GridViewAccountCifs.Rows.Count > 0)
                {                    
                   
                    
                        ContactInfo cinfo = new ContactInfo();
                        GridViewRow PRow =  GridViewAccountCifs.Rows.Cast<GridViewRow>().FirstOrDefault(c => ((Label)c.FindControl("IS_PRIMARY_ACCOUNT_HOLDER")).Text == "1");
                        string cif =  ((Label) PRow.FindControl("CUSTOMER_CIF_NO")).Text;
                        cinfo.GetIndividualContactInfo(Convert.ToInt32(cif));
                        AdListCountry.ClearSelection();
                        AdListCity.ClearSelection();
                        AdListProvince.ClearSelection();
                        ListExtensions.SetDropdownValue(cinfo.COUNTRY_CODE.ID, AdListCountry);
                        ListExtensions.SetDropdownValue(cinfo.CITY_PERMANENT.ID, AdListCity);
                        ListExtensions.SetDropdownValue(cinfo.PROVINCE.ID, AdListProvince);
                      //  AdListCountry.Items.FindByValue("1").Selected = true;
                     //   AdListCity.Items.FindByValue("1").Selected = true;
                        AdTxtBuilding.Text = cinfo.BIULDING_SUITE;
                        AdTxtFloor.Text = cinfo.FLOOR;
                        AdTxtStreet.Text = cinfo.STREET;
                        AdTxtStreet.Text = cinfo.STREET;
                        AdPoBox.Text = cinfo.POST_OFFICE;
                        AdPostalCode.Text = cinfo.POSTAL_CODE;

                        AdOffice.Text = cinfo.OFFICE_CONTACT;
                        AdResidence.Text = cinfo.RESIDENCE_CONTACT;
                        AdMobileNo.Text = cinfo.MOBILE_NO;
                        AdFaxNo.Text = cinfo.FAX_NO;
                        AdEmail.Text = cinfo.EMAIL;
                    
                }
               
               
            }
            else
            {
                if (ApCustomerCif.Text.Length > 0)
                {
                    ///// Setting Cif Contact info in search
                    ContactInfo cinfo = new ContactInfo();
                    cinfo.GetIndividualContactInfo(Convert.ToInt32(ApCustomerCif.Text));
                    AdListCountry.ClearSelection();
                    AdListCity.ClearSelection();
                    AdListProvince.ClearSelection();
                    ListExtensions.SetDropdownValue(cinfo.COUNTRY_CODE.ID, AdListCountry);
                    ListExtensions.SetDropdownValue(cinfo.CITY_PERMANENT.ID, AdListCity);
                    ListExtensions.SetDropdownValue(cinfo.PROVINCE.ID, AdListProvince);
                    AdTxtBuilding.Text = cinfo.BIULDING_SUITE;
                    AdTxtFloor.Text = cinfo.FLOOR;
                    AdTxtStreet.Text = cinfo.STREET;
                    AdTxtStreet.Text = cinfo.STREET;
                    AdPoBox.Text = cinfo.POST_OFFICE;
                    AdPostalCode.Text = cinfo.POSTAL_CODE;
                    
                    AdOffice.Text = cinfo.OFFICE_CONTACT;
                    AdResidence.Text = cinfo.RESIDENCE_CONTACT;
                    AdMobileNo.Text = cinfo.MOBILE_NO;
                    AdFaxNo.Text = cinfo.FAX_NO;
                    AdEmail.Text = cinfo.EMAIL;
                }
            }


           

         
        }

        protected void btnSearchCifKin_Click(object sender, EventArgs e)
        {
            if (NkCifNo.Text.Length > 0)
            {
                CIF cif = new CIF(-1);

                String[] CifKin = cif.SearchCifKin(Convert.ToInt32((NkCifNo.Text)));
                NkName.Text = CifKin[0];
                NkCNIC.Text = CifKin[1];
            }
        }

        protected void AcAccountModeRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (AcAccountModeRadio1.Checked)
            {
                ApIsPrimaryRadio1.Checked = true;
                ApIsPrimaryRadio2.Checked = false;
                GridViewAccountCifs.Visible = false;
                GridViewAccountCifs.Enabled = false;
                btnGridAddCif.Visible = false;
            }
            else
            {
                ApIsPrimaryRadio1.Checked = false;
                ApIsPrimaryRadio2.Checked = true;

                GridViewAccountCifs.DataSource = new List<String>();
                GridViewAccountCifs.DataBind();

                GridViewAccountCifs.Visible = true;
                GridViewAccountCifs.Enabled = true;
                btnGridAddCif.Visible = true;

                
            }
        }

        protected void GridViewAccountCifs_DataBound(object sender, EventArgs e)
        {

        }

        protected void GridViewAccountCifs_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnGridAddCif_Click(object sender, EventArgs e)
        {
            if (Session["GridCif"] == null)
                Session["GridCif"] = new List<ApplicantInformationCifs>();

            string CIFID = ApCustomerCif.Text;
            bool IsPrimary = ApIsPrimaryRadio1.Checked;
            bool IsSignatry = ApSignatureRadio1.Checked;
            bool IsPower = ApPowerAttornyRadio1.Checked;
            string Investment = ApInvestmentShare.Text;
            string App_Status = ApListApplicantStatus.SelectedItem.Text;

            List<ApplicantInformationCifs> CifsData = Session["GridCif"] as List<ApplicantInformationCifs>;

            if (!CifsData.Where(c => c.CUSTOMER_CIF_NO == CIFID).Any())
            {
                ApplicantInformationCifs newCif = new ApplicantInformationCifs()
                {
                    CUSTOMER_CIF_NO = CIFID,
                    IS_PRIMARY_ACCOUNT_HOLDER = Convert.ToInt32(IsPrimary),
                    POWER_OF_ATTORNY = Convert.ToInt32(IsPower),
                    SIGNATURE_AUTHORITY = Convert.ToInt32(IsSignatry),
                    INVESTMENT_SHARE = Investment,
                    APPLICANT_STATUS = App_Status,
                    NEG_LIST = Convert.ToInt32(ApApplicantNegativeRadio1.Checked),
                    CUSTOMER_NAME = ApCustomerName.Text,
                    CUSTOMER_IDENTITY = ApCustomerCNIC.Text
                    
                };

                CifsData.Add(newCif);
            }
           
           
            GridViewAccountCifs.DataSource = CifsData;
            GridViewAccountCifs.DataBind();

            Session["GridCif"] = CifsData;

        }

        protected void delete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            Label CUSTOMER_CIF_NO = (Label) gvr.FindControl("CUSTOMER_CIF_NO");
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

        protected void CustomValidatorPrimaryAccount_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Session["GridCif"] == null)
                Session["GridCif"] = new List<ApplicantInformationCifs>();

            List<ApplicantInformationCifs> CifsData = Session["GridCif"] as List<ApplicantInformationCifs>;
            args.IsValid = CifsData.Where
                            (
                                c => c.IS_PRIMARY_ACCOUNT_HOLDER == 1
                            ).Any();
        }

        protected void CustomValidatorJoinCustomers_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = GridViewAccountCifs.Rows.Count >= 2;
        }

        protected void CustomValidatorPrimaryAccountGreater_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Session["GridCif"] == null)
                Session["GridCif"] = new List<ApplicantInformationCifs>();

            List<ApplicantInformationCifs> CifsData = Session["GridCif"] as List<ApplicantInformationCifs>;
            args.IsValid = CifsData.Where
                            (
                                c => c.IS_PRIMARY_ACCOUNT_HOLDER == 1
                            ).Count()  < 2;
        }

        protected void CustomValidatorAnyContact_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (AdOffice.Text.Length > 0 || AdResidence.Text.Length > 0 || AdMobileNo.Text.Length > 0)
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void RadioButtonSms1_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonSms1.Checked)
                RequiredFieldValidatorMobile.Enabled = true;
            else
                RequiredFieldValidatorMobile.Enabled = false;
        }

        protected void AdEstatementCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (AdEstatementCheckbox.Checked)
                RequiredFieldValidatorEmail.Enabled = true;
            else
                RequiredFieldValidatorEmail.Enabled = false;

        }

        protected void ZakatDeductionRadio1_CheckedChanged(object sender, EventArgs e)
        {
            if (ZakatDeductionRadio1.Checked)
                AuListZakatExemption.Enabled = true;
            else
                AuListZakatExemption.Enabled = false;
        }

        protected void AtmRequiredRadio1_CheckedChanged(object sender, EventArgs e)
        {
            if (AtmRequiredRadio1.Checked)
                RequiredFieldValidatoratm.Enabled = true;
            else
                RequiredFieldValidatoratm.Enabled = false;
        }

        protected void MobileBankRequirRadio1_CheckedChanged(object sender, EventArgs e)
        {
            if (MobileBankRequirRadio1.Checked)
                RequiredFieldValidatorMobileBanking.Enabled = true;
            else
                RequiredFieldValidatorMobileBanking.Enabled = false;
        }

        protected void IsFedRadio1_CheckedChanged(object sender, EventArgs e)
        {
            if (IsFedRadio1.Checked)
                RequiredFieldValidatorBirth.Enabled = true;
            else
                RequiredFieldValidatorBirth.Enabled = false;
        }

        protected void WhtProfitRadio1_CheckedChanged(object sender, EventArgs e)
        {
            if (WhtProfitRadio2.Checked)
                RequiredFieldValidatorWhtProfitExpiry.Enabled = true;
            else
                RequiredFieldValidatorWhtProfitExpiry.Enabled = false;
        }

        protected void WhtTransactionRadio1_CheckedChanged(object sender, EventArgs e)
        {
            if (WhtTransactionRadio2.Checked)
                RequiredFieldValidatorWhtTrans.Enabled = true;
            else
                RequiredFieldValidatorWhtTrans.Enabled = false;
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

                AcListAccountType.Items.Remove(AcListAccountType.Items.FindByText("4599 -- Branch Office Account"));
            }
        }

        protected void AcListAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AcListAccountType.SelectedItem.Text != "Select")
            {
                AccountModes am = new AccountModes();


                AcListAccountMode.DataSource = am.GetAccountModes(Convert.ToInt32(AcListAccountType.SelectedItem.Value));
                AcListAccountMode.DataValueField = "ID";
                AcListAccountMode.DataTextField = "NAME";
                AcListAccountMode.DataBind();
                AcListAccountMode.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        // Next of kin validations
        protected void CustomValidatorNkAnyContact_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (NktxtResidenceContact.Text.Length > 0 || NkTxtMobileNo.Text.Length > 0 || NkTxtOfficeNo.Text.Length > 0)
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void btnBioMetricVerify_Click(object sender, EventArgs e)
        {
            if (ApCustomerCNIC.Text.Length < 1)
                cnicbiolbl.Visible = true;
            else
            {
                BasicInformations BI = new BasicInformations();
                int BID = Convert.ToInt32(ApCustomerCif.Text);
                int AccountId = Convert.ToInt32(Session["BID"]);
                BI.GetIndividual(BID);

                if (BI.PRIMARY_DOCUMENT_TYPE.ID != 1)
                {
                    cnicbiolbl.Visible = true;
                }
                else
                {
                    ContactInfo c = new ContactInfo();
                    c.GetIndividualContactInfo(BID);
                    clsSkillOrbitObject obj = new clsSkillOrbitObject();
                    User LoggedUser = Session["User"] as User;
                    obj.CNIC = ApCustomerCNIC.Text.Replace("-", string.Empty);
                    obj.TOTAccount = "Saving";
                    if (c.MOBILE_NO.Length > 0)
                        obj.ContactNumber = c.MOBILE_NO;
                    else if (c.RESIDENCE_CONTACT.Length > 0)
                        obj.ContactNumber = c.RESIDENCE_CONTACT;
                    else
                        obj.ContactNumber = c.OFFICE_CONTACT;
                    obj.UserId = LoggedUser.USER_ID.ToString();
                    obj.BranchCode = LoggedUser.Branch.BRANCH_CODE;
                    obj.NameOfArea = LoggedUser.Branch.AREA;
                    obj.AccountId = AccountId;
                    obj.CIF = Convert.ToInt32(ApCustomerCif.Text);

                    Session["clsSkillOrbitObject"] = obj;
                    Response.Redirect("BioMetric.aspx");
                }

            }
            
 

        }

        public void SetBioMetric()
        {
            string CNIC = Request.QueryString["CNIC"];
            int CIF = Convert.ToInt32(Request.QueryString["CIF"]);
            if (CNIC != null)
            {
                NadraInfo nadra = new NadraInfo();
                nadra = nadra.GetNadraInfo(CNIC);
                string success = "0";
                string Msg = "";

                if (nadra != null)
                {
                    if (nadra.STATUSCODE == ((int)ResponseCodes.successful).ToString())
                    {
                        success = "1";
                        Msg = "Nadra Information Successfully Integrated";
                    }
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

                    // ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "BioMetric('" + success + "','" + Msg + "');", true);
                    BioMetricPopUp = true;
                    mesgg = Msg;
                    successs = success;
                }
             

                BasicInformations BI = new BasicInformations();
                BI.GetIndividual(CIF);
                ApCustomerCif.Text = CIF.ToString();
                ApCustomerName.Text = BI.NAME;
                ApCustomerCNIC.Text = BI.CNIC;
                
              //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "BioMetricAccount('" + success + "','" + Msg + "');", true);

                

                
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
            bool Other = KnListPurposeOfAccount.Items.FindByText("OTHER").Selected == true;
            if (Other)
                ReqValidatorPurposeAccountOther.Enabled = true;
            else
                ReqValidatorPurposeAccountOther.Enabled = false;
        }

        protected void KnListSourceOfFunds_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool Other = KnListSourceOfFunds.Items.FindByText("OTHERS").Selected == true;
            if (Other)
                ReqValidatorSourceDesc.Enabled = true;
            else
                ReqValidatorSourceDesc.Enabled = false;
        }

        protected void KnListModeOfTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool Other = KnListModeOfTransaction.Items.FindByText("Other").Selected == true;
            if (Other)
                ReqValidatorMotOther.Enabled = true;
            else
                ReqValidatorMotOther.Enabled = false;
        }

       

        protected void knListResidence_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (knListResidence.SelectedItem.Value == "2")
                RequiredFieldValidatorDescResi.Enabled = true;
            else
                RequiredFieldValidatorDescResi.Enabled = false;
        }

        protected void KnListRealBenef_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KnListRealBenef.SelectedItem.Text == "OTHER")
            {
                RequiredFieldValidatorBName.Enabled = true;
                RequiredFieldValidatorRelaWithAcc.Enabled = true;
                RequiredFieldValidatorBAddress.Enabled = true;
                RequiredFieldValidatorNationality.Enabled = true;
                RequiredFieldValidatorKnResidence.Enabled = true;
                RequiredFieldValidatorDocType.Enabled = true;
                RequiredFieldValidatorIdNumber.Enabled = true;
                RequiredFieldValidatorExpiry.Enabled = true;
                RequiredFieldValidatorBSOF.Enabled = true;
                KnNameOther.Enabled = true;
                KnListRelationAccountHolder.Enabled = true;
                KntxtAddress.Enabled = true;
                knNationality.Enabled = true;
                knListResidence.Enabled = true;
                KnTxtResOther.Enabled = true;
                knListDocType.Enabled = true;
                KnCnicOther.Enabled = true;
                KntxtExpiry.Enabled = true;
                KnListSourceOfFund.Enabled = true;
            }
            else
            {
                
                RequiredFieldValidatorBName.Enabled = false;
                RequiredFieldValidatorRelaWithAcc.Enabled = false;
                RequiredFieldValidatorBAddress.Enabled = false;
                RequiredFieldValidatorNationality.Enabled = false;
                RequiredFieldValidatorKnResidence.Enabled = false;
                RequiredFieldValidatorDescResi.Enabled = false;
                RequiredFieldValidatorDocType.Enabled = false;
                RequiredFieldValidatorIdNumber.Enabled = false;
                RequiredFieldValidatorExpiry.Enabled = false;
                RequiredFieldValidatorBSOF.Enabled = false;
                KnNameOther.Enabled = false;
                KnListRelationAccountHolder.Enabled = false;
                KntxtAddress.Enabled = false;
                knNationality.Enabled = false;
                knListResidence.Enabled = false;
                KnTxtResOther.Enabled = false;
                knListDocType.Enabled = false;
                KnCnicOther.Enabled = false;
                KntxtExpiry.Enabled = false;
                KnListSourceOfFund.Enabled = false;


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

        protected void AdListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AdListCountry.SelectedItem.Text.Trim() != "PAKISTAN")
            {
                RequiredFieldValidatorAdProvince.Enabled = false;
                RequiredFieldValidatorCity.Enabled = false;
            }
            else
            {
                RequiredFieldValidatorAdProvince.Enabled = true;
                RequiredFieldValidatorCity.Enabled = true;
            }
        }

        protected void CustomValidatorAccountType_ServerValidate(object source, ServerValidateEventArgs args)
        {
          
            if (GridViewAccountCifs.Rows.Count > 0)
            {
                List<ApplicantInformationCifs> CifsData = Session["GridCif"] as List<ApplicantInformationCifs>;
                ApplicantInformationCifs SCifObj = new ApplicantInformationCifs();

                foreach (var SCif in CifsData)
                {
                    bool flag = SCifObj.CheckAccountType(Convert.ToInt32(SCif.CUSTOMER_CIF_NO), Convert.ToInt32(AcListAccountType.SelectedItem.Value));
                    int CID = SCifObj.GetCustomerType(Convert.ToInt32(SCif.CUSTOMER_CIF_NO));
                    if (flag == false)
                    {
                        if (CID == 1)
                        {
                            CustomValidatorAccountType.Text = string.Format("CIF No {0} (Identity: {1}) is Conventional which is not permitted for this account type", SCif.CUSTOMER_CIF_NO, SCif.CUSTOMER_IDENTITY);
                            args.IsValid = false;
                            return;
                        }
                        else
                        {
                            CustomValidatorAccountType.Text = string.Format("CIF No {0} (Identity: {1}) is Asaan which is not permitted for this account type", SCif.CUSTOMER_CIF_NO, SCif.CUSTOMER_IDENTITY);
                            args.IsValid = false;
                            return;
                        }
                    }
                }

                args.IsValid = true;

            }
            else
            {
                ApplicantInformationCifs SCifObj = new ApplicantInformationCifs();

                bool flag = SCifObj.CheckAccountType(Convert.ToInt32(ApCustomerCif.Text), Convert.ToInt32(AcListAccountType.SelectedItem.Value));
                int CID = SCifObj.GetCustomerType(Convert.ToInt32(ApCustomerCif.Text));

                if (flag == false)
                {
                    if (CID == 1)
                    {
                        CustomValidatorAccountType.Text = string.Format("CIF No {0} (Identity: {1}) is Conventional which is not permitted for this account type", ApCustomerCif.Text, ApCustomerCNIC.Text);
                        args.IsValid = false;
                    }
                    else
                    {
                        CustomValidatorAccountType.Text = string.Format("CIF No {0} (Identity: {1}) is Asaan which is not permitted for this account type", ApCustomerCif.Text, ApCustomerCNIC.Text);
                        args.IsValid = false;
                    }
                }
                else
                    args.IsValid = true;
            }
        }
    }
}