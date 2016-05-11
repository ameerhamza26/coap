using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class FrequencyGrossSale
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<FrequencyGrossSale> GetFrequencyGrossSale()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var FrequencyGrossSalesList = db.FREQUENCY_GROSS_SALES.Select(c => new FrequencyGrossSale { ID = c.ID, Name = c.NAME }).ToList();
                return FrequencyGrossSalesList;
            }
        }
    }
}
