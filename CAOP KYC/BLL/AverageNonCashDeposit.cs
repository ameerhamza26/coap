using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class AverageNonCashDeposit
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<AverageNonCashDeposit> GetAverageNonCashDeposit()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AverageNonCashDepositList = db.AVERAGE_NON_CASH_DEPOSITS.Select(c => new AverageNonCashDeposit { ID = c.ID, Name = c.NAME }).ToList();
                return AverageNonCashDepositList;
            }
        }
    }
}
