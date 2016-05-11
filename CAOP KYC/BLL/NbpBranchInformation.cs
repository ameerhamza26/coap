using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class NbpBranchInformation
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<NbpBranchInformation> GetNbpBranchInformation()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var NbpBranchInformationList = db.NBP_BRANCH_INFORMATIONS.Select(c => new NbpBranchInformation { ID = c.ID, Name = c.Name }).ToList();
                return NbpBranchInformationList;
            }
        }
    }
}
