using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class BusinessCustomerClassification
    {
       
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<BusinessCustomerClassification> GetBusinessCustomerClassifications()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var BusinessCustomerClassificationsList = db.BUSINESS_CUSTOMER_CLASSIFICATIONS.Select(c => new BusinessCustomerClassification { ID = c.ID, Name = c.NAME }).ToList();
                return BusinessCustomerClassificationsList;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
