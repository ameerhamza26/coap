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
        public int ID { get; set; }
        public string ZEMPGRP { get; set; }
        public string ZEMPSUBG { get; set; }
        public string ZEMPNO { get; set; }
        public string Name { get; set; }

        public List<EmployerCode> GetEmployerCode(string ZEMPGRP, string ZEMPSUBG)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var EmployerCodeList = db.EMPLOYER_CODE.Where(e => e.ZEMPGRP == ZEMPGRP && e.ZEMPSUBG == ZEMPSUBG).Select(c => new EmployerCode { ID = c.ID, Name = c.Name }).ToList();
                return EmployerCodeList;
            }
        }
    }
}
