using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class AccountStatementFrequency
    {


        public int ID { get; set; }
        public string NAME { get; set; }

        public List<AccountStatementFrequency> GetAccountTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountTypeList = db.ACCOUNT_STATEMENT_FREQUENCY.Select(c => new AccountStatementFrequency { ID = c.ID, NAME = c.NAME }).ToList();
                return AccountTypeList;
            }
        }
    }
}
