using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class MartialStatus
    {

        public int ID { get; set; }
        public string Name { get; set; }

        public List<MartialStatus> GetMartialStatus()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var MartialStatusList = db.MARTIAL_STATUSES.Select(c => new MartialStatus { ID = c.ID, Name = c.Name }).ToList();
                return MartialStatusList;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
