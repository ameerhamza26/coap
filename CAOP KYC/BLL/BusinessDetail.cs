using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class BusinessDetail
    {

        #region model BusinessDetail
        public int BI_ID { get; set; }
        public InfoTypeBds InfoType { get; set; }
        public string INFO_DESC { get; set; }
        public List<City> BusinessCities { get; set; }
        public List<Country> BusinessCountries { get; set; }

        //New Fields
        public string MAJOR_SUPPLIERS { get; set; }
        public string MAIN_BUSINESS_ACTIVITY { get; set; }
        public string MAIN_CUSTOMERS { get; set; }

        #endregion 

        public void SaveBusinesDetail()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                BUSINESS_DETAIL BD = new BUSINESS_DETAIL();

                BD.BI_ID = this.BI_ID;
                BD.INFO_TYPE = this.InfoType.ID;
                BD.INFO_DESC = this.INFO_DESC.ToUpper();
                BD.MAIN_BUSINESS_ACTIVITY = this.MAIN_BUSINESS_ACTIVITY.ToUpper();
                BD.MAIN_CUSTOMERS = this.MAIN_CUSTOMERS.ToUpper();
                BD.MOJOR_SUPPLIERS = this.MAJOR_SUPPLIERS.ToUpper();

                db.BUSINESS_DETAIL.Add(BD);
                db.SaveChanges();

                foreach(var c in BusinessCities)
                {
                    CITIES_BUSINESS_DETAILS newCity = new CITIES_BUSINESS_DETAILS();

                    newCity.BI_ID = BD.BI_ID;
                    newCity.CITY_ID = c.ID;
                    newCity.CITY_NAME = c.Name;

                    db.CITIES_BUSINESS_DETAILS.Add(newCity);

                }

                foreach (var c in BusinessCountries)
                {
                    COUNTRIES_BUSINESS_DETAILS newCounry = new COUNTRIES_BUSINESS_DETAILS();

                    newCounry.BI_ID = BD.BI_ID;
                    newCounry.COUNTRY_ID = c.ID;
                    newCounry.COUNTRY_NAME = c.Name;

                    db.COUNTRIES_BUSINESS_DETAILS.Add(newCounry);

                }
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.SaveChanges();

            }
        }

        public void UpdateBusinesDetail()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                BUSINESS_DETAIL BD = db.BUSINESS_DETAIL.FirstOrDefault(b => b.BI_ID == this.BI_ID);
                BD.INFO_TYPE = this.InfoType.ID;
                BD.INFO_DESC = this.INFO_DESC.ToUpper();
                BD.MAIN_BUSINESS_ACTIVITY = this.MAIN_BUSINESS_ACTIVITY.ToUpper();
                BD.MAIN_CUSTOMERS = this.MAIN_CUSTOMERS.ToUpper();
                BD.MOJOR_SUPPLIERS = this.MAJOR_SUPPLIERS.ToUpper();
                db.CITIES_BUSINESS_DETAILS.RemoveRange(db.CITIES_BUSINESS_DETAILS.Where(c => c.BI_ID == this.BI_ID));
                db.COUNTRIES_BUSINESS_DETAILS.RemoveRange(db.COUNTRIES_BUSINESS_DETAILS.Where(c => c.BI_ID == this.BI_ID));
                db.SaveChanges();

                foreach (var c in BusinessCities)
                {
                    CITIES_BUSINESS_DETAILS newCity = new CITIES_BUSINESS_DETAILS();

                    newCity.BI_ID = BD.BI_ID;
                    newCity.CITY_ID = c.ID;
                    newCity.CITY_NAME = c.Name;

                    db.CITIES_BUSINESS_DETAILS.Add(newCity);

                }

                foreach (var c in BusinessCountries)
                {
                    COUNTRIES_BUSINESS_DETAILS newCounry = new COUNTRIES_BUSINESS_DETAILS();

                    newCounry.BI_ID = BD.BI_ID;
                    newCounry.COUNTRY_ID = c.ID;
                    newCounry.COUNTRY_NAME = c.Name;

                    db.COUNTRIES_BUSINESS_DETAILS.Add(newCounry);

                }
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.SaveChanges();

            }

        }


        public bool CheckBusDetailCompleted(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.BUSINESS_DETAIL.Where(b => b.BI_ID == BID).Any();
            }
        }
        public bool GetBusinessDetail(int BI_ID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.BUSINESS_DETAIL.Where(b => b.BI_ID == BI_ID).Any())
                {

                    var BD = db.BUSINESS_DETAIL.FirstOrDefault(b => b.BI_ID == BI_ID);

                    this.BI_ID = (int) BD.BI_ID;
                    this.InfoType = new InfoTypeBds { ID = (int) BD.INFO_TYPE };
                    this.INFO_DESC = BD.INFO_DESC;
                    this.MAIN_BUSINESS_ACTIVITY = BD.MAIN_BUSINESS_ACTIVITY;
                    this.MAIN_CUSTOMERS = BD.MAIN_CUSTOMERS;
                    this.MAJOR_SUPPLIERS = BD.MOJOR_SUPPLIERS;
                    this.BusinessCountries = db.COUNTRIES_BUSINESS_DETAILS.Where(c => c.BI_ID == BI_ID).Select(c => new Country { ID = (int) c.COUNTRY_ID, Name = c.COUNTRY_NAME }).ToList();
                    this.BusinessCities = db.CITIES_BUSINESS_DETAILS.Where(c => c.BI_ID == BI_ID).Select(c => new City { ID = (int)c.CITY_ID, Name = c.CITY_NAME }).ToList();

                    return true;
                }
                else
                    return false;
            }
        }
    }
}
