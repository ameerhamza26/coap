using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class District
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<District> GetDistrictList()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var DistrictsList = db.DISTRICTS.Select(c => new District { ID = c.ID, Name = c.NAME }).ToList();
                return DistrictsList;
            }
        }
    }
}
