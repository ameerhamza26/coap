using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class OtherBankCodes
    {

        public int? ID { get; set; }
        public string Name { get; set; }


        public List<OtherBankCodes> GetOtherBankCodes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var OtherBankCodesList = db.OTHER_BANK_CODES.Select(c => new OtherBankCodes { ID = c.ID, Name = c.Name }).ToList();
                return OtherBankCodesList;
            }
        }
    }
}
