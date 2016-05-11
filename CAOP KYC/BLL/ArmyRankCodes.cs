using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public  class ArmyRankCodes
    {
       
        public int? ID { get; set; }
        public string Code { get; set; }

        public List<ArmyRankCodes> GetArmyCodes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var ArmyCodesList = db.ARMY_RANK_CODES.Select(c => new ArmyRankCodes { ID = c.ID, Code = c.CODE }).ToList();
                return ArmyCodesList;
            }
        }
    }
}
