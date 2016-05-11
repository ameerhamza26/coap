using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Identity
    {

        #region Identity Model
        public int ID { get; set; }
        public int BI_ID { get; set; }
        public string CNIC_DATE_ISSUE { get; set; }
        public string EXPIRY_DATE { get; set; }
        public string IDENTIFICATION_MARK { get; set; }
        public string FAMILY_NO { get; set; }
        public string TOKEN_NO { get; set; }
        public System.DateTime TOKEN_ISSUE_DATE { get; set; }
        public string NTN { get; set; }
        public string NIC_OLD { get; set; }
        public IdentityType IDENTITY_TYPE { get; set; }
        public string IDENTITY_NO { get; set; }
        public Country COUNTRY_ISSUE { get; set; }
        public string OTHER_IDENTITY_ISSUE_DATE { get; set; }
        public string PLACE_ISSUE { get; set; }
        public string OTHER_IDENTITY_EXPIRY_DATE { get; set; }

        // New Fields
        public Country COUNTRY_ISSUE_CNIC { get; set; }
        public string PLACE_ISSUE_CNIC { get; set; }

        #endregion


        public void SaveIdentity()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                IDENTITIES newIdentity = new IDENTITIES();

                newIdentity.BI_ID = this.BI_ID;
                newIdentity.CNIC_DATE_ISSUE = this.CNIC_DATE_ISSUE;
                newIdentity.EXPIRY_DATE = this.EXPIRY_DATE;
                newIdentity.IDENTIFICATION_MARK = this.IDENTIFICATION_MARK.ToUpper();
                newIdentity.FAMILY_NO = this.FAMILY_NO.ToUpper();
                newIdentity.TOKEN_NO = this.TOKEN_NO.ToUpper();
                newIdentity.TOKEN_ISSUE_DATE = this.TOKEN_ISSUE_DATE;
                newIdentity.NTN = this.NTN.ToUpper();
                newIdentity.NIC_OLD = this.NIC_OLD;
                newIdentity.IDENTITY_TYPE = this.IDENTITY_TYPE.ID;
                newIdentity.IDENTITY_NO = this.IDENTITY_NO;
                newIdentity.COUNTRY_ISSUE = this.COUNTRY_ISSUE.ID;
                newIdentity.OTHER_IDENTITY_ISSUE_DATE = this.OTHER_IDENTITY_ISSUE_DATE;
                newIdentity.PLACE_ISSUE = this.PLACE_ISSUE.ToUpper();
                newIdentity.EXPIRY_DATE = this.EXPIRY_DATE;
                newIdentity.COUNTRY_ISSUE_CNIC = this.COUNTRY_ISSUE_CNIC.ID;
                newIdentity.PLACE_ISSUE_CNIC = this.PLACE_ISSUE_CNIC.ToUpper();
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;
                

                db.IDENTITIES.Add(newIdentity);
                db.SaveChanges();

            }
        }

        public void UpdateIdentity()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                IDENTITIES newIdentity = db.IDENTITIES.FirstOrDefault(i => i.BI_ID == this.BI_ID);
                newIdentity.CNIC_DATE_ISSUE = this.CNIC_DATE_ISSUE;
                newIdentity.EXPIRY_DATE = this.EXPIRY_DATE;
                newIdentity.IDENTIFICATION_MARK = this.IDENTIFICATION_MARK.ToUpper();
                newIdentity.FAMILY_NO = this.FAMILY_NO.ToUpper();
                newIdentity.TOKEN_NO = this.TOKEN_NO.ToUpper();
                newIdentity.TOKEN_ISSUE_DATE = this.TOKEN_ISSUE_DATE;
                newIdentity.NTN = this.NTN.ToUpper();
                newIdentity.NIC_OLD = this.NIC_OLD;
                newIdentity.IDENTITY_TYPE = this.IDENTITY_TYPE.ID;
                newIdentity.IDENTITY_NO = this.IDENTITY_NO.ToUpper();
                newIdentity.COUNTRY_ISSUE = this.COUNTRY_ISSUE.ID;
                newIdentity.OTHER_IDENTITY_ISSUE_DATE = this.OTHER_IDENTITY_ISSUE_DATE;
                newIdentity.PLACE_ISSUE = this.PLACE_ISSUE.ToUpper();
                newIdentity.EXPIRY_DATE = this.EXPIRY_DATE;
                newIdentity.COUNTRY_ISSUE_CNIC = this.COUNTRY_ISSUE_CNIC.ID;
                newIdentity.PLACE_ISSUE_CNIC = this.PLACE_ISSUE_CNIC.ToUpper();
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.SaveChanges();
            }
        }

        public bool GetIndividualIdentity(int BI_ID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.IDENTITIES.Where(i => i.BI_ID == BI_ID).Any())
                {
                    var Identity = db.IDENTITIES.FirstOrDefault(i => i.BI_ID == BI_ID);

                    this.BI_ID = (int)Identity.BI_ID;
                    this.CNIC_DATE_ISSUE = Identity.CNIC_DATE_ISSUE;
                    this.EXPIRY_DATE = Identity.EXPIRY_DATE;
                    this.IDENTIFICATION_MARK = Identity.IDENTIFICATION_MARK;
                    this.FAMILY_NO = Identity.FAMILY_NO;
                    this.TOKEN_NO = Identity.TOKEN_NO;
                    this.TOKEN_ISSUE_DATE = (DateTime)Identity.TOKEN_ISSUE_DATE;
                    this.NTN = Identity.NTN;
                    this.NIC_OLD = Identity.NIC_OLD;
                    this.IDENTITY_TYPE = new IdentityType { ID = Identity.IDENTITY_TYPE };
                    this.IDENTITY_NO = Identity.IDENTITY_NO;
                    this.COUNTRY_ISSUE = new Country { ID = Identity.COUNTRY_ISSUE };
                    this.OTHER_IDENTITY_ISSUE_DATE = Identity.OTHER_IDENTITY_ISSUE_DATE;
                    this.PLACE_ISSUE = Identity.PLACE_ISSUE;
                    this.EXPIRY_DATE = Identity.EXPIRY_DATE;
                    this.COUNTRY_ISSUE_CNIC = new Country() { ID = Convert.ToInt32(Identity.COUNTRY_ISSUE_CNIC) };
                    this.PLACE_ISSUE_CNIC = Identity.PLACE_ISSUE_CNIC;
                    return true;
                }
                else
                    return false;
            }
        }


        public void SaveIdentityNextOfKin()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                IDENTITIES newIdentity = new IDENTITIES();

                newIdentity.BI_ID = this.BI_ID;
                newIdentity.IDENTITY_TYPE = this.IDENTITY_TYPE.ID;
                newIdentity.IDENTITY_NO = this.IDENTITY_NO.ToUpper();
                newIdentity.COUNTRY_ISSUE = this.COUNTRY_ISSUE.ID;
                newIdentity.OTHER_IDENTITY_ISSUE_DATE = this.OTHER_IDENTITY_ISSUE_DATE;
                newIdentity.PLACE_ISSUE = this.PLACE_ISSUE.ToUpper();
                newIdentity.OTHER_IDENTITY_EXPIRY_DATE = this.OTHER_IDENTITY_EXPIRY_DATE;
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.IDENTITIES.Add(newIdentity);
                db.SaveChanges();
            }
        }

        public bool GetNextOFKinIdentity(int BI_ID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.IDENTITIES.Where(i => i.BI_ID == BI_ID).Any())
                {
                    var Identity = db.IDENTITIES.FirstOrDefault(i => i.BI_ID == BI_ID);

                    this.IDENTITY_TYPE = new IdentityType { ID = Identity.IDENTITY_TYPE };
                    this.IDENTITY_NO = Identity.IDENTITY_NO;
                    this.COUNTRY_ISSUE = new Country { ID = Identity.COUNTRY_ISSUE };
                    this.OTHER_IDENTITY_ISSUE_DATE =  Identity.OTHER_IDENTITY_ISSUE_DATE;
                    this.PLACE_ISSUE = Identity.PLACE_ISSUE;
                    this.OTHER_IDENTITY_EXPIRY_DATE = Identity.OTHER_IDENTITY_EXPIRY_DATE;

                    return true;
                }
                else
                    return false;
            }
        }

        public void UpdateIdentityNextOfKin()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                IDENTITIES newIdentity = db.IDENTITIES.FirstOrDefault(i => i.BI_ID == this.BI_ID);

                newIdentity.IDENTITY_TYPE = this.IDENTITY_TYPE.ID;
                newIdentity.IDENTITY_NO = this.IDENTITY_NO.ToUpper();
                newIdentity.COUNTRY_ISSUE = this.COUNTRY_ISSUE.ID;
                newIdentity.OTHER_IDENTITY_ISSUE_DATE = this.OTHER_IDENTITY_ISSUE_DATE;
                newIdentity.PLACE_ISSUE = this.PLACE_ISSUE.ToUpper();
                newIdentity.OTHER_IDENTITY_EXPIRY_DATE = this.OTHER_IDENTITY_EXPIRY_DATE;
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.SaveChanges();
            }
        }

        public bool CheckIdentity(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.IDENTITIES.Where(b => b.BI_ID == BID).Any();
            }
        }

    }
}
