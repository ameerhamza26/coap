using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public  class IdentityType
    {

        public int? ID { get; set; }
        public string Name { get; set; }

        public List<IdentityType> GetIdentityTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var TitleList = db.IDENTITY_TYPES.Select(i => new IdentityType { ID = i.ID, Name = i.NAME }).ToList();
                return TitleList;
            }
        }
    }
}
