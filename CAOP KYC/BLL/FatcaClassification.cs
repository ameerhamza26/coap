using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class FatcaClassification
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<FatcaClassification> GetFatcaClassification()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var FatcaClassificationList = db.FTCA_CLASSIFICATIONS.Select(c => new FatcaClassification { ID = c.ID, Name = c.Name }).ToList();
                return FatcaClassificationList;
            }
        }
    }
}
