using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class UsaResidenceCard
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<UsaResidenceCard> GetUsaResidenceCards()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var UsaResidenceCardList = db.USA_RESIDENCE_CARDS.Select(c => new UsaResidenceCard { ID = c.ID, Name = c.Name }).ToList();
                return UsaResidenceCardList;
            }
        }
    }
}
