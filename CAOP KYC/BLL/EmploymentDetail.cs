﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;


namespace BLL
{
   public class EmploymentDetail
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<EmploymentDetail> GetEmploymentDetail()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var EmploymentDetailList = db.EMPLOYMENT_DETAILS.Select(c => new EmploymentDetail { ID = c.ID, Name = c.NAME }).ToList();
                return EmploymentDetailList;
            }
        }

        public bool GetEmpSubgMand(int EmpDetId)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return (bool) db.EMPLOYMENT_DETAILS.FirstOrDefault(e => e.ID == EmpDetId).EMP_SUBG_MAND;
            }
        }
    }
}
