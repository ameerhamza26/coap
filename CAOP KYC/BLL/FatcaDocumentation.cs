using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class FatcaDocumentation
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<FatcaDocumentation> GetFatcaDocumentation()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var FatcaClassificationList = db.FTCA_DOCUMENTATIONS.Select(c => new FatcaDocumentation { ID = c.ID, Name = c.Name }).ToList();
                return FatcaClassificationList;
            }
        }
    }
}
