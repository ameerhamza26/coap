using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Country
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<Country> GetCountries()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var CountryList = db.COUNTRIES.Select(c => new Country { ID = c.ID, Name = c.Name.Trim() }).OrderBy(c => c.Name).ToList();
                return CountryList;
            }
        }

        public string GetCountryProfileCode(string CountryName)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                string PCode = db.COUNTRIES.FirstOrDefault(c => c.Name == CountryName).ProfileCode;
                return PCode;
            }
        }

        public string GetCountyName(string ProfileCode)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.COUNTRIES.Where(c => c.ProfileCode == ProfileCode).Any())
                {
                    string name = db.COUNTRIES.FirstOrDefault(c => c.ProfileCode == ProfileCode).Name;
                    return name;
                }
                else
                    return "";
                    
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
