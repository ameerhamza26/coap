using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Education
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<Education> GetEducation()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var EducationList = db.EDUCATION.Select(c => new Education { ID = c.ID, Name = c.NAME }).ToList();
                return EducationList;
            }
        }
    }
}
