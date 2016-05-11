using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class AccountNature
    {
       
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<AccountNature> GetAccountNature()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountNaturesList = db.BUSINESS_ACOUNTS.Select(c => new AccountNature { ID = c.ID, Name = c.NAME }).ToList();
                return AccountNaturesList;
            }
        }

        public override string ToString()
        {
            return Name;
        }


    }
}
