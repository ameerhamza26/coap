using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Reflection;

namespace BLL
{
   public class BasicInformations
    {


        #region BI Model
        public int ID { get; set; }
        public DateTime CIF_ENTRY_DATE { get; set; }
        public CifTypes CIF_TYPE { get; set; }
        public PrimaryDocumentType PRIMARY_DOCUMENT_TYPE { get; set; }
        public string CNIC { get; set; }
        public Title TITLE { get; set; }
        public string NAME { get; set; }
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public Title TITLE_FH { get; set; }
        public string NAME_FH { get; set; }
        public string CNIC_FH { get; set; }
        public string CIF_FH { get; set; }
        public string NAME_MOTHER { get; set; }
        public string CNIC_MOTHER { get; set; }
        public string CNIC_MOTHER_OLD { get; set; }
        public string DATE_BIRTH { get; set; }
        public string PLACE_BIRTH { get; set; }
        public Country Country_Birth { get; set; }
        public MartialStatus MARTIAL_STATUS { get; set; }
        public Gender GENDER { get; set; }
        public Religion RELIGION { get; set; }
        public ResidentType RESIDENT_TYPE { get; set; }
        public List<Nationality> NATIONALITIES { get; set; }
        public string MONTHLY_INCOME { get; set; }
        public Country COUNTRY_RESIDENCE { get; set; }
        public CustomerDeal CUSTOMER_DEAL { get; set; }
        public string NAME_OFFICE { get; set; }
        public string SALES_TAX_NO { get; set; }
        public string NTN { get; set; }
        public IssuingAgency Issuing_Agency { get; set; }
        public string REG_NO { get; set; }
        public string REG_DATE { get; set; }
        public string COMMENCEMENT_DATE { get; set; }
        public string PAST_BUSS_EXP { get; set; }
        public AccountNature ACCOUNT_NATURE { get; set; }
        public BusinessCustomerClassification CUSTOMER_CLASSIFICATION { get; set; }
        public string CIF_GROUP { get; set; }
        public NatureBusiness NATURE_BUSINESS { get; set; }
        public string NATURE_BUSINESS_DESCRP { get; set; }
        public NbpCategories CATERGORY_NBP { get; set; }
        public SbpCategories CATERGORY_SBP { get; set; }
        public BaseCategories CATERGORY_BASE { get; set; }
        public string SHARE { get; set; }
        public ApplicantStatuses APPLICANT_STATUS { get; set; }
        public string STATUS { get; set; }
        public int UserId { get; set; }
        public bool IsAvlbl { get; set; }
        public Country COUNTRY_INCORPORATION { get; set; }
        public BusinessType BUSINESS_TYPE { get; set; }
        public InstitutionType INSTITUTION_TYPE { get; set; }
        public SicCode SIC_CODES { get; set; }
        public SubIndustry SUB_INDUSTRY { get; set; }
        public string RISK_CATEGORY { get; set; }
        public string RISK_SCORE { get; set; }
        public string BRANCH_CODE { get; set; }
        public bool DOCUMENT_VERIFIED { get; set; }
        public string PROFILE_CIF_NO { get; set; }

        public CifCustomerType CUSTOMER_TYPE { get; set; }
        public Nullable<System.DateTime> LAST_UPDATED { get; set; }

        public Nullable<int> GOV_TYPE { get; set; }

        public string WHO_NAME { get; set; }
        public string WHO_DESIG { get; set; }
        public Nullable<int> CIF_OFFICER_CODE { get; set; }

        public string ISSUING_AGENCY_OTHER { get; set; }
        #endregion


        public int SaveOffice()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                BASIC_INFORMATIONS b = new BASIC_INFORMATIONS();
                b.UserId = this.UserId;
                b.BRANCH_CODE = this.BRANCH_CODE;
                b.CIF_TYPE = this.CIF_TYPE.ID;
                b.WHO_NAME = this.WHO_NAME;
                b.WHO_DESIG = this.WHO_DESIG;
                b.NAME_OFFICE = this.NAME_OFFICE;
                b.LAST_UPDATED = DateTime.Now;
                b.STATUS = Status.SUBMITTED.ToString();
                db.BASIC_INFORMATIONS.Add(b);
                db.SaveChanges();

                return b.ID;

            }
        }

        public void UpdateOffice()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                BASIC_INFORMATIONS b = db.BASIC_INFORMATIONS.FirstOrDefault(BI => BI.ID == this.ID);
                b.WHO_NAME = this.WHO_NAME;
                b.WHO_DESIG = this.WHO_DESIG;
                b.NAME_OFFICE = this.NAME_OFFICE;
                b.LAST_UPDATED = DateTime.Now;
                db.SaveChanges();
            }
        }

        public bool GetOffice(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.BASIC_INFORMATIONS.Where(BI => BI.ID == BID).Any())
                {
                    BASIC_INFORMATIONS b = db.BASIC_INFORMATIONS.FirstOrDefault(BI => BI.ID == BID);
                    this.WHO_NAME = b.WHO_NAME;
                    this.WHO_DESIG = b.WHO_DESIG;
                    this.NAME_OFFICE = b.NAME_OFFICE;
                    return true;
                }
                else
                    return false;
               
            }
        }
        public void InsertRisk(string RiskCategory, string RiskScore,int BID)
        {
            using(CAOPDbContext db = new CAOPDbContext())
            {
                var BI = db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == BID);
                BI.RISK_CATEGORY = RiskCategory;
                BI.RISK_SCORE = RiskScore;
                db.SaveChanges();
            }
        }

        public void InsertProfileNumber(int BID, string Profilenum)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var BI = db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == BID);
                BI.PROFILE_CIF_NO = Profilenum;
                db.SaveChanges();
            }
        }

        public int SaveIndividual()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                BASIC_INFORMATIONS BI = new BASIC_INFORMATIONS();

                    BI.CIF_ENTRY_DATE = CIF_ENTRY_DATE;
                    BI.CIF_TYPE = (int) CIF_TYPE.ID;
                    BI.DOCUMENT_TYPE_PRIMARY = (int?)PRIMARY_DOCUMENT_TYPE.ID;
                    BI.CNIC = CNIC;
                    BI.TITLE = (int)TITLE.ID;
                    BI.NAME = this.FIRST_NAME.ToUpper() + " " + this.MIDDLE_NAME.ToUpper() + " " + this.LAST_NAME.ToUpper();
                    BI.FIRST_NAME = this.FIRST_NAME.ToUpper();
                    BI.MIDDLE_NAME = this.MIDDLE_NAME.ToUpper();
                    BI.LAST_NAME = this.LAST_NAME.ToUpper();
                    BI.TITLE_FH = (int)TITLE_FH.ID;
                    BI.NAME_FH = NAME_FH.ToUpper();
                    BI.CNIC_FH = CNIC_FH;
                    BI.CIF_FH = CIF_FH;
                    BI.NAME_MOTHER = NAME_MOTHER.ToUpper();
                    BI.CNIC_MOTHER = CNIC_MOTHER;
                    BI.CNIC_MOTHER_OLD = CNIC_MOTHER_OLD;
                    BI.DOB = DATE_BIRTH;
                    BI.POB = PLACE_BIRTH;
                    BI.COB = (int) Country_Birth.ID;
                    BI.MARTIAL_STATUS = (int) MARTIAL_STATUS.ID;
                    BI.GENDER = (int)GENDER.ID;
                    BI.RELIGION = (int)RELIGION.ID;
                    BI.RESIDENT_TYPE = (int)RESIDENT_TYPE.ID;
                    BI.MONTHLY_INCOME = MONTHLY_INCOME;
                    BI.COUNTRY_RESIDENCE = COUNTRY_RESIDENCE.ID;
                    BI.COSTUMER_DEAL =  CUSTOMER_DEAL.ID;
                    BI.DOCUMENT_VERIFIED = this.DOCUMENT_VERIFIED;
                    BI.BRANCH_CODE = this.BRANCH_CODE;
                    BI.STATUS = Status.SAVED.ToString();
                    BI.PROFILE_STATUS = "PENDING";
                    BI.PROFILE_CIF_NO = this.PROFILE_CIF_NO;
                    BI.UserId = this.UserId;
                    BI.CUSTOMER_TYPE = this.CUSTOMER_TYPE.ID;
                    BI.CIF_OFFICER_CODE = this.CIF_OFFICER_CODE;
                    BI.LAST_UPDATED = DateTime.Now;
                    db.BASIC_INFORMATIONS.Add(BI);
                    db.SaveChanges();


                    foreach (Nationality n in NATIONALITIES)
                    {
                        NATIONALITIES_BASIC_INFORMATION na = new NATIONALITIES_BASIC_INFORMATION();

                        na.BI_ID = BI.ID;
                        na.COUNTRY_ID = n.CountryID;
                        na.COUNTRY = n.Country;

                        db.NATIONALITIES_BASIC_INFORMATION.Add(na);
                    }
                    db.SaveChanges();

                    return BI.ID;

            }
        }

        public int SaveMinor()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                BASIC_INFORMATIONS BI = new BASIC_INFORMATIONS();

                BI.CIF_ENTRY_DATE = CIF_ENTRY_DATE;
                BI.CIF_TYPE = (int)CIF_TYPE.ID;
                BI.DOCUMENT_TYPE_PRIMARY = (int?)PRIMARY_DOCUMENT_TYPE.ID;
                BI.CNIC = CNIC;
                BI.TITLE = (int)TITLE.ID;
                BI.NAME = this.FIRST_NAME.ToUpper() + " " + this.MIDDLE_NAME.ToUpper() + " " + this.LAST_NAME.ToUpper();
                BI.FIRST_NAME = this.FIRST_NAME.ToUpper();
                BI.MIDDLE_NAME = this.MIDDLE_NAME.ToUpper();
                BI.LAST_NAME = this.LAST_NAME.ToUpper();
                BI.TITLE_FH = (int)TITLE_FH.ID;
                BI.NAME_FH = NAME_FH.ToUpper();
                BI.CNIC_FH = CNIC_FH;
                BI.CIF_FH = CIF_FH;
                BI.NAME_MOTHER = NAME_MOTHER.ToUpper();
                BI.CNIC_MOTHER = CNIC_MOTHER;
                BI.CNIC_MOTHER_OLD = CNIC_MOTHER_OLD;
                BI.DOB = DATE_BIRTH;
                BI.POB = PLACE_BIRTH;
                BI.COB = (int)Country_Birth.ID;
                BI.MARTIAL_STATUS = (int)MARTIAL_STATUS.ID;
                BI.GENDER = (int)GENDER.ID;
                BI.RELIGION = (int)RELIGION.ID;
                BI.RESIDENT_TYPE = (int)RESIDENT_TYPE.ID;
                BI.MONTHLY_INCOME = MONTHLY_INCOME;
                BI.COUNTRY_RESIDENCE = COUNTRY_RESIDENCE.ID;
                BI.COSTUMER_DEAL = CUSTOMER_DEAL.ID;
                BI.DOCUMENT_VERIFIED = this.DOCUMENT_VERIFIED;
                BI.BRANCH_CODE = this.BRANCH_CODE;
                BI.STATUS = Status.SAVED.ToString();
                BI.PROFILE_STATUS = "PENDING";
                BI.PROFILE_CIF_NO = this.PROFILE_CIF_NO;
                BI.UserId = this.UserId;
                BI.CUSTOMER_TYPE = this.CUSTOMER_TYPE.ID;
                BI.LAST_UPDATED = DateTime.Now;
                db.BASIC_INFORMATIONS.Add(BI);
                db.SaveChanges();


                foreach (Nationality n in NATIONALITIES)
                {
                    NATIONALITIES_BASIC_INFORMATION na = new NATIONALITIES_BASIC_INFORMATION();

                    na.BI_ID = BI.ID;
                    na.COUNTRY_ID = n.CountryID;
                    na.COUNTRY = n.Country;

                    db.NATIONALITIES_BASIC_INFORMATION.Add(na);
                }
                db.SaveChanges();

                return BI.ID;

            }
        }

        public void UpdateIndividual()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                BASIC_INFORMATIONS BI = db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.ID);

                BI.DOCUMENT_TYPE_PRIMARY = (int?)PRIMARY_DOCUMENT_TYPE.ID;
                BI.CNIC = CNIC;
                BI.TITLE = (int)TITLE.ID;
                BI.TITLE_FH = (int)TITLE_FH.ID;
                BI.NAME_FH = NAME_FH.ToUpper();
                BI.NAME = this.FIRST_NAME.ToUpper() + " " + this.MIDDLE_NAME.ToUpper() + " " + this.LAST_NAME.ToUpper();
                BI.FIRST_NAME = this.FIRST_NAME.ToUpper();
                BI.MIDDLE_NAME = this.MIDDLE_NAME.ToUpper();
                BI.LAST_NAME = this.LAST_NAME.ToUpper();
                BI.CNIC_FH = CNIC_FH;
                BI.CIF_FH = CIF_FH;
                BI.NAME_MOTHER = NAME_MOTHER.ToUpper();
                BI.CNIC_MOTHER = CNIC_MOTHER;
                BI.CNIC_MOTHER_OLD = CNIC_MOTHER_OLD;
                BI.DOB = DATE_BIRTH;
                BI.POB = PLACE_BIRTH;
                BI.COB = (int)Country_Birth.ID;
                BI.MARTIAL_STATUS = (int)MARTIAL_STATUS.ID;
                BI.GENDER = (int)GENDER.ID;
                BI.RELIGION = (int)RELIGION.ID;
                BI.RESIDENT_TYPE = (int)RESIDENT_TYPE.ID;
                BI.MONTHLY_INCOME = MONTHLY_INCOME;
                BI.COUNTRY_RESIDENCE = COUNTRY_RESIDENCE.ID;
                BI.COSTUMER_DEAL = CUSTOMER_DEAL.ID;
                BI.DOCUMENT_VERIFIED = this.DOCUMENT_VERIFIED;
                BI.CUSTOMER_TYPE = this.CUSTOMER_TYPE.ID;
                BI.CIF_OFFICER_CODE = this.CIF_OFFICER_CODE;
                BI.LAST_UPDATED = DateTime.Now;

                db.NATIONALITIES_BASIC_INFORMATION.RemoveRange(db.NATIONALITIES_BASIC_INFORMATION.Where(n => n.BI_ID == this.ID));
                db.SaveChanges();

                foreach (Nationality n in NATIONALITIES)
                {
                    NATIONALITIES_BASIC_INFORMATION na = new NATIONALITIES_BASIC_INFORMATION();

                    na.BI_ID = this.ID;
                    na.COUNTRY_ID = n.CountryID;
                    na.COUNTRY = n.Country;
                    db.NATIONALITIES_BASIC_INFORMATION.Add(na);
                }
                db.SaveChanges();

               

            }
        }



        public int SaveNextOFKin()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                BASIC_INFORMATIONS BI = new BASIC_INFORMATIONS();

                BI.CIF_TYPE = this.CIF_TYPE.ID;
                BI.CNIC = this.CNIC;
                BI.TITLE = this.TITLE.ID;
                BI.FIRST_NAME = this.FIRST_NAME;
                BI.MIDDLE_NAME = this.MIDDLE_NAME;
                BI.LAST_NAME = this.LAST_NAME;
                BI.RESIDENT_TYPE = this.RESIDENT_TYPE.ID;
                BI.STATUS = Status.SUBMITTED.ToString();
                BI.UserId = this.UserId;
                BI.LAST_UPDATED = DateTime.Now;

                db.BASIC_INFORMATIONS.Add(BI);
                db.SaveChanges();

                foreach (Nationality n in NATIONALITIES)
                {
                    NATIONALITIES_BASIC_INFORMATION na = new NATIONALITIES_BASIC_INFORMATION();

                    na.BI_ID = BI.ID;
                    na.COUNTRY_ID = n.CountryID;
                    na.COUNTRY = n.Country;

                    db.NATIONALITIES_BASIC_INFORMATION.Add(na);
                }

                db.SaveChanges();

                return BI.ID;

            }
        }

     
        public void UpdateNextOfKin()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                BASIC_INFORMATIONS BI = db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.ID);
                BI.CNIC = this.CNIC;
                BI.TITLE = this.TITLE.ID;
              //  BI.NAME = this.NAME;
                BI.FIRST_NAME = this.FIRST_NAME;
                BI.MIDDLE_NAME = this.MIDDLE_NAME;
                BI.LAST_NAME = this.LAST_NAME;
                BI.RESIDENT_TYPE = this.RESIDENT_TYPE.ID;
                BI.LAST_UPDATED = this.LAST_UPDATED;
                db.NATIONALITIES_BASIC_INFORMATION.RemoveRange(db.NATIONALITIES_BASIC_INFORMATION.Where(n => n.BI_ID == this.ID));
                db.SaveChanges();

                foreach (Nationality n in NATIONALITIES)
                {
                    NATIONALITIES_BASIC_INFORMATION na = new NATIONALITIES_BASIC_INFORMATION();

                    na.BI_ID = BI.ID;
                    na.COUNTRY_ID = n.CountryID;
                    na.COUNTRY = n.Country;
                    db.NATIONALITIES_BASIC_INFORMATION.Add(na);
                }

                db.SaveChanges();
            }
        }

        public int SaveBusiness()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                BASIC_INFORMATIONS BI = new BASIC_INFORMATIONS();

                BI.CIF_TYPE = this.CIF_TYPE.ID;
                BI.CIF_ENTRY_DATE = DateTime.Now;
                BI.NAME_OFFICE = this.NAME_OFFICE.ToUpper();
                BI.NTN = this.NTN.ToUpper();
                BI.SALES_TAX_NO = this.SALES_TAX_NO.ToUpper();
                BI.ISSUING_AGENCY = this.Issuing_Agency.ID;
                BI.REG_NO = this.REG_NO.ToUpper();
                BI.REG_DATE = this.REG_DATE;
                BI.COMMENCEMENT_DATE = this.COMMENCEMENT_DATE;
                BI.PAST_BUSS_EXP = this.PAST_BUSS_EXP.ToUpper();
                BI.ACCOUNT_NATURE = this.ACCOUNT_NATURE.ID;
                BI.CUSTOMER_CLASSIFICATION = this.CUSTOMER_CLASSIFICATION.ID;
                BI.CIF_GROUP = this.CIF_GROUP.ToUpper();
                BI.NATURE_BUSINESS = this.NATURE_BUSINESS.ID;
                BI.NATURE_BUSINESS_DESCRP = this.NATURE_BUSINESS_DESCRP.ToUpper();
                BI.CATERGORY_NBP = this.CATERGORY_NBP.ID;
                BI.CATERGORY_SBP = this.CATERGORY_SBP.ID;
                BI.CATERGORY_BASE = this.CATERGORY_BASE.ID;
                BI.COSTUMER_DEAL = this.CUSTOMER_DEAL.ID;
                BI.STATUS = Status.SAVED.ToString();
                BI.PROFILE_STATUS = "PENDING";
                BI.UserId = this.UserId;
                BI.LAST_UPDATED = DateTime.Now;
                BI.COUNTRY_INCORPORATION = this.COUNTRY_INCORPORATION.ID;
                BI.BUSINESS_TYPE = this.BUSINESS_TYPE.ID;
                BI.INSTITUTION_TYPE = this.INSTITUTION_TYPE.ID;
                BI.SIC_CODE = this.SIC_CODES.ID;
                BI.SUB_INDUSTRY = this.SUB_INDUSTRY.ID;
                BI.DOCUMENT_VERIFIED = this.DOCUMENT_VERIFIED;
                BI.BRANCH_CODE = this.BRANCH_CODE;
                BI.ISSUING_AGENCY_OTHER = this.ISSUING_AGENCY_OTHER;
                if (this.GOV_TYPE != null)
                    BI.GOV_TYPE = this.GOV_TYPE;
                db.BASIC_INFORMATIONS.Add(BI);
                db.SaveChanges();
                return BI.ID;

            }
        }

        public void UpdateBusiness()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                BASIC_INFORMATIONS BI = db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.ID);

                BI.NAME_OFFICE = this.NAME_OFFICE.ToUpper();
                BI.NTN = this.NTN.ToUpper();
                BI.SALES_TAX_NO = this.SALES_TAX_NO.ToUpper();
                BI.ISSUING_AGENCY = this.Issuing_Agency.ID;
                BI.REG_NO = this.REG_NO.ToUpper();
                BI.REG_DATE = this.REG_DATE;
                BI.COMMENCEMENT_DATE = this.COMMENCEMENT_DATE;
                BI.PAST_BUSS_EXP = this.PAST_BUSS_EXP.ToUpper();
                BI.ACCOUNT_NATURE = this.ACCOUNT_NATURE.ID;
                BI.CUSTOMER_CLASSIFICATION = this.CUSTOMER_CLASSIFICATION.ID;
                BI.CIF_GROUP = this.CIF_GROUP.ToUpper();
                BI.NATURE_BUSINESS = this.NATURE_BUSINESS.ID;
                BI.NATURE_BUSINESS_DESCRP = this.NATURE_BUSINESS_DESCRP.ToUpper();
                BI.CATERGORY_NBP = this.CATERGORY_NBP.ID;
                BI.CATERGORY_SBP = this.CATERGORY_SBP.ID;
                BI.CATERGORY_BASE = this.CATERGORY_BASE.ID;
                BI.COSTUMER_DEAL = this.CUSTOMER_DEAL.ID;
                BI.STATUS = Status.SAVED.ToString();
                BI.LAST_UPDATED = this.LAST_UPDATED;
                BI.COUNTRY_INCORPORATION = this.COUNTRY_INCORPORATION.ID;
                BI.BUSINESS_TYPE = this.BUSINESS_TYPE.ID;
                BI.INSTITUTION_TYPE = this.INSTITUTION_TYPE.ID;
                BI.SIC_CODE = this.SIC_CODES.ID;
                BI.SUB_INDUSTRY = this.SUB_INDUSTRY.ID;
                BI.DOCUMENT_VERIFIED = this.DOCUMENT_VERIFIED;
                BI.ISSUING_AGENCY_OTHER = this.ISSUING_AGENCY_OTHER;
                if (this.GOV_TYPE != null)
                    BI.GOV_TYPE = this.GOV_TYPE;
                db.SaveChanges();
            }
        }

       
      

      

     


        public bool GetIndividual(int BI_ID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.BASIC_INFORMATIONS.Where(b => b.ID == BI_ID).Any())
                {
                    var BI = db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == BI_ID);

                    this.CIF_ENTRY_DATE = (DateTime)BI.CIF_ENTRY_DATE;
                    this.CIF_TYPE = new CifTypes { ID = (int)BI.CIF_TYPE };
                    this.PRIMARY_DOCUMENT_TYPE = new PrimaryDocumentType { ID =  BI.DOCUMENT_TYPE_PRIMARY };
                    this.CNIC = BI.CNIC;
                    this.CNIC_FH = BI.CNIC_FH;
                    this.TITLE = new Title { ID = (int)BI.TITLE };
                    this.NAME = BI.NAME;
                    this.FIRST_NAME = BI.FIRST_NAME;
                    this.MIDDLE_NAME = BI.MIDDLE_NAME;
                    this.LAST_NAME = BI.LAST_NAME;
                    this.TITLE_FH = new Title { ID = (int)BI.TITLE_FH };
                    this.NAME_FH = BI.NAME_FH;
                    this.CIF_FH = BI.CIF_FH;
                    this.NAME_MOTHER = BI.NAME_MOTHER;
                    this.CNIC_MOTHER = BI.CNIC_MOTHER;
                    this.CNIC_MOTHER_OLD = BI.CNIC_MOTHER_OLD;
                    this.DATE_BIRTH = BI.DOB;
                    this.PLACE_BIRTH = BI.POB;
                    this.Country_Birth = new Country { ID = (int)BI.COB };
                    this.MARTIAL_STATUS = new MartialStatus { ID = (int)BI.MARTIAL_STATUS };
                    this.GENDER = new Gender { ID = (int)BI.GENDER };
                    this.RELIGION = new Religion { ID = (int)BI.RELIGION };
                    this.RESIDENT_TYPE = new ResidentType { ID = (int)BI.RESIDENT_TYPE };
                    this.MONTHLY_INCOME = BI.MONTHLY_INCOME;
                    this.COUNTRY_RESIDENCE = new Country { ID = BI.COUNTRY_RESIDENCE };
                    this.CUSTOMER_DEAL = new CustomerDeal { ID = (int?)BI.COSTUMER_DEAL };
                    this.DOCUMENT_VERIFIED = (bool) BI.DOCUMENT_VERIFIED;
                    this.CUSTOMER_TYPE = new CifCustomerType() { ID = BI.CUSTOMER_TYPE };
                    this.CIF_OFFICER_CODE = BI.CIF_OFFICER_CODE;
                    this.STATUS = "SAVED";
                    this.UserId = (int)BI.UserId;
                    this.LAST_UPDATED = BI.LAST_UPDATED;

                    NATIONALITIES = db.NATIONALITIES_BASIC_INFORMATION.Where(n => n.BI_ID == BI_ID).Select(n => new Nationality { Country = n.COUNTRY, CountryID = (int)n.COUNTRY_ID }).ToList();

                    return true;
                }
                else
                    return false;
            }
        }

        public bool GetMinor(int BI_ID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.BASIC_INFORMATIONS.Where(b => b.ID == BI_ID).Any())
                {
                    var BI = db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == BI_ID);

                    this.CIF_ENTRY_DATE = (DateTime)BI.CIF_ENTRY_DATE;
                    this.CIF_TYPE = new CifTypes { ID = (int)BI.CIF_TYPE };
                    this.PRIMARY_DOCUMENT_TYPE = new PrimaryDocumentType { ID = BI.DOCUMENT_TYPE_PRIMARY };
                    this.CNIC = BI.CNIC;
                    this.CNIC_FH = BI.CNIC_FH;
                    this.TITLE = new Title { ID = (int)BI.TITLE };
                    this.NAME = BI.NAME;
                    this.FIRST_NAME = BI.FIRST_NAME;
                    this.MIDDLE_NAME = BI.MIDDLE_NAME;
                    this.LAST_NAME = BI.LAST_NAME;
                    this.TITLE_FH = new Title { ID = (int)BI.TITLE_FH };
                    this.NAME_FH = BI.NAME_FH;
                    this.CIF_FH = BI.CIF_FH;
                    this.NAME_MOTHER = BI.NAME_MOTHER;
                    this.CNIC_MOTHER = BI.CNIC_MOTHER;
                    this.CNIC_MOTHER_OLD = BI.CNIC_MOTHER_OLD;
                    this.DATE_BIRTH = BI.DOB;
                    this.PLACE_BIRTH = BI.POB;
                    this.Country_Birth = new Country { ID = (int)BI.COB };
                    this.MARTIAL_STATUS = new MartialStatus { ID = (int)BI.MARTIAL_STATUS };
                    this.GENDER = new Gender { ID = (int)BI.GENDER };
                    this.RELIGION = new Religion { ID = (int)BI.RELIGION };
                    this.RESIDENT_TYPE = new ResidentType { ID = (int)BI.RESIDENT_TYPE };
                    this.MONTHLY_INCOME = BI.MONTHLY_INCOME;
                    this.COUNTRY_RESIDENCE = new Country { ID = BI.COUNTRY_RESIDENCE };
                    this.CUSTOMER_DEAL = new CustomerDeal { ID = (int)BI.COSTUMER_DEAL };
                    this.DOCUMENT_VERIFIED = (bool)BI.DOCUMENT_VERIFIED;
                    this.CUSTOMER_TYPE = new CifCustomerType() { ID = BI.CUSTOMER_TYPE };
                    this.STATUS = "SAVED";
                    this.UserId = (int)BI.UserId;
                    this.LAST_UPDATED = BI.LAST_UPDATED;

                    NATIONALITIES = db.NATIONALITIES_BASIC_INFORMATION.Where(n => n.BI_ID == BI_ID).Select(n => new Nationality { Country = n.COUNTRY, CountryID = (int)n.COUNTRY_ID }).ToList();

                    return true;
                }
                else
                    return false;
            }
        }

        public bool GetBusiness(int BI_ID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.BASIC_INFORMATIONS.Where(b => b.ID == BI_ID).Any())
                {
                    var BI = db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == BI_ID);

                    this.CIF_TYPE = new CifTypes { ID = (int) BI.CIF_TYPE };
                    this.NAME_OFFICE = BI.NAME_OFFICE;
                    this.NTN = BI.NTN;
                    this.SALES_TAX_NO = BI.SALES_TAX_NO;
                    this.Issuing_Agency = new IssuingAgency { ID = BI.ISSUING_AGENCY };
                    this.REG_NO = BI.REG_NO;
                    this.REG_DATE = BI.REG_DATE;
                    this.COMMENCEMENT_DATE = BI.COMMENCEMENT_DATE;
                    this.PAST_BUSS_EXP = BI.PAST_BUSS_EXP;
                    this.ACCOUNT_NATURE = new AccountNature { ID = (int)BI.ACCOUNT_NATURE };
                    this.CUSTOMER_CLASSIFICATION = new BusinessCustomerClassification { ID = (int) BI.CUSTOMER_CLASSIFICATION };
                    this.CIF_GROUP = BI.CIF_GROUP;
                    this.NATURE_BUSINESS = new NatureBusiness { ID = (int) BI.NATURE_BUSINESS };
                    this.NATURE_BUSINESS_DESCRP = BI.NATURE_BUSINESS_DESCRP;
                    this.CATERGORY_NBP = new NbpCategories { ID = BI.CATERGORY_NBP };
                    this.CATERGORY_SBP = new SbpCategories { ID =  BI.CATERGORY_SBP };
                    this.CATERGORY_BASE = new BaseCategories { ID = BI.CATERGORY_BASE };
                    this.CUSTOMER_DEAL = new CustomerDeal { ID = BI.COSTUMER_DEAL };
                    this.STATUS = "SAVED";
                    this.UserId = (int) BI.UserId;
                    this.LAST_UPDATED = BI.LAST_UPDATED;
                    this.COUNTRY_INCORPORATION = new Country() { ID = BI.COUNTRY_INCORPORATION };
                    this.BUSINESS_TYPE = new BusinessType() { ID = (int) BI.BUSINESS_TYPE };
                    this.INSTITUTION_TYPE = new InstitutionType() { ID = (int?) BI.INSTITUTION_TYPE };
                    this.SIC_CODES = new SicCode() { ID = (int)BI.SIC_CODE };
                    this.SUB_INDUSTRY = new SubIndustry() { ID = (int)BI.SUB_INDUSTRY };
                    this.DOCUMENT_VERIFIED = (bool)BI.DOCUMENT_VERIFIED;
                    this.GOV_TYPE = BI.GOV_TYPE;
                    this.ISSUING_AGENCY_OTHER = BI.ISSUING_AGENCY_OTHER;
                    return true;
                }
                else
                    return false;
            }
        }

        public bool GetShareHolderInformation(int BI_ID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.BASIC_INFORMATIONS.Where(b => b.ID == BI_ID).Any())
                {
                    var sinfo = db.BASIC_INFORMATIONS.FirstOrDefault(s => s.ID == BI_ID);
                    this.CNIC = sinfo.CNIC;
                    this.SHARE = sinfo.SHARE;
                    this.APPLICANT_STATUS = new ApplicantStatuses { ID = sinfo.APPLICANT_STATUS };

                    return true;
                }
                else
                    return false;
            }
        }

        public bool GetNextOfKin(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.BASIC_INFORMATIONS.Where(b => b.ID == BID).Any())
                {
                    var BI = db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == BID);

                    this.CIF_TYPE = new CifTypes { ID = (int)BI.CIF_TYPE };
                    this.CNIC = BI.CNIC;
                    this.TITLE = new Title { ID = (int)BI.TITLE };
                    this.LAST_NAME = BI.LAST_NAME;
                    this.FIRST_NAME = BI.FIRST_NAME;
                    this.MIDDLE_NAME = BI.MIDDLE_NAME;
                    this.RESIDENT_TYPE = new ResidentType { ID = (int)BI.RESIDENT_TYPE };
                    NATIONALITIES = db.NATIONALITIES_BASIC_INFORMATION.Where(n => n.BI_ID == BID).Select(n => new Nationality { Country = n.COUNTRY, CountryID = (int)n.COUNTRY_ID }).ToList();
                    this.LAST_UPDATED = BI.LAST_UPDATED;
                    return true;
                }
                else
                    return false;
            }
        }

        public bool CheckIndividualBi(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.BASIC_INFORMATIONS.Where(b => b.ID == BID).Any();
            }
        }



        public bool CheckShareHolder(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
              return  db.BASIC_INFORMATIONS.Where(b => b.ID == BID && b.SHARE_HOLDER_FILLED == true).Any();
            }
        }

        public bool IsCnicExists(string CNIC)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.BASIC_INFORMATIONS.Where(b => b.CNIC == CNIC && b.DOCUMENT_TYPE_PRIMARY == db.DOCUMENT_TYPES_PRIMARY.FirstOrDefault(d => d.Name == "CNIC").ID).Any();
            }
        }

        public bool IsCnicExistsPArtially(string CNIC)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.BASIC_INFORMATIONS.Where(b => b.CNIC == CNIC && b.DOCUMENT_TYPE_PRIMARY == db.DOCUMENT_TYPES_PRIMARY.FirstOrDefault(d => d.Name == "CNIC").ID && b.STATUS == Status.SAVED.ToString()).Any();
            }
        }

        public bool IsNtnExists(string NTN, int CIF_TYPE)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.BASIC_INFORMATIONS.Where(b => b.NTN == NTN && b.CIF_TYPE == CIF_TYPE).Any();
            }
        }

        public bool IsSaleTaxNoExixts(string SNo)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.BASIC_INFORMATIONS.Where(b => b.SALES_TAX_NO == SNo).Any();
            }
        }

        public bool IsRegNoExixts(string RNo)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.BASIC_INFORMATIONS.Where(b => b.REG_NO == RNo).Any();
            }
        }

      


    }
}
