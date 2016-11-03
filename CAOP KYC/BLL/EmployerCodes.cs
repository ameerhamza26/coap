using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class EmployerCodes
    {
        public int? ID { get; set; }
        public string Code { get; set; }

        public List<EmployerCodes> GetEmployerCode()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var EmployerCodeList = db.EMPLOYER_CODES.Select(c => new EmployerCodes { ID = c.ID, Code = c.CODE }).ToList();
                return EmployerCodeList;
            }
        }


    }
}
