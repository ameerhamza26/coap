using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class ExpectedCounterParties
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<ExpectedCounterParties> GetExpectedCounterParties()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var List = db.EXPECTED_COUNTER_PARTIES.Select(c => new ExpectedCounterParties { ID = c.ID, Name = c.NAME }).ToList();
                return List;
            }
        }
    }
}
