using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class ModeOfTransactions
    {
        public int ID { get; set; }
        public string NAME { get; set; }

        public List<ModeOfTransactions> GetAccountTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountTypeList = db.MODE_OF_TRANSACTIONS.Select(c => new ModeOfTransactions { ID = c.ID, NAME = c.NAME.Trim() }).ToList();
                return AccountTypeList;
            }
        }
    }
}
