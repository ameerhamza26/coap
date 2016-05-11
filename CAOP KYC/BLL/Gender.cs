using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Gender
    {
      
        public int ID { get; set; }
        public string Name { get; set; }

        public List<Gender> GetGenders()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var GenderList = db.GENDERS.Select(c => new Gender { ID = c.ID, Name = c.Name }).ToList();
                return GenderList;
            }
        }

        public string GetTextGender(string Profile)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.GENDERS.Where(g => g.ProfileCode == Profile).Any())
                {
                    string Gender = db.GENDERS.FirstOrDefault(g => g.ProfileCode == Profile).Name;
                    return Gender;
                }
                else
                    return "";
                
            }
        }
        public int GetValueOfGender(string PROFILE)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                int Value = db.GENDERS.FirstOrDefault(g => g.ProfileCode == PROFILE).ID;
                return Value;
            }
        }

        public string GetProfileCodeOfGender(string gender)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                string GenderProfile = db.GENDERS.FirstOrDefault(g => g.Name == gender).ProfileCode;
                return GenderProfile;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
