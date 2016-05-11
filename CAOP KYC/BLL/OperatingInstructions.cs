using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class OperatingInstructions
    {

        public int ID { get; set; }
        public Nullable<int> BI_ID { get; set; }
        public AuthorityToOperate AUTHORITY_TO_OPERATE { get; set; }
        public string DESCRIPTION_IF_OTHER { get; set; }
        public Nullable<bool> ZAKAT_DEDUCTION { get; set; }
        public ZakatExemptionType ZAKAT_EXEMPTION_TYPE { get; set; }
        public string EXEMPTION_REASON_DETAIL { get; set; }
        public AccountStatementFrequency ACCOUNT_STATEMENT_FREQUENCY { get; set; }
        public string DESCRIPTION_IF_HOLD_MAIL { get; set; }
        public Nullable<bool> ATM_CARD_REQUIRED { get; set; }
        public string CUSTOMER_NAME_ON_ATMCARD { get; set; }
        public E_Statement_Required E_STATEMENT_REQUIRED { get; set; }
        public Nullable<bool> MOBILE_BANKING_REQUIRED { get; set; }
        public string MOBILE_NO { get; set; }
        public Nullable<bool> IBT_ALLOWED { get; set; }
        public Nullable<bool> IS_PROFIT_APPLICABLE { get; set; }
        public Nullable<bool> IS_FED_EXEMPTED { get; set; }
        public string EXPIRY_DATE_EXEMPTED { get; set; }
        public string APPLICABLE_PROFIT_RATE { get; set; }
        public ProfitPayment PROFIT_PAYMENT { get; set; }
        public Nullable<bool> WHT_DEDUCTED_ON_PROFIT { get; set; }
        public string EXPIRY_DATE_PROFIT { get; set; }
        public Nullable<bool> WHT_DEDUCTED_ON_TRANSACTION { get; set; }
        public string EXPIRY_DATE_TRANSACTION { get; set; }
        public string SPECIAL_PROFIT_VALUE { get; set; }


        public void SaveOperatingInstructions()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                OPERATING_INSTRUCTIONS o = new OPERATING_INSTRUCTIONS();
                o.BI_ID = this.BI_ID;
                o.AUTHORITY_TO_OPERATE = this.AUTHORITY_TO_OPERATE.ID;
                o.DESCRIPTION_IF_OTHER = this.DESCRIPTION_IF_OTHER;
                o.ZAKAT_DEDUCTION = this.ZAKAT_DEDUCTION;
                o.ZAKAT_EXEMPTION_TYPE = this.ZAKAT_EXEMPTION_TYPE.ID;
                o.EXEMPTION_REASON_DETAIL = this.EXEMPTION_REASON_DETAIL;
                o.ACCOUNT_STATEMENT_FREQUENCY = this.ACCOUNT_STATEMENT_FREQUENCY.ID;
                o.DESCRIPTION_IF_HOLD_MAIL = this.DESCRIPTION_IF_HOLD_MAIL;
                o.ATM_CARD_REQUIRED = this.ATM_CARD_REQUIRED;
                o.CUSTOMER_NAME_ON_ATMCARD = this.CUSTOMER_NAME_ON_ATMCARD;
                o.E_STATEMENT_REQUIRED = this.E_STATEMENT_REQUIRED.ID;
                o.MOBILE_BANKING_REQUIRED = this.MOBILE_BANKING_REQUIRED;
                o.MOBILE_NO = this.MOBILE_NO;
                o.IBT_ALLOWED = this.IBT_ALLOWED;
                o.IS_PROFIT_APPLICABLE = this.IS_PROFIT_APPLICABLE;
                o.IS_FED_EXEMPTED = this.IS_FED_EXEMPTED;
                o.EXPIRY_DATE_EXEMPTED = this.EXPIRY_DATE_EXEMPTED;
                o.APPLICABLE_PROFIT_RATE = this.APPLICABLE_PROFIT_RATE;
                o.SPECIAL_PROFIT_VALUE = this.SPECIAL_PROFIT_VALUE;
                o.PROFIT_PAYMENT = this.PROFIT_PAYMENT.ID;
                o.WHT_DEDUCTED_ON_PROFIT = this.WHT_DEDUCTED_ON_PROFIT;
                o.EXPIRY_DATE_PROFIT = this.EXPIRY_DATE_PROFIT;
                o.WHT_DEDUCTED_ON_TRANSACTION = this.WHT_DEDUCTED_ON_TRANSACTION;
                o.EXPIRY_DATE_TRANSACTION = this.EXPIRY_DATE_TRANSACTION;
                db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.OPERATING_INSTRUCTIONS.Add(o);
                db.SaveChanges();
                

            }
        }

        public void Update()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                OPERATING_INSTRUCTIONS o = db.OPERATING_INSTRUCTIONS.FirstOrDefault(b => b.BI_ID == this.BI_ID);
                o.AUTHORITY_TO_OPERATE = this.AUTHORITY_TO_OPERATE.ID;
                o.DESCRIPTION_IF_OTHER = this.DESCRIPTION_IF_OTHER;
                o.ZAKAT_DEDUCTION = this.ZAKAT_DEDUCTION;
                o.ZAKAT_EXEMPTION_TYPE = this.ZAKAT_EXEMPTION_TYPE.ID;
                o.EXEMPTION_REASON_DETAIL = this.EXEMPTION_REASON_DETAIL;
                o.ACCOUNT_STATEMENT_FREQUENCY = this.ACCOUNT_STATEMENT_FREQUENCY.ID;
                o.DESCRIPTION_IF_HOLD_MAIL = this.DESCRIPTION_IF_HOLD_MAIL;
                o.ATM_CARD_REQUIRED = this.ATM_CARD_REQUIRED;
                o.CUSTOMER_NAME_ON_ATMCARD = this.CUSTOMER_NAME_ON_ATMCARD;
                o.E_STATEMENT_REQUIRED = this.E_STATEMENT_REQUIRED.ID;
                o.MOBILE_BANKING_REQUIRED = this.MOBILE_BANKING_REQUIRED;
                o.MOBILE_NO = this.MOBILE_NO;
                o.IBT_ALLOWED = this.IBT_ALLOWED;
                o.IS_PROFIT_APPLICABLE = this.IS_PROFIT_APPLICABLE;
                o.IS_FED_EXEMPTED = this.IS_FED_EXEMPTED;
                o.EXPIRY_DATE_EXEMPTED = this.EXPIRY_DATE_EXEMPTED;
                o.APPLICABLE_PROFIT_RATE = this.APPLICABLE_PROFIT_RATE;
                o.SPECIAL_PROFIT_VALUE = this.SPECIAL_PROFIT_VALUE;
                o.PROFIT_PAYMENT = this.PROFIT_PAYMENT.ID;
                o.WHT_DEDUCTED_ON_PROFIT = this.WHT_DEDUCTED_ON_PROFIT;
                o.EXPIRY_DATE_PROFIT = this.EXPIRY_DATE_PROFIT;
                o.WHT_DEDUCTED_ON_TRANSACTION = this.WHT_DEDUCTED_ON_TRANSACTION;
                o.EXPIRY_DATE_TRANSACTION = this.EXPIRY_DATE_TRANSACTION;
                db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.SaveChanges();


            }
        }

        public bool GetOperatingInstruction(int id)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.OPERATING_INSTRUCTIONS.Where(c => c.BI_ID == id).Any())
                {
                    var a = db.OPERATING_INSTRUCTIONS.FirstOrDefault(b => b.BI_ID == id);
                    this.BI_ID = a.BI_ID;
                    this.AUTHORITY_TO_OPERATE = new AuthorityToOperate { ID = (int)a.AUTHORITY_TO_OPERATE };
                    this.DESCRIPTION_IF_OTHER = a.DESCRIPTION_IF_OTHER;
                    this.ZAKAT_DEDUCTION = a.ZAKAT_DEDUCTION;
                    this.ZAKAT_EXEMPTION_TYPE = new ZakatExemptionType { ID = (int)a.ZAKAT_EXEMPTION_TYPE };
                    this.EXEMPTION_REASON_DETAIL = a.EXEMPTION_REASON_DETAIL;
                    this.ACCOUNT_STATEMENT_FREQUENCY = new AccountStatementFrequency { ID = (int)a.ACCOUNT_STATEMENT_FREQUENCY };
                    this.DESCRIPTION_IF_HOLD_MAIL = a.DESCRIPTION_IF_HOLD_MAIL;
                    this.ATM_CARD_REQUIRED = a.ATM_CARD_REQUIRED;
                    this.CUSTOMER_NAME_ON_ATMCARD = a.CUSTOMER_NAME_ON_ATMCARD;
                    this.E_STATEMENT_REQUIRED = new E_Statement_Required { ID = (int)a.E_STATEMENT_REQUIRED };
                    this.MOBILE_BANKING_REQUIRED = a.MOBILE_BANKING_REQUIRED;
                    this.MOBILE_NO = a.MOBILE_NO;
                    this.IBT_ALLOWED = a.IBT_ALLOWED;
                    this.IS_PROFIT_APPLICABLE = a.IS_PROFIT_APPLICABLE;
                    this.IS_FED_EXEMPTED = a.IS_FED_EXEMPTED;
                    this.EXPIRY_DATE_EXEMPTED = a.EXPIRY_DATE_EXEMPTED;
                    this.APPLICABLE_PROFIT_RATE = a.APPLICABLE_PROFIT_RATE;
                    this.SPECIAL_PROFIT_VALUE = a.SPECIAL_PROFIT_VALUE;
                    this.PROFIT_PAYMENT = new ProfitPayment { ID =(int) a.PROFIT_PAYMENT};
                    this.WHT_DEDUCTED_ON_PROFIT = a.WHT_DEDUCTED_ON_PROFIT;
                    this.EXPIRY_DATE_PROFIT = a.EXPIRY_DATE_PROFIT;
                    this.WHT_DEDUCTED_ON_TRANSACTION = a.WHT_DEDUCTED_ON_TRANSACTION;
                    this.EXPIRY_DATE_TRANSACTION = a.EXPIRY_DATE_TRANSACTION;


                    return true;
                }
                else
                    return false;

            }

        }

        public bool CheckIndividualOperatinInstruction(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.OPERATING_INSTRUCTIONS.Where(b => b.BI_ID == BID).Any();
            }
        }

    }
}
