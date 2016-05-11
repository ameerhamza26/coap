using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public  class UsaTaxType
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<UsaTaxType> GetUsaTaxType()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var UsaTaxTypeList = db.USA_TAXID_TYPES.Select(c => new UsaTaxType { ID = c.ID, Name = c.Name }).ToList();
                return UsaTaxTypeList;
            }
        }
    }
}
