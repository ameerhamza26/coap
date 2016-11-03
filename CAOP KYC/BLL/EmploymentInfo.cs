using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class EmploymentInfo
   {

       #region EmploymentInfo Model
       public int ID { get; set; }
        public int BI_ID { get; set; }
        public EmploymentDetail EMPLOYMENT_DETAIL { get; set; }
        public string EMPLOYMENT_DETAIL_OTHER_DESCRIPTION { get; set; }
        public ConsumerSegment CONSUMER_SEGMENT { get; set; }
        public string DEPARTMENT { get; set; }
        public bool RETIRED { get; set; }
        public string DESIGNATION { get; set; }
        public string PF_NO { get; set; }
        public string PPQ_NO { get; set; }
        public EmployerCodes EMPLOYER_CODE { get; set; }
        public string EMPLOYER_DESC { get; set; }
        public string EMPLOYER_BUSINESS_ADDRESS { get; set; }
        public ArmyRankCodes ARMY_RANK_CODE { get; set; }
        public Country COUNTRY_EMPLOYMENT { get; set; }

        public Nullable<int> EMPLOYER_GROUP { get; set; }
        public Nullable<int> EMPLOYER_SUB_GROUP { get; set; }
        public Nullable<int> EMPLOYER_NUMBER { get; set; }

        #endregion 

        public void SaveEmploymentInfo()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                EMPLOYMENT_INFORMATIONS EmpInfo = new EMPLOYMENT_INFORMATIONS();

                EmpInfo.BI_ID = this.BI_ID;
                EmpInfo.EMPLOYMENT_DETAIL = this.EMPLOYMENT_DETAIL.ID;
                EmpInfo.EMPLOYMENT_DETAIL_OTHER_DESCRIPTION = this.EMPLOYMENT_DETAIL_OTHER_DESCRIPTION;
                EmpInfo.CONSUMER_SEGMENT = this.CONSUMER_SEGMENT.ID;
                EmpInfo.DEPARTMENT = this.DEPARTMENT.ToUpper();
                EmpInfo.RETIRED = this.RETIRED;
                EmpInfo.DESIGNATION = this.DESIGNATION.ToUpper();
                EmpInfo.PF_NO = this.PF_NO.ToUpper();
                EmpInfo.PPQ_NO = this.PPQ_NO.ToUpper();
                EmpInfo.EMPLOYER_DESC = this.EMPLOYER_DESC;
                EmpInfo.EMPLOYER_CODE = this.EMPLOYER_CODE.ID;
                EmpInfo.EMPLOYER_BUSINESS_ADDRESS = this.EMPLOYER_BUSINESS_ADDRESS.ToUpper();
                EmpInfo.ARMY_RANK_CODE = this.ARMY_RANK_CODE.ID;
                EmpInfo.COUNTRY_EMPLOYMENT = this.COUNTRY_EMPLOYMENT.ID;
                EmpInfo.EMPLOYER_GROUP = this.EMPLOYER_GROUP;
                EmpInfo.EMPLOYER_SUB_GROUP = this.EMPLOYER_SUB_GROUP;
                EmpInfo.EMPLOYER_NUMBER = this.EMPLOYER_NUMBER;

                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.EMPLOYMENT_INFORMATIONS.Add(EmpInfo);

                db.SaveChanges();

            }
        }

        public void UpdateEmploymentInfo()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                EMPLOYMENT_INFORMATIONS EmpInfo = db.EMPLOYMENT_INFORMATIONS.FirstOrDefault(e => e.BI_ID == this.BI_ID);

                EmpInfo.CONSUMER_SEGMENT = this.CONSUMER_SEGMENT.ID;
                EmpInfo.EMPLOYMENT_DETAIL = this.EMPLOYMENT_DETAIL.ID;
                EmpInfo.EMPLOYMENT_DETAIL_OTHER_DESCRIPTION = this.EMPLOYMENT_DETAIL_OTHER_DESCRIPTION;
                EmpInfo.DEPARTMENT = this.DEPARTMENT.ToUpper();
                EmpInfo.RETIRED = this.RETIRED;
                EmpInfo.DESIGNATION = this.DESIGNATION.ToUpper();
                EmpInfo.PF_NO = this.PF_NO.ToUpper();
                EmpInfo.PPQ_NO = this.PPQ_NO.ToUpper();
                EmpInfo.EMPLOYER_DESC = this.EMPLOYER_DESC;
                EmpInfo.EMPLOYER_CODE = this.EMPLOYER_CODE.ID;
                EmpInfo.EMPLOYER_BUSINESS_ADDRESS = this.EMPLOYER_BUSINESS_ADDRESS.ToUpper();
                EmpInfo.ARMY_RANK_CODE = this.ARMY_RANK_CODE.ID;
                EmpInfo.COUNTRY_EMPLOYMENT = this.COUNTRY_EMPLOYMENT.ID;
                EmpInfo.EMPLOYER_GROUP = this.EMPLOYER_GROUP;
                EmpInfo.EMPLOYER_SUB_GROUP = this.EMPLOYER_SUB_GROUP;
                EmpInfo.EMPLOYER_NUMBER = this.EMPLOYER_NUMBER;

                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.SaveChanges();

            }
        }


        public bool CheckIndividualEmploymentInfo(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.EMPLOYMENT_INFORMATIONS.Where(b => b.BI_ID == BID).Any();
            }
        }
        public bool GetIndividualEmploymentInfo(int BI_ID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.EMPLOYMENT_INFORMATIONS.Where(e => e.BI_ID == BI_ID).Any())
                {
                    var EInfo = db.EMPLOYMENT_INFORMATIONS.FirstOrDefault(e => e.BI_ID == BI_ID);

                    this.BI_ID = (int) EInfo.BI_ID;
                    this.EMPLOYMENT_DETAIL = new EmploymentDetail { ID = (int) EInfo.EMPLOYMENT_DETAIL };
                    this.EMPLOYMENT_DETAIL_OTHER_DESCRIPTION = EInfo.EMPLOYMENT_DETAIL_OTHER_DESCRIPTION;
                    this.CONSUMER_SEGMENT = new ConsumerSegment { ID = (int)EInfo.CONSUMER_SEGMENT };
                    this.DEPARTMENT = EInfo.DEPARTMENT;
                    this.RETIRED = (bool) EInfo.RETIRED;
                    this.DESIGNATION = EInfo.DESIGNATION;
                    this.PF_NO = EInfo.PF_NO;
                    this.PPQ_NO = EInfo.PPQ_NO;
                    this.EMPLOYER_DESC = EInfo.EMPLOYER_DESC;
                    this.EMPLOYER_CODE = new EmployerCodes { ID = EInfo.EMPLOYER_CODE };
                    this.EMPLOYER_BUSINESS_ADDRESS = EInfo.EMPLOYER_BUSINESS_ADDRESS;
                    this.ARMY_RANK_CODE = new ArmyRankCodes { ID =  EInfo.ARMY_RANK_CODE };
                    this.COUNTRY_EMPLOYMENT = new Country { ID =  EInfo.COUNTRY_EMPLOYMENT };
                    this.EMPLOYER_GROUP = EInfo.EMPLOYER_GROUP;
                    this.EMPLOYER_SUB_GROUP = EInfo.EMPLOYER_SUB_GROUP;
                    this.EMPLOYER_NUMBER = EInfo.EMPLOYER_NUMBER;

                    return true;
                }
                else
                    return false;
            }
        }
    }
}
