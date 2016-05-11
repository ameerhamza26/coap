using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class MonthlyTurnOverCredit
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<MonthlyTurnOverCredit> GetMonthlyTurnOverCredit()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var MonthlyTurnOverCreditList = db.MONTHLY_TURNOVER_CREDITS.Select(c => new MonthlyTurnOverCredit { ID = c.ID, Name = c.Name }).ToList();
                return MonthlyTurnOverCreditList;
            }
        }
    }
}
