using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class ApplicantStatuses
    {
       
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<ApplicantStatuses> GetApplicantStatuses()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var ApplicantStatusesList = db.APPLICANT_STATUSES.Select(c => new ApplicantStatuses { ID = c.ID, Name = c.NAME }).ToList();
                return ApplicantStatusesList;
            }
        }

        public List<ApplicantStatuses> GetApplicantStatusesBusiness()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var ApplicantStatusesList = db.APPLICANT_STATUSES_BUSINESS.Select(c => new ApplicantStatuses { ID = c.ID, Name = c.NAME }).ToList();
                return ApplicantStatusesList;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
