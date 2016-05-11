using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
  public  class BaseCategories
    {
       
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<BaseCategories> GetBaseCategories()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var BaseCategoriesList = db.BASE_CATEGORIES.Select(c => new BaseCategories { ID = c.ID, Name = c.NAME }).ToList();
                return BaseCategoriesList;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
