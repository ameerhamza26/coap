using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Sl_code
    {
        public int? ID { get; set; }
        public string NAME { get; set; }

        public List<Sl_code> GetGlCodeTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var slCodes = db.SL_CODE.Select(c => new Sl_code { ID = c.ID, NAME = c.NAME }).ToList();
                return slCodes;
            }
        }
    }
}
