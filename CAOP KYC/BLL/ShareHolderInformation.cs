using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class ShareHolderInformation
    {
        public int ID { get; set; }
        public Nullable<int> BID { get; set; }
        public string NAME { get; set; }
        public string ADDRESS { get; set; }
        public string IDENTITY_TYPE { get; set; }
        public Nullable<int> IDENTITY_TYPE_VALUE { get; set; }
        public string IDENTITY_NO { get; set; }
        public string IDENTITY_EXPIRY_DATE { get; set; }
        public string RESIDENCE_PHONE { get; set; }
        public string OFFICE_PHONE { get; set; }
        public string MOBILE_NO { get; set; }
        public string FAX_NO { get; set; }
        public string EMAIL { get; set; }
        public string NO_SHARES { get; set; }
        public string AMOUNT_SHARES { get; set; }
        public string SHAREHOLDER_PERCENTAGE { get; set; }
        public string NET_WORTH { get; set; }
        public string DIRECTOR_STATUS { get; set; }
        public Nullable<int> DIRECTOR_STATUS_VALUE { get; set; }

       public List<ShareHolderInformation> SHARE_HOLDERS { get; set; }


       public void SAVE()
       {
           using (CAOPDbContext db = new CAOPDbContext())
           {
                if (this.SHARE_HOLDERS != null)
                {
                    foreach (var SH in this.SHARE_HOLDERS)
                    {
                        SHAREHOLDER_INFORMATION NSH = new SHAREHOLDER_INFORMATION();
                        NSH.BID = this.BID;
                        NSH.NAME = SH.NAME;
                        NSH.ADDRESS = SH.ADDRESS;
                        NSH.IDENTITY_TYPE = SH.IDENTITY_TYPE;
                        NSH.IDENTITY_TYPE_VALUE = SH.IDENTITY_TYPE_VALUE;
                        NSH.IDENTITY_NO = SH.IDENTITY_NO;
                        NSH.IDENTITY_EXPIRY_DATE = SH.IDENTITY_EXPIRY_DATE;
                        NSH.RESIDENCE_PHONE = SH.RESIDENCE_PHONE;
                        NSH.OFFICE_PHONE = SH.OFFICE_PHONE;
                        NSH.MOBILE_NO = SH.MOBILE_NO;
                        NSH.FAX_NO = SH.FAX_NO;
                        NSH.EMAIL = SH.EMAIL;
                        NSH.NO_SHARES = SH.NO_SHARES;
                        NSH.AMOUNT_SHARES = SH.AMOUNT_SHARES;
                        NSH.SHAREHOLDER_PERCENTAGE = SH.SHAREHOLDER_PERCENTAGE;
                        NSH.NET_WORTH = SH.NET_WORTH;
                        NSH.DIRECTOR_STATUS = SH.DIRECTOR_STATUS;
                        NSH.DIRECTOR_STATUS_VALUE = SH.DIRECTOR_STATUS_VALUE;
                        db.SHAREHOLDER_INFORMATION.Add(NSH);

                    }
                }
                else
                {
                    SHAREHOLDER_INFORMATION NSH = new SHAREHOLDER_INFORMATION();
                    NSH.BID = this.BID;
                    db.SHAREHOLDER_INFORMATION.Add(NSH);
                }


                db.SaveChanges();

            }
       }

       public void UPDATE()
       {
           using (CAOPDbContext db = new CAOPDbContext())
           {
               db.SHAREHOLDER_INFORMATION.RemoveRange(db.SHAREHOLDER_INFORMATION.Where(s => s.BID == this.BID));
               db.SaveChanges();

               SAVE();
           }
       }

       public bool GET()
       {
           using (CAOPDbContext db = new CAOPDbContext())
           {
               if (db.SHAREHOLDER_INFORMATION.Where(s => s.BID == this.BID).Any())
               {
                   var SHS = db.SHAREHOLDER_INFORMATION.Where(s => s.BID == this.BID).ToList();
                   this.SHARE_HOLDERS = new List<ShareHolderInformation>();

                   foreach (var SH in SHS)
                   {
                       ShareHolderInformation NSH = new ShareHolderInformation();

                       NSH.BID = this.BID;
                       NSH.NAME = SH.NAME;
                       NSH.ADDRESS = SH.ADDRESS;
                       NSH.IDENTITY_TYPE = SH.IDENTITY_TYPE;
                       NSH.IDENTITY_TYPE_VALUE = SH.IDENTITY_TYPE_VALUE;
                       NSH.IDENTITY_NO = SH.IDENTITY_NO;
                       NSH.IDENTITY_EXPIRY_DATE = SH.IDENTITY_EXPIRY_DATE;
                       NSH.RESIDENCE_PHONE = SH.RESIDENCE_PHONE;
                       NSH.OFFICE_PHONE = SH.OFFICE_PHONE;
                       NSH.MOBILE_NO = SH.MOBILE_NO;
                       NSH.FAX_NO = SH.FAX_NO;
                       NSH.EMAIL = SH.EMAIL;
                       NSH.NO_SHARES = SH.NO_SHARES;
                       NSH.AMOUNT_SHARES = SH.AMOUNT_SHARES;
                       NSH.SHAREHOLDER_PERCENTAGE = SH.SHAREHOLDER_PERCENTAGE;
                       NSH.NET_WORTH = SH.NET_WORTH;
                       NSH.DIRECTOR_STATUS = SH.DIRECTOR_STATUS;
                       NSH.DIRECTOR_STATUS_VALUE = SH.DIRECTOR_STATUS_VALUE;
                       this.SHARE_HOLDERS.Add(NSH);
                   }


                   return true;
               }
               else
                   return false;

              
           }
       }

       public bool CheckSHComp(int BID)
       {
           using (CAOPDbContext db = new CAOPDbContext())
           {
               if (db.SHAREHOLDER_INFORMATION.Where(s => s.BID == BID).Any())
                   return true;
               else
                   return false;
           }
       }

    }
}
