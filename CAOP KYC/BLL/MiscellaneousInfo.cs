using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class MiscellaneousInfo
    {
        #region MiscellaneousInfo Model
        public int ID { get; set; }
        public int BI_ID { get; set; }
        public Education EDUCATION { get; set; }
        public string SOCIAL_STATUS { get; set; }
        public AccomodationTypes ACCOMODATION_TYPE { get; set; }
        public string ACCOMODATION_TYPE_DESCRIPTION { get; set; }

        public bool BLIND_VISUALLY_IMPARIED { get; set; }
        public TransportationType TRANSPORTATION_TYPE { get; set; }
        public Pep PEP { get; set; }

        public Nullable<bool> PEP_NATURE_SINGLE { get; set; }
        public string PEP_RELATIONSHIP { get; set; }
        public string PEP_DESC { get; set; }
        public bool PARDA_NASHEEN { get; set; }
        public MonthlyTurnOverDebit MONTHLY_TURNOVER_DEBIT { get; set; }
        public MonthlyTurnOverCredit MONTHLY_TURNOVER_CREDIT { get; set; }
        public AverageCashDeposit AVERAGE_CASH_DEPOSIT { get; set; }
        public AverageNonCashDeposit AVERAGE_CASH_NON_DEPOSIT { get; set; }
        public string TOTAL_ASSET_VALUE { get; set; }
        public string LIABILITIES { get; set; }
        public string NET_WORTH { get; set; }

        public List<Country> MiscellaneousInfoCountryTax { get; set; }
        public string GROSS_SALE { get; set; }
        public FrequencyGrossSale FREQUENCY_GROSS_SALE { get; set; }
        public Nullable<int> SOURCE_OF_FUND { get; set; }

        #endregion 

        public void SaveIndividualMiscellaneousInfo()
        {
            using(CAOPDbContext db = new CAOPDbContext())
            {
                MISCELLANEOUS_INFORMATIONS newMiscellaneousInfo = new MISCELLANEOUS_INFORMATIONS();

                newMiscellaneousInfo.BI_ID = this.BI_ID;
                newMiscellaneousInfo.EDUCATION = this.EDUCATION.ID;
                newMiscellaneousInfo.SOCIAL_STATUS = this.SOCIAL_STATUS.ToUpper();
                newMiscellaneousInfo.ACCOMODATION_TYPE = this.ACCOMODATION_TYPE.ID;
                newMiscellaneousInfo.ACCOMODATION_TYPE_DESCRIPTION = this.ACCOMODATION_TYPE_DESCRIPTION.ToUpper();
                newMiscellaneousInfo.TRANSPORTATION_TYPE = this.TRANSPORTATION_TYPE.ID;
                newMiscellaneousInfo.PEP = this.PEP.ID;
                newMiscellaneousInfo.PEP_NATURE_SINGLE = this.PEP_NATURE_SINGLE;
                newMiscellaneousInfo.PEP_RELATIONSHIP = PEP_RELATIONSHIP;
                newMiscellaneousInfo.PEP_DESC = PEP_DESC;
                newMiscellaneousInfo.PARDA_NASHEEN = this.PARDA_NASHEEN;
              //  newMiscellaneousInfo.MONTHLY_TURNOVER_DEBIT = this.MONTHLY_TURNOVER_DEBIT.ID;
              //  newMiscellaneousInfo.MONTHLY_TURNOVER_CREDIT = this.MONTHLY_TURNOVER_CREDIT.ID;
             //   newMiscellaneousInfo.AVERAGE_CASH_DEPOSIT = this.AVERAGE_CASH_DEPOSIT.ID;
             //   newMiscellaneousInfo.AVERAGE_CASH_NON_DEPOSIT = this.AVERAGE_CASH_NON_DEPOSIT.ID;
                newMiscellaneousInfo.TOTAL_ASSET_VALUE = this.TOTAL_ASSET_VALUE;
                newMiscellaneousInfo.LIABILITIES = this.LIABILITIES;
                newMiscellaneousInfo.NET_WORTH = this.NET_WORTH;
                newMiscellaneousInfo.BLIND_VISUALLY_IMPAIRED = this.BLIND_VISUALLY_IMPARIED;
                newMiscellaneousInfo.SOURCE_OF_FUND = this.SOURCE_OF_FUND;
                newMiscellaneousInfo.BI_ID = this.BI_ID;
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;


                db.MISCELLANEOUS_INFORMATIONS.Add(newMiscellaneousInfo);
                db.SaveChanges();


                foreach (var country in MiscellaneousInfoCountryTax)
                {
                    COUNTRIES_TAX_MISCELLANEOUS_INFORMATION ctax = new COUNTRIES_TAX_MISCELLANEOUS_INFORMATION();

                    ctax.BI_ID = this.BI_ID;
                    ctax.COUNTRY_ID = country.ID;
                    ctax.COUNTRY_NAME = country.Name;

                    db.COUNTRIES_TAX_MISCELLANEOUS_INFORMATION.Add(ctax);
                }

                db.SaveChanges();


            }
        }

        public void UpdateIndividualMiscellaneousInfo()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                MISCELLANEOUS_INFORMATIONS newMiscellaneousInfo = db.MISCELLANEOUS_INFORMATIONS.FirstOrDefault(m => m.BI_ID == this.BI_ID);

                newMiscellaneousInfo.BI_ID = this.BI_ID;
                newMiscellaneousInfo.EDUCATION = this.EDUCATION.ID;
                newMiscellaneousInfo.SOCIAL_STATUS = this.SOCIAL_STATUS.ToUpper();
                newMiscellaneousInfo.ACCOMODATION_TYPE = this.ACCOMODATION_TYPE.ID;
                newMiscellaneousInfo.ACCOMODATION_TYPE_DESCRIPTION = this.ACCOMODATION_TYPE_DESCRIPTION.ToUpper();
                newMiscellaneousInfo.TRANSPORTATION_TYPE = this.TRANSPORTATION_TYPE.ID;
                newMiscellaneousInfo.PEP = this.PEP.ID;
                newMiscellaneousInfo.PEP_NATURE_SINGLE = this.PEP_NATURE_SINGLE;
                newMiscellaneousInfo.PEP_RELATIONSHIP = PEP_RELATIONSHIP;
                newMiscellaneousInfo.PEP_DESC = PEP_DESC;
                newMiscellaneousInfo.PARDA_NASHEEN = this.PARDA_NASHEEN;
             //   newMiscellaneousInfo.MONTHLY_TURNOVER_DEBIT = this.MONTHLY_TURNOVER_DEBIT.ID;
             //   newMiscellaneousInfo.MONTHLY_TURNOVER_CREDIT = this.MONTHLY_TURNOVER_CREDIT.ID;
             //   newMiscellaneousInfo.AVERAGE_CASH_DEPOSIT = this.AVERAGE_CASH_DEPOSIT.ID;
             //   newMiscellaneousInfo.AVERAGE_CASH_NON_DEPOSIT = this.AVERAGE_CASH_NON_DEPOSIT.ID;
                newMiscellaneousInfo.TOTAL_ASSET_VALUE = this.TOTAL_ASSET_VALUE;
                newMiscellaneousInfo.LIABILITIES = this.LIABILITIES;
                newMiscellaneousInfo.NET_WORTH = this.NET_WORTH;
                newMiscellaneousInfo.SOURCE_OF_FUND = this.SOURCE_OF_FUND;
                newMiscellaneousInfo.BLIND_VISUALLY_IMPAIRED = this.BLIND_VISUALLY_IMPARIED;
                db.COUNTRIES_TAX_MISCELLANEOUS_INFORMATION.RemoveRange(db.COUNTRIES_TAX_MISCELLANEOUS_INFORMATION.Where(n => n.BI_ID == this.ID));
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;


                db.SaveChanges();


                foreach (var country in MiscellaneousInfoCountryTax)
                {
                    COUNTRIES_TAX_MISCELLANEOUS_INFORMATION ctax = new COUNTRIES_TAX_MISCELLANEOUS_INFORMATION();
                    ctax.BI_ID = this.BI_ID;
                    ctax.COUNTRY_ID = country.ID;
                    ctax.COUNTRY_NAME = country.Name;
                    db.COUNTRIES_TAX_MISCELLANEOUS_INFORMATION.Add(ctax);
                }
                db.SaveChanges();

            }
        }


        public bool CheckIndividualMiscInfo(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
               return db.MISCELLANEOUS_INFORMATIONS.Where(b => b.BI_ID == BID).Any();
            }
        }
        public void SaveBusinessMiscellaneousInfo()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                MISCELLANEOUS_INFORMATIONS newMiscellaneousInfo = new MISCELLANEOUS_INFORMATIONS();

                newMiscellaneousInfo.BI_ID = this.BI_ID;
                newMiscellaneousInfo.TOTAL_ASSET_VALUE = this.TOTAL_ASSET_VALUE;
                newMiscellaneousInfo.LIABILITIES = this.LIABILITIES;
                newMiscellaneousInfo.NET_WORTH = this.NET_WORTH;
            //    newMiscellaneousInfo.MONTHLY_TURNOVER_DEBIT = this.MONTHLY_TURNOVER_DEBIT.ID;
            //    newMiscellaneousInfo.MONTHLY_TURNOVER_CREDIT = this.MONTHLY_TURNOVER_CREDIT.ID;
            //    newMiscellaneousInfo.AVERAGE_CASH_DEPOSIT = this.AVERAGE_CASH_DEPOSIT.ID;
            //    newMiscellaneousInfo.AVERAGE_CASH_NON_DEPOSIT = this.AVERAGE_CASH_NON_DEPOSIT.ID;
                newMiscellaneousInfo.SOURCE_OF_FUND = this.SOURCE_OF_FUND;
                newMiscellaneousInfo.GROSS_SALE = this.GROSS_SALE.ToUpper(); 
                newMiscellaneousInfo.FREQUENCY_GROSS_SALE = this.FREQUENCY_GROSS_SALE.ID;
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.MISCELLANEOUS_INFORMATIONS.Add(newMiscellaneousInfo);
                db.SaveChanges();

            }
        }

        public void UpdateBusinessMiscellaneousInfo()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                MISCELLANEOUS_INFORMATIONS newMiscellaneousInfo = db.MISCELLANEOUS_INFORMATIONS.FirstOrDefault(m => m.BI_ID == this.BI_ID);
                newMiscellaneousInfo.TOTAL_ASSET_VALUE = this.TOTAL_ASSET_VALUE;
                newMiscellaneousInfo.LIABILITIES = this.LIABILITIES;
                newMiscellaneousInfo.NET_WORTH = this.NET_WORTH;
             //   newMiscellaneousInfo.MONTHLY_TURNOVER_DEBIT = this.MONTHLY_TURNOVER_DEBIT.ID;
            //    newMiscellaneousInfo.MONTHLY_TURNOVER_CREDIT = this.MONTHLY_TURNOVER_CREDIT.ID;
            //    newMiscellaneousInfo.AVERAGE_CASH_DEPOSIT = this.AVERAGE_CASH_DEPOSIT.ID;
            //    newMiscellaneousInfo.AVERAGE_CASH_NON_DEPOSIT = this.AVERAGE_CASH_NON_DEPOSIT.ID;
                newMiscellaneousInfo.SOURCE_OF_FUND = this.SOURCE_OF_FUND;
                newMiscellaneousInfo.GROSS_SALE = this.GROSS_SALE.ToUpper();
                newMiscellaneousInfo.FREQUENCY_GROSS_SALE = this.FREQUENCY_GROSS_SALE.ID;
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.SaveChanges();

            }
        }

        public bool GetIndividualMiscellaneousInfo(int BI_ID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.MISCELLANEOUS_INFORMATIONS.Where(m => m.BI_ID == BI_ID).Any())
                {

                    var mInfo = db.MISCELLANEOUS_INFORMATIONS.First(m => m.BI_ID == BI_ID);

                    this.BI_ID = (int) mInfo.BI_ID;
                    this.EDUCATION = new Education { ID = (int)mInfo.EDUCATION };
                    this.SOCIAL_STATUS = mInfo.SOCIAL_STATUS;
                    this.ACCOMODATION_TYPE = new AccomodationTypes { ID = (int) mInfo.ACCOMODATION_TYPE };
                    this.ACCOMODATION_TYPE_DESCRIPTION = mInfo.ACCOMODATION_TYPE_DESCRIPTION;
                    this.TRANSPORTATION_TYPE = new TransportationType { ID = mInfo.TRANSPORTATION_TYPE };
                    this.PEP = new Pep { ID = mInfo.PEP };
                    this.PEP_NATURE_SINGLE = mInfo.PEP_NATURE_SINGLE;
                    this.PEP_RELATIONSHIP = mInfo.PEP_RELATIONSHIP;
                    this.PEP_DESC = mInfo.PEP_DESC;
                    this.PARDA_NASHEEN = (bool) mInfo.PARDA_NASHEEN;
                   // this.MONTHLY_TURNOVER_DEBIT = new MonthlyTurnOverDebit { ID = (int) mInfo.MONTHLY_TURNOVER_DEBIT };
                   // this.MONTHLY_TURNOVER_CREDIT = new MonthlyTurnOverCredit { ID = (int)mInfo.MONTHLY_TURNOVER_CREDIT };
                   // this.AVERAGE_CASH_DEPOSIT = new AverageCashDeposit { ID = (int)mInfo.AVERAGE_CASH_DEPOSIT };
                  //  this.AVERAGE_CASH_NON_DEPOSIT = new AverageNonCashDeposit { ID = (int)mInfo.AVERAGE_CASH_NON_DEPOSIT };
                    this.TOTAL_ASSET_VALUE = mInfo.TOTAL_ASSET_VALUE;
                    this.LIABILITIES = mInfo.LIABILITIES;
                    this.NET_WORTH = mInfo.NET_WORTH;
                    this.SOURCE_OF_FUND = mInfo.SOURCE_OF_FUND;
                    this.BLIND_VISUALLY_IMPARIED = (bool) mInfo.BLIND_VISUALLY_IMPAIRED;
                    this.BI_ID = (int) mInfo.BI_ID;

                    MiscellaneousInfoCountryTax = db.COUNTRIES_TAX_MISCELLANEOUS_INFORMATION.Where(t => t.BI_ID == BI_ID).Select(t => new Country { ID = (int)t.COUNTRY_ID, Name = t.COUNTRY_NAME }).ToList();

                    return true;
                }
                else
                    return false;
            }
        }

        public bool GetBusinessMiscellaneousInfo(int BI_ID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.MISCELLANEOUS_INFORMATIONS.Where(m => m.BI_ID == BI_ID).Any())
                {
                    var mInfo = db.MISCELLANEOUS_INFORMATIONS.First(m => m.BI_ID == BI_ID);

                    this.BI_ID = (int) mInfo.BI_ID;
                    this.TOTAL_ASSET_VALUE = mInfo.TOTAL_ASSET_VALUE;
                    this.LIABILITIES = mInfo.LIABILITIES;
                    this.NET_WORTH = mInfo.NET_WORTH;
                    this.SOURCE_OF_FUND = mInfo.SOURCE_OF_FUND;
                 //   this.MONTHLY_TURNOVER_DEBIT = new MonthlyTurnOverDebit { ID = (int) mInfo.MONTHLY_TURNOVER_DEBIT };
                 //   this.MONTHLY_TURNOVER_CREDIT = new MonthlyTurnOverCredit { ID = (int) mInfo.MONTHLY_TURNOVER_CREDIT };
                //    this.AVERAGE_CASH_DEPOSIT = new AverageCashDeposit { ID = (int) mInfo.AVERAGE_CASH_DEPOSIT };
                //    this.AVERAGE_CASH_NON_DEPOSIT = new AverageNonCashDeposit { ID = (int)mInfo.AVERAGE_CASH_NON_DEPOSIT } ;
                    this.GROSS_SALE = mInfo.GROSS_SALE;
                    this.FREQUENCY_GROSS_SALE = new FrequencyGrossSale { ID = (int)mInfo.FREQUENCY_GROSS_SALE };
                   

                    return true;
                }
                else
                    return false;
            }
        }
    }
}
