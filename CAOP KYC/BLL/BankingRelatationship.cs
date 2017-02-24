using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class BankingRelatationship
   {

       #region Model BankingRelatationship
       public int ID { get; set; }
        public int BI_ID { get; set; }
        public NbpBranchInformation NBP_BRANCH_INFORMATION { get; set; }
        public AccountType NBP_ACCOUNT_TYPE { get; set; }
        public string NBP_ACCOUNT_NUMBER { get; set; }
        public string NBP_ACCOUNT_TITLE { get; set; }
        public string NBP_RELATIONSHIP_SINCE { get; set; }
        public OtherBankCodes OTHER_BANK_CODE { get; set; }
        public string OTHER_BRANCH_NAME { get; set; }
        public string OTHER_ACCOUNT_NUMBER { get; set; }
        public string OTHER_ACCOUNT_TITLE { get; set; }
        public string OTHER_RELATIONSHIP_SINCE { get; set; }

       #endregion

        public void SaveBankingRelatationship()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                BANKING_RELATIONSHPS newBR = new BANKING_RELATIONSHPS();

                newBR.BI_ID = this.BI_ID;
                newBR.NBP_BRANCH_INFORMATION = this.NBP_BRANCH_INFORMATION.ID;
                newBR.NBP_ACCOUNT_TYPE = this.NBP_ACCOUNT_TYPE.ID;
                newBR.NBP_ACCOUNT_NUMBER = this.NBP_ACCOUNT_NUMBER.ToUpper();
                newBR.NBP_ACCOUNT_TITLE = this.NBP_ACCOUNT_TITLE.ToUpper();
                newBR.NBP_RELATIONSHIP_SINCE = this.NBP_RELATIONSHIP_SINCE.ToUpper();
                newBR.OTHER_BANK_CODE = this.OTHER_BANK_CODE.ID;
                newBR.OTHER_BRANCH_NAME = this.OTHER_BRANCH_NAME.ToUpper();
                newBR.OTHER_ACCOUNT_NUMBER = this.OTHER_ACCOUNT_NUMBER.ToUpper();
                newBR.OTHER_ACCOUNT_TITLE = this.OTHER_ACCOUNT_TITLE.ToUpper();
                newBR.OTHER_RELATIONSHIP_SINCE = this.OTHER_RELATIONSHIP_SINCE.ToUpper();
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;


                db.BANKING_RELATIONSHPS.Add(newBR);
                db.SaveChanges();
            }
        }

        public void UpdateBankingRelationship()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                BANKING_RELATIONSHPS newBR = db.BANKING_RELATIONSHPS.FirstOrDefault(b => b.BI_ID == this.BI_ID);

                newBR.NBP_BRANCH_INFORMATION = this.NBP_BRANCH_INFORMATION.ID;
                newBR.NBP_ACCOUNT_TYPE = this.NBP_ACCOUNT_TYPE.ID;
                newBR.NBP_ACCOUNT_NUMBER = this.NBP_ACCOUNT_NUMBER.ToUpper();
                newBR.NBP_ACCOUNT_TITLE = this.NBP_ACCOUNT_TITLE.ToUpper();
                newBR.NBP_RELATIONSHIP_SINCE = this.NBP_RELATIONSHIP_SINCE.ToUpper();
                newBR.OTHER_BANK_CODE = this.OTHER_BANK_CODE.ID;
                newBR.OTHER_BRANCH_NAME = this.OTHER_BRANCH_NAME.ToUpper();
                newBR.OTHER_ACCOUNT_NUMBER = this.OTHER_ACCOUNT_NUMBER.ToUpper();
                newBR.OTHER_ACCOUNT_TITLE = this.OTHER_ACCOUNT_TITLE.ToUpper();
                newBR.OTHER_RELATIONSHIP_SINCE = this.OTHER_RELATIONSHIP_SINCE;
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;


                db.SaveChanges();
            }
        }
        //Process Starts Again
        public void UpdateBankingRelationshipNew()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                BANKING_RELATIONSHPS newBR = db.BANKING_RELATIONSHPS.FirstOrDefault(b => b.BI_ID == this.BI_ID);

                newBR.NBP_BRANCH_INFORMATION = this.NBP_BRANCH_INFORMATION.ID;
                newBR.NBP_ACCOUNT_TYPE = this.NBP_ACCOUNT_TYPE.ID;
                newBR.NBP_ACCOUNT_NUMBER = this.NBP_ACCOUNT_NUMBER.ToUpper();
                newBR.NBP_ACCOUNT_TITLE = this.NBP_ACCOUNT_TITLE.ToUpper();
                newBR.NBP_RELATIONSHIP_SINCE = this.NBP_RELATIONSHIP_SINCE.ToUpper();
                newBR.OTHER_BANK_CODE = this.OTHER_BANK_CODE.ID;
                newBR.OTHER_BRANCH_NAME = this.OTHER_BRANCH_NAME.ToUpper();
                newBR.OTHER_ACCOUNT_NUMBER = this.OTHER_ACCOUNT_NUMBER.ToUpper();
                newBR.OTHER_ACCOUNT_TITLE = this.OTHER_ACCOUNT_TITLE.ToUpper();
                newBR.OTHER_RELATIONSHIP_SINCE = this.OTHER_RELATIONSHIP_SINCE;
               db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).STATUS = Status.UPDATED_BY_BRANCH_OPERATOR.ToString();


                db.SaveChanges();
            }
        }

 

        public bool CheckBankingRelationship(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.BANKING_RELATIONSHPS.Where(b => b.BI_ID == BID).Any();
            }
        }
        public bool GetBanikingRelationship(int BI_ID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.BANKING_RELATIONSHPS.Where(b => b.BI_ID == BI_ID).Any())
                {
                    var BR = db.BANKING_RELATIONSHPS.FirstOrDefault(b => b.BI_ID == BI_ID);


                    this.BI_ID = (int) BR.BI_ID;
                    this.NBP_BRANCH_INFORMATION = new NbpBranchInformation { ID =  BR.NBP_BRANCH_INFORMATION };
                    this.NBP_ACCOUNT_TYPE = new AccountType { ID = (int) BR.NBP_ACCOUNT_TYPE };
                    this.NBP_ACCOUNT_NUMBER = BR.NBP_ACCOUNT_NUMBER;
                    this.NBP_ACCOUNT_TITLE = BR.NBP_ACCOUNT_TITLE;
                    this.NBP_RELATIONSHIP_SINCE = BR.NBP_RELATIONSHIP_SINCE;
                    this.OTHER_BANK_CODE = new OtherBankCodes { ID = BR.OTHER_BANK_CODE };
                    this.OTHER_BRANCH_NAME = BR.OTHER_BRANCH_NAME;
                    this.OTHER_ACCOUNT_NUMBER = BR.OTHER_ACCOUNT_NUMBER;
                    this.OTHER_ACCOUNT_TITLE = BR.OTHER_ACCOUNT_TITLE;
                    this.OTHER_RELATIONSHIP_SINCE = BR.OTHER_RELATIONSHIP_SINCE;


                    return true;
                }
                else
                    return false;
            }
        }
   }
}
