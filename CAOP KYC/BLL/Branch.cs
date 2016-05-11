using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class Branch
    {
        public int BRANCH_ID { get; set; }
        public string NAME { get; set; }
        public string BRANCH_CODE { get; set; }
        public string AREA { get; set; }
        public Region Region { get; set; }
       public Branch(int BRANCH_ID, string NAME, string BRANCH_CODE, Region Region, string AREA)
       {
           this.BRANCH_ID = BRANCH_ID;
           this.NAME = NAME;
           this.BRANCH_CODE = BRANCH_CODE;
           this.Region = Region;
           this.AREA = AREA;
          
       }

       public Branch()
       {
 
       }

        public string GetBranchNameWithCode(string BranchCode)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.BRANCHES.Where(b => b.BRANCH_CODE == BranchCode).Any())
                {
                    var Branch = db.BRANCHES.FirstOrDefault(b => b.BRANCH_CODE == BranchCode);
                    return Branch.BRANCH_CODE + "-" + Branch.NAME;
                }
                else
                    return "";
            }
        }
        
    }
}
