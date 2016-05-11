using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class FatcasTin
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<FatcasTin> GetFatcasTin()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var FatcasTins = db.FATCAS_TINS.Select(c => new FatcasTin { ID = c.ID, Name = c.Name }).ToList();
                return FatcasTins;
            }
        }

    }
}
