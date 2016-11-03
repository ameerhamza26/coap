using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public  class OfficerCodes
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<OfficerCodes> GetOfficerCodes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var List = db.OFFICE_CODES.Select(c => new OfficerCodes { ID = c.ID, Name = c.NAME }).ToList();
                return List;
            }
        }
    }
}
