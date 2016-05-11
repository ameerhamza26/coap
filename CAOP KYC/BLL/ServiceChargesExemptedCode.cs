using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class ServiceChargesExemptedCode
    {

        public int ID { get; set; }
        public string NAME { get; set; }

        public List<ServiceChargesExemptedCode> GetAccountTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountTypeList = db.SERVICE_CHARGES_EXEMPTED_CODE.Select(c => new ServiceChargesExemptedCode { ID = c.ID, NAME = c.NAME }).ToList();
                return AccountTypeList;
            }
        }
    }
}
