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
    public partial class UpdateOffice : System.Web.UI.Page
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
                    }

                }
                else
                {
                    if (!IsPostBack)
                    {
                        Session["BID"] = queryid;
                        SetData();
                        SetDataOpen(queryid);
                        btnUpdateOfc.Visible = true;
                        btnSubmitSaveOfc.Visible = true;
                        // temp risk checking
                        // CalculateRisk();

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

        private void SetDataOpen(int queryid)
        {
            BasicInformations b = new BasicInformations();

            if (b.GetOffice(queryid))
            {
                OfftxtName.Text = b.NAME_OFFICE;
                Offtxt.Text = b.WHO_NAME;
                OffDesignation.Text = b.WHO_DESIG;

            }

            ContactInfo c = new ContactInfo();
            if (c.GetIndividualContactInfo(queryid))
            {
                ListExtensions.SetDropdownValue(c.COUNTRY_CODE.ID, OffListCountry);
                ListExtensions.SetDropdownValue(c.CITY_PERMANENT.ID, OffListCity);
                ListExtensions.SetDropdownValue(c.PROVINCE.ID, OffListProvince);
                OffTxtBuilding.Text = c.BIULDING_SUITE;
                OffTxtFloor.Text = c.FLOOR;
                OffTxtStreet.Text = c.STREET;
                OffTxtDistrict.Text = c.DISTRICT;
                OffPoBox.Text = c.POST_OFFICE;
                OffPostalCode.Text = c.POSTAL_CODE;

                ListExtensions.SetDropdownValue(c.COUNTRY_CODE_PRE.ID, OffListCountryPre);
                ListExtensions.SetDropdownValue(c.CITY_PRESENT.ID, OffListCityPre);
                ListExtensions.SetDropdownValue(c.PROVINCE_PRE.ID, OffListProvincePre);
                OffTxtBuildingPre.Text = c.BIULDING_SUITE_PRE;
                OffTxtFloorPre.Text = c.FLOOR_PRE;
                OffTxtStreetPre.Text = c.STREET_PRE;
                OffTxtDistrictPre.Text = c.DISTRICT_PRE;
                OffPoBoxPre.Text = c.POST_OFFICE_PRE;
                OffPostalCodePre.Text = c.POSTAL_CODE_PRE;

                OfficeNo.Text = c.OFFICE_CONTACT;
                MobileNo.Text = c.MOBILE_NO;
                FaxNo.Text = c.FAX_NO;

            }

            btnSubmitSaveOfc.Visible = false;
        }

        private void SetData()
        {
            Country co = new Country();
            City c = new City();
            Province p = new Province();
            CifTypes cif = new CifTypes();


            OffListCifType.DataSource = cif.GetCifTypes();
            OffListCifType.DataValueField = "ID";
            OffListCifType.DataTextField = "Name";
            OffListCifType.DataBind();
            OffListCifType.Items.FindByText("OFFICE").Selected = true;

            OffListCountry.DataSource = co.GetCountries();
            OffListCountry.DataValueField = "ID";
            OffListCountry.DataTextField = "Name";
            OffListCountry.DataBind();
            OffListCountry.Items.Insert(0, new ListItem("Select", "0"));

            OffListCity.DataSource = c.GetCifTypes();
            OffListCity.DataValueField = "ID";
            OffListCity.DataTextField = "Name";
            OffListCity.DataBind();
            OffListCity.Items.Insert(0, new ListItem("Select", "0"));

            OffListProvince.DataSource = p.GetProvinces();
            OffListProvince.DataValueField = "ID";
            OffListProvince.DataTextField = "Name";
            OffListProvince.DataBind();
            OffListProvince.Items.Insert(0, new ListItem("Select", "0"));

            OffListCountryPre.DataSource = co.GetCountries();
            OffListCountryPre.DataValueField = "ID";
            OffListCountryPre.DataTextField = "Name";
            OffListCountryPre.DataBind();
            OffListCountryPre.Items.Insert(0, new ListItem("Select", "0"));

            OffListCityPre.DataSource = c.GetCifTypes();
            OffListCityPre.DataValueField = "ID";
            OffListCityPre.DataTextField = "Name";
            OffListCityPre.DataBind();
            OffListCityPre.Items.Insert(0, new ListItem("Select", "0"));

            OffListProvincePre.DataSource = p.GetProvinces();
            OffListProvincePre.DataValueField = "ID";
            OffListProvincePre.DataTextField = "Name";
            OffListProvincePre.DataBind();
            OffListProvincePre.Items.Insert(0, new ListItem("Select", "0"));

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

        protected void btnSubmitSaveOfc_Click(object sender, EventArgs e)
        {
            BasicInformations b = new BasicInformations();
            User LogedUser = Session["User"] as User;
            b.UserId = LogedUser.USER_ID;
            b.BRANCH_CODE = LogedUser.Branch.BRANCH_CODE;
            b.CIF_TYPE = new CifTypes() { ID = Convert.ToInt32(OffListCifType.SelectedItem.Value), Name = OffListCifType.SelectedItem.Text };
            b.NAME_OFFICE = OfftxtName.Text;
            b.WHO_NAME = Offtxt.Text;
            b.WHO_DESIG = OffDesignation.Text;
            int BID = b.SaveOffice();

            ContactInfo c = new ContactInfo();
            c.BI_ID = BID;
            c.COUNTRY_CODE = new Country() { ID = Convert.ToInt32(OffListCountry.SelectedItem.Value) };
            if (OffListCountry.SelectedItem.Text.Trim() != "PAKISTAN")
            {
                c.PROVINCE = new Province() { ID = null };
                c.CITY_PERMANENT = new City() { ID = null };
            }
            else
            {
                c.PROVINCE = new Province() { ID = Convert.ToInt32(OffListProvince.SelectedItem.Value) };
                c.CITY_PERMANENT = new City() { ID = Convert.ToInt32(OffListCity.SelectedItem.Value), Name = OffListCity.SelectedItem.Text };
            }

            c.STREET = OffTxtStreet.Text;
            c.FLOOR = OffTxtFloor.Text;
            c.BIULDING_SUITE = OffTxtBuilding.Text;
            c.DISTRICT = OffTxtDistrict.Text;
            c.POST_OFFICE = OffPoBox.Text;
            c.POSTAL_CODE = OffPostalCode.Text;

            c.COUNTRY_CODE_PRE = new Country() { ID = Convert.ToInt32(OffListCountryPre.SelectedItem.Value) };
            if (OffListCountryPre.SelectedItem.Text.Trim() != "PAKISTAN")
            {
                c.PROVINCE_PRE = new Province() { ID = null };
                c.CITY_PRESENT = new City() { ID = null };
            }
            else
            {
                c.PROVINCE_PRE = new Province() { ID = Convert.ToInt32(OffListProvincePre.SelectedItem.Value) };
                c.CITY_PRESENT = new City() { ID = Convert.ToInt32(OffListCityPre.SelectedItem.Value), Name = OffListCityPre.SelectedItem.Text };
            }

            c.STREET_PRE = OffTxtStreetPre.Text;
            c.FLOOR_PRE = OffTxtFloorPre.Text;
            c.BIULDING_SUITE_PRE = OffTxtBuildingPre.Text;
            c.DISTRICT_PRE = OffTxtDistrictPre.Text;
            c.POST_OFFICE_PRE = OffPoBoxPre.Text;
            c.POSTAL_CODE_PRE = OffPostalCodePre.Text;


            c.OFFICE_CONTACT = OfficeNo.Text;
            c.MOBILE_NO = MobileNo.Text;
            c.FAX_NO = FaxNo.Text;
            c.RESIDENCE_CONTACT = "";
            c.EMAIL = "";
            c.SaveContactInfo();

            Response.Redirect("CifAccount.aspx");
        }

        protected void btnSubmitOfc_Click(object sender, EventArgs e)
        {
            int BID = (int)Session["BID"];
            CIF cif = new CIF(BID, CifType.GOVERNMENT);
            User LoggedUser = Session["User"] as User;
            cif.ChangeStatus(Status.SUBMITTED, LoggedUser);
            // CalculateRisk();
            Response.Redirect("CifAccount.aspx");
        }

        protected void btnUpdateOfc_Click(object sender, EventArgs e)
        {
            BasicInformations b = new BasicInformations();
            int BID = (int)Session["BID"];
            b.ID = BID;
            b.NAME_OFFICE = OfftxtName.Text;
            b.WHO_NAME = Offtxt.Text;
            b.WHO_DESIG = OffDesignation.Text;
            b.UpdateOffice();

            ContactInfo c = new ContactInfo();
            c.BI_ID = BID;
            c.COUNTRY_CODE = new Country() { ID = Convert.ToInt32(OffListCountry.SelectedItem.Value) };
            if (OffListCountry.SelectedItem.Text.Trim() != "PAKISTAN")
            {
                c.PROVINCE = new Province() { ID = null };
                c.CITY_PERMANENT = new City() { ID = null };
            }
            else
            {
                c.PROVINCE = new Province() { ID = Convert.ToInt32(OffListProvince.SelectedItem.Value) };
                c.CITY_PERMANENT = new City() { ID = Convert.ToInt32(OffListCity.SelectedItem.Value), Name = OffListCity.SelectedItem.Text };
            }

            c.STREET = OffTxtStreet.Text;
            c.FLOOR = OffTxtFloor.Text;
            c.BIULDING_SUITE = OffTxtBuilding.Text;
            c.DISTRICT = OffTxtDistrict.Text;
            c.POST_OFFICE = OffPoBox.Text;
            c.POSTAL_CODE = OffPostalCode.Text;

            c.COUNTRY_CODE_PRE = new Country() { ID = Convert.ToInt32(OffListCountryPre.SelectedItem.Value) };
            if (OffListCountryPre.SelectedItem.Text.Trim() != "PAKISTAN")
            {
                c.PROVINCE_PRE = new Province() { ID = null };
                c.CITY_PRESENT = new City() { ID = null };
            }
            else
            {
                c.PROVINCE_PRE = new Province() { ID = Convert.ToInt32(OffListProvincePre.SelectedItem.Value) };
                c.CITY_PRESENT = new City() { ID = Convert.ToInt32(OffListCityPre.SelectedItem.Value), Name = OffListCityPre.SelectedItem.Text };
            }

            c.STREET_PRE = OffTxtStreetPre.Text;
            c.FLOOR_PRE = OffTxtFloorPre.Text;
            c.BIULDING_SUITE_PRE = OffTxtBuildingPre.Text;
            c.DISTRICT_PRE = OffTxtDistrictPre.Text;
            c.POST_OFFICE_PRE = OffPoBoxPre.Text;
            c.POSTAL_CODE_PRE = OffPostalCodePre.Text;


            c.OFFICE_CONTACT = OfficeNo.Text;
            c.MOBILE_NO = MobileNo.Text;
            c.FAX_NO = FaxNo.Text;
            c.RESIDENCE_CONTACT = "";
            c.EMAIL = "";
            c.UpdateContactInfo();

            String mesg = "OFFICE CIF has been Updated";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showNextOfKinAlert('" + mesg + "');", true);

        }


    }
}