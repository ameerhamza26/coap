using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class AccountReview
    {
        public int ID { get; set; }
        public int BID { get; set; }
        public string TAB { get; set; }
        public string FNAME { get; set; }
        public string FID { get; set; }
        public DateTime DATEC { get; set; }
        public int USERID { get; set; }
        public bool ACTIVE { get; set; }

        public string COMMENT { get; set; }



        public List<AccountReview> GetReviews(int BID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                List<AccountReview> reviews = new List<AccountReview>();

                var dbReviews = db.ACCOUNT_REVIEW.Where(c => c.BID == BID && c.ACTIVE == true)
                                .Select(c => new AccountReview
                                {
                                    BID = c.BID,
                                    TAB = c.TAB,
                                    DATEC = c.DATEC,
                                    FID = c.FID,
                                    FNAME = c.FNAME,
                                    ACTIVE = c.ACTIVE,
                                    ID = c.ID,
                                    USERID = c.USERID,
                                    COMMENT = c.COMMENT
                                }).ToList();

                return dbReviews;


            }
        }


        public void AddRviewerComents(List<AccountReview> comments, int ID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                ChangeComentsStatus(ID);

                foreach (var c in comments)
                {
                    ACCOUNT_REVIEW newComment = new ACCOUNT_REVIEW()
                    {
                        BID = c.BID,
                        TAB = c.TAB,
                        FNAME = c.FNAME,
                        FID = c.FID,
                        DATEC = c.DATEC,
                        USERID = c.USERID,
                        ACTIVE = c.ACTIVE,
                        COMMENT = c.COMMENT
                    };

                    db.ACCOUNT_REVIEW.Add(newComment);
                }


                db.SaveChanges();
            }

        }
      

        private void ChangeComentsStatus(int ID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var comments = db.ACCOUNT_REVIEW.Where(c => c.BID == ID);

                foreach (var c in comments)
                {
                    c.ACTIVE = false;
                }

                db.SaveChanges();
            }
        }

       
    }
}
