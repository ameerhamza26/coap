using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class AccountModes
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<AccountModes> GetAccountModes(int AccountTypeVal)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                string grp = db.ACCOUNT_TYPES.FirstOrDefault(a => a.ID == AccountTypeVal).GRP;

                var AccountModesList = db.ACCOUNT_MODES.Where(a => a.GRP == grp && a.TYPE == "INDIVIDUAL").Select(a => new AccountModes() { ID = a.ID, Name = a.NAME }).ToList();
                return AccountModesList;
               
            }
        }

        public List<AccountModes> GetAccountModesBusiness(int AccountTypeVal)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                string grp = db.ACCOUNT_TYPES.FirstOrDefault(a => a.ID == AccountTypeVal).GRP;

                var AccountModesList = db.ACCOUNT_MODES.Where(a => a.GRP == grp && a.TYPE == "BUS_GOV").Select(a => new AccountModes() { ID = a.ID, Name = a.NAME }).ToList();
                return AccountModesList;

            }
        }

        public string GetAccountTag(int ModeVal)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                string tag = db.ACCOUNT_MODES.FirstOrDefault(m => m.ID == ModeVal).CATEGORY;
                return tag;
            }
        }
    }
}
