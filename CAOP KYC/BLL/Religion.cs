using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class Religion
    {

        public int ID { get; set; }
        public string Name { get; set; }

        public List<Religion> GetReligions()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var ReligionList = db.RELIGIONS.Select(c => new Religion { ID = c.ID, Name = c.Name }).ToList();
                return ReligionList;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
