using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class MeansOfVerification
    {
        public int ID { get; set; }
        public string NAME { get; set; }

        public List<MeansOfVerification> GetAccountTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountTypeList = db.MEANS_OF_VERIFICATION.Select(c => new MeansOfVerification { ID = c.ID, NAME = c.NAME }).ToList();
                return AccountTypeList;
            }
        }
    }
}
