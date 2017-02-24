using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class SicCode
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<SicCode> GetSicCodes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var List = db.SIC_CODES.Select(c => new SicCode { ID = c.ID, Name = c.Name }).ToList();
                return List;
            }
        }
    }
}
