using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class CifCustomerType
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<CifCustomerType> GetCustomerType()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var List = db.CIF_CUSTOMER_TYPE.Select(c => new CifCustomerType { ID = c.ID, Name = c.NAME }).ToList();
                return List;
            }
        }
    }
}
