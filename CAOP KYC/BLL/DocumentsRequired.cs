using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class DocumentsRequired
    {
        public int ID { get; set; }
        public Nullable<int> BI_ID { get; set; }
        public Nullable<bool> DESCRIPTION { get; set; }

        public void Save()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                DOCUMENT_REQUIRED d = new DOCUMENT_REQUIRED();
                d.BI_ID = this.BI_ID;
                d.DESCRIPTION = this.DESCRIPTION;
                db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.DOCUMENT_REQUIRED.Add(d);
                db.SaveChanges();
            }
        }

        public void Update()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                DOCUMENT_REQUIRED d = db.DOCUMENT_REQUIRED.FirstOrDefault(b => b.BI_ID == this.BI_ID);
                d.BI_ID = this.BI_ID;
                d.DESCRIPTION = this.DESCRIPTION;
                db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).LAST_UPDATED = DateTime.Now;

                db.SaveChanges();
            }
        }

        public bool Get(int id)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.DOCUMENT_REQUIRED.Where(b => b.BI_ID == id).Any())
                {
                    var a = db.DOCUMENT_REQUIRED.FirstOrDefault(c => c.BI_ID == id);
                    this.BI_ID = a.BI_ID;
                    this.DESCRIPTION = a.DESCRIPTION;

                    return true;

                }
                else
                    return false;
            }
        }

        public bool CheckDocumentRequired(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.DOCUMENT_REQUIRED.Where(b => b.BI_ID == BID).Any();
            }
        }
    }
}
