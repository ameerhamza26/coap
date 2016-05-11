using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class AccountGroup
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<AccountGroup> GetAccountGroupTypes(string cls)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountGroupList = db.ACCOUNT_GROUP.Where(c => c.CLS == cls).Select(c => new AccountGroup { ID = c.ID, Name = c.DESCRIPTION }).ToList();
                return AccountGroupList;
            }
        }

        public List<AccountGroup> GetAccountGroupTypes(string cls, string grp)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountGroupList = db.ACCOUNT_GROUP.Where(c => c.CLS == cls && c.GRP == grp).Select(c => new AccountGroup { ID = c.ID, Name = c.DESCRIPTION }).ToList();
                return AccountGroupList;
            }
        }

        public string GetCls(int val)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                string cls = db.ACCOUNT_GROUP.FirstOrDefault(a => a.ID == val).CLS;
                return cls;
            }
        }

        public string GetAccountClassClsGrp(int val)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var Data = db.ACCOUNT_GROUP.FirstOrDefault(c => c.ID == val);
                return Data.CLS + "," + Data.GRP;
            }
        }
    }
}
