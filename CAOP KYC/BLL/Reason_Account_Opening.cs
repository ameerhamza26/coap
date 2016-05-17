using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Reason_Account_Opening
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<Reason_Account_Opening> GetReason_Account_OpeningTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var TitleList = db.REASON_ACCOUNT_OPENING.Select(i => new Reason_Account_Opening { ID = i.ID, Name = i.Name }).ToList();
                return TitleList;
            }
        }
    }
}
