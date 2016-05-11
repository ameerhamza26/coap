using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Emails
    {
        public int ID { get; set; }
        public string EMAIL { get; set; }
        public Nullable<bool> REQUIRED_ESTATEMEN { get; set; }
        public Nullable<int> BI_ID { get; set; }


        public void SaveEmail()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                    EMAILS e = new EMAILS();
                    e.EMAIL = this.EMAIL;
                    e.BI_ID = this.BI_ID;
                    e.REQUIRED_ESTATEMEN = this.REQUIRED_ESTATEMEN;
                    db.EMAILS.Add(e);
                    db.SaveChanges();             

            }
        }



        public void UpdateEmail()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.EMAILS.Where(b => b.BI_ID == this.BI_ID).Any())
                {
                    EMAILS e = db.EMAILS.FirstOrDefault(b => b.BI_ID == this.BI_ID);
                    e.EMAIL = this.EMAIL;
                    e.REQUIRED_ESTATEMEN = this.REQUIRED_ESTATEMEN;
                    db.SaveChanges();
                }
                else
                    this.SaveEmail();
               


            }
        }
        public bool GetEmail(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.EMAILS.Where(c => c.BI_ID == BID).Any())
                {
                    var a = db.EMAILS.FirstOrDefault(b => b.BI_ID == BID);

                    this.BI_ID = a.BI_ID;
                    this.EMAIL = a.EMAIL;
                    this.REQUIRED_ESTATEMEN = a.REQUIRED_ESTATEMEN;

                    return true;

                }

                else
                    return false;
            }
        }
    }
}
