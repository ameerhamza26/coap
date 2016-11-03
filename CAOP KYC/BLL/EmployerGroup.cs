using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class EmployerGroup
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ZEMPGRP { get; set; }

        public List<EmployerGroup> GetEmpoyerGroup()
        {
            using(CAOPDbContext db = new CAOPDbContext())
            {
                var EmployerList = db.EMPLOYER_GROUP.Select(c => new EmployerGroup { ID = c.ID, Name = c.Name }).ToList();
                return EmployerList;
            }
        }

        public string GetZEMPGRP(int EmpGrpId)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.EMPLOYER_GROUP.FirstOrDefault(e => e.ID == EmpGrpId).ZEMPGRP;
            }
        }
    }
}
