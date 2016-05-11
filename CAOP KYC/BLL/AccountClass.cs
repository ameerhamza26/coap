using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public  class AccountClass
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<AccountClass> GetAccountClassTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountClassList = db.ACCOUNT_CLASS.Select(c => new AccountClass { ID = c.ID, Name = c.Name }).ToList();
                return AccountClassList;
            }
        }

        public List<AccountClass> GetAccountClassTypes(string cls)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountClassList = db.ACCOUNT_CLASS.Where(c => c.CLS == cls).Select(c => new AccountClass { ID = c.ID, Name = c.Name }).ToList();
                return AccountClassList;
            }
        }

        public string GetAccountTypeCls(int val)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                string Cls = db.ACCOUNT_CLASS.FirstOrDefault(c => c.ID == val).CLS;
                return Cls;
            }
        }
    }
}
