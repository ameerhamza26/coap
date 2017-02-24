using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class Fatca
   {

       #region 
       public int ID { get; set; }
        public int BI_ID { get; set; }
        public bool RESIDENT { get; set; }
        public bool CITIZEN { get; set; }
        public bool BIRTH_USA { get; set; }
        public bool ADDRESS_USA { get; set; }
        public string CONTACT_OFFICE { get; set; }
        public string CONTACT_RESIDENCE { get; set; }
        public string MOBNO { get; set; }
        public string FAXNO { get; set; }
        public UsaResidenceCard RESIDENCE_CARD { get; set; }
        public UsaFund FUND_TRANSFER { get; set; }
        public FatcaClassification FTCA_CLASSIFICATION { get; set; }
        public UsaTaxType US_TAXID { get; set; }
        public string TAXNO { get; set; }
        public UsaPhone USA_PHONE { get; set; }

        public Nullable<int> COUNTRY_INCORP { get; set; }
        public Nullable<int> COUNTRY_BUSINESS { get; set; }

        public List<FatcaDocumentation> FATCA_DOCUMENTS { get; set; }

       // New fields 2016-03-03
       public FatcasTin TYPE_TIN { get; set; }

       public string TIN { get; set; }
       public DateTime? FATCA_DOCUMENTATION_DATE { get; set; }

       #endregion 


        public void SaveFatca()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                FATCAS newFatca = new FATCAS();

                newFatca.BI_ID = this.BI_ID;
                newFatca.RESIDENT = this.RESIDENT;
                newFatca.CITIZEN = this.CITIZEN;
                newFatca.BIRTH_USA = this.BIRTH_USA;
                newFatca.ADDRESS_USA = this.ADDRESS_USA;
                newFatca.CONTACT_OFFICE = this.CONTACT_OFFICE.ToUpper();
                newFatca.CONTACT_RESIDENCE = this.CONTACT_RESIDENCE.ToUpper();
                newFatca.MOBNO = this.MOBNO;
                newFatca.FAXNO = this.FAXNO.ToUpper();
                if (this.RESIDENCE_CARD != null)
                newFatca.RESIDENCE_CARD = this.RESIDENCE_CARD.ID;
                newFatca.FUND_TRANSFER = this.FUND_TRANSFER.ID;
                newFatca.FTCA_CLASSIFICATION = this.FTCA_CLASSIFICATION.ID;
                newFatca.US_TAXID = this.US_TAXID.ID;
                newFatca.TAXNO = this.TAXNO.ToUpper();
                newFatca.USA_PHONE = this.USA_PHONE.ID;
                if (this.TYPE_TIN != null)
                newFatca.TYPE_TIN = this.TYPE_TIN.ID;
                if (this.TIN != null)
                newFatca.TIN = this.TIN.ToUpper();
                newFatca.FATCA_DOCUMENTATION_DATE = this.FATCA_DOCUMENTATION_DATE;
                if (COUNTRY_INCORP != null)
                    newFatca.COUNTRY_INCORP = this.COUNTRY_INCORP;
                if (COUNTRY_BUSINESS != null)
                    newFatca.COUNTRY_BUSINESS = this.COUNTRY_BUSINESS; 
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.FATCAS.Add(newFatca);
                db.SaveChanges();

                foreach (var doc in FATCA_DOCUMENTS)
                {
                    DOCUMENTS_FATCA newdoc = new DOCUMENTS_FATCA();

                    newdoc.BI_ID = this.BI_ID;
                    newdoc.DOCUMENT_ID = doc.ID;
                    newdoc.DOCUMENT_NAME = doc.Name;

                    db.DOCUMENTS_FATCA.Add(newdoc);
                }

                if (FATCA_DOCUMENTS.Count == 0)
                {
                    FTCA_DOCUMENTATIONS f = db.FTCA_DOCUMENTATIONS.FirstOrDefault(d => d.Name.Trim() == "N/A");

                    DOCUMENTS_FATCA newdoc = new DOCUMENTS_FATCA();

                    newdoc.BI_ID = this.BI_ID;
                    newdoc.DOCUMENT_ID = f.ID;
                    newdoc.DOCUMENT_NAME = f.Name;

                    db.DOCUMENTS_FATCA.Add(newdoc);
                }

                db.SaveChanges();

            }
        }

        public void UpdateFatca()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                FATCAS newFatca = db.FATCAS.FirstOrDefault(f => f.BI_ID == this.BI_ID);

                newFatca.BI_ID = this.BI_ID;
                newFatca.RESIDENT = this.RESIDENT;
                newFatca.CITIZEN = this.CITIZEN;
                newFatca.BIRTH_USA = this.BIRTH_USA;
                newFatca.ADDRESS_USA = this.ADDRESS_USA;
                newFatca.CONTACT_OFFICE = this.CONTACT_OFFICE.ToUpper();
                newFatca.CONTACT_RESIDENCE = this.CONTACT_RESIDENCE.ToUpper();
                newFatca.MOBNO = this.MOBNO;
                newFatca.FAXNO = this.FAXNO.ToUpper();
                if (this.RESIDENCE_CARD != null)
                newFatca.RESIDENCE_CARD = this.RESIDENCE_CARD.ID;
                newFatca.FUND_TRANSFER = this.FUND_TRANSFER.ID;
                newFatca.FTCA_CLASSIFICATION = this.FTCA_CLASSIFICATION.ID;
                newFatca.US_TAXID = this.US_TAXID.ID;
                newFatca.TAXNO = this.TAXNO.ToUpper();
                newFatca.USA_PHONE = this.USA_PHONE.ID;
                if (this.TYPE_TIN != null)
                newFatca.TYPE_TIN = this.TYPE_TIN.ID;
                if (this.TIN != null)
                newFatca.TIN = this.TIN.ToUpper();
                newFatca.FATCA_DOCUMENTATION_DATE = this.FATCA_DOCUMENTATION_DATE;
                if (COUNTRY_INCORP != null)
                    newFatca.COUNTRY_INCORP = this.COUNTRY_INCORP;
                if (COUNTRY_BUSINESS != null)
                    newFatca.COUNTRY_BUSINESS = this.COUNTRY_BUSINESS; 

                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.DOCUMENTS_FATCA.RemoveRange(db.DOCUMENTS_FATCA.Where(d => d.BI_ID == this.BI_ID));
                db.SaveChanges();

                foreach (var doc in FATCA_DOCUMENTS)
                {
                    DOCUMENTS_FATCA newdoc = new DOCUMENTS_FATCA();

                    newdoc.BI_ID = this.BI_ID;
                    newdoc.DOCUMENT_ID = doc.ID;
                    newdoc.DOCUMENT_NAME = doc.Name;

                    db.DOCUMENTS_FATCA.Add(newdoc);
                }

                if (FATCA_DOCUMENTS.Count == 0)
                {
                    FTCA_DOCUMENTATIONS f = db.FTCA_DOCUMENTATIONS.FirstOrDefault(d => d.Name.Trim() == "N/A");

                    DOCUMENTS_FATCA newdoc = new DOCUMENTS_FATCA();

                    newdoc.BI_ID = this.BI_ID;
                    newdoc.DOCUMENT_ID = f.ID;
                    newdoc.DOCUMENT_NAME = f.Name;

                    db.DOCUMENTS_FATCA.Add(newdoc);
                }

                db.SaveChanges();

            }
        }

       
        public void UpdateFatcaNew()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                FATCAS newFatca = db.FATCAS.FirstOrDefault(f => f.BI_ID == this.BI_ID);

                newFatca.BI_ID = this.BI_ID;
                newFatca.RESIDENT = this.RESIDENT;
                newFatca.CITIZEN = this.CITIZEN;
                newFatca.BIRTH_USA = this.BIRTH_USA;
                newFatca.ADDRESS_USA = this.ADDRESS_USA;
                newFatca.CONTACT_OFFICE = this.CONTACT_OFFICE.ToUpper();
                newFatca.CONTACT_RESIDENCE = this.CONTACT_RESIDENCE.ToUpper();
                newFatca.MOBNO = this.MOBNO;
                newFatca.FAXNO = this.FAXNO.ToUpper();
                if (this.RESIDENCE_CARD != null)
                    newFatca.RESIDENCE_CARD = this.RESIDENCE_CARD.ID;
                newFatca.FUND_TRANSFER = this.FUND_TRANSFER.ID;
                newFatca.FTCA_CLASSIFICATION = this.FTCA_CLASSIFICATION.ID;
                newFatca.US_TAXID = this.US_TAXID.ID;
                newFatca.TAXNO = this.TAXNO.ToUpper();
                newFatca.USA_PHONE = this.USA_PHONE.ID;
                if (this.TYPE_TIN != null)
                    newFatca.TYPE_TIN = this.TYPE_TIN.ID;
                if (this.TIN != null)
                    newFatca.TIN = this.TIN.ToUpper();
                newFatca.FATCA_DOCUMENTATION_DATE = this.FATCA_DOCUMENTATION_DATE;
                if (COUNTRY_INCORP != null)
                    newFatca.COUNTRY_INCORP = this.COUNTRY_INCORP;
                if (COUNTRY_BUSINESS != null)
                    newFatca.COUNTRY_BUSINESS = this.COUNTRY_BUSINESS;

                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;
                db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).STATUS=Status.UPDATED_BY_BRANCH_OPERATOR.ToString();
                db.DOCUMENTS_FATCA.RemoveRange(db.DOCUMENTS_FATCA.Where(d => d.BI_ID == this.BI_ID));
                db.SaveChanges();

                foreach (var doc in FATCA_DOCUMENTS)
                {
                    DOCUMENTS_FATCA newdoc = new DOCUMENTS_FATCA();

                    newdoc.BI_ID = this.BI_ID;
                    newdoc.DOCUMENT_ID = doc.ID;
                    newdoc.DOCUMENT_NAME = doc.Name;

                    db.DOCUMENTS_FATCA.Add(newdoc);
                }

                if (FATCA_DOCUMENTS.Count == 0)
                {
                    FTCA_DOCUMENTATIONS f = db.FTCA_DOCUMENTATIONS.FirstOrDefault(d => d.Name.Trim() == "N/A");

                    DOCUMENTS_FATCA newdoc = new DOCUMENTS_FATCA();

                    newdoc.BI_ID = this.BI_ID;
                    newdoc.DOCUMENT_ID = f.ID;
                    newdoc.DOCUMENT_NAME = f.Name;

                    db.DOCUMENTS_FATCA.Add(newdoc);
                }

                db.SaveChanges();

            }
        }

        public bool CheckIndividualFatca(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.FATCAS.Where(b => b.BI_ID == BID).Any();
            }
        }

       public bool GetFatcaIndividual(int BI_ID)
       {
           using (CAOPDbContext db = new CAOPDbContext())
           {
               if (db.FATCAS.Where(f => f.BI_ID == BI_ID).Any())
               {
                   var FA = db.FATCAS.FirstOrDefault(f => f.BI_ID == BI_ID);

                   this.BI_ID = (int) FA.BI_ID;
                   this.RESIDENT = (bool) FA.RESIDENT;
                   this.CITIZEN = (bool)FA.CITIZEN;
                   this.BIRTH_USA = (bool)FA.BIRTH_USA;
                   this.ADDRESS_USA = (bool) FA.ADDRESS_USA;
                   this.CONTACT_OFFICE = FA.CONTACT_OFFICE;
                   this.CONTACT_RESIDENCE = FA.CONTACT_RESIDENCE;
                   this.MOBNO = FA.MOBNO;
                   this.FAXNO = FA.FAXNO;
                   this.RESIDENCE_CARD = new UsaResidenceCard { ID =  FA.RESIDENCE_CARD };
                   this.FUND_TRANSFER = new UsaFund { ID = FA.FUND_TRANSFER };
                   this.FTCA_CLASSIFICATION = new FatcaClassification { ID = FA.FTCA_CLASSIFICATION };
                   this.US_TAXID = new UsaTaxType { ID = FA.US_TAXID };
                   this.TAXNO = FA.TAXNO;
                   this.USA_PHONE = new UsaPhone { ID = FA.USA_PHONE };
                   this.TYPE_TIN = new FatcasTin { ID = FA.TYPE_TIN };
                   this.TIN = FA.TIN;
                   this.FATCA_DOCUMENTATION_DATE =  FA.FATCA_DOCUMENTATION_DATE;
                   this.COUNTRY_INCORP = FA.COUNTRY_INCORP;
                   this.COUNTRY_BUSINESS = FA.COUNTRY_BUSINESS;

                   FATCA_DOCUMENTS = db.DOCUMENTS_FATCA.Where(d => d.BI_ID == BI_ID).Select(d => new FatcaDocumentation { ID = (int)d.DOCUMENT_ID, Name = d.DOCUMENT_NAME }).ToList();

                   return true;
               }
               else
                   return false;
           }
       }
   }
}
