using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class AverageCashDeposit
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<AverageCashDeposit> GetAverageCashDeposits()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AverageCashDepositList = db.AVERAGE_CASH_DEPOSITS.Select(c => new AverageCashDeposit { ID = c.ID, Name = c.NAME }).ToList();
                return AverageCashDepositList;
            }
        }
    }
}
