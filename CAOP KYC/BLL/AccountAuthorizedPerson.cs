using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class AccountAuthorizedPerson
    {
        public int ID { get; set; }
        public Nullable<int> BI_ID { get; set; }
        public string CIF_NO { get; set; }
        public string NAME { get; set; }
        public string CNIC { get; set; }
        public Nullable<bool> APPLICANT_IN_NEGATIVE_LIST { get; set; }
        public Nullable<bool> POWER_OF_ATTORNY { get; set; }
        public Nullable<bool> SIGNATURE_AUTHORITY { get; set; }
        public ApplicantStatuses APPLICANT_STATUS { get; set; }
        public List<ApplicantInformationCifs> Cifs { get; set; }

        public void Save()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                ACCOUNT_AUTHORIZED_PERSONS a = new ACCOUNT_AUTHORIZED_PERSONS();

                
               // a.CIF_NO = this.CIF_NO;
              //  a.NAME = this.NAME;
              //  a.CNIC = this.CNIC;
             //   a.APPLICANT_IN_NEGATIVE_LIST = this.APPLICANT_IN_NEGATIVE_LIST;
             //   a.POWER_OF_ATTORNY = this.POWER_OF_ATTORNY;
            //    a.SIGNATURE_AUTHORITY = this.SIGNATURE_AUTHORITY;
            //    a.APPLICANT_STATUS = this.APPLICANT_STATUS.ID;
                db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;
                foreach (var Cif in this.Cifs)
                {
                    APPLICANT_INFORMATION_CIFS NCIF = new APPLICANT_INFORMATION_CIFS();
                    NCIF.BI_ID = (int)this.BI_ID;
                    a.BI_ID = (int)this.BI_ID;
                    NCIF.CUSTOMER_CIF_NO = Cif.CUSTOMER_CIF_NO;
                    a.CIF_NO = Cif.CUSTOMER_CIF_NO;
                    NCIF.IS_PRIMARY_ACCOUNT_HOLDER = Cif.IS_PRIMARY_ACCOUNT_HOLDER;
                    NCIF.SIGNATURE_AUTHORITY = Cif.SIGNATURE_AUTHORITY;
                    if (Cif.SIGNATURE_AUTHORITY == 1)
                    {
                        a.SIGNATURE_AUTHORITY = true;
                    }
                    else
                    {
                        a.SIGNATURE_AUTHORITY = false;
                    }
                    if (Cif.POWER_OF_ATTORNY == 1)
                    {
                        a.POWER_OF_ATTORNY = true;
                    }
                    else
                    {
                        a.POWER_OF_ATTORNY = false;
                    }

                    a.NAME = Cif.CUSTOMER_NAME;
                    a.APPLICANT_STATUS = Cif.APPLICANT_STATUS;

                    NCIF.POWER_OF_ATTORNY = Cif.POWER_OF_ATTORNY;
                    NCIF.INVESTMENT_SHARE = Cif.INVESTMENT_SHARE;
                    NCIF.APPLICANT_STATUS = Cif.APPLICANT_STATUS;
                    NCIF.CUSTOMER_NAME = Cif.CUSTOMER_NAME;
                    NCIF.CUSTOMER_IDENTITY = Cif.CUSTOMER_IDENTITY;
                    db.APPLICANT_INFORMATION_CIFS.Add(NCIF);
                    db.ACCOUNT_AUTHORIZED_PERSONS.Add(a);
                }

                
                db.SaveChanges();

            }
        }

        public void Update()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                ACCOUNT_AUTHORIZED_PERSONS a = db.ACCOUNT_AUTHORIZED_PERSONS.FirstOrDefault(b => b.BI_ID == this.BI_ID);

                a.BI_ID = (int)this.BI_ID;
             //   a.CIF_NO = this.CIF_NO;
             //   a.NAME = this.NAME;
             //   a.CNIC = this.CNIC;
             //   a.APPLICANT_IN_NEGATIVE_LIST = this.APPLICANT_IN_NEGATIVE_LIST;
             //   a.POWER_OF_ATTORNY = this.POWER_OF_ATTORNY;
             //   a.SIGNATURE_AUTHORITY = this.SIGNATURE_AUTHORITY;
             //   a.APPLICANT_STATUS = this.APPLICANT_STATUS.ID;
             //   db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;
                db.APPLICANT_INFORMATION_CIFS
                       .RemoveRange
                       (
                           db.APPLICANT_INFORMATION_CIFS
                           .Where(c => c.BI_ID == this.BI_ID)
                       );

                foreach (var Cif in this.Cifs)
                {
                    APPLICANT_INFORMATION_CIFS NCIF = new APPLICANT_INFORMATION_CIFS();
                    NCIF.BI_ID = (int)this.BI_ID;
                    NCIF.CUSTOMER_CIF_NO = Cif.CUSTOMER_CIF_NO;
                    NCIF.IS_PRIMARY_ACCOUNT_HOLDER = Cif.IS_PRIMARY_ACCOUNT_HOLDER;
                    NCIF.SIGNATURE_AUTHORITY = Cif.SIGNATURE_AUTHORITY;
                    NCIF.POWER_OF_ATTORNY = Cif.POWER_OF_ATTORNY;
                    NCIF.INVESTMENT_SHARE = Cif.INVESTMENT_SHARE;
                    NCIF.APPLICANT_STATUS = Cif.APPLICANT_STATUS;
                    NCIF.CUSTOMER_NAME = Cif.CUSTOMER_NAME;
                    NCIF.CUSTOMER_IDENTITY = Cif.CUSTOMER_IDENTITY;
                    db.APPLICANT_INFORMATION_CIFS.Add(NCIF);
                }
                db.SaveChanges();

            }

        }
        public bool Get(int id)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.ACCOUNT_AUTHORIZED_PERSONS.Where(b => b.BI_ID == id).Any())
                {
                    var a = db.ACCOUNT_AUTHORIZED_PERSONS.FirstOrDefault(c => c.BI_ID == id);
                    this.BI_ID = a.BI_ID;
                 //   this.CIF_NO = a.CIF_NO;
                 //   this.NAME = a.NAME;
                 //   this.CNIC = a.CNIC;
                 //   this.APPLICANT_IN_NEGATIVE_LIST = a.APPLICANT_IN_NEGATIVE_LIST;
                 //   this.POWER_OF_ATTORNY = a.POWER_OF_ATTORNY;
                 //   this.SIGNATURE_AUTHORITY = a.SIGNATURE_AUTHORITY;
                 //   this.APPLICANT_STATUS = new ApplicantStatuses { ID = (int)a.APPLICANT_STATUS };

                    this.Cifs = db.APPLICANT_INFORMATION_CIFS
                            .Where(c => c.BI_ID == id)
                            .Select(c =>
                                new ApplicantInformationCifs()
                                {
                                    BI_ID = c.BI_ID,
                                    INVESTMENT_SHARE = c.INVESTMENT_SHARE,
                                    CUSTOMER_CIF_NO = c.CUSTOMER_CIF_NO,
                                    IS_PRIMARY_ACCOUNT_HOLDER = c.IS_PRIMARY_ACCOUNT_HOLDER,
                                    POWER_OF_ATTORNY = c.POWER_OF_ATTORNY,
                                    SIGNATURE_AUTHORITY = c.SIGNATURE_AUTHORITY,
                                    APPLICANT_STATUS = c.APPLICANT_STATUS,
                                    CUSTOMER_NAME = c.CUSTOMER_NAME,
                                    CUSTOMER_IDENTITY = c.CUSTOMER_IDENTITY
                                }
                                ).ToList();


                    return true;
                }
                else
                    return false;
            }
        }
        public bool CheckIndividualAuthorizedPerson(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.ACCOUNT_AUTHORIZED_PERSONS.Where(b => b.BI_ID == BID).Any();
            }
        }

    }
}
