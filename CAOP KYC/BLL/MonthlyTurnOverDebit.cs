using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class MonthlyTurnOverDebit
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<MonthlyTurnOverDebit> GetMonthlyTurnOverDebit()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var MonthlyTurnOverDebitList = db.MONTHLY_TURNOVER_DEBITS.Select(c => new MonthlyTurnOverDebit { ID = c.ID, Name = c.Name }).ToList();
                return MonthlyTurnOverDebitList;
            }
        }
    }
}
