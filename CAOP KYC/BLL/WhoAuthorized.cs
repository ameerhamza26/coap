using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class WhoAuthorized
    {
        #region Model
        public int ID { get; set; }
        public Nullable<int> BI_ID { get; set; }
        public int CIF_NO { get; set; }
        public string REFERENCE_DOCUMENT_NO { get; set; }
        public string REFERENCE_DOCUMENT_DATE { get; set; }
        public string NAME { get; set; }
        public string IDENTITY_NO { get; set; }
        #endregion
        public List<WhoAuthorized> Cifs { get; set; }

        public void SAVE()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                foreach (var Cif in Cifs)
                {
                    WHO_AUTHORIZED newWhoCif = new WHO_AUTHORIZED();
                    newWhoCif.BI_ID = Cif.BI_ID;
                    newWhoCif.CIF_NO = Cif.CIF_NO;
                    newWhoCif.REFERENCE_DOCUMENT_DATE = Cif.REFERENCE_DOCUMENT_DATE;
                    newWhoCif.REFERENCE_DOCUMENT_NO = Cif.REFERENCE_DOCUMENT_NO;
                    newWhoCif.NAME = Cif.NAME;
                    newWhoCif.IDENTITY_NO = Cif.IDENTITY_NO;
                    db.WHO_AUTHORIZED.Add(newWhoCif);
                }
                db.SaveChanges();
            }
        }

        public void Update()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                int BID = (int) Cifs[0].BI_ID;
                db.WHO_AUTHORIZED.RemoveRange(db.WHO_AUTHORIZED.Where(c => c.BI_ID == BID));
                db.SaveChanges();

                this.SAVE();
            }
        }

        public bool GetWhoAuthorized(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.WHO_AUTHORIZED.Where(c => c.BI_ID == BID).Any())
                {
                    this.Cifs = db.WHO_AUTHORIZED
                           .Where(c => c.BI_ID == BID)
                           .Select(
                           c => new WhoAuthorized
                           {
                               BI_ID = c.BI_ID,
                               CIF_NO = c.CIF_NO
                               ,
                               REFERENCE_DOCUMENT_NO = c.REFERENCE_DOCUMENT_NO,
                               REFERENCE_DOCUMENT_DATE = c.REFERENCE_DOCUMENT_DATE,
                               NAME = c.NAME,
                               IDENTITY_NO = c.IDENTITY_NO
                           }
                           ).ToList();

                    return true;
                }
                else
                    return false;

               
            }
        }

        public bool CheckWhoAuthorized(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.WHO_AUTHORIZED.Where(c => c.BI_ID == BID).Any())
                    return true;
                else
                    return false;
            }
        }

    }
}
