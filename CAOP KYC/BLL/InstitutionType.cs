using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class InstitutionType
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<InstitutionType> GetInstitutionTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var List = db.INSTITUTION_TYPE.Select(c => new InstitutionType { ID = c.ID, Name = c.Name }).ToList();
                return List;
            }
        }
    }
}
