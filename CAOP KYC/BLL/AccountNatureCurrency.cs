using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class AccountNatureCurrency
    {
        public Nullable<bool> CNIC_VERIFIED { get; set; }
        public DateTime ACCOUNT_ENTRY_DATE { get; set; }
     //   public Gl_code GL_CODE { get; set; }
     //   public Sl_code SL_CODE { get; set; }
        public AccountType ACCOUNT_TYPE { get; set; }
        public Currency CURRENCY { get; set; }
        public string ACCOUNT_NUMBER { get; set; }
        public string ACCOUNT_TITLE { get; set; }
        public string INITIAL_DEPOSIT { get; set; }
        public Nullable<bool> ACCOUNT_MODE { get; set; }
        public int ACCOUNT_MODE_DETAIL { get; set; }
    //    public Nullable<bool> MINOR_ACCOUNT { get; set; }
        public int ID { get; set; }
        public AccountOpenType ACCOUNT_OPEN_TYPE { get; set; }
        public Products PRODUCT { get; set; }
        public string PROFILE_ACCOUNT_NO { get; set; }
        public string STATUS { get; set; }
        public Nullable<int> USERID { get; set; }
        public string BRANCH_CODE { get; set; }

        public Nullable<System.DateTime> LAST_UPDATED { get; set; }


        public int SetAccountNatureIndividual()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                ACCOUNT_NATURE_CURRENCY a = new ACCOUNT_NATURE_CURRENCY();

                a.CNIC_VERIFIED = this.CNIC_VERIFIED;
                a.ACCOUNT_ENTRY_DATE = this.ACCOUNT_ENTRY_DATE;
                //  a.GL_CODE = this.GL_CODE.ID;
                //  a.SL_CODE = this.SL_CODE.ID;
                a.ACCOUNT_TYPE = this.ACCOUNT_TYPE.ID;
                a.CURRENCY = this.CURRENCY.ID;
                a.ACCOUNT_NUMBER = this.ACCOUNT_NUMBER;
                a.ACCOUNT_TITLE = this.ACCOUNT_TITLE.ToUpper();
                a.INITIAL_DEPOSIT = this.INITIAL_DEPOSIT;
                a.ACCOUNT_MODE = this.ACCOUNT_MODE;
                a.ACCOUNT_MODE_DETAIL = this.ACCOUNT_MODE_DETAIL;
                // a.MINOR_ACCOUNT = this.MINOR_ACCOUNT;
                a.ACCOUNT_OPEN_TYPE = this.ACCOUNT_OPEN_TYPE.ID;
                a.BRANCH_CODE = this.BRANCH_CODE;
                a.STATUS = "SAVED";
                a.PROFILE_STATUS = "PENDING";
                a.USERID = this.USERID;
                a.LAST_UPDATED = DateTime.Now;

                db.ACCOUNT_NATURE_CURRENCY.Add(a);
                db.SaveChanges();

                return a.ID;

            }
        }

        public int SetAccountNature()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                ACCOUNT_NATURE_CURRENCY a = new ACCOUNT_NATURE_CURRENCY();

                a.CNIC_VERIFIED = this.CNIC_VERIFIED;
                a.ACCOUNT_ENTRY_DATE = this.ACCOUNT_ENTRY_DATE;
                a.PRODUCT = this.PRODUCT.ID;
              //  a.GL_CODE = this.GL_CODE.ID;
              //  a.SL_CODE = this.SL_CODE.ID;
                a.ACCOUNT_TYPE = this.ACCOUNT_TYPE.ID;
                a.CURRENCY = this.CURRENCY.ID;
                a.ACCOUNT_NUMBER = this.ACCOUNT_NUMBER;
                a.ACCOUNT_TITLE = this.ACCOUNT_TITLE.ToUpper();
                a.INITIAL_DEPOSIT = this.INITIAL_DEPOSIT;
                a.ACCOUNT_MODE = this.ACCOUNT_MODE;
                a.ACCOUNT_MODE_DETAIL = this.ACCOUNT_MODE_DETAIL;
               // a.MINOR_ACCOUNT = this.MINOR_ACCOUNT;
                a.ACCOUNT_OPEN_TYPE = this.ACCOUNT_OPEN_TYPE.ID;
                a.BRANCH_CODE = this.BRANCH_CODE;
                a.STATUS = "SAVED";
                a.PROFILE_STATUS = "PENDING";
                a.USERID = this.USERID;
                a.LAST_UPDATED = DateTime.Now;

                db.ACCOUNT_NATURE_CURRENCY.Add(a);
                db.SaveChanges();

                return a.ID;

            }
        }

        public void Update()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                ACCOUNT_NATURE_CURRENCY a = db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.ID);

              //  a.GL_CODE = this.GL_CODE.ID;
              //  a.SL_CODE = this.SL_CODE.ID;
                a.PRODUCT = this.PRODUCT.ID;
                a.ACCOUNT_TYPE = this.ACCOUNT_TYPE.ID;
                a.CURRENCY = this.CURRENCY.ID;
                a.ACCOUNT_NUMBER = this.ACCOUNT_NUMBER;
                a.ACCOUNT_TITLE = this.ACCOUNT_TITLE;
                a.INITIAL_DEPOSIT = this.INITIAL_DEPOSIT;
                a.ACCOUNT_MODE = this.ACCOUNT_MODE;
                a.ACCOUNT_MODE_DETAIL = this.ACCOUNT_MODE_DETAIL;
              //  a.MINOR_ACCOUNT = this.MINOR_ACCOUNT;
                a.LAST_UPDATED = DateTime.Now;
                db.SaveChanges();

            }
        }

        public void UpdateIndividual()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                ACCOUNT_NATURE_CURRENCY a = db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.ID);

                //  a.GL_CODE = this.GL_CODE.ID;
                //  a.SL_CODE = this.SL_CODE.ID;
                a.ACCOUNT_TYPE = this.ACCOUNT_TYPE.ID;
                a.CURRENCY = this.CURRENCY.ID;
                a.ACCOUNT_NUMBER = this.ACCOUNT_NUMBER;
                a.ACCOUNT_TITLE = this.ACCOUNT_TITLE;
                a.INITIAL_DEPOSIT = this.INITIAL_DEPOSIT;
                a.ACCOUNT_MODE = this.ACCOUNT_MODE;
                a.ACCOUNT_MODE_DETAIL = this.ACCOUNT_MODE_DETAIL;
                //  a.MINOR_ACCOUNT = this.MINOR_ACCOUNT;
                a.LAST_UPDATED = DateTime.Now;
                db.SaveChanges();

            }
        }

        public bool GetAccountNature(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.ACCOUNT_NATURE_CURRENCY.Where(b => b.ID == BID).Any())
                {
                    var a = db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(c => c.ID == BID);
                    this.CNIC_VERIFIED = a.CNIC_VERIFIED;
                    this.ACCOUNT_ENTRY_DATE = (DateTime)a.ACCOUNT_ENTRY_DATE;
                    this.PRODUCT = new Products() { ID = (int) a.PRODUCT };
                  //  this.GL_CODE = new Gl_code { ID = (int)a.GL_CODE };
                  //  this.SL_CODE = new Sl_code { ID = (int)a.SL_CODE };
                    this.ACCOUNT_TYPE = new AccountType { ID = (int)a.ACCOUNT_TYPE };
                    this.CURRENCY = new Currency { ID = (int)a.CURRENCY };
                    this.ACCOUNT_NUMBER = a.ACCOUNT_NUMBER;
                    this.ACCOUNT_TITLE = a.ACCOUNT_TITLE;
                    this.INITIAL_DEPOSIT = a.INITIAL_DEPOSIT;
                    this.ACCOUNT_MODE = a.ACCOUNT_MODE;
                    this.ACCOUNT_MODE_DETAIL = (int) a.ACCOUNT_MODE_DETAIL;
                 //   this.MINOR_ACCOUNT = a.MINOR_ACCOUNT;
                    this.ACCOUNT_OPEN_TYPE = new AccountOpenType { ID = (int)a.ACCOUNT_OPEN_TYPE };
                    this.STATUS = a.STATUS;
                    this.USERID = a.USERID;
                    this.LAST_UPDATED = a.LAST_UPDATED;


                    return true;

                }
                else
                    return false;
            }
        }

        public bool GetAccountNatureIndividual(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.ACCOUNT_NATURE_CURRENCY.Where(b => b.ID == BID).Any())
                {
                    var a = db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(c => c.ID == BID);
                    this.CNIC_VERIFIED = a.CNIC_VERIFIED;
                    this.ACCOUNT_ENTRY_DATE = (DateTime)a.ACCOUNT_ENTRY_DATE;
                    //  this.GL_CODE = new Gl_code { ID = (int)a.GL_CODE };
                    //  this.SL_CODE = new Sl_code { ID = (int)a.SL_CODE };
                    this.ACCOUNT_TYPE = new AccountType { ID = (int)a.ACCOUNT_TYPE };
                    this.CURRENCY = new Currency { ID = (int)a.CURRENCY };
                    this.ACCOUNT_NUMBER = a.ACCOUNT_NUMBER;
                    this.ACCOUNT_TITLE = a.ACCOUNT_TITLE;
                    this.INITIAL_DEPOSIT = a.INITIAL_DEPOSIT;
                    this.ACCOUNT_MODE = a.ACCOUNT_MODE;
                    this.ACCOUNT_MODE_DETAIL = (int)a.ACCOUNT_MODE_DETAIL;
                    //   this.MINOR_ACCOUNT = a.MINOR_ACCOUNT;
                    this.ACCOUNT_OPEN_TYPE = new AccountOpenType { ID = (int)a.ACCOUNT_OPEN_TYPE };
                    this.STATUS = a.STATUS;
                    this.USERID = a.USERID;
                    this.LAST_UPDATED = a.LAST_UPDATED;


                    return true;

                }
                else
                    return false;
            }
        }

    }
}
