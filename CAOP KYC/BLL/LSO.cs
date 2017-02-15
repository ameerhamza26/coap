using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class LSO
    {
        public int? ID { get; set; }
        public string NAME { get; set; }
        public string ProfileCode { get; set; }

        public List<LSO> GetLSOList()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var LSOList = db.OFFICE_CODES.Select(c => new LSO { ID = c.ID, NAME = c.NAME }).ToList();
                return LSOList;
            }
        }

        public override string ToString()
        {
            return NAME;
        }
    }
}
