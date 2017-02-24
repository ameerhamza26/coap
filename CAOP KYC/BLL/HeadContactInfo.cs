using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public  class HeadContactInfo
    {
        #region HeadContactInfo model

        public int ID { get; set; }
        public int BI_ID { get; set; }
        public string ADDRESS { get; set; }
        public District DISTRICT { get; set; }
        public string POBOX { get; set; }
        public City   CITY { get; set; }
        public string POSTAL_CODE { get; set; }
        public Country COUNTRY { get; set; }
        public string PHONE_OFFICE { get; set; }
        public string MOBILE_NO { get; set; }
        public string FAX_NO { get; set; }
        public string EMAIL { get; set; }
        public string WEB { get; set; }

        /// New Fields
        public string STREET { get; set; }
        public string BIULDING_SUITE { get; set; }
        public string FLOOR { get; set; }
        public string DISTRICT_HEAD { get; set; }

        public Country COUNTRY_CODE_REG { get; set; }
        public City CITY_REG { get; set; }
        public string STREET_REG { get; set; }
        public string BIULDING_SUITE_REG { get; set; }
        public string FLOOR_REG { get; set; }
        public string DISTRICT_REG { get; set; }
        public string POST_OFFICE_REG { get; set; }
        public string POSTAL_CODE_REG { get; set; }
        public Province PROVINCE { get; set; }
        public Province PROVINCE_REG { get; set; }

      
        #endregion 

        public void SaveHeadContactInfo()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                HEAD_CONTACT_INFOS HInfo = new HEAD_CONTACT_INFOS();

                HInfo.BI_ID = this.BI_ID;
             //   HInfo.ADDRESS = this.ADDRESS;
             //   HInfo.DISTRICT = this.DISTRICT.ID;
                HInfo.POBOX = this.POBOX.ToUpper();
                HInfo.CITY = this.CITY.ID;
                HInfo.POSTAL_CODE = this.POSTAL_CODE.ToUpper();
                HInfo.COUNTRY = this.COUNTRY.ID;
                HInfo.PHONE_OFFICE = this.PHONE_OFFICE.ToUpper();
                HInfo.MOBILE_NO = this.MOBILE_NO;
                HInfo.FAX_NO = this.FAX_NO.ToUpper();
                HInfo.EMAIL = this.EMAIL.ToUpper();
              //  HInfo.WEB = Capital(this.WEB);

                HInfo.STREET = this.STREET.ToUpper();
                HInfo.BIULDING_SUITE = this.BIULDING_SUITE.ToUpper();
                HInfo.FLOOR = this.FLOOR.ToUpper();
                HInfo.DISTRICT_HEAD = this.DISTRICT_HEAD.ToUpper();
              //  HInfo.STREET = this.STREET.ToUpper();

                HInfo.COUNTRY_CODE_REG = this.COUNTRY_CODE_REG.ID;
                HInfo.CITY_REG = this.CITY_REG.ID;
                HInfo.STREET_REG = this.STREET_REG.ToUpper();
                HInfo.BIULDING_SUITE_REG = this.BIULDING_SUITE_REG.ToUpper();
                HInfo.FLOOR_REG = this.FLOOR_REG.ToUpper();
                HInfo.DISTRICT_REG = this.DISTRICT_REG.ToUpper();
                HInfo.POBOX_REG = this.POST_OFFICE_REG.ToUpper();
                HInfo.POSTAL_REG_CODE = this.POSTAL_CODE_REG.ToUpper();
                HInfo.PROVINCE = this.PROVINCE.ID;
                HInfo.PROVINCE_REG = this.PROVINCE.ID;


                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.HEAD_CONTACT_INFOS.Add(HInfo);
                db.SaveChanges();



            }
        }

        public void UpdateHeadContactInfo()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                HEAD_CONTACT_INFOS HInfo = db.HEAD_CONTACT_INFOS.FirstOrDefault(h => h.BI_ID == this.BI_ID);
                HInfo.BI_ID = this.BI_ID;
                //   HInfo.ADDRESS = this.ADDRESS;
                //   HInfo.DISTRICT = this.DISTRICT.ID;
                HInfo.COUNTRY = this.COUNTRY.ID;
                HInfo.PROVINCE = this.PROVINCE.ID;
                HInfo.CITY = this.CITY.ID;
                HInfo.STREET = this.STREET.ToUpper();
                HInfo.BIULDING_SUITE = this.BIULDING_SUITE.ToUpper();
                HInfo.FLOOR = this.FLOOR.ToUpper();
                HInfo.DISTRICT_HEAD = this.DISTRICT_HEAD.ToUpper();
                HInfo.POBOX = this.POBOX.ToUpper();              
                HInfo.POSTAL_CODE = this.POSTAL_CODE.ToUpper();
               
                HInfo.PHONE_OFFICE = this.PHONE_OFFICE.ToUpper();
                HInfo.MOBILE_NO = this.MOBILE_NO;
                HInfo.FAX_NO = this.FAX_NO.ToUpper();
                HInfo.EMAIL = this.EMAIL.ToUpper();
                //  HInfo.WEB = Capital(this.WEB);

              
                //  HInfo.STREET = this.STREET.ToUpper();

                HInfo.COUNTRY_CODE_REG = this.COUNTRY_CODE_REG.ID;
                HInfo.CITY_REG = this.CITY_REG.ID;
                HInfo.PROVINCE_REG = this.PROVINCE.ID; ;
                HInfo.STREET_REG = this.STREET_REG.ToUpper();
                HInfo.BIULDING_SUITE_REG = this.BIULDING_SUITE_REG.ToUpper();
                HInfo.FLOOR_REG = this.FLOOR_REG.ToUpper();
                HInfo.DISTRICT_REG = this.DISTRICT_REG.ToUpper();
                HInfo.POBOX_REG = this.POST_OFFICE_REG.ToUpper();
                HInfo.POSTAL_REG_CODE = this.POSTAL_CODE_REG.ToUpper();
              
                
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.SaveChanges();
            }
        }
        //Process starts again
        public void UpdateHeadContactInfoNew()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                HEAD_CONTACT_INFOS HInfo = db.HEAD_CONTACT_INFOS.FirstOrDefault(h => h.BI_ID == this.BI_ID);
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).STATUS = Status.UPDATED_BY_BRANCH_OPERATOR.ToString();
                HInfo.BI_ID = this.BI_ID;
                //   HInfo.ADDRESS = this.ADDRESS;
                //   HInfo.DISTRICT = this.DISTRICT.ID;
                HInfo.COUNTRY = this.COUNTRY.ID;
                HInfo.PROVINCE = this.PROVINCE.ID;
                HInfo.CITY = this.CITY.ID;
                HInfo.STREET = this.STREET.ToUpper();
                HInfo.BIULDING_SUITE = this.BIULDING_SUITE.ToUpper();
                HInfo.FLOOR = this.FLOOR.ToUpper();
                HInfo.DISTRICT_HEAD = this.DISTRICT_HEAD.ToUpper();
                HInfo.POBOX = this.POBOX.ToUpper();
                HInfo.POSTAL_CODE = this.POSTAL_CODE.ToUpper();

                HInfo.PHONE_OFFICE = this.PHONE_OFFICE.ToUpper();
                HInfo.MOBILE_NO = this.MOBILE_NO;
                HInfo.FAX_NO = this.FAX_NO.ToUpper();
                HInfo.EMAIL = this.EMAIL.ToUpper();
                //  HInfo.WEB = Capital(this.WEB);


                //  HInfo.STREET = this.STREET.ToUpper();

                HInfo.COUNTRY_CODE_REG = this.COUNTRY_CODE_REG.ID;
                HInfo.CITY_REG = this.CITY_REG.ID;
                HInfo.PROVINCE_REG = this.PROVINCE.ID; ;
                HInfo.STREET_REG = this.STREET_REG.ToUpper();
                HInfo.BIULDING_SUITE_REG = this.BIULDING_SUITE_REG.ToUpper();
                HInfo.FLOOR_REG = this.FLOOR_REG.ToUpper();
                HInfo.DISTRICT_REG = this.DISTRICT_REG.ToUpper();
                HInfo.POBOX_REG = this.POST_OFFICE_REG.ToUpper();
                HInfo.POSTAL_REG_CODE = this.POSTAL_CODE_REG.ToUpper();


                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.SaveChanges();
            }
        }

        public bool CheckHeadContactCompleted(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.HEAD_CONTACT_INFOS.Where(b => b.BI_ID == BID).Any();
            }
        }

        public bool GetHeadContactInfo(int BI_ID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.HEAD_CONTACT_INFOS.Where(h => h.BI_ID == BI_ID).Any())
                {
                    var HInfo = db.HEAD_CONTACT_INFOS.FirstOrDefault(h => h.BI_ID == BI_ID);

                    this.BI_ID = (int) HInfo.BI_ID;
                    this.ADDRESS = HInfo.ADDRESS;
                    this.DISTRICT = new District { ID =  HInfo.DISTRICT };
                    this.POBOX = HInfo.POBOX;
                    this.CITY = new City { ID = HInfo.CITY };
                    this.POSTAL_CODE = HInfo.POSTAL_CODE;
                    this.COUNTRY = new Country { ID =  HInfo.COUNTRY };
                    this.PHONE_OFFICE = HInfo.PHONE_OFFICE;
                    this.MOBILE_NO = HInfo.MOBILE_NO;
                    this.FAX_NO = HInfo.FAX_NO;
                    this.EMAIL = HInfo.EMAIL;
                  //  this.WEB = HInfo.WEB;

                    this.STREET = HInfo.STREET;
                    this.BIULDING_SUITE = HInfo.BIULDING_SUITE;
                    this.FLOOR = HInfo.FLOOR;
                    this.DISTRICT_HEAD = HInfo.DISTRICT_HEAD;
                    this.STREET = HInfo.STREET;

                    this.COUNTRY_CODE_REG = new Country() {ID = Convert.ToInt32(HInfo.COUNTRY_CODE_REG)} ;
                    this.CITY_REG = new City { ID = HInfo.CITY_REG };
                    this.STREET_REG = HInfo.STREET_REG;
                    this.BIULDING_SUITE_REG = HInfo.BIULDING_SUITE_REG;
                    this.FLOOR_REG = HInfo.FLOOR_REG;
                    this.DISTRICT_REG = HInfo.DISTRICT_REG;
                    this.POST_OFFICE_REG = HInfo.POBOX_REG;
                    this.POSTAL_CODE_REG = HInfo.POSTAL_REG_CODE;

                    this.PROVINCE = new Province() { ID = Convert.ToInt32(HInfo.PROVINCE) };
                    this.PROVINCE_REG = new Province() { ID = Convert.ToInt32(HInfo.PROVINCE_REG) };
                    return true;
                }
                else
                    return false;
            }
        }
    }
}
