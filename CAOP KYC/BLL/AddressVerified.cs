using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class AddressVerified
    {
        public int ID { get; set; }
        public string NAME { get; set; }

        public List<AddressVerified> GetAccountTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountTypeList = db.ADDRESS_VERIFIED.Select(c => new AddressVerified { ID = c.ID, NAME = c.NAME }).ToList();
                return AccountTypeList;
            }
        }

    }
}
