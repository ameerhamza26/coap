using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class Pep
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<Pep> GetPeps()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var PepList = db.PEPS.Select(c => new Pep { ID = c.ID, Name = c.Name }).ToList();
                return PepList;
            }
        }
    }
}
