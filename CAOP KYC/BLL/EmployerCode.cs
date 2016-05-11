using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class EmployerCode
    {
        public int? ID { get; set; }
        public string Code { get; set; }

        public List<EmployerCode> GetEmployerCode()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var EmployerCodeList = db.EMPLOYER_CODES.Select(c => new EmployerCode { ID = c.ID, Code = c.CODE }).ToList();
                return EmployerCodeList;
            }
        }
    }
}
