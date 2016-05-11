using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Province
    {
        public int? ID { get; set; }
        public string Name { get; set; } 

        public List<Province> GetProvinces()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var ProvinceList = db.PROVINCES.Select(c => new Province { ID = c.ID, Name = c.Name }).ToList();
                return ProvinceList;
            }
        }
    }
}
