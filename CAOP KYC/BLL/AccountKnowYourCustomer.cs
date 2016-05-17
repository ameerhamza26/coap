using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class AccountKnowYourCustomer
    {

        public int ID { get; set; }
        public Nullable<int> BI_ID { get; set; }
        public CustomerType CUSTOMER_TYPE { get; set; }
        public string DESCRIPTION_IF_REFFERED { get; set; }

        public Reason_Account_Opening RAC { get; set; }
        public string RAC_DETAIL { get; set; }

        public Education EDUCATION { get; set; }
      //  public PurposeOfAccount PURPOSE_OF_ACCOUNT { get; set; }

        public List<int> PURPOSE_OF_ACCOUNT = new List<int>();
        public string DESCRIPTION_IF_OTHER { get; set; }
       //  public SourceOfFunds SOURCE_OF_FUNDS { get; set; }

        public List<int> SOURCE_OF_FUNDS = new List<int>();
        public string DESCRIPTION_OF_SOURCE { get; set; }
        public Nullable<bool> SERVICE_CHARGES_EXEMPTED { get; set; }
        public ServiceChargesExemptedCode SERVICE_CHARGES_EXEMPTED_CODE { get; set; }
        public string REASON_IF_EXEMPTED { get; set; }
        public string EXPECTED_MONTHLY_INCOME { get; set; }
        public bool MODE_OF_TRANSACTIONS { get; set; }
        public string OTHER_MODE_OF_TRANSACTIONS { get; set; }
        public string MAX_TRANS_AMOUNT_DR { get; set; }
        public string MAX_TRANS_AMOUNT_CR { get; set; }
      //  public string RELATIONSHIP_MANAGER { get; set; }

        public Know_Your_Customer_Relationship RELATIONSHIP_MANAGER;
        public Nullable<bool> OCCUPATION_VERIFIED { get; set; }
        public int ADDRESS_VERIFIED { get; set; }
        public MeansOfVerification MEANS_OF_VERIFICATION { get; set; }
        public string MEANS_OF_VERI_OTHER { get; set; }
        public Nullable<bool> IS_VERI_SATISFACTORY { get; set; }
        public string DETAIL_IF_NOT_SATISFACTORY { get; set; }
        public Country COUNTRY_HOME_REMITTANCE { get; set; }
        public RealBeneficiaryAccount REAL_BENEFICIARY_ACCOUNT { get; set; }
        public string NAME_OTHER { get; set; }
        public string CNIC_OTHER { get; set; }
        public Relationship RELATIONSHIP_WITH_ACCOUNTHOLDER { get; set; }
        public string RELATIONSHIP_DETAIL_OTHER { get; set; }

        public string BENEFICIAL_ADDRESS { get; set; }
        public Nullable<int> BENEFICIAL_NATIONALITY { get; set; }
        public Nullable<int> BENEFICIAL_RESIDENCE { get; set; }
        public string BENEFICIAL_RESIDENCE_DESC { get; set; }
        public Nullable<int> BENEFICIAL_IDENTITY { get; set; }
        public string BENEFICIAL_IDENTITY_EXPIRY { get; set; }
        public Nullable<int> BENEFICIAL_SOURCE_OF_FUND { get; set; }

        public List<KycBeneficialEntity> KycBeneficial = new List<KycBeneficialEntity>();
        public string NODT { get; set; }
        public string PEDT { get; set; }
        public string NOCT { get; set; }
        public string PECT { get; set; }

        public List<int> KYC_EXPECTED_COUNTER_PARTIES = new List<int>();
        public List<int> KYC_GEOGRAPHIES_COUNTER_PARTIES = new List<int>();


        public void SaveKnowYourCustomer()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                KNOW_YOUR_CUSTOMER k = new KNOW_YOUR_CUSTOMER();             
                k.BI_ID = this.BI_ID;
                k.CUSTOMER_TYPE = this.CUSTOMER_TYPE.ID;
                k.RAC = this.RAC.ID;
                k.RAC_DETAIL = this.RAC_DETAIL;
                k.DESCRIPTION_IF_REFFERED = this.DESCRIPTION_IF_REFFERED;
                k.EDUCATION = this.EDUCATION.ID;
              //  k.PURPOSE_OF_ACCOUNT = this.PURPOSE_OF_ACCOUNT.ID;
                k.DESCRIPTION_IF_OTHER = this.DESCRIPTION_IF_OTHER;
               // k.SOURCE_OF_FUNDS = this.SOURCE_OF_FUNDS.ID;
                k.DESCRIPTION_OF_SOURCE = this.DESCRIPTION_OF_SOURCE;
                k.SERVICE_CHARGES_EXEMPTED = this.SERVICE_CHARGES_EXEMPTED;
                k.SERVICE_CHARGES_EXEMPTED_CODE = this.SERVICE_CHARGES_EXEMPTED_CODE.ID;
                k.REASON_IF_EXEMPTED = this.REASON_IF_EXEMPTED;
                k.EXPECTED_MONTHLY_INCOME = this.EXPECTED_MONTHLY_INCOME;
                k.MODE_OF_TRANSACTIONS =this.MODE_OF_TRANSACTIONS;
                k.OTHER_MODE_OF_TRANSACTIONS = this.OTHER_MODE_OF_TRANSACTIONS;
                k.MAX_TRANS_AMOUNT_DR = this.MAX_TRANS_AMOUNT_DR;
                k.MAX_TRANS_AMOUNT_CR = this.MAX_TRANS_AMOUNT_CR;
                k.RELATIONSHIP_MANAGER = this.RELATIONSHIP_MANAGER.ID;
                k.OCCUPATION_VERIFIED = this.OCCUPATION_VERIFIED;
                k.ADDRESS_VERIFIED = this.ADDRESS_VERIFIED;
                k.MEANS_OF_VERIFICATION = this.MEANS_OF_VERIFICATION.ID;
                k.MEANS_OF_VERI_OTHER = this.MEANS_OF_VERI_OTHER;
                k.IS_VERI_SATISFACTORY = this.IS_VERI_SATISFACTORY;
                k.DETAIL_IF_NOT_SATISFACTORY = this.DETAIL_IF_NOT_SATISFACTORY;
                k.COUNTRY_HOME_REMITTANCE = this.COUNTRY_HOME_REMITTANCE.ID;

                k.REAL_BENEFICIARY_ACCOUNT = this.REAL_BENEFICIARY_ACCOUNT.ID;
                k.NAME_OTHER = this.NAME_OTHER;
                k.CNIC_OTHER = this.CNIC_OTHER;
                k.RELATIONSHIP_WITH_ACCOUNTHOLDER = this.RELATIONSHIP_WITH_ACCOUNTHOLDER.ID;
                k.RELATIONSHIP_DETAIL_OTHER = this.RELATIONSHIP_DETAIL_OTHER;

                k.BENEFICIAL_ADDRESS = this.BENEFICIAL_ADDRESS;
                k.BENEFICIAL_NATIONALITY = this.BENEFICIAL_NATIONALITY;
                k.BENEFICIAL_RESIDENCE = this.BENEFICIAL_RESIDENCE;
                k.BENEFICIAL_RESIDENCE_DESC = this.BENEFICIAL_RESIDENCE_DESC;
                k.BENEFICIAL_IDENTITY = this.BENEFICIAL_IDENTITY;
                k.BENEFICIAL_IDENTITY_EXPIRY = this.BENEFICIAL_IDENTITY_EXPIRY;
                k.BENEFICIAL_SOURCE_OF_FUND = this.BENEFICIAL_SOURCE_OF_FUND;
                k.NODT = this.NODT;
                k.PEDT = this.PEDT;
                k.NOCT = this.NOCT;
                k.PECT = this.PECT;

                db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.KNOW_YOUR_CUSTOMER.Add(k);
                db.SaveChanges();

                foreach (int POF in this.PURPOSE_OF_ACCOUNT)
                {
                    KYC_PURPOSE_OF_AC newPOA = new KYC_PURPOSE_OF_AC();
                    newPOA.BID = (int) this.BI_ID;
                    newPOA.PURPOSE_OF_AC = POF;

                    db.KYC_PURPOSE_OF_AC.Add(newPOA);
                }

                foreach (int SOF in this.SOURCE_OF_FUNDS)
                {
                    KYC_SOURCE_OF_FUND NewSof = new KYC_SOURCE_OF_FUND();
                    NewSof.BID = (int)this.BI_ID;
                    NewSof.SOURCE_OF_FUND = SOF;

                    db.KYC_SOURCE_OF_FUND.Add(NewSof);
                }

                foreach (int ECP in this.KYC_EXPECTED_COUNTER_PARTIES)
                {
                    KYC_EXPECTED_COUNTER_PARTIES NECP = new KYC_EXPECTED_COUNTER_PARTIES();
                    NECP.BID = this.BI_ID;
                    NECP.EXPECTED_CP = ECP;

                    db.KYC_EXPECTED_COUNTER_PARTIES.Add(NECP);
                }

                foreach (int GCP in this.KYC_GEOGRAPHIES_COUNTER_PARTIES)
                {
                    KYC_GEOGRAPHIES_COUNTER_PARTIES NGCP = new KYC_GEOGRAPHIES_COUNTER_PARTIES();
                    NGCP.BID = this.BI_ID;
                    NGCP.GEOGRAPHIES_CP = GCP;

                    db.KYC_GEOGRAPHIES_COUNTER_PARTIES.Add(NGCP);
                }

                db.SaveChanges();



            }
        }

        public void SaveKnowYourCustomerBusiness()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                KNOW_YOUR_CUSTOMER k = new KNOW_YOUR_CUSTOMER();
                k.BI_ID = this.BI_ID;
                k.CUSTOMER_TYPE = this.CUSTOMER_TYPE.ID;
                k.RAC = this.RAC.ID;
                k.RAC_DETAIL = this.RAC_DETAIL;
                k.DESCRIPTION_IF_REFFERED = this.DESCRIPTION_IF_REFFERED;
                k.EDUCATION = this.EDUCATION.ID;
                k.DESCRIPTION_IF_OTHER = this.DESCRIPTION_IF_OTHER;
                k.DESCRIPTION_OF_SOURCE = this.DESCRIPTION_OF_SOURCE;
                k.SERVICE_CHARGES_EXEMPTED = this.SERVICE_CHARGES_EXEMPTED;
                k.SERVICE_CHARGES_EXEMPTED_CODE = this.SERVICE_CHARGES_EXEMPTED_CODE.ID;
                k.REASON_IF_EXEMPTED = this.REASON_IF_EXEMPTED;
                k.EXPECTED_MONTHLY_INCOME = this.EXPECTED_MONTHLY_INCOME;
                k.MODE_OF_TRANSACTIONS = this.MODE_OF_TRANSACTIONS;
                k.OTHER_MODE_OF_TRANSACTIONS = this.OTHER_MODE_OF_TRANSACTIONS;
                k.MAX_TRANS_AMOUNT_DR = this.MAX_TRANS_AMOUNT_DR;
                k.MAX_TRANS_AMOUNT_CR = this.MAX_TRANS_AMOUNT_CR;
                k.RELATIONSHIP_MANAGER = this.RELATIONSHIP_MANAGER.ID;
                k.OCCUPATION_VERIFIED = this.OCCUPATION_VERIFIED;
                k.ADDRESS_VERIFIED = this.ADDRESS_VERIFIED;
                k.MEANS_OF_VERIFICATION = this.MEANS_OF_VERIFICATION.ID;
                k.MEANS_OF_VERI_OTHER = this.MEANS_OF_VERI_OTHER;
                k.IS_VERI_SATISFACTORY = this.IS_VERI_SATISFACTORY;
                k.DETAIL_IF_NOT_SATISFACTORY = this.DETAIL_IF_NOT_SATISFACTORY;
                k.COUNTRY_HOME_REMITTANCE = this.COUNTRY_HOME_REMITTANCE.ID;
                k.REAL_BENEFICIARY_ACCOUNT = this.REAL_BENEFICIARY_ACCOUNT.ID;
                k.NODT = this.NODT;
                k.PEDT = this.PEDT;
                k.NOCT = this.NOCT;
                k.PECT = this.PECT;

                db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.KNOW_YOUR_CUSTOMER.Add(k);

                if (this.KycBeneficial.Count > 0)
                {
                    foreach (var kb in this.KycBeneficial)
                    {
                        KYC_BENEFICIAL_ENTITY nkyc = new KYC_BENEFICIAL_ENTITY();
                        nkyc.BID = this.BI_ID;
                        nkyc.NAME = kb.NAME;
                        nkyc.IDENTITY_DOCUMENT = kb.IDENTITY_DOCUMENT;
                        nkyc.IDENTITY_NUMBER = kb.IDENTITY_NUMBER;
                        nkyc.EXPIRY_DATE = kb.EXPIRY_DATE;
                        nkyc.POB = kb.POB;
                        nkyc.OWNERSHIP = kb.OWNERSHIP;
                        nkyc.US = kb.US;

                        db.KYC_BENEFICIAL_ENTITY.Add(nkyc);
                    }
                   
                }
               

                foreach (int POF in this.PURPOSE_OF_ACCOUNT)
                {
                    KYC_PURPOSE_OF_AC newPOA = new KYC_PURPOSE_OF_AC();
                    newPOA.BID = (int)this.BI_ID;
                    newPOA.PURPOSE_OF_AC = POF;

                    db.KYC_PURPOSE_OF_AC.Add(newPOA);
                }

                foreach (int SOF in this.SOURCE_OF_FUNDS)
                {
                    KYC_SOURCE_OF_FUND NewSof = new KYC_SOURCE_OF_FUND();
                    NewSof.BID = (int)this.BI_ID;
                    NewSof.SOURCE_OF_FUND = SOF;

                    db.KYC_SOURCE_OF_FUND.Add(NewSof);
                }

                foreach (int ECP in this.KYC_EXPECTED_COUNTER_PARTIES)
                {
                    KYC_EXPECTED_COUNTER_PARTIES NECP = new KYC_EXPECTED_COUNTER_PARTIES();
                    NECP.BID = this.BI_ID;
                    NECP.EXPECTED_CP = ECP;

                    db.KYC_EXPECTED_COUNTER_PARTIES.Add(NECP);
                }

                foreach (int GCP in this.KYC_GEOGRAPHIES_COUNTER_PARTIES)
                {
                    KYC_GEOGRAPHIES_COUNTER_PARTIES NGCP = new KYC_GEOGRAPHIES_COUNTER_PARTIES();
                    NGCP.BID = this.BI_ID;
                    NGCP.GEOGRAPHIES_CP = GCP;

                    db.KYC_GEOGRAPHIES_COUNTER_PARTIES.Add(NGCP);
                }

                db.SaveChanges();
            }

           
        }

        public void Update()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                KNOW_YOUR_CUSTOMER k = db.KNOW_YOUR_CUSTOMER.FirstOrDefault(b => b.BI_ID == this.BI_ID);
             
                k.CUSTOMER_TYPE = this.CUSTOMER_TYPE.ID;
                k.RAC = this.RAC.ID;
                k.RAC_DETAIL = this.RAC_DETAIL;
                k.DESCRIPTION_IF_REFFERED = this.DESCRIPTION_IF_REFFERED;
                k.EDUCATION = this.EDUCATION.ID;
             //   k.PURPOSE_OF_ACCOUNT = this.PURPOSE_OF_ACCOUNT.ID;
                k.DESCRIPTION_IF_OTHER = this.DESCRIPTION_IF_OTHER;
              //  k.SOURCE_OF_FUNDS = this.SOURCE_OF_FUNDS.ID;
                k.DESCRIPTION_OF_SOURCE = this.DESCRIPTION_OF_SOURCE;
                k.SERVICE_CHARGES_EXEMPTED = this.SERVICE_CHARGES_EXEMPTED;
                k.SERVICE_CHARGES_EXEMPTED_CODE = this.SERVICE_CHARGES_EXEMPTED_CODE.ID;
                k.REASON_IF_EXEMPTED = this.REASON_IF_EXEMPTED;
                k.EXPECTED_MONTHLY_INCOME = this.EXPECTED_MONTHLY_INCOME;
                k.MODE_OF_TRANSACTIONS = this.MODE_OF_TRANSACTIONS;
                k.OTHER_MODE_OF_TRANSACTIONS = this.OTHER_MODE_OF_TRANSACTIONS;
                k.MAX_TRANS_AMOUNT_DR = this.MAX_TRANS_AMOUNT_DR;
                k.MAX_TRANS_AMOUNT_CR = this.MAX_TRANS_AMOUNT_CR;
                k.RELATIONSHIP_MANAGER = this.RELATIONSHIP_MANAGER.ID;
                k.OCCUPATION_VERIFIED = this.OCCUPATION_VERIFIED;
                k.ADDRESS_VERIFIED = this.ADDRESS_VERIFIED;
                k.MEANS_OF_VERIFICATION = this.MEANS_OF_VERIFICATION.ID;
                k.MEANS_OF_VERI_OTHER = this.MEANS_OF_VERI_OTHER;
                k.IS_VERI_SATISFACTORY = this.IS_VERI_SATISFACTORY;
                k.DETAIL_IF_NOT_SATISFACTORY = this.DETAIL_IF_NOT_SATISFACTORY;
                k.COUNTRY_HOME_REMITTANCE = this.COUNTRY_HOME_REMITTANCE.ID;

                k.REAL_BENEFICIARY_ACCOUNT = this.REAL_BENEFICIARY_ACCOUNT.ID;
                k.NAME_OTHER = this.NAME_OTHER;
                k.CNIC_OTHER = this.CNIC_OTHER;
                k.RELATIONSHIP_WITH_ACCOUNTHOLDER = this.RELATIONSHIP_WITH_ACCOUNTHOLDER.ID;
                k.RELATIONSHIP_DETAIL_OTHER = this.RELATIONSHIP_DETAIL_OTHER;

                k.BENEFICIAL_ADDRESS = this.BENEFICIAL_ADDRESS;
                k.BENEFICIAL_NATIONALITY = this.BENEFICIAL_NATIONALITY;
                k.BENEFICIAL_RESIDENCE = this.BENEFICIAL_RESIDENCE;
                k.BENEFICIAL_RESIDENCE_DESC = this.BENEFICIAL_RESIDENCE_DESC;
                k.BENEFICIAL_IDENTITY = this.BENEFICIAL_IDENTITY;
                k.BENEFICIAL_IDENTITY_EXPIRY = this.BENEFICIAL_IDENTITY_EXPIRY;
                k.BENEFICIAL_SOURCE_OF_FUND = this.BENEFICIAL_SOURCE_OF_FUND;

                k.NODT = this.NODT;
                k.PEDT = this.PEDT;
                k.NOCT = this.NOCT;
                k.PECT = this.PECT;

                db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;
                db.KYC_PURPOSE_OF_AC.RemoveRange(db.KYC_PURPOSE_OF_AC.Where(p => p.BID == this.BI_ID));
                db.KYC_SOURCE_OF_FUND.RemoveRange(db.KYC_SOURCE_OF_FUND.Where(s => s.BID == this.BI_ID));
                db.KYC_EXPECTED_COUNTER_PARTIES.RemoveRange(db.KYC_EXPECTED_COUNTER_PARTIES.Where(p => p.BID == this.BI_ID));
                db.KYC_GEOGRAPHIES_COUNTER_PARTIES.RemoveRange(db.KYC_GEOGRAPHIES_COUNTER_PARTIES.Where(p => p.GEOGRAPHIES_CP == this.BI_ID));

                db.SaveChanges();


                foreach (int POF in this.PURPOSE_OF_ACCOUNT)
                {
                    KYC_PURPOSE_OF_AC newPOA = new KYC_PURPOSE_OF_AC();
                    newPOA.BID = (int)this.BI_ID;
                    newPOA.PURPOSE_OF_AC = POF;

                    db.KYC_PURPOSE_OF_AC.Add(newPOA);
                }

                foreach (int SOF in this.SOURCE_OF_FUNDS)
                {
                    KYC_SOURCE_OF_FUND NewSof = new KYC_SOURCE_OF_FUND();
                    NewSof.BID = (int)this.BI_ID;
                    NewSof.SOURCE_OF_FUND = SOF;

                    db.KYC_SOURCE_OF_FUND.Add(NewSof);
                }

                foreach (int ECP in this.KYC_EXPECTED_COUNTER_PARTIES)
                {
                    KYC_EXPECTED_COUNTER_PARTIES NECP = new KYC_EXPECTED_COUNTER_PARTIES();
                    NECP.BID = this.BI_ID;
                    NECP.EXPECTED_CP = ECP;

                    db.KYC_EXPECTED_COUNTER_PARTIES.Add(NECP);
                }

                foreach (int GCP in this.KYC_GEOGRAPHIES_COUNTER_PARTIES)
                {
                    KYC_GEOGRAPHIES_COUNTER_PARTIES NGCP = new KYC_GEOGRAPHIES_COUNTER_PARTIES();
                    NGCP.BID = this.BI_ID;
                    NGCP.GEOGRAPHIES_CP = GCP;

                    db.KYC_GEOGRAPHIES_COUNTER_PARTIES.Add(NGCP);
                }

                db.SaveChanges();



            }
        }

        public void UpdateBusiness()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                KNOW_YOUR_CUSTOMER k = db.KNOW_YOUR_CUSTOMER.FirstOrDefault(b => b.BI_ID == this.BI_ID);

                k.CUSTOMER_TYPE = this.CUSTOMER_TYPE.ID;
                k.RAC = this.RAC.ID;
                k.RAC_DETAIL = this.RAC_DETAIL;
                k.DESCRIPTION_IF_REFFERED = this.DESCRIPTION_IF_REFFERED;
                k.EDUCATION = this.EDUCATION.ID;
                k.DESCRIPTION_IF_OTHER = this.DESCRIPTION_IF_OTHER;
                k.DESCRIPTION_OF_SOURCE = this.DESCRIPTION_OF_SOURCE;
                k.SERVICE_CHARGES_EXEMPTED = this.SERVICE_CHARGES_EXEMPTED;
                k.SERVICE_CHARGES_EXEMPTED_CODE = this.SERVICE_CHARGES_EXEMPTED_CODE.ID;
                k.REASON_IF_EXEMPTED = this.REASON_IF_EXEMPTED;
                k.EXPECTED_MONTHLY_INCOME = this.EXPECTED_MONTHLY_INCOME;
                k.MODE_OF_TRANSACTIONS = this.MODE_OF_TRANSACTIONS;
                k.OTHER_MODE_OF_TRANSACTIONS = this.OTHER_MODE_OF_TRANSACTIONS;
                k.MAX_TRANS_AMOUNT_DR = this.MAX_TRANS_AMOUNT_DR;
                k.MAX_TRANS_AMOUNT_CR = this.MAX_TRANS_AMOUNT_CR;
                k.RELATIONSHIP_MANAGER = this.RELATIONSHIP_MANAGER.ID;
                k.OCCUPATION_VERIFIED = this.OCCUPATION_VERIFIED;
                k.ADDRESS_VERIFIED = this.ADDRESS_VERIFIED;
                k.MEANS_OF_VERIFICATION = this.MEANS_OF_VERIFICATION.ID;
                k.MEANS_OF_VERI_OTHER = this.MEANS_OF_VERI_OTHER;
                k.IS_VERI_SATISFACTORY = this.IS_VERI_SATISFACTORY;
                k.DETAIL_IF_NOT_SATISFACTORY = this.DETAIL_IF_NOT_SATISFACTORY;
                k.COUNTRY_HOME_REMITTANCE = this.COUNTRY_HOME_REMITTANCE.ID;
                k.REAL_BENEFICIARY_ACCOUNT = this.REAL_BENEFICIARY_ACCOUNT.ID;
                k.NODT = this.NODT;
                k.PEDT = this.PEDT;
                k.NOCT = this.NOCT;
                k.PECT = this.PECT;

                db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;
                db.KYC_PURPOSE_OF_AC.RemoveRange(db.KYC_PURPOSE_OF_AC.Where(p => p.BID == this.BI_ID));
                db.KYC_SOURCE_OF_FUND.RemoveRange(db.KYC_SOURCE_OF_FUND.Where(s => s.BID == this.BI_ID));
                db.KYC_BENEFICIAL_ENTITY.RemoveRange(db.KYC_BENEFICIAL_ENTITY.Where(b => b.BID == this.BI_ID));
                db.KYC_EXPECTED_COUNTER_PARTIES.RemoveRange(db.KYC_EXPECTED_COUNTER_PARTIES.Where(p => p.BID == this.BI_ID));
                db.KYC_GEOGRAPHIES_COUNTER_PARTIES.RemoveRange(db.KYC_GEOGRAPHIES_COUNTER_PARTIES.Where(p => p.GEOGRAPHIES_CP == this.BI_ID));

                db.SaveChanges();

                if (this.KycBeneficial.Count > 0)
                {
                    foreach (var kb in this.KycBeneficial)
                    {
                        KYC_BENEFICIAL_ENTITY nkyc = new KYC_BENEFICIAL_ENTITY();
                        nkyc.BID = this.BI_ID;
                        nkyc.NAME = kb.NAME;
                        nkyc.IDENTITY_DOCUMENT = kb.IDENTITY_DOCUMENT;
                        nkyc.IDENTITY_NUMBER = kb.IDENTITY_NUMBER;
                        nkyc.EXPIRY_DATE = kb.EXPIRY_DATE;
                        nkyc.POB = kb.POB;
                        nkyc.OWNERSHIP = kb.OWNERSHIP;
                        nkyc.US = kb.US;

                        db.KYC_BENEFICIAL_ENTITY.Add(nkyc);
                    }

                }


                foreach (int POF in this.PURPOSE_OF_ACCOUNT)
                {
                    KYC_PURPOSE_OF_AC newPOA = new KYC_PURPOSE_OF_AC();
                    newPOA.BID = (int)this.BI_ID;
                    newPOA.PURPOSE_OF_AC = POF;

                    db.KYC_PURPOSE_OF_AC.Add(newPOA);
                }

                foreach (int SOF in this.SOURCE_OF_FUNDS)
                {
                    KYC_SOURCE_OF_FUND NewSof = new KYC_SOURCE_OF_FUND();
                    NewSof.BID = (int)this.BI_ID;
                    NewSof.SOURCE_OF_FUND = SOF;

                    db.KYC_SOURCE_OF_FUND.Add(NewSof);
                }

                foreach (int ECP in this.KYC_EXPECTED_COUNTER_PARTIES)
                {
                    KYC_EXPECTED_COUNTER_PARTIES NECP = new KYC_EXPECTED_COUNTER_PARTIES();
                    NECP.BID = this.BI_ID;
                    NECP.EXPECTED_CP = ECP;

                    db.KYC_EXPECTED_COUNTER_PARTIES.Add(NECP);
                }

                foreach (int GCP in this.KYC_GEOGRAPHIES_COUNTER_PARTIES)
                {
                    KYC_GEOGRAPHIES_COUNTER_PARTIES NGCP = new KYC_GEOGRAPHIES_COUNTER_PARTIES();
                    NGCP.BID = this.BI_ID;
                    NGCP.GEOGRAPHIES_CP = GCP;

                    db.KYC_GEOGRAPHIES_COUNTER_PARTIES.Add(NGCP);
                }

                db.SaveChanges();
            }
        }


        public bool GetKnowYourCustomer(int id)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.KNOW_YOUR_CUSTOMER.Where(b=>b.BI_ID == id).Any())
                {
                    var a = db.KNOW_YOUR_CUSTOMER.FirstOrDefault(c => c.BI_ID == id);
                    this.BI_ID = a.BI_ID;
                    this.CUSTOMER_TYPE = new CustomerType { ID = (int)a.CUSTOMER_TYPE };
                    this.RAC = new Reason_Account_Opening { ID = (int?)a.RAC };
                    this.RAC_DETAIL = a.RAC_DETAIL;
                    this.DESCRIPTION_IF_REFFERED = a.DESCRIPTION_IF_REFFERED;
                    this.EDUCATION = new Education { ID = (int)a.EDUCATION };
                  //  this.PURPOSE_OF_ACCOUNT = new PurposeOfAccount { ID = (int)a.PURPOSE_OF_ACCOUNT };
                    this.DESCRIPTION_IF_OTHER = a.DESCRIPTION_IF_OTHER;
                 //   this.SOURCE_OF_FUNDS = new SourceOfFunds { ID = (int)a.SOURCE_OF_FUNDS };
                    this.DESCRIPTION_OF_SOURCE = a.DESCRIPTION_OF_SOURCE;
                    this.SERVICE_CHARGES_EXEMPTED = a.SERVICE_CHARGES_EXEMPTED;
                    this.SERVICE_CHARGES_EXEMPTED_CODE = new ServiceChargesExemptedCode { ID = (int)a.SERVICE_CHARGES_EXEMPTED_CODE };
                    this.REASON_IF_EXEMPTED = a.REASON_IF_EXEMPTED;
                    this.EXPECTED_MONTHLY_INCOME = a.EXPECTED_MONTHLY_INCOME;
                    this.MODE_OF_TRANSACTIONS = (bool)a.MODE_OF_TRANSACTIONS;
                    this.OTHER_MODE_OF_TRANSACTIONS = a.OTHER_MODE_OF_TRANSACTIONS;
                    this.MAX_TRANS_AMOUNT_DR = a.MAX_TRANS_AMOUNT_DR;
                    this.MAX_TRANS_AMOUNT_CR = a.MAX_TRANS_AMOUNT_CR;
                    this.RELATIONSHIP_MANAGER = new Know_Your_Customer_Relationship { ID = (int) a.RELATIONSHIP_MANAGER};
                    this.OCCUPATION_VERIFIED = a.OCCUPATION_VERIFIED;
                    this.ADDRESS_VERIFIED = (int) a.ADDRESS_VERIFIED;
                    this.MEANS_OF_VERIFICATION = new MeansOfVerification { ID = (int)a.MEANS_OF_VERIFICATION };
                    this.MEANS_OF_VERI_OTHER = a.MEANS_OF_VERI_OTHER;
                    this.IS_VERI_SATISFACTORY = a.IS_VERI_SATISFACTORY;
                    this.DETAIL_IF_NOT_SATISFACTORY = a.DETAIL_IF_NOT_SATISFACTORY;
                    this.COUNTRY_HOME_REMITTANCE = new Country { ID = (int)a.COUNTRY_HOME_REMITTANCE };
                    this.REAL_BENEFICIARY_ACCOUNT = new RealBeneficiaryAccount { ID = (int)a.REAL_BENEFICIARY_ACCOUNT };
                    this.NAME_OTHER = a.NAME_OTHER;
                    this.CNIC_OTHER = a.CNIC_OTHER;
                    this.RELATIONSHIP_WITH_ACCOUNTHOLDER = new Relationship { ID = a.RELATIONSHIP_WITH_ACCOUNTHOLDER };
                    this.RELATIONSHIP_DETAIL_OTHER = a.RELATIONSHIP_DETAIL_OTHER;

                    this.BENEFICIAL_ADDRESS = a.BENEFICIAL_ADDRESS;
                    this.BENEFICIAL_NATIONALITY = a.BENEFICIAL_NATIONALITY;
                    this.BENEFICIAL_RESIDENCE = a.BENEFICIAL_RESIDENCE;
                    this.BENEFICIAL_RESIDENCE_DESC = a.BENEFICIAL_RESIDENCE_DESC;
                    this.BENEFICIAL_IDENTITY = a.BENEFICIAL_IDENTITY;
                    this.BENEFICIAL_IDENTITY_EXPIRY = a.BENEFICIAL_IDENTITY_EXPIRY;
                    this.BENEFICIAL_SOURCE_OF_FUND = a.BENEFICIAL_SOURCE_OF_FUND;


                    List<KYC_PURPOSE_OF_AC> POA = db.KYC_PURPOSE_OF_AC.Where(p => p.BID == id).ToList();
                    foreach (var p in POA)
                    {
                        this.PURPOSE_OF_ACCOUNT.Add(p.PURPOSE_OF_AC);
                    }

                    List<KYC_SOURCE_OF_FUND> SOF = db.KYC_SOURCE_OF_FUND.Where(p => p.BID == id).ToList();
                    foreach (var s in SOF)
                    {
                        this.SOURCE_OF_FUNDS.Add(s.SOURCE_OF_FUND);
                    }
                    List<KYC_EXPECTED_COUNTER_PARTIES> ECP = db.KYC_EXPECTED_COUNTER_PARTIES.Where(p => p.BID == id).ToList();
                    foreach (var e in ECP)
                    {
                        this.KYC_EXPECTED_COUNTER_PARTIES.Add((int)e.EXPECTED_CP);
                    }
                    List<KYC_GEOGRAPHIES_COUNTER_PARTIES> GCP = db.KYC_GEOGRAPHIES_COUNTER_PARTIES.Where(p => p.BID == id).ToList();
                    foreach (var e in GCP)
                    {
                        this.KYC_GEOGRAPHIES_COUNTER_PARTIES.Add((int)e.GEOGRAPHIES_CP);
                    }
                    this.NODT = a.NODT;
                    this.PEDT = a.PEDT;
                    this.NOCT = a.NOCT;
                    this.PECT = a.PECT;

                    List<KYC_BENEFICIAL_ENTITY> BE = db.KYC_BENEFICIAL_ENTITY.Where(b => b.BID == id).ToList();
                    foreach (var b in BE)
                    {
                        KycBeneficialEntity nbe = new KycBeneficialEntity();
                        nbe.NAME = b.NAME;
                        nbe.IDENTITY_DOCUMENT = b.IDENTITY_DOCUMENT;
                        nbe.IDENTITY_NUMBER = b.IDENTITY_NUMBER;
                        nbe.EXPIRY_DATE = b.EXPIRY_DATE;
                        nbe.POB = b.POB;
                        nbe.US = b.US;
                        nbe.OWNERSHIP = b.OWNERSHIP;
                        this.KycBeneficial.Add(nbe);
                    }
                    return true;

                }

                return false;
            }
        }

        public bool CheckIndividualKnowYourCustomer(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.KNOW_YOUR_CUSTOMER.Where(b => b.BI_ID == BID).Any();
            }
        }

    }
}
