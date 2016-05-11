using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;


namespace BLL
{
    public class AccountCertDepositInfo
    {
        public int ID { get; set; }
        public Nullable<int> BI_ID { get; set; }
        public string EXPIRY_DATE { get; set; }
        public string CERTIFICATE_PERIOD { get; set; }
        public Nullable<bool> AUTO_ROLL_ON_EXPIRY { get; set; }
        public string SPECIAL_INSTR_ANY { get; set; }
        public string PROFIT_ACCOUNT_NUMBER { get; set; }
        public TransactionType TRANSACTION_TYPE { get; set; }
        public string CHEQUE_PREFIX { get; set; }
        public string CHEQUE_NUMBER { get; set; }
        public string CERTIFICATE_NUMBER { get; set; }
        public string CERTIFCATE_AMOUNT { get; set; }

        public AccountType PROFIT_ACCOUNT_TYPE { get; set; }
        public PrinciparRenewalOption PRINCIPAL_RENEWAL_OPTION { get; set; }
        public string MARK_UP_RATE { get; set; }

        public void Sava()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                CERTIFICATE_DEPOSIT_INFO c = new CERTIFICATE_DEPOSIT_INFO();
                c.BI_ID = this.BI_ID;
                c.EXPIRY_DATE = this.EXPIRY_DATE;
                c.CERTIFICATE_PERIOD = this.CERTIFICATE_PERIOD;
                c.AUTO_ROLL_ON_EXPIRY = this.AUTO_ROLL_ON_EXPIRY;
                c.SPECIAL_INSTR_ANY = this.SPECIAL_INSTR_ANY;
                c.PROFIT_ACCOUNT_NUMBER = this.PROFIT_ACCOUNT_NUMBER;
                c.PROFIT_ACCOUNT_TYPE = this.PROFIT_ACCOUNT_TYPE.ID;
                c.TRANSACTION_TYPE = this.TRANSACTION_TYPE.ID;
                c.CHEQUE_NUMBER = this.CHEQUE_NUMBER;
                c.CHEQUE_PREFIX = this.CHEQUE_PREFIX;
                c.CERTIFICATE_NUMBER = this.CERTIFICATE_NUMBER;
                c.CERTIFCATE_AMOUNT = this.CERTIFCATE_AMOUNT;
                c.MARK_UP_RATE = this.MARK_UP_RATE;
                c.PRINCIPAL_RENEWAL_OPTION = this.PRINCIPAL_RENEWAL_OPTION.ID;
                db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.CERTIFICATE_DEPOSIT_INFO.Add(c);
                db.SaveChanges();


            }
        }


        public void Update()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                CERTIFICATE_DEPOSIT_INFO c = db.CERTIFICATE_DEPOSIT_INFO.FirstOrDefault(b => b.BI_ID == this.BI_ID);
                
                c.EXPIRY_DATE = this.EXPIRY_DATE;
                c.CERTIFICATE_PERIOD = this.CERTIFICATE_PERIOD;
                c.AUTO_ROLL_ON_EXPIRY = this.AUTO_ROLL_ON_EXPIRY;
                c.SPECIAL_INSTR_ANY = this.SPECIAL_INSTR_ANY;
                c.PROFIT_ACCOUNT_NUMBER = this.PROFIT_ACCOUNT_NUMBER;
                c.PROFIT_ACCOUNT_TYPE = this.PROFIT_ACCOUNT_TYPE.ID;
                c.TRANSACTION_TYPE = this.TRANSACTION_TYPE.ID;
                c.CHEQUE_NUMBER = this.CHEQUE_NUMBER;
                c.CHEQUE_PREFIX = this.CHEQUE_PREFIX;
                c.CERTIFICATE_NUMBER = this.CERTIFICATE_NUMBER;
                c.CERTIFCATE_AMOUNT = this.CERTIFCATE_AMOUNT;
                c.MARK_UP_RATE = this.MARK_UP_RATE;
                c.PRINCIPAL_RENEWAL_OPTION = this.PRINCIPAL_RENEWAL_OPTION.ID;
                db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.SaveChanges();


            }
        }
        public bool Get(int id)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.CERTIFICATE_DEPOSIT_INFO.Where(b => b.BI_ID == id).Any())
                {
                    var a = db.CERTIFICATE_DEPOSIT_INFO.FirstOrDefault(c => c.BI_ID == id);
                    this.BI_ID = a.BI_ID;
                    this.EXPIRY_DATE = a.EXPIRY_DATE;
                    this.CERTIFICATE_PERIOD = a.CERTIFICATE_PERIOD;
                    this.AUTO_ROLL_ON_EXPIRY = a.AUTO_ROLL_ON_EXPIRY;
                    this.SPECIAL_INSTR_ANY = a.SPECIAL_INSTR_ANY;
                    this.PROFIT_ACCOUNT_TYPE = new AccountType { ID = (int)a.PROFIT_ACCOUNT_TYPE };
                    this.PROFIT_ACCOUNT_NUMBER = a.PROFIT_ACCOUNT_NUMBER;
                    this.TRANSACTION_TYPE = new TransactionType { ID = (int)a.TRANSACTION_TYPE };
                    this.CHEQUE_PREFIX = a.CHEQUE_PREFIX;
                    this.CHEQUE_NUMBER = a.CHEQUE_NUMBER;
                    this.CERTIFICATE_NUMBER = a.CERTIFICATE_NUMBER;
                    this.CERTIFCATE_AMOUNT = a.CERTIFCATE_AMOUNT;
                    this.MARK_UP_RATE = a.MARK_UP_RATE;
                    this.PRINCIPAL_RENEWAL_OPTION = new PrinciparRenewalOption { ID = (int?)a.PRINCIPAL_RENEWAL_OPTION };
                    return true;
                }
                else
                    return false;
            }
        }

        public bool CheckCertDeposit(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.CERTIFICATE_DEPOSIT_INFO.Where(b => b.BI_ID == BID).Any();
            }
        }

    }
}
