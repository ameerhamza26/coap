using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class EmployerSubGroup
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ZEMPGRP { get; set; }
        public string ZEMPSUBG { get; set; }

        public List<EmployerSubGroup> GetEmpoyerSubGroup(string ZEMPGRP)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var EmployerList = db.EMPLOYER_SUB_GROUP.Where(c => c.ZEMPGRP == ZEMPGRP).Select(c => new EmployerSubGroup { ID = c.ID, Name = c.Name }).ToList();
                return EmployerList;
            }
        }

        public string GetZEMPGRP(int EmpSubGrpId)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.EMPLOYER_SUB_GROUP.FirstOrDefault(e => e.ID == EmpSubGrpId).ZEMPGRP;
            }
        }

        public string GetZEMPSUBG(int EmpSubGrpId)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.EMPLOYER_SUB_GROUP.FirstOrDefault(e => e.ID == EmpSubGrpId).ZEMPSUBG;
            }
        }
    }
}
