using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class AccountType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<AccountType> GetAccountTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountTypeList = db.ACCOUNT_TYPES.Select(c => new AccountType { ID = c.ID, Name = c.Name }).ToList();
                return AccountTypeList;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public List<AccountType> GetAccountTypes(string ClsGrp)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                string cls = ClsGrp.Split(',')[0];
                string grp = ClsGrp.Split(',')[1];
                var AccountTypeList = db.ACCOUNT_TYPES.Where(c => c.CLS == cls && c.GRP == grp).Select(c => new AccountType { ID = c.ID, Name = c.Name.Trim() }).ToList();
                return AccountTypeList;
            }
        }

        public List<AccountType> GetAccountTypesByVal(int val)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var SelectedItem = db.ACCOUNT_TYPES.FirstOrDefault(a => a.ID == val);
                string cls = SelectedItem.CLS;
                string grp = SelectedItem.GRP;
                var AccountTypeList = db.ACCOUNT_TYPES.Where(c => c.CLS == cls && c.GRP == grp).Select(c => new AccountType { ID = c.ID, Name = c.Name }).ToList();
                return AccountTypeList;
            }
        }

        public string GetAccountTypeClsGrp(int val)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var SelectedItem = db.ACCOUNT_TYPES.FirstOrDefault(a => a.ID == val);
                string cls = SelectedItem.CLS;
                string grp = SelectedItem.GRP;
                return cls + "," + grp;
            }
        }
    }
}
