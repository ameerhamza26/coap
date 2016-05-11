using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class AccountNexofKinInfo
    {

        public int ID { get; set; }
        public Nullable<int> BI_ID { get; set; }
        public string NEXT_OF_KIN { get; set; }
        public string NEXT_OF_KIN_NAME { get; set; }
        public string NEXT_OF_KIN_CNIC { get; set; }
        public Relationship RELATIONSHIP { get; set; }
        public string RELATIONSHIP_DETAIL { get; set; }
        public Country COUNTRY { get; set; }
        public City CITY { get; set; }
        public string BUILDING { get; set; }
        public string STREET { get; set; }
        public string FLOOR { get; set; }
        public string DISTRICT { get; set; }
        public string POST_OFFICE { get; set; }
        public string POSTAL_CODE { get; set; }
        public string RESIDENCE_CONTACT { get; set; }
        public string OFFICE_NO { get; set; }
        public string MOB_NO { get; set; }

        public void SaveAccountNextofKinInfo()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                NEXT_OF_KIN_INFO n = new NEXT_OF_KIN_INFO();
                n.BI_ID = this.BI_ID;
                n.NEXT_OF_KIN = this.NEXT_OF_KIN;
                n.NEXT_OF_KIN_CNIC = this.NEXT_OF_KIN_CNIC;
                n.NEXT_OF_KIN_NAME = this.NEXT_OF_KIN_NAME;
                n.RELATIONSHIP = this.RELATIONSHIP.ID;
                n.RELATIONSHIP_DETAIL = this.RELATIONSHIP_DETAIL;
                db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                n.COUNTRY = this.COUNTRY.ID;
                n.CITY = this.CITY.ID;
                n.BUILDING = this.BUILDING;
                n.STREET = this.STREET;
                n.FLOOR = this.FLOOR;
                n.DISTRICT = this.DISTRICT;
                n.POST_OFFICE = this.POST_OFFICE;
                n.POSTAL_CODE = this.POSTAL_CODE;
                n.RESIDENCE_CONTACT = this.RESIDENCE_CONTACT;
                n.OFFICE_NO = this.OFFICE_NO;
                n.MOB_NO = this.MOB_NO;

                db.NEXT_OF_KIN_INFO.Add(n);
                db.SaveChanges();

            }

        }


        public void Update()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                NEXT_OF_KIN_INFO n = db.NEXT_OF_KIN_INFO.FirstOrDefault(b => b.BI_ID == this.BI_ID);
                
                n.NEXT_OF_KIN = this.NEXT_OF_KIN;
                n.NEXT_OF_KIN_CNIC = this.NEXT_OF_KIN_CNIC;
                n.NEXT_OF_KIN_NAME = this.NEXT_OF_KIN_NAME;
                n.RELATIONSHIP = this.RELATIONSHIP.ID;
                n.RELATIONSHIP_DETAIL = this.RELATIONSHIP_DETAIL;
                db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                n.COUNTRY = this.COUNTRY.ID;
                n.CITY = this.CITY.ID;
                n.BUILDING = this.BUILDING;
                n.STREET = this.STREET;
                n.FLOOR = this.FLOOR;
                n.DISTRICT = this.DISTRICT;
                n.POST_OFFICE = this.POST_OFFICE;
                n.POSTAL_CODE = this.POSTAL_CODE;
                n.RESIDENCE_CONTACT = this.RESIDENCE_CONTACT;
                n.OFFICE_NO = this.OFFICE_NO;
                n.MOB_NO = this.MOB_NO;

                db.SaveChanges();

            }

        }

        public bool GetAccountNextofKinInfo(int id)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.NEXT_OF_KIN_INFO.Where(c => c.BI_ID == id).Any())
                {
                    var a = db.NEXT_OF_KIN_INFO.FirstOrDefault(b => b.BI_ID == id);
                    this.BI_ID = a.BI_ID;
                    this.NEXT_OF_KIN = a.NEXT_OF_KIN;
                    this.NEXT_OF_KIN_NAME = a.NEXT_OF_KIN_NAME;
                    this.NEXT_OF_KIN_CNIC = a.NEXT_OF_KIN_CNIC;
                    this.RELATIONSHIP = new Relationship { ID = (int)a.RELATIONSHIP };
                    this.RELATIONSHIP_DETAIL = a.RELATIONSHIP_DETAIL;

                    this.COUNTRY = new Country() { ID = a.COUNTRY };
                    this.CITY = new City() { ID = a.CITY };
                    this.BUILDING = a.BUILDING;
                    this.STREET = a.STREET;
                    this.FLOOR = a.FLOOR;
                    this.DISTRICT = a.DISTRICT;
                    this.POST_OFFICE = a.POST_OFFICE;
                    this.POSTAL_CODE = a.POSTAL_CODE;
                    this.RESIDENCE_CONTACT = a.RESIDENCE_CONTACT;
                    this.OFFICE_NO = a.OFFICE_NO;
                    this.MOB_NO = a.MOB_NO;

                    return true;
                }
                else
                    return false;
            }

        }

        public bool CheckIndividualNextOfKin(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.NEXT_OF_KIN_INFO.Where(b => b.BI_ID == BID).Any();
            }
        }
    }
}
