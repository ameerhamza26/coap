using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class AccomodationTypes
    {

        public int ID { get; set; }
        public string Name { get; set; }

        public List<AccomodationTypes> GetAccomodationTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccomodationTypesList = db.ACCOMODATION_TYPES.Select(c => new AccomodationTypes { ID = c.ID, Name = c.Name }).ToList();
                return AccomodationTypesList;
            }
        }
    }
}
