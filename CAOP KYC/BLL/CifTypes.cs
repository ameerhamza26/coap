using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public  class CifTypes
    {
        
        public int ID { get; set; }
        public string Name { get; set; }

        public List<CifTypes> GetCifTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var CifTypesList = db.CIF_TYPES.Select(c => new CifTypes { ID = c.ID, Name = c.Name }).ToList();
               return CifTypesList;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
