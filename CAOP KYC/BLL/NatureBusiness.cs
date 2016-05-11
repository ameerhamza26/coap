using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class NatureBusiness
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<NatureBusiness> GetAccountTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var NatureBusinessList = db.NATURE_BUSINESS.Select(c => new NatureBusiness { ID = c.ID, Name = c.NAME }).ToList();
                return NatureBusinessList;
            }
        }

        public List<NatureBusiness> GetNatureBusinessGovv()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var NatureBusinessList = db.NATURE_BUSINESS_GOV.Select(c => new NatureBusiness { ID = c.ID, Name = c.NAME }).ToList();
                return NatureBusinessList;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
