using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class AccountPurpose
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<AccountPurpose> GetAccountPurposes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountPurposesList = db.ACCOUNT_PURPOSE.Select(c => new AccountPurpose { ID = c.ID, Name = c.Name }).ToList();
                return AccountPurposesList;
            }
        }
    }
}
