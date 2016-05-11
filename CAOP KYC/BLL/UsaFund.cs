using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public  class UsaFund
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<UsaFund> GetUsaFund()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var UsaFundList = db.USA_FUNDSTRANSFERS.Select(c => new UsaFund { ID = c.ID, Name = c.Name }).ToList();
                return UsaFundList;
            }
        }
    }
}
