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
    public partial class UpdateNextOfKin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    User LoggedUser = Session["User"] as User;
            //    CheckPermissions(LoggedUser);
            //    SetData();


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
                    }

                }
                else
                {
                    if (!IsPostBack)
                    {
                        Session["BID"] = queryid;
                        SetData();
                        SetDataOpen(queryid);
                        btnUpdateNK.Visible = true;
                        btnSubmitCif.Visible = true;
                        CIF cif = new CIF(LoggedUser.USER_ID);

                        //if (cif.CheckStatus(queryid, Status.REJECTEBY_COMPLIANCE_MANAGER.ToString()))
                        //{
                           // rev.Visible = true;
                            rev.Reviewer = false;
                         
                       // }
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
                }
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

            CifTypes c1 = new CifTypes();
            Title t1 = new BLL.Title();
            ResidentType r1 = new ResidentType();
            Country country1 = new Country();
            IdentityType i1 = new IdentityType();
            District d1 = new District();
            City city1 = new City();

            NKListIndividualCIF.DataSource = c1.GetCifTypes();
            NKListIndividualCIF.DataValueField = "ID";
            NKListIndividualCIF.DataTextField = "Name";
            NKListIndividualCIF.DataBind();
            NKListIndividualCIF.Enabled = false;

            NKListTitle.DataSource = t1.GetTitles();
            NKListTitle.DataValueField = "ID";
            NKListTitle.DataTextField = "Name";
            NKListTitle.DataBind();
            NKListTitle.Items.Insert(0, new ListItem("Select", "0"));

            NKListResident.DataSource = r1.GetResidentTypes();
            NKListResident.DataValueField = "ID";
            NKListResident.DataTextField = "Name";
            NKListResident.DataBind();
            NKListResident.Items.Insert(0, new ListItem("Select", "0"));

            NKListNationality.DataSource = country1.GetCountries();
            NKListNationality.DataValueField = "ID";
            NKListNationality.DataTextField = "Name";
            NKListNationality.DataBind();
            NKListNationality.Items.Insert(0, new ListItem("Select", "0"));

            NKListIdentityType.DataSource = i1.GetIdentityTypes();
            NKListIdentityType.DataValueField = "ID";
            NKListIdentityType.DataTextField = "Name";
            NKListIdentityType.DataBind();
            NKListIdentityType.Items.Insert(0, new ListItem("Select", "0"));

            NKListCountryIssue.DataSource = country1.GetCountries();
            NKListCountryIssue.DataValueField = "ID";
            NKListCountryIssue.DataTextField = "Name";
            NKListCountryIssue.DataBind();
            NKListCountryIssue.Items.Insert(0, new ListItem("Select", "0"));


            NKListCountry.DataSource = country1.GetCountries();
            NKListCountry.DataValueField = "ID";
            NKListCountry.DataTextField = "Name";
            NKListCountry.DataBind();
            NKListCountry.Items.Insert(0, new ListItem("Select", "0"));

            NKListCity.DataSource = city1.GetCifTypes();
            NKListCity.DataValueField = "ID";
            NKListCity.DataTextField = "Name";
            NKListCity.DataBind();
            NKListCity.Items.Insert(0, new ListItem("Select", "0"));

            //  NKIssueDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //  NKExpDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            NKListIndividualCIF.Items.FindByText("NEXT_OF_KIN").Selected = true;



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

        protected void btnSubmitNKN_Click(object sender, EventArgs e)
        {
            List<Nationality> countries = new List<Nationality>();

            BasicInformations b1 = new BasicInformations();

            User LogedUser = Session["User"] as User;
            b1.UserId = LogedUser.USER_ID;
            b1.CIF_TYPE = new CifTypes() { ID = Convert.ToInt32(NKListIndividualCIF.SelectedItem.Value), Name = NKListIndividualCIF.SelectedItem.Text };
            b1.CNIC = NKCnic.Text;
            b1.TITLE = new BLL.Title() { ID = Convert.ToInt32(NKListTitle.SelectedItem.Value), Name = NKListTitle.SelectedItem.Text };
            // b1.NAME = NKName.Text;
            b1.FIRST_NAME = NktxtFirstName.Text;
            b1.MIDDLE_NAME = NktxtMiddleName.Text;
            b1.LAST_NAME = NktxtLastName.Text;
            b1.RESIDENT_TYPE = new ResidentType() { ID = Convert.ToInt32(NKListResident.SelectedItem.Value), Name = NKListResident.SelectedItem.Text };
            Nationality c1 = new Nationality() { CountryID = Convert.ToInt32(NKListNationality.SelectedItem.Value), Country = NKListNationality.SelectedItem.Text };
            countries.Add(c1);
            b1.NATIONALITIES = countries;
            int BID = b1.SaveNextOFKin();

            Identity i1 = new Identity();
            i1.BI_ID = BID;
            i1.IDENTITY_TYPE = new IdentityType() { ID = Convert.ToInt32(NKListIdentityType.SelectedItem.Value), Name = NKListIdentityType.SelectedItem.Text };
            i1.IDENTITY_NO = NKIdentityNo.Text;
            i1.COUNTRY_ISSUE = new Country() { ID = Convert.ToInt32(NKListCountryIssue.SelectedItem.Value), Name = NKListCountryIssue.SelectedItem.Text };
            i1.CNIC_DATE_ISSUE = NKIssueDate.Text;
            i1.PLACE_ISSUE = NKPlaceOfIssue.Text;
            i1.EXPIRY_DATE = NKExpDate.Text;
            i1.SaveIdentityNextOfKin();

            ContactInfo contact = new ContactInfo();
            contact.BI_ID = BID;
            contact.COUNTRY_CODE = new Country { ID = Convert.ToInt32(NKListCountry.SelectedItem.Value), Name = NKListCountry.SelectedItem.Text };
            contact.CITY_PERMANENT = new City() { ID = Convert.ToInt32(NKListCity.SelectedItem.Value), Name = NKListCity.SelectedItem.Text };
            contact.BIULDING_SUITE = NkTxtBuilding.Text;
            contact.FLOOR = NktxtFloor.Text;
            contact.STREET = NkTxtStreet.Text;
            contact.DISTRICT = NkTxtDistrict.Text;
            contact.POST_OFFICE = NKPoBox.Text;
            contact.POSTAL_CODE = NKPostalCode.Text;
            contact.OFFICE_CONTACT = NKContactNoOffice.Text;
            contact.RESIDENCE_CONTACT = NKContactNoResidence.Text;
            contact.MOBILE_NO = NKMobileNo.Text;
            contact.FAX_NO = NKFaxNo.Text;
            contact.EMAIL = NKEmail.Text;

            contact.SaveContactInfoNextOfKin();
            String mesg = "Basic Information has been saved";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showNextOfKinAlert('" + mesg + "');", true);

            Response.Redirect("CifAccount.aspx");



        }

        protected void NKListIndividualCIF_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NKListIndividualCIF.SelectedItem.Text == "INDIVIDUAL")
            {

                Response.Redirect("Individual.aspx");
            }
            else if (NKListIndividualCIF.SelectedItem.Text == "BUSINESS / OTHER")
            {
                Response.Redirect("Business.aspx");
            }
        }

        private void SetDataOpen(int id)
        {
            BasicInformations b1 = new BasicInformations();
            if (b1.GetNextOfKin(id))
            {
                NKCnic.Text = b1.CNIC;
                ListExtensions.SetDropdownValue(b1.CIF_TYPE.ID, NKListIndividualCIF);
                ListExtensions.SetDropdownValue(b1.TITLE.ID, NKListTitle);
                NktxtFirstName.Text = b1.FIRST_NAME;
                NktxtMiddleName.Text = b1.MIDDLE_NAME;
                NktxtLastName.Text = b1.LAST_NAME;
                ListExtensions.SetDropdownValue(b1.RESIDENT_TYPE.ID, NKListResident);
                ListExtensions.SetDropdownValue(b1.NATIONALITIES[0].CountryID, NKListNationality);
            }

            Identity i1 = new Identity();
            if (i1.GetNextOFKinIdentity(id))
            {
                ListExtensions.SetDropdownValue(i1.IDENTITY_TYPE.ID, NKListIdentityType);
                NKIdentityNo.Text = i1.IDENTITY_NO;
                ListExtensions.SetDropdownValue(i1.COUNTRY_ISSUE.ID, NKListCountryIssue);
                // ToString("yyyy-MM-dd");
                NKIssueDate.Text = i1.OTHER_IDENTITY_ISSUE_DATE;
                NKPlaceOfIssue.Text = i1.PLACE_ISSUE;
                NKExpDate.Text = i1.EXPIRY_DATE;
            }

            ContactInfo contact = new ContactInfo();

            if (contact.GetContactNextOFKin(id))
            {
                ListExtensions.SetDropdownValue(contact.COUNTRY_CODE.ID, NKListCountry);
                ListExtensions.SetDropdownValue(contact.CITY_PERMANENT.ID, NKListCity);
                NkTxtBuilding.Text = contact.BIULDING_SUITE;
                NktxtFloor.Text = contact.FLOOR;
                NkTxtStreet.Text = contact.STREET;
                NkTxtDistrict.Text = contact.DISTRICT;
                NKPoBox.Text = contact.POST_OFFICE;
                NKPostalCode.Text = contact.POSTAL_CODE;

                NKContactNoOffice.Text = contact.OFFICE_CONTACT;
                NKContactNoResidence.Text = contact.RESIDENCE_CONTACT;
                NKMobileNo.Text = contact.MOBILE_NO;
                NKFaxNo.Text = contact.FAX_NO;
                NKEmail.Text = contact.EMAIL;
            }
            btnSubmitNKN.Visible = false;

        }


        #region Update Code

        protected void btnUpdateNK_Click(object sender, EventArgs e)
        {
            List<Nationality> countries = new List<Nationality>();
            BasicInformations b1 = new BasicInformations();

            b1.ID = (int)Session["BID"];
            b1.CNIC = NKCnic.Text;
            b1.TITLE = new BLL.Title() { ID = Convert.ToInt32(NKListTitle.SelectedItem.Value), Name = NKListTitle.SelectedItem.Text };
            b1.FIRST_NAME = NktxtFirstName.Text;
            b1.MIDDLE_NAME = NktxtMiddleName.Text;
            b1.LAST_NAME = NktxtLastName.Text;
            b1.RESIDENT_TYPE = new ResidentType() { ID = Convert.ToInt32(NKListResident.SelectedItem.Value), Name = NKListResident.SelectedItem.Text };
            Nationality c1 = new Nationality() { CountryID = Convert.ToInt32(NKListNationality.SelectedItem.Value), Country = NKListNationality.SelectedItem.Text };
            countries.Add(c1);
            b1.NATIONALITIES = countries;
            b1.UpdateNextOfKin();

            Identity i1 = new Identity();
            i1.BI_ID = (int)Session["BID"];
            i1.IDENTITY_TYPE = new IdentityType() { ID = Convert.ToInt32(NKListIdentityType.SelectedItem.Value), Name = NKListIdentityType.SelectedItem.Text };
            i1.IDENTITY_NO = NKIdentityNo.Text;
            i1.COUNTRY_ISSUE = new Country() { ID = Convert.ToInt32(NKListCountryIssue.SelectedItem.Value), Name = NKListCountryIssue.SelectedItem.Text };
            i1.CNIC_DATE_ISSUE = NKIssueDate.Text;
            i1.PLACE_ISSUE = NKPlaceOfIssue.Text;
            i1.EXPIRY_DATE = NKExpDate.Text;
            i1.UpdateIdentityNextOfKin();

            ContactInfo contact = new ContactInfo();
            contact.BI_ID = (int)Session["BID"];
            //  contact.ADDRESS_PERMANENT = NKAddress.Text;
            //   contact.DISTRICT_PERMANENT = new District() { ID = Convert.ToInt32(NKListDistrict.SelectedItem.Value), Name = NKListDistrict.SelectedItem.Text };
            //   contact.POBOX_PERMANENT = NKPoBox.Text;

            //    contact.POSTAL_CODE_PERMANENT = NKPostalCode.Text;
            contact.COUNTRY_CODE = new Country { ID = Convert.ToInt32(NKListCountry.SelectedItem.Value), Name = NKListCountry.SelectedItem.Text };
            contact.CITY_PERMANENT = new City() { ID = Convert.ToInt32(NKListCity.SelectedItem.Value), Name = NKListCity.SelectedItem.Text };
            contact.BIULDING_SUITE = NkTxtBuilding.Text;
            contact.FLOOR = NktxtFloor.Text;
            contact.STREET = NkTxtStreet.Text;
            contact.DISTRICT = NkTxtDistrict.Text;
            contact.POST_OFFICE = NKPoBox.Text;
            contact.POSTAL_CODE = NKPostalCode.Text;
            contact.OFFICE_CONTACT = NKContactNoOffice.Text;
            contact.RESIDENCE_CONTACT = NKContactNoResidence.Text;
            contact.MOBILE_NO = NKMobileNo.Text;
            contact.FAX_NO = NKFaxNo.Text;
            contact.EMAIL = NKEmail.Text;
            contact.UpdateContactInfoNextOfKin();

            String mesg = "Basic Information has been Updated";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showNextOfKinAlert('" + mesg + "');", true);

        }

        protected void btnSubmitCif_Click(object sender, EventArgs e)
        {
            int BID = (int)Session["BID"];
            CIF cif = new CIF(BID, CifType.NEXT_OF_KIN);
            User LogedUser = Session["User"] as User;
            cif.ChangeStatus(Status.SUBMITTED, LogedUser);
            Response.Redirect("CifAccount.aspx");
        }

        #endregion

        protected void CustomValidatorAnyContact_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (NKMobileNo.Text.Length > 0 || NKContactNoResidence.Text.Length > 0 || NKContactNoOffice.Text.Length > 0)
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        //protected void CustomValidatorCnic_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    BasicInformations b = new BasicInformations();
        //    if (b.IsRegNoExixts(NKCnic.Text))
        //        args.IsValid = false;
        //    else
        //        args.IsValid = true;
        //}

    }
}