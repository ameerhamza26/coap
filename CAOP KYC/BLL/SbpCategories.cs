using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
  public  class SbpCategories
    {
      
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<SbpCategories> GetSbpCategories()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var SbpCategoriesList = db.SBP_CATEGORIES.Select(c => new SbpCategories { ID = c.ID, Name = c.NAME }).ToList();
                return SbpCategoriesList;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
