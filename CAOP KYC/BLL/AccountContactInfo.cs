using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class AccountContactInfo
    {

        public int ID { get; set; }
        public Nullable<int> BI_ID { get; set; }
        public string CIF_NO { get; set; }
        public string NAME { get; set; }
        public string NATIONAL_TAXNO { get; set; }
        public string SALES_TAXNO { get; set; }
        public string REGISTRATION_NO { get; set; }
        public IssuingAgency REGISTRATION_ISSUING_AGENCY { get; set; }
        public NatureBusiness NATURE_OF_BUSINESS { get; set; }
        public Country COUNTRY { get; set; }
        public City CITY { get; set; }
        public Province PROVINCE { get; set; }
        public string BUILDING { get; set; }
        public string FLOOR { get; set; }
        public string STREET { get; set; }
        public string DISTRICT { get; set; }
        public string PO_BOX { get; set; }      
        public string POSTAL_CODE { get; set; }
       
        public string TEL_OFFICE { get; set; }
        public string MOBILE_NO { get; set; }
        public string FAX_NO { get; set; }
        public Sms_Alert_Required SMS_ALERT_REQUIRED { get; set; }
        public string WEB_ADDRESS_URL { get; set; }
        public Nullable<bool> EMAIL { get; set; }
        public string GROUP_CIF_NO { get; set; }
        public string GROUP_NAME { get; set; }

        public string TEL_RESIDENCE { get; set; }

        public Nullable<bool> REQUIRED_ESTATEMENT { get; set; }

        public void Save()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                ACCOUNT_CONTACT_INFO a = new ACCOUNT_CONTACT_INFO();
                a.BI_ID = this.BI_ID;
                a.CIF_NO = this.CIF_NO;
                a.NAME = this.NAME;
                a.NATIONAL_TAXNO = this.NATIONAL_TAXNO;
                a.SALES_TAXNO = this.SALES_TAXNO;
                a.REGISTRATION_NO = this.REGISTRATION_NO;
                a.REGISTRATION_ISSUING_AGENCY = this.REGISTRATION_ISSUING_AGENCY.ID;
                a.NATURE_OF_BUSINESS = this.NATURE_OF_BUSINESS.ID;
                a.BUILDING = this.BUILDING;
                a.FLOOR = this.FLOOR;
                a.STREET = this.STREET;
                a.DISTRICT = this.DISTRICT;
                a.PO_BOX = this.PO_BOX;
                a.CITY = this.CITY.ID;
                a.POSTAL_CODE = this.POSTAL_CODE;
                a.COUNTRY = this.COUNTRY.ID;
                a.PROVINCE = this.PROVINCE.ID;
                a.TEL_RESIDENCE = this.TEL_RESIDENCE;
                a.TEL_OFFICE = this.TEL_OFFICE;
                a.MOBILE_NO = this.MOBILE_NO;
                a.FAX_NO = this.FAX_NO;
                a.SMS_ALERT_REQUIRED = this.SMS_ALERT_REQUIRED.ID;
                a.WEB_ADDRESS_URL = this.WEB_ADDRESS_URL;
                a.EMAIL = this.EMAIL;
                a.GROUP_CIF_NO = this.GROUP_CIF_NO;
                a.GROUP_NAME = this.GROUP_NAME;
                a.REQUIRED_ESTATEMENT = this.REQUIRED_ESTATEMENT;
                db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.ACCOUNT_CONTACT_INFO.Add(a);
                db.SaveChanges();
            }
        }

        public void Update()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                ACCOUNT_CONTACT_INFO a = db.ACCOUNT_CONTACT_INFO.FirstOrDefault(b => b.BI_ID == this.BI_ID);
                a.BI_ID = this.BI_ID;
                a.CIF_NO = this.CIF_NO;
                a.NAME = this.NAME;
                a.NATIONAL_TAXNO = this.NATIONAL_TAXNO;
                a.SALES_TAXNO = this.SALES_TAXNO;
                a.REGISTRATION_NO = this.REGISTRATION_NO;
                a.REGISTRATION_ISSUING_AGENCY = this.REGISTRATION_ISSUING_AGENCY.ID;
                a.NATURE_OF_BUSINESS = this.NATURE_OF_BUSINESS.ID;
                a.FLOOR = this.FLOOR;
                a.STREET = this.STREET;
                a.DISTRICT = this.DISTRICT;
                a.PO_BOX = this.PO_BOX;
                a.CITY = this.CITY.ID;
                a.PROVINCE = this.PROVINCE.ID;
                a.POSTAL_CODE = this.POSTAL_CODE;
                a.COUNTRY = this.COUNTRY.ID;
                a.TEL_RESIDENCE = this.TEL_RESIDENCE;
                a.TEL_OFFICE = this.TEL_OFFICE;
                a.MOBILE_NO = this.MOBILE_NO;
                a.FAX_NO = this.FAX_NO;
                a.SMS_ALERT_REQUIRED = this.SMS_ALERT_REQUIRED.ID;
                a.WEB_ADDRESS_URL = this.WEB_ADDRESS_URL;
                a.EMAIL = this.EMAIL;
                a.GROUP_CIF_NO = this.GROUP_CIF_NO;
                a.GROUP_NAME = this.GROUP_NAME;
                a.REQUIRED_ESTATEMENT = this.REQUIRED_ESTATEMENT;
                db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.SaveChanges();
            }
        }
        public bool Get(int id)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.ACCOUNT_CONTACT_INFO.Where(b => b.BI_ID == id).Any())
                {
                    var a = db.ACCOUNT_CONTACT_INFO.FirstOrDefault(c => c.BI_ID == id);
                    this.BI_ID = a.BI_ID;
                    this.CIF_NO = a.CIF_NO;
                    this.NAME = a.NAME;
                    this.NATIONAL_TAXNO = a.NATIONAL_TAXNO;
                    this.SALES_TAXNO = a.SALES_TAXNO;
                    this.REGISTRATION_NO = a.REGISTRATION_NO;
                    this.REGISTRATION_ISSUING_AGENCY = new IssuingAgency { ID = a.REGISTRATION_ISSUING_AGENCY };
                    this.NATURE_OF_BUSINESS = new NatureBusiness { ID = a.NATURE_OF_BUSINESS };
                    this.BUILDING = a.BUILDING;
                    this.FLOOR = a.FLOOR;
                    this.STREET = a.STREET;
                    this.DISTRICT = a.DISTRICT;
                    this.PO_BOX = a.PO_BOX;
                    this.PROVINCE = new Province { ID =  a.PROVINCE };
                    this.CITY = new City() { ID = a.CITY };
                    this.POSTAL_CODE = a.POSTAL_CODE;
                    this.COUNTRY = new Country() { ID = Convert.ToInt32(a.COUNTRY) };
                    this.TEL_RESIDENCE = a.TEL_RESIDENCE;
                    this.TEL_OFFICE = a.TEL_OFFICE;
                    this.MOBILE_NO = a.MOBILE_NO;
                    this.FAX_NO = a.FAX_NO;
                    this.SMS_ALERT_REQUIRED = new Sms_Alert_Required { ID =(int) a.SMS_ALERT_REQUIRED };
                    this.WEB_ADDRESS_URL = a.WEB_ADDRESS_URL;
                    this.EMAIL = a.EMAIL;
                    this.GROUP_CIF_NO = a.GROUP_CIF_NO;
                    this.GROUP_NAME = a.GROUP_NAME;
                    this.REQUIRED_ESTATEMENT = a.REQUIRED_ESTATEMENT;

                    return true;
                }
                else
                    return false;
            }
        }

        public bool CheckAccountContactInfo(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.ACCOUNT_CONTACT_INFO.Where(b => b.BI_ID == BID).Any();
            }
        }
    }
}
