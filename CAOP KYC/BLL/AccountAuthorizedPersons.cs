using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class AccountAuthorizedPersons
    {
        public int ID { get; set; }
        public int BI_ID { get; set; }
        public string CIF_NO { get; set; }
        public string NAME { get; set; }
        public string CNIC { get; set; }
        public Nullable<bool> APPLICANT_IN_NEGATIVE_LIST { get; set; }
        public Nullable<bool> POWER_OF_ATTORNY { get; set; }
        public Nullable<bool> SIGNATURE_AUTHORITY { get; set; }
        public string APPLICANT_STATUS { get; set; }

        public void SaveaACBusiness()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                ACCOUNT_AUTHORIZED_PERSONS a = new ACCOUNT_AUTHORIZED_PERSONS();
                a.BI_ID = this.BI_ID;
                a.CIF_NO = this.CIF_NO;
                a.NAME = this.NAME;
                a.CNIC = this.CNIC;
                a.APPLICANT_IN_NEGATIVE_LIST = this.APPLICANT_IN_NEGATIVE_LIST;
                a.POWER_OF_ATTORNY = this.POWER_OF_ATTORNY;
                a.SIGNATURE_AUTHORITY = this.SIGNATURE_AUTHORITY;
                a.APPLICANT_STATUS = this.APPLICANT_STATUS;

                db.ACCOUNT_AUTHORIZED_PERSONS.Add(a);
                db.SaveChanges();

            }
        }

        public void Update()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {

                ACCOUNT_AUTHORIZED_PERSONS a = db.ACCOUNT_AUTHORIZED_PERSONS.FirstOrDefault(b => b.BI_ID == this.BI_ID);
                a.BI_ID = this.BI_ID;
                a.CIF_NO = this.CIF_NO;
                a.NAME = this.NAME;
                a.CNIC = this.CNIC;
                a.APPLICANT_IN_NEGATIVE_LIST = this.APPLICANT_IN_NEGATIVE_LIST;
                a.POWER_OF_ATTORNY = this.POWER_OF_ATTORNY;
                a.SIGNATURE_AUTHORITY = this.SIGNATURE_AUTHORITY;
                a.APPLICANT_STATUS = this.APPLICANT_STATUS;

                db.SaveChanges();
            }
        }
        public bool GetAccountAuthorizedBusiness(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.ACCOUNT_AUTHORIZED_PERSONS.Where(b => b.BI_ID == BID).Any())
                {
                    var Account = db.ACCOUNT_AUTHORIZED_PERSONS.FirstOrDefault(c => c.BI_ID == BID);
                    this.BI_ID = (int)Account.BI_ID;
                    this.NAME = Account.NAME;
                    this.CNIC = Account.CNIC;
                    this.CIF_NO = Account.CIF_NO;
                    this.APPLICANT_IN_NEGATIVE_LIST = Account.APPLICANT_IN_NEGATIVE_LIST;
                    this.POWER_OF_ATTORNY = Account.POWER_OF_ATTORNY;
                    this.SIGNATURE_AUTHORITY = Account.SIGNATURE_AUTHORITY;
                    //this.APPLICANT_STATUS = new ApplicantStatuses { ID = (int)Account.APPLICANT_STATUS };
                    this.APPLICANT_STATUS = Account.APPLICANT_STATUS;
                    return true;

                }
                else
                    return false;
            }
        }
    }
}
