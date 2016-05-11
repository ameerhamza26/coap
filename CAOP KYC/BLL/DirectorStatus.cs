using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class DirectorStatus
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<DirectorStatus> GetDirectorStatus()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var List = db.DIRECTOR_STATUS.Select(c => new DirectorStatus { ID = c.ID, Name = c.NAME }).ToList();
                return List;
            }
        }
    }
}
