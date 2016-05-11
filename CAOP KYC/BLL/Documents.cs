using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
     public class DocumentsList
    {
        public int ID { get; set; }
        public int Documents { get; set; }
        public Nullable<int> BI_ID { get; set; }
        public string value { get; set; }
        public List<DocumentsList> UpdatedDocuments { get; set; }

        public void Save()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                DOCUMENTS d = new DOCUMENTS();
                d.BI_ID = this.BI_ID;
                d.Documents = this.Documents;
                d.value = this.value;

                db.DOCUMENTS.Add(d);
                db.SaveChanges();
            }
        }

        public void Update()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                int ID = (int)UpdatedDocuments[0].BI_ID;
                db.DOCUMENTS.RemoveRange(db.DOCUMENTS.Where(docu => docu.BI_ID == ID));
                db.SaveChanges();

                foreach (var doc in UpdatedDocuments)
                {
                    DOCUMENTS d = new DOCUMENTS();
                    d.BI_ID = doc.BI_ID;
                    d.Documents = doc.Documents;
                    d.value = doc.value;

                    db.DOCUMENTS.Add(d);
                }

              //  DOCUMENTS d = db.DOCUMENTS.FirstOrDefault(b => b.BI_ID == this.BI_ID);
               
             //   d.Documents = this.Documents;
            //    d.value = this.value;

                db.SaveChanges();
            }
        }

        public List<DocumentsList> GetDocumentList(int id)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
              
                List<DocumentsList> List = db.DOCUMENTS.Where(c => c.BI_ID == id).Select(a => new DocumentsList { ID = a.ID, BI_ID = a.BI_ID, Documents =(int) a.Documents, value = a.value }).ToList();
                return List;
            }
        }

        public bool Get(int id)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.DOCUMENTS.Where(b => b.BI_ID == id).Any())
                {
                    var a = db.DOCUMENTS.FirstOrDefault(c => c.BI_ID == id);
                    this.BI_ID = a.BI_ID;
                    this.Documents = (int)a.Documents;
                    this.value = a.value;

                    return true;
                }
                else
                    return false;
            }
        }
    }
}
