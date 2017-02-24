using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class SubIndustry
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<SubIndustry> GetSubIndustrys(int Sicval)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                string sic = db.SIC_CODES.FirstOrDefault(s => s.ID == Sicval).SIC;
                var List = db.SUB_INDUSTRY.Where(c => c.SIC == sic).Select(c => new SubIndustry { ID = c.ID, Name = c.Name }).ToList();
                return List;
            }
        }
    }
}
