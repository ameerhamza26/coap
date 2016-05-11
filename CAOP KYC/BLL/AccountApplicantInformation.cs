using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class AccountApplicantInformation
    {

        public int ID { get; set; }
        public Nullable<int> BI_ID { get; set; }
        public string CUSTOMER_CIF_NO { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string CUSTOMER_CNIC { get; set; }
        public Nullable<int> IS_PRIMARY_ACCOUNT_HOLDER { get; set; }
        public Nullable<int> ACCOUNT_IN_NEGATIVE_LIST { get; set; }
        public Nullable<int> POWER_OF_ATTORNY { get; set; }
        public Nullable<int> SIGNATURE_AUTHORITY { get; set; }
        public ApplicantStatuses APPLICANT_STATUS { get; set; }
        public Relationship RELATIONSHIP_NOT_PRIMARY { get; set; }
        public string RELATIONSHIP_DETAIL { get; set; }
        public string INVESTMENT_SHARE { get; set; }
        public List<ApplicantInformationCifs> Cifs { get; set; }


        public void SaveAccountOpen()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                APPLICANT_INFORMATION a = new APPLICANT_INFORMATION();

                a.BI_ID = this.BI_ID;
                if (this.Cifs == null)
                {
                    a.CUSTOMER_CIF_NO = this.CUSTOMER_CIF_NO;
                    a.CUSTOMER_NAME = this.CUSTOMER_NAME;
                    a.CUSTOMER_CNIC = this.CUSTOMER_CNIC;
                    a.IS_PRIMARY_ACCOUNT_HOLDER = this.IS_PRIMARY_ACCOUNT_HOLDER;
                    a.ACCOUNT_IN_NEGATIVE_LIST = this.ACCOUNT_IN_NEGATIVE_LIST;
                    a.POWER_OF_ATTORNY = this.POWER_OF_ATTORNY;
                    a.SIGNATURE_AUTHORITY = this.SIGNATURE_AUTHORITY;
                    a.APPLICANT_STATUS = this.APPLICANT_STATUS.ID;
                    a.RELATIONSHIP_NOT_PRIMARY = this.RELATIONSHIP_NOT_PRIMARY.ID;
                    a.RELATIONSHIP_DETAIL = this.RELATIONSHIP_DETAIL;
                    a.INVESTMENT_SHARE = this.INVESTMENT_SHARE;
                    APPLICANT_INFORMATION_CIFS NCIF = new APPLICANT_INFORMATION_CIFS();

                    NCIF.BI_ID = (int)this.BI_ID;
                    NCIF.CUSTOMER_CIF_NO = this.CUSTOMER_CIF_NO;
                    NCIF.IS_PRIMARY_ACCOUNT_HOLDER = (int) this.IS_PRIMARY_ACCOUNT_HOLDER;
                    NCIF.SIGNATURE_AUTHORITY = (int)this.SIGNATURE_AUTHORITY;
                    NCIF.POWER_OF_ATTORNY = (int)this.POWER_OF_ATTORNY;
                    NCIF.INVESTMENT_SHARE = this.INVESTMENT_SHARE;
                    NCIF.APPLICANT_STATUS = this.APPLICANT_STATUS.Name;
                    db.APPLICANT_INFORMATION_CIFS.Add(NCIF);
                }
                else
                {
                    foreach (var Cif in this.Cifs)
                    {
                        APPLICANT_INFORMATION_CIFS NCIF = new APPLICANT_INFORMATION_CIFS();
                        NCIF.BI_ID = (int) this.BI_ID;
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
                }
                             
                db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.APPLICANT_INFORMATION.Add(a);
                db.SaveChanges();



            }
        }

        public void Update()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {              
                if (this.Cifs == null)
                {
                     APPLICANT_INFORMATION a = db.APPLICANT_INFORMATION.FirstOrDefault(b => b.BI_ID == this.BI_ID);
                     APPLICANT_INFORMATION_CIFS aic = db.APPLICANT_INFORMATION_CIFS.FirstOrDefault(b => b.BI_ID == this.BI_ID);

                    a.CUSTOMER_CIF_NO = this.CUSTOMER_CIF_NO;
                    a.CUSTOMER_NAME = this.CUSTOMER_NAME;
                    a.CUSTOMER_CNIC = this.CUSTOMER_CNIC;
                    a.IS_PRIMARY_ACCOUNT_HOLDER = this.IS_PRIMARY_ACCOUNT_HOLDER;
                    a.ACCOUNT_IN_NEGATIVE_LIST = this.ACCOUNT_IN_NEGATIVE_LIST;
                    a.POWER_OF_ATTORNY = this.POWER_OF_ATTORNY;
                    a.SIGNATURE_AUTHORITY = this.SIGNATURE_AUTHORITY;
                    a.APPLICANT_STATUS = this.APPLICANT_STATUS.ID;
                    a.RELATIONSHIP_NOT_PRIMARY = this.RELATIONSHIP_NOT_PRIMARY.ID;
                    a.RELATIONSHIP_DETAIL = this.RELATIONSHIP_DETAIL;
                    a.INVESTMENT_SHARE = this.INVESTMENT_SHARE;

                    aic.CUSTOMER_CIF_NO = this.CUSTOMER_CIF_NO;
                    aic.IS_PRIMARY_ACCOUNT_HOLDER = (int) this.IS_PRIMARY_ACCOUNT_HOLDER;
                    aic.POWER_OF_ATTORNY = (int) this.POWER_OF_ATTORNY;
                    aic.SIGNATURE_AUTHORITY = (int)this.SIGNATURE_AUTHORITY;
                    aic.INVESTMENT_SHARE = this.INVESTMENT_SHARE;
                    aic.APPLICANT_STATUS = this.APPLICANT_STATUS.Name;


                }
                else
                {
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
                }



                
                db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.SaveChanges();



            }
        }
        public bool GetApplicantOpen(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.APPLICANT_INFORMATION.Where(b => b.BI_ID == BID).Any())
                {
                    var a = db.APPLICANT_INFORMATION.FirstOrDefault(b => b.BI_ID == BID);
                    if ((bool)db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == BID).ACCOUNT_MODE)
                    {
                        this.BI_ID = a.BI_ID;
                        this.CUSTOMER_CIF_NO = a.CUSTOMER_CIF_NO;
                        this.CUSTOMER_NAME = a.CUSTOMER_NAME;
                        this.CUSTOMER_CNIC = a.CUSTOMER_CNIC;
                        this.IS_PRIMARY_ACCOUNT_HOLDER = a.IS_PRIMARY_ACCOUNT_HOLDER;
                        this.ACCOUNT_IN_NEGATIVE_LIST = a.ACCOUNT_IN_NEGATIVE_LIST;
                        this.POWER_OF_ATTORNY = a.POWER_OF_ATTORNY;
                        this.SIGNATURE_AUTHORITY = a.SIGNATURE_AUTHORITY;
                        this.APPLICANT_STATUS = new ApplicantStatuses { ID = (int)a.APPLICANT_STATUS };
                        this.RELATIONSHIP_NOT_PRIMARY = new Relationship { ID = (int)a.RELATIONSHIP_NOT_PRIMARY };
                        this.RELATIONSHIP_DETAIL = a.RELATIONSHIP_DETAIL;
                        this.INVESTMENT_SHARE = a.INVESTMENT_SHARE;
                    }
                    else
                    {
                      this.Cifs =  db.APPLICANT_INFORMATION_CIFS
                            .Where(c => c.BI_ID == BID)
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

                    }

                   

                    return true;
                }
                else
                    return false;
            }
        }

        public bool CheckIndividualApplicantInformation(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.APPLICANT_INFORMATION.Where(b => b.BI_ID == BID).Any();
            }
        }
    }
}
