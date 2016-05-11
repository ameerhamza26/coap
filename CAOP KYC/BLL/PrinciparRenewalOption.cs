using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class PrinciparRenewalOption
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<PrinciparRenewalOption> GetPrincipalRenewalOtions()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var List = db.PRINCIPAL_RENEWAL_OPTION.Select(c => new PrinciparRenewalOption { ID = c.ID, Name = c.Name }).ToList();
                return List;
            }
        }
    }
}
