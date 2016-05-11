using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class AuthorityToOperate
    {
        public int ID { get; set; }
        public string NAME { get; set; }

        public List<AuthorityToOperate> GetAccountTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountTypeList = db.AUTHORITY_TO_OPERATE.Select(c => new AuthorityToOperate { ID = c.ID, NAME = c.NAME }).ToList();
                return AccountTypeList;
            }
        }
    }
}
