    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Know_Customer_Transaction_mode
    {

        public int ID { get; set; }
        public Nullable<int> BI_ID { get; set; }
        public ModeOfTransactions MODE_OF_TRANSACTIONS { get; set; }

        public void Save()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                KNOW_CUSTOMER_TRANSACTIONS_MODE k = new KNOW_CUSTOMER_TRANSACTIONS_MODE();
                k.BI_ID = this.BI_ID;
                k.MODE_OF_TRANSACTIONS = this.MODE_OF_TRANSACTIONS.ID;

                db.KNOW_CUSTOMER_TRANSACTIONS_MODE.Add(k);
                db.SaveChanges();
            }
        }


        public void Update()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                KNOW_CUSTOMER_TRANSACTIONS_MODE k = db.KNOW_CUSTOMER_TRANSACTIONS_MODE.FirstOrDefault(b => b.BI_ID == this.BI_ID);
                k.MODE_OF_TRANSACTIONS = this.MODE_OF_TRANSACTIONS.ID;

                db.SaveChanges();
            }
        }

        public List<Know_Customer_Transaction_mode> GetDocumentList(int id)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {

                List<Know_Customer_Transaction_mode> List = db.KNOW_CUSTOMER_TRANSACTIONS_MODE.Where(c => c.BI_ID == id).Select(a => new Know_Customer_Transaction_mode { ID = a.ID, BI_ID = a.BI_ID, MODE_OF_TRANSACTIONS= new ModeOfTransactions { ID = (int)a.MODE_OF_TRANSACTIONS } }).ToList();
                return List;
            }
        }



        public bool Get(int id)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.KNOW_CUSTOMER_TRANSACTIONS_MODE.Where(b => b.BI_ID == id).Any())
                {
                    var a = db.KNOW_CUSTOMER_TRANSACTIONS_MODE.FirstOrDefault(c => c.BI_ID == id);
                    this.BI_ID = a.BI_ID;
                    this.MODE_OF_TRANSACTIONS = new ModeOfTransactions() { ID = (int)a.MODE_OF_TRANSACTIONS };

                    return true;

                }
                else
                    return false;
            }
        }
    }
}
