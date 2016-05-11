using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class GovCifType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<GovCifType> GetGovTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var List = db.GOV_CIF_TYPE.Select(c => new GovCifType { ID = c.ID, Name = c.Name }).ToList();
                return List;
            }
        }
    }
}
