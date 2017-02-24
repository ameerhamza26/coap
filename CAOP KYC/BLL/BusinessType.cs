using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class BusinessType
    {
        public int? ID { get; set; }
        public string Name { get; set; }


        public List<BusinessType> GetBusinessTypes(int BusinessSegmentVal)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                string ZBIZSEG = db.BUSINESS_CUSTOMER_CLASSIFICATIONS.FirstOrDefault(b => b.ID == BusinessSegmentVal).ZBIZSEG;
                var ListBusinessTypes = db.BUSINESS_TYPE.Where(b => b.ZBIZSEG == ZBIZSEG).Select(b => new BusinessType() { ID = b.ID, Name = b.Name }).ToList();
                return ListBusinessTypes;
            }
        }
    }
}
