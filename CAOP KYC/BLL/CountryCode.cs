using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
  public class CountryCode
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<CountryCode> GetCountryCodes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var CityList = db.COUNTRY_CODES.Select(c => new CountryCode { ID = c.ID, Name = c.NAME }).ToList();
                return CityList;
            }
        }
    }
}
