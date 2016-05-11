using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class CustomerType
    {
        public int ID { get; set; }
        public string NAME { get; set; }

        public List<CustomerType> GetCustomerType()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountTypeList = db.CUSTOMER_TYPE.Select(c => new CustomerType { ID = c.ID, NAME = c.NAME }).ToList();
                return AccountTypeList;
            }
        }

        public List<CustomerType> GetCustomerTypeBusiness()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountTypeList = db.CUSTOMER_TYPE_BUSINESS.Select(c => new CustomerType { ID = c.ID, NAME = c.NAME }).ToList();
                return AccountTypeList;
            }
        }
    }
}
