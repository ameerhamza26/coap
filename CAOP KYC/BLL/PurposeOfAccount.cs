using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class PurposeOfAccount
    {

        public int ID { get; set; }
        public string NAME { get; set; }

        public List<PurposeOfAccount> GetAccountTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountTypeList = db.PURPOSE_OF_ACCOUNT.Select(c => new PurposeOfAccount { ID = c.ID, NAME = c.NAME.Trim() }).ToList();
                return AccountTypeList;
            }
        }

        public List<PurposeOfAccount> GetPAccountTypesBusiness()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountTypeList = db.PURPOSE_OF_ACCOUNT_BUSINESS.Select(c => new PurposeOfAccount { ID = c.ID, NAME = c.NAME.Trim() }).ToList();
                return AccountTypeList;
            }
        }
    }
}
