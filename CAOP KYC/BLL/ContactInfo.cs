using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class ContactInfo
   {

       #region ContactInfo Model
       public int ID { get; set; }
        public int BI_ID { get; set; }
        public Province PROVINCE  { get; set; }
        public Province PROVINCE_PRE { get; set; }
        public City CITY_PERMANENT { get; set; }
        public City CITY_PRESENT { get; set; }
        public string POSTAL_CODE_PRESENT { get; set; }
        public Country COUNTRY_PRESENT { get; set; }
        public string OFFICE_CONTACT { get; set; }
        public string RESIDENCE_CONTACT { get; set; }
        public string MOBILE_NO { get; set; }
        public string FAX_NO { get; set; }
        public string EMAIL { get; set; }
        public string WEB { get; set; }

       // New fields 2016-03-03

       public Country COUNTRY_CODE { get; set; }
        public string STREET { get; set; }
        public string BIULDING_SUITE { get; set; }
        public string FLOOR { get; set; }
        public string DISTRICT { get; set; }
        public string POST_OFFICE { get; set; }
        public string POSTAL_CODE { get; set; }

        public Country COUNTRY_CODE_PRE { get; set; }
        public string STREET_PRE { get; set; }
        public string BIULDING_SUITE_PRE { get; set; }
        public string FLOOR_PRE { get; set; }
        public string DISTRICT_PRE { get; set; }
        public string POST_OFFICE_PRE { get; set; }
        public string POSTAL_CODE_PRE { get; set; }

       //
        public string CP_NAME { get; set; }
        public string CP_DESIGNATION { get; set; }
        public string CP_CNIC { get; set; }
        public string CP_CNIC_EXPIRY { get; set; }
        public string CP_NTN { get; set; }

      
       #endregion 


        public void SaveContactInfo()
        {
            using(CAOPDbContext db = new  CAOPDbContext())
            {
                CONTACT_INFOS newInfo = new CONTACT_INFOS();

                newInfo.BI_ID = this.BI_ID;
                newInfo.COUNTRY_CODE = this.COUNTRY_CODE.ID;
                newInfo.PROVINCE = this.PROVINCE.ID;
                newInfo.PROVINCE_PRE = this.PROVINCE_PRE.ID;
                newInfo.STREET = this.STREET.ToUpper();
                newInfo.BIULDING_SUITE = this.BIULDING_SUITE.ToUpper();
                newInfo.FLOOR = this.FLOOR.ToUpper();
                newInfo.STREET = this.STREET.ToUpper();
                newInfo.DISTRICT = this.DISTRICT.ToUpper();
                newInfo.POST_OFFICE = this.POST_OFFICE.ToUpper();
                newInfo.POSTAL_CODE = this.POSTAL_CODE.ToUpper();
                newInfo.COUNTRY_CODE_PRE = this.COUNTRY_CODE_PRE.ID;
                newInfo.STREET_PRE = this.STREET_PRE.ToUpper();
                newInfo.BIULDING_SUITE_PRE = this.BIULDING_SUITE_PRE.ToUpper();
                newInfo.FLOOR_PRE = this.FLOOR_PRE.ToUpper();
                newInfo.STREET_PRE = this.STREET_PRE.ToUpper();
                newInfo.DISTRICT_PRE = this.DISTRICT_PRE.ToUpper();
                newInfo.POST_OFFICE_PRE = this.POST_OFFICE_PRE.ToUpper();
                newInfo.POSTAL_CODE_PRE = this.POSTAL_CODE_PRE.ToUpper();            
                newInfo.CITY_PERMANENT = this.CITY_PERMANENT.ID;         
                newInfo.CITY_PRESENT = this.CITY_PRESENT.ID;        
                newInfo.OFFICE_CONTACT = this.OFFICE_CONTACT.ToUpper();
                newInfo.RESIDENCE_CONTACT = this.RESIDENCE_CONTACT.ToUpper();
                newInfo.MOBILE_NO = this.MOBILE_NO;
                newInfo.FAX_NO = this.FAX_NO.ToUpper();
                newInfo.EMAIL = this.EMAIL;

                db.BASIC_INFORMATIONS.FirstOrDefault(b=> b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.CONTACT_INFOS.Add(newInfo);
                db.SaveChanges();

            }
        }

        public void UpdateContactInfo()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                CONTACT_INFOS newInfo = db.CONTACT_INFOS.FirstOrDefault(c => c.BI_ID == this.BI_ID);

                newInfo.COUNTRY_CODE = this.COUNTRY_CODE.ID;
                newInfo.PROVINCE = this.PROVINCE.ID;
                newInfo.PROVINCE_PRE = this.PROVINCE_PRE.ID;
                newInfo.STREET = this.STREET.ToUpper();
                newInfo.BIULDING_SUITE = this.BIULDING_SUITE.ToUpper();
                newInfo.FLOOR = this.FLOOR.ToUpper();
                newInfo.STREET = this.STREET.ToUpper();
                newInfo.DISTRICT = this.DISTRICT.ToUpper();
                newInfo.POST_OFFICE = this.POST_OFFICE.ToUpper();
                newInfo.POSTAL_CODE = this.POSTAL_CODE.ToUpper();
                newInfo.COUNTRY_CODE_PRE = this.COUNTRY_CODE_PRE.ID;
                newInfo.STREET_PRE = this.STREET_PRE.ToUpper();
                newInfo.BIULDING_SUITE_PRE = this.BIULDING_SUITE_PRE.ToUpper();
                newInfo.FLOOR_PRE = this.FLOOR_PRE.ToUpper();
                newInfo.STREET_PRE = this.STREET_PRE.ToUpper();
                newInfo.DISTRICT_PRE = this.DISTRICT_PRE.ToUpper();
                newInfo.POST_OFFICE_PRE = this.POST_OFFICE_PRE.ToUpper();
                newInfo.POSTAL_CODE_PRE = this.POSTAL_CODE_PRE.ToUpper();           
                newInfo.CITY_PERMANENT = this.CITY_PERMANENT.ID;       
                newInfo.CITY_PRESENT = this.CITY_PRESENT.ID;         
                newInfo.OFFICE_CONTACT = this.OFFICE_CONTACT;
                newInfo.RESIDENCE_CONTACT = this.RESIDENCE_CONTACT;
                newInfo.MOBILE_NO = this.MOBILE_NO;
                newInfo.FAX_NO = this.FAX_NO;
                newInfo.EMAIL = this.EMAIL;
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.SaveChanges();

            }
        }
        // Process Starts again
        public void UpdateContactInfoNew()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                CONTACT_INFOS newInfo = db.CONTACT_INFOS.FirstOrDefault(c => c.BI_ID == this.BI_ID);

                newInfo.COUNTRY_CODE = this.COUNTRY_CODE.ID;
                newInfo.PROVINCE = this.PROVINCE.ID;
                newInfo.PROVINCE_PRE = this.PROVINCE_PRE.ID;
                newInfo.STREET = this.STREET.ToUpper();
                newInfo.BIULDING_SUITE = this.BIULDING_SUITE.ToUpper();
                newInfo.FLOOR = this.FLOOR.ToUpper();
                newInfo.STREET = this.STREET.ToUpper();
                newInfo.DISTRICT = this.DISTRICT.ToUpper();
                newInfo.POST_OFFICE = this.POST_OFFICE.ToUpper();
                newInfo.POSTAL_CODE = this.POSTAL_CODE.ToUpper();
                newInfo.COUNTRY_CODE_PRE = this.COUNTRY_CODE_PRE.ID;
                newInfo.STREET_PRE = this.STREET_PRE.ToUpper();
                newInfo.BIULDING_SUITE_PRE = this.BIULDING_SUITE_PRE.ToUpper();
                newInfo.FLOOR_PRE = this.FLOOR_PRE.ToUpper();
                newInfo.STREET_PRE = this.STREET_PRE.ToUpper();
                newInfo.DISTRICT_PRE = this.DISTRICT_PRE.ToUpper();
                newInfo.POST_OFFICE_PRE = this.POST_OFFICE_PRE.ToUpper();
                newInfo.POSTAL_CODE_PRE = this.POSTAL_CODE_PRE.ToUpper();
                newInfo.CITY_PERMANENT = this.CITY_PERMANENT.ID;
                newInfo.CITY_PRESENT = this.CITY_PRESENT.ID;
                newInfo.OFFICE_CONTACT = this.OFFICE_CONTACT;
                newInfo.RESIDENCE_CONTACT = this.RESIDENCE_CONTACT;
                newInfo.MOBILE_NO = this.MOBILE_NO;
                newInfo.FAX_NO = this.FAX_NO;
                newInfo.EMAIL = this.EMAIL;
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).STATUS=Status.UPDATED_BY_BRANCH_OPERATOR.ToString();

                db.SaveChanges();

            }
        }

        public void SaveContactInfoGovernment()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                CONTACT_INFOS newInfo = new CONTACT_INFOS();
                newInfo.BI_ID = this.BI_ID;
                newInfo.COUNTRY_CODE = this.COUNTRY_CODE.ID;
                newInfo.CITY_PERMANENT = this.CITY_PERMANENT.ID;
                newInfo.PROVINCE = this.PROVINCE.ID;
                newInfo.STREET = this.STREET.ToUpper();
                newInfo.BIULDING_SUITE = this.BIULDING_SUITE.ToUpper();
                newInfo.FLOOR = this.FLOOR.ToUpper();
                newInfo.STREET = this.STREET.ToUpper();
                newInfo.DISTRICT = this.DISTRICT.ToUpper();
                newInfo.POST_OFFICE = this.POST_OFFICE.ToUpper();
                newInfo.POSTAL_CODE = this.POSTAL_CODE.ToUpper();
                newInfo.OFFICE_CONTACT = this.OFFICE_CONTACT.ToUpper();
                newInfo.MOBILE_NO = this.MOBILE_NO;
                newInfo.FAX_NO = this.FAX_NO.ToUpper();
                
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.CONTACT_INFOS.Add(newInfo);
                db.SaveChanges();
            }
           
        }

        public void UpdateContactInfoGovernment()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                CONTACT_INFOS newInfo = db.CONTACT_INFOS.FirstOrDefault(c => c.BI_ID == this.BI_ID);
                newInfo.BI_ID = this.BI_ID;
                newInfo.COUNTRY_CODE = this.COUNTRY_CODE.ID;
                newInfo.CITY_PERMANENT = this.CITY_PERMANENT.ID;
                newInfo.PROVINCE = this.PROVINCE.ID;
                newInfo.STREET = this.STREET.ToUpper();
                newInfo.BIULDING_SUITE = this.BIULDING_SUITE.ToUpper();
                newInfo.FLOOR = this.FLOOR.ToUpper();
                newInfo.STREET = this.STREET.ToUpper();
                newInfo.DISTRICT = this.DISTRICT.ToUpper();
                newInfo.POST_OFFICE = this.POST_OFFICE.ToUpper();
                newInfo.POSTAL_CODE = this.POSTAL_CODE.ToUpper();
                newInfo.OFFICE_CONTACT = this.OFFICE_CONTACT.ToUpper();
                newInfo.MOBILE_NO = this.MOBILE_NO;
                newInfo.FAX_NO = this.FAX_NO.ToUpper();
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;
                db.SaveChanges();
            }
        }

        public bool GetContactInfoGovernment(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.CONTACT_INFOS.Where(c => c.BI_ID == BID).Any())
                {
                    var Cinfo = db.CONTACT_INFOS.FirstOrDefault(c => c.BI_ID == BID);

                    this.COUNTRY_CODE = new Country { ID = (int?)Cinfo.COUNTRY_CODE };
                    this.CITY_PERMANENT = new City { ID = Cinfo.CITY_PERMANENT };
                    this.PROVINCE = new Province { ID =  Cinfo.PROVINCE };
                    this.STREET = Cinfo.STREET;
                    this.BIULDING_SUITE = Cinfo.BIULDING_SUITE;
                    this.FLOOR = Cinfo.FLOOR;
                    this.DISTRICT = Cinfo.DISTRICT;
                    this.POST_OFFICE = Cinfo.POST_OFFICE;
                    this.POSTAL_CODE = Cinfo.POSTAL_CODE;
                    this.OFFICE_CONTACT = Cinfo.OFFICE_CONTACT;
                    this.MOBILE_NO = Cinfo.MOBILE_NO;
                    this.FAX_NO = Cinfo.FAX_NO;
                    return true;
                }
                else
                    return false;
            }
        }

        public void SaveContactInfoNextOfKin()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {

                CONTACT_INFOS newInfo = new CONTACT_INFOS();
                newInfo.BI_ID = this.BI_ID;
                newInfo.COUNTRY_CODE = this.COUNTRY_CODE.ID;
                newInfo.CITY_PERMANENT = this.CITY_PERMANENT.ID;
                newInfo.BIULDING_SUITE = this.BIULDING_SUITE;
                newInfo.FLOOR = this.FLOOR;
                newInfo.STREET = this.STREET;
                newInfo.DISTRICT = this.DISTRICT;
                newInfo.POST_OFFICE = this.POST_OFFICE;
                newInfo.POSTAL_CODE = this.POSTAL_CODE;
                newInfo.OFFICE_CONTACT = this.OFFICE_CONTACT;
                newInfo.RESIDENCE_CONTACT = this.RESIDENCE_CONTACT;
                newInfo.MOBILE_NO = this.MOBILE_NO;
                newInfo.FAX_NO = this.FAX_NO;
                newInfo.EMAIL = this.EMAIL;
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.CONTACT_INFOS.Add(newInfo);
                db.SaveChanges();
            }
        }

        public bool GetContactNextOFKin(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.CONTACT_INFOS.Where(c => c.BI_ID == BID).Any())
                {

                    var Cinfo = db.CONTACT_INFOS.FirstOrDefault(c => c.BI_ID == BID);

                  
                    this.CITY_PERMANENT = new City { ID = Cinfo.CITY_PERMANENT };
                    this.COUNTRY_CODE = new Country { ID = Cinfo.COUNTRY_CODE };
                    this.BIULDING_SUITE = Cinfo.BIULDING_SUITE;
                    this.FLOOR = Cinfo.FLOOR;
                    this.STREET = Cinfo.STREET;
                    this.DISTRICT = Cinfo.DISTRICT;
                    this.POST_OFFICE = Cinfo.POST_OFFICE;
                    this.POSTAL_CODE = Cinfo.POSTAL_CODE;
                    this.OFFICE_CONTACT = Cinfo.OFFICE_CONTACT;
                    this.RESIDENCE_CONTACT = Cinfo.RESIDENCE_CONTACT;
                    this.MOBILE_NO = Cinfo.MOBILE_NO;
                    this.FAX_NO = Cinfo.FAX_NO;
                    this.EMAIL = Cinfo.EMAIL;


                    return true;
                }
                else
                    return false;
            }
       }

        public void UpdateContactInfoNextOfKin()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                CONTACT_INFOS newInfo = db.CONTACT_INFOS.FirstOrDefault(c => c.BI_ID == this.BI_ID);

                newInfo.COUNTRY_CODE = this.COUNTRY_CODE.ID;
                newInfo.CITY_PERMANENT = this.CITY_PERMANENT.ID;
                newInfo.BIULDING_SUITE = this.BIULDING_SUITE;
                newInfo.FLOOR = this.FLOOR;
                newInfo.STREET = this.STREET;
                newInfo.DISTRICT = this.DISTRICT;
                newInfo.POST_OFFICE = this.POST_OFFICE;
                newInfo.POSTAL_CODE = this.POSTAL_CODE;
                newInfo.OFFICE_CONTACT = this.OFFICE_CONTACT;
                newInfo.RESIDENCE_CONTACT = this.RESIDENCE_CONTACT;
                newInfo.MOBILE_NO = this.MOBILE_NO;
                newInfo.FAX_NO = this.FAX_NO;
                newInfo.EMAIL = this.EMAIL;
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.SaveChanges();
            }
        }

        public void SaveContactInfoBusiness()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                CONTACT_INFOS newInfo = new CONTACT_INFOS();

                newInfo.BI_ID = this.BI_ID;           
                newInfo.POST_OFFICE = this.POST_OFFICE.ToUpper();
                newInfo.CITY_PERMANENT = this.CITY_PERMANENT.ID;
                newInfo.POSTAL_CODE = this.POSTAL_CODE.ToUpper();
                newInfo.COUNTRY_CODE = this.COUNTRY_CODE.ID;
                newInfo.BIULDING_SUITE = this.BIULDING_SUITE.ToUpper();
                newInfo.STREET = this.STREET.ToUpper();
                newInfo.DISTRICT = this.DISTRICT.ToUpper();
                newInfo.PROVINCE = this.PROVINCE.ID;
                newInfo.FLOOR = this.FLOOR.ToUpper();
                newInfo.OFFICE_CONTACT = this.OFFICE_CONTACT.ToUpper();           
                newInfo.MOBILE_NO = this.MOBILE_NO.ToUpper();
                newInfo.FAX_NO = this.FAX_NO.ToUpper();
                newInfo.EMAIL = this.EMAIL;
                newInfo.WEB = this.WEB.ToUpper();
                newInfo.CP_NAME = this.CP_NAME.ToUpper();
                newInfo.CP_DESIGNATION = this.CP_DESIGNATION.ToUpper();
                newInfo.CP_CNIC = this.CP_CNIC;
                newInfo.CP_CNIC_EXPIRY = this.CP_CNIC_EXPIRY;
                newInfo.CP_NTN = this.CP_NTN;
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.CONTACT_INFOS.Add(newInfo);
                db.SaveChanges();

            }
        }

        public void UpdateContactInfoBusiness()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                CONTACT_INFOS newInfo = db.CONTACT_INFOS.FirstOrDefault(c => c.BI_ID == this.BI_ID);          
                newInfo.POST_OFFICE = this.POST_OFFICE.ToUpper();
                newInfo.CITY_PERMANENT = this.CITY_PERMANENT.ID;
                newInfo.POSTAL_CODE = this.POSTAL_CODE.ToUpper();
                newInfo.COUNTRY_CODE = this.COUNTRY_CODE.ID;
                newInfo.BIULDING_SUITE = this.BIULDING_SUITE.ToUpper();
                newInfo.STREET = this.STREET.ToUpper();
                newInfo.DISTRICT = this.DISTRICT.ToUpper();
                newInfo.PROVINCE = this.PROVINCE.ID;
                newInfo.FLOOR = this.FLOOR.ToUpper();
                newInfo.OFFICE_CONTACT = this.OFFICE_CONTACT.ToUpper();
              //  newInfo.RESIDENCE_CONTACT = this.RESIDENCE_CONTACT.ToUpper();
                newInfo.MOBILE_NO = this.MOBILE_NO;
                newInfo.FAX_NO = this.FAX_NO.ToUpper();
                newInfo.EMAIL = this.EMAIL;
                newInfo.WEB = this.WEB.ToUpper();
                newInfo.CP_NAME = this.CP_NAME.ToUpper();
                newInfo.CP_DESIGNATION = this.CP_DESIGNATION.ToUpper();
                newInfo.CP_CNIC = this.CP_CNIC;
                newInfo.CP_CNIC_EXPIRY = this.CP_CNIC_EXPIRY;
                newInfo.CP_NTN = this.CP_NTN;
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.SaveChanges();

            }
        }
        //Process starts again
        public void UpdateContactInfoBusinessNew()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                CONTACT_INFOS newInfo = db.CONTACT_INFOS.FirstOrDefault(c => c.BI_ID == this.BI_ID);
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).STATUS = Status.UPDATED_BY_BRANCH_OPERATOR.ToString();
                newInfo.POST_OFFICE = this.POST_OFFICE.ToUpper();
                newInfo.CITY_PERMANENT = this.CITY_PERMANENT.ID;
                newInfo.POSTAL_CODE = this.POSTAL_CODE.ToUpper();
                newInfo.COUNTRY_CODE = this.COUNTRY_CODE.ID;
                newInfo.BIULDING_SUITE = this.BIULDING_SUITE.ToUpper();
                newInfo.STREET = this.STREET.ToUpper();
                newInfo.DISTRICT = this.DISTRICT.ToUpper();
                newInfo.PROVINCE = this.PROVINCE.ID;
                newInfo.FLOOR = this.FLOOR.ToUpper();
                newInfo.OFFICE_CONTACT = this.OFFICE_CONTACT.ToUpper();
                //  newInfo.RESIDENCE_CONTACT = this.RESIDENCE_CONTACT.ToUpper();
                newInfo.MOBILE_NO = this.MOBILE_NO;
                newInfo.FAX_NO = this.FAX_NO.ToUpper();
                newInfo.EMAIL = this.EMAIL;
                newInfo.WEB = this.WEB.ToUpper();
                newInfo.CP_NAME = this.CP_NAME.ToUpper();
                newInfo.CP_DESIGNATION = this.CP_DESIGNATION.ToUpper();
                newInfo.CP_CNIC = this.CP_CNIC;
                newInfo.CP_CNIC_EXPIRY = this.CP_CNIC_EXPIRY;
                newInfo.CP_NTN = this.CP_NTN;
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.SaveChanges();

            }
        }
        public bool GetIndividualContactInfo(int BI_ID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.CONTACT_INFOS.Where(c => c.BI_ID == BI_ID).Any())
                {

                    var Cinfo = db.CONTACT_INFOS.FirstOrDefault(c => c.BI_ID == BI_ID);

                    this.BI_ID = (int) Cinfo.BI_ID;
                    this.COUNTRY_CODE = new Country { ID = (int?)Cinfo.COUNTRY_CODE };
                    this.PROVINCE = new Province { ID =  Cinfo.PROVINCE };
                    this.PROVINCE_PRE = new Province { ID = Cinfo.PROVINCE_PRE };
                    this.STREET = Cinfo.STREET;
                    this.BIULDING_SUITE = Cinfo.BIULDING_SUITE;
                    this.FLOOR = Cinfo.FLOOR;
                    this.DISTRICT = Cinfo.DISTRICT;
                    this.POST_OFFICE = Cinfo.POST_OFFICE;
                    this.POSTAL_CODE = Cinfo.POSTAL_CODE;
                    this.COUNTRY_CODE_PRE = new Country { ID = (int?)Cinfo.COUNTRY_CODE_PRE };
                    this.STREET_PRE = Cinfo.STREET_PRE;
                    this.BIULDING_SUITE_PRE = Cinfo.BIULDING_SUITE_PRE;
                    this.FLOOR_PRE = Cinfo.FLOOR_PRE;
                    this.DISTRICT_PRE = Cinfo.DISTRICT_PRE;
                    this.POST_OFFICE_PRE = Cinfo.POST_OFFICE_PRE;
                    this.POSTAL_CODE_PRE = Cinfo.POSTAL_CODE_PRE;                  
                    this.CITY_PERMANENT = new City { ID =  Cinfo.CITY_PERMANENT };                
                    this.CITY_PRESENT = new City { ID = Cinfo.CITY_PRESENT };             
                    this.OFFICE_CONTACT = Cinfo.OFFICE_CONTACT;
                    this.RESIDENCE_CONTACT = Cinfo.RESIDENCE_CONTACT;
                    this.MOBILE_NO = Cinfo.MOBILE_NO;
                    this.FAX_NO = Cinfo.FAX_NO;
                    this.EMAIL = Cinfo.EMAIL;


                    return true;
                }
                else
                    return false;
            }
        }

        public bool GetBusinessContactInfo(int BI_ID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.CONTACT_INFOS.Where(c => c.BI_ID == BI_ID).Any())
                {

                    var Cinfo = db.CONTACT_INFOS.FirstOrDefault(c => c.BI_ID == BI_ID);

                    this.BI_ID = (int)Cinfo.BI_ID;
                 
                    this.POST_OFFICE = Cinfo.POST_OFFICE;
                    this.CITY_PERMANENT = new City { ID = Cinfo.CITY_PERMANENT };
                    this.POSTAL_CODE = Cinfo.POSTAL_CODE;
                    this.COUNTRY_CODE = new Country { ID = (int)Cinfo.COUNTRY_CODE };
                    this.BIULDING_SUITE = Cinfo.BIULDING_SUITE;
                    this.STREET = Cinfo.STREET;
                    this.DISTRICT = Cinfo.DISTRICT;
                    this.PROVINCE = new Province() { ID = Cinfo.PROVINCE };
                    this.FLOOR = Cinfo.FLOOR;
                    this.OFFICE_CONTACT = Cinfo.OFFICE_CONTACT;
                    this.RESIDENCE_CONTACT = Cinfo.RESIDENCE_CONTACT;
                    this.MOBILE_NO = Cinfo.MOBILE_NO;
                    this.FAX_NO = Cinfo.FAX_NO;
                    this.EMAIL = Cinfo.EMAIL;
                    this.WEB = Cinfo.WEB;
                    this.CP_NAME = Cinfo.CP_NAME;
                    this.CP_DESIGNATION = Cinfo.CP_DESIGNATION;
                    this.CP_CNIC = Cinfo.CP_CNIC;
                    this.CP_CNIC_EXPIRY = Cinfo.CP_CNIC_EXPIRY;
                    this.CP_NTN = Cinfo.CP_NTN;


                    return true;
                }
                else
                    return false;
            }
        }


        public bool CheckIndividualContactInfo(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.CONTACT_INFOS.Where(b => b.BI_ID == BID).Any();
            }
        }
      
   }
}
