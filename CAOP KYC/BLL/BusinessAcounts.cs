using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    class BusinessAcounts
    {

        public int ID { get; set; }
        public string Name { get; set; }

        public List<BusinessAcounts> GetBusinessAcounts()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var BusinessAcountsList = db.BUSINESS_ACOUNTS.Select(c => new BusinessAcounts { ID = c.ID, Name = c.NAME }).ToList();
                return BusinessAcountsList;
            }
        }
    }
}
