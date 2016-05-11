using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class GeographiesCounterParties
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<GeographiesCounterParties> GetGeographiesCounterParties()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var List = db.GEOGRAPHIES_COUNTER_PARTIES.Select(c => new GeographiesCounterParties { ID = c.ID, Name = c.NAME }).ToList();
                return List;
            }
        }
    }
}
