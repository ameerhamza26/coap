using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class CustomerDeal
    {
       
        public int? ID { get; set; }
        public string Name { get; set; }


        public List<CustomerDeal> GetCustomerDeals()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var CustomerDealList = db.COSTUMER_DEALINGS.Select(c => new CustomerDeal { ID = c.ID, Name = c.Name }).ToList();
                return CustomerDealList;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
