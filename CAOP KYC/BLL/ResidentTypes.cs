using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class ResidentType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<ResidentType> GetResidentTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var ResidentTypeList = db.RESIDENT_TYPES.Select(c => new ResidentType { ID = c.ID, Name = c.Name.Trim() }).ToList();
                return ResidentTypeList;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
