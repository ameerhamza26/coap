using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class IssuingAgency
    {
       
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<IssuingAgency> GetIssuingAgency()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var IssuingAgencyList = db.ISSUING_AGENCY.Select(c => new IssuingAgency { ID = c.ID, Name = c.NAME }).ToList();
                return IssuingAgencyList;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
