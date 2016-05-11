using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class City
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<City> GetCifTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var CityList = db.CITIES.Select(c => new City { ID = c.ID, Name = c.NAME.Trim() }).ToList();
                return CityList;
            }
        }
    }
}
