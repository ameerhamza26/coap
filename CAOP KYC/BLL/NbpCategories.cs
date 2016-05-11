using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class NbpCategories
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<NbpCategories> GetNbpCategories()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var NbpCategoriesList = db.NBP_CATEGORIES.Select(c => new NbpCategories { ID = c.ID, Name = c.NAME }).ToList();
                return NbpCategoriesList;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
