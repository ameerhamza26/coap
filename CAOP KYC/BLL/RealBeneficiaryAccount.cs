using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
     public class RealBeneficiaryAccount
    {
        public int ID { get; set; }
        public string NAME { get; set; }

        public List<RealBeneficiaryAccount> GetAccountTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountTypeList = db.REAL_BENEFICIARY_ACCOUNT.Select(c => new RealBeneficiaryAccount { ID = c.ID, NAME = c.NAME }).ToList();
                return AccountTypeList;
            }
        }
    }
}
