using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class AccountAddressInformation
    {

        public int ID { get; set; }
        public Nullable<int> BI_ID { get; set; }
        public Country COUNTRY { get; set; }
        public City CITY { get; set; }
        public Province PROVINCE { get; set; }
        public string BUILDING_SUITE { get; set; }
        public string STREET { get; set; }
        public string FLOOR { get; set; }
        public string DISTRICT { get; set; }
        public string PO_BOX { get; set; }
        public string POSTAL_CODE { get; set; }
        public string TEL_OFFICE { get; set; }
        public string TEL_RESIDENCE { get; set; }
        public string MOBILE_NO { get; set; }
        public string FAX_NO { get; set; }
        public Nullable<int> SMS_ALERT_REQUIRED { get; set; }
        public Nullable<bool> EMAIL { get; set; }
        public Nullable<System.DateTime> UPDATED_TIME { get; set; }

        public void SaveAccountAddress()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                ADDRESS_INFORMATION a = new ADDRESS_INFORMATION();
                a.BI_ID = this.BI_ID;
                a.COUNTRY = this.COUNTRY.ID;
                a.CITY = this.CITY.ID;
                a.PROVINCE = this.PROVINCE.ID;
                a.DISTRICT = this.DISTRICT;
                a.PO_BOX = this.PO_BOX;
                a.POSTAL_CODE = this.POSTAL_CODE;
                a.BUILDING_SUITE = this.BUILDING_SUITE;
                a.FLOOR = this.FLOOR;
                a.STREET = this.STREET;

                a.TEL_OFFICE = this.TEL_OFFICE;
                a.TEL_RESIDENCE = this.TEL_RESIDENCE;
                a.MOBILE_NO = this.MOBILE_NO;
                a.FAX_NO = this.FAX_NO;
                a.SMS_ALERT_REQUIRED = this.SMS_ALERT_REQUIRED;
                a.EMAIL = this.EMAIL;
                db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;
                db.ADDRESS_INFORMATION.Add(a);
                db.SaveChanges();

                
            }
        }

        public void Update()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                ADDRESS_INFORMATION a = db.ADDRESS_INFORMATION.FirstOrDefault(b => b.BI_ID == this.BI_ID);
                a.COUNTRY = this.COUNTRY.ID;
                a.CITY = this.CITY.ID;
                a.PROVINCE = this.PROVINCE.ID;
                a.DISTRICT = this.DISTRICT;
                a.PO_BOX = this.PO_BOX;
                a.POSTAL_CODE = this.POSTAL_CODE;
                a.BUILDING_SUITE = this.BUILDING_SUITE;
                a.FLOOR = this.FLOOR;
                a.STREET = this.STREET;

                a.TEL_OFFICE = this.TEL_OFFICE;
                a.TEL_RESIDENCE = this.TEL_RESIDENCE;
                a.MOBILE_NO = this.MOBILE_NO;
                a.FAX_NO = this.FAX_NO;
                a.SMS_ALERT_REQUIRED = this.SMS_ALERT_REQUIRED;
                db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                a.EMAIL = this.EMAIL;
                db.SaveChanges();


            }
        }

        public bool GetAccountAddress(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.ADDRESS_INFORMATION.Where(b => b.BI_ID == BID).Any())
                {
                    var a = db.ADDRESS_INFORMATION.FirstOrDefault(c => c.BI_ID == BID);
                    this.BI_ID = a.BI_ID;

                    this.COUNTRY = new Country() { ID = a.COUNTRY };
                    this.CITY = new City() { ID = a.CITY };
                    this.PROVINCE = new Province() { ID =  a.PROVINCE };
                    this.DISTRICT = a.DISTRICT;
                    this.PO_BOX = a.PO_BOX;
                    this.POSTAL_CODE = a.POSTAL_CODE;
                    this.BUILDING_SUITE = a.BUILDING_SUITE;
                    this.FLOOR = a.FLOOR;
                    this.STREET = a.STREET;
                    this.TEL_OFFICE = a.TEL_OFFICE;
                    this.TEL_RESIDENCE = a.TEL_RESIDENCE;
                    this.MOBILE_NO = a.MOBILE_NO;
                    this.FAX_NO = a.FAX_NO;
                    this.SMS_ALERT_REQUIRED = (int) a.SMS_ALERT_REQUIRED;
                    this.EMAIL =(bool) a.EMAIL; 



                    return true;
                }
                else
                    return false;
            }
        }

        public bool CheckIndividualAddressInformation(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.ADDRESS_INFORMATION.Where(b => b.BI_ID == BID).Any();
            }
        }

    }
}
