using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class AccountOpenType
    {
        public int? ID { get; set; }
        public string NAME { get; set; }

        public List<AccountOpenType> GetAccountTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountTypeList = db.ACCOUNT_OPEN_TYPE.Select(c => new AccountOpenType { ID = c.ID, NAME = c.NAME }).ToList();
                return AccountTypeList;
            }
        }

        public override string ToString()
        {
            return NAME;
        }
    }
}
