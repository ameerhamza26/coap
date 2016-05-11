using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class ProfitPayment
    {


        public int ID { get; set; }
        public string NAME { get; set; }

        public List<ProfitPayment> GetAccountTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountTypeList = db.PROFIT_PAYMENT.Select(c => new ProfitPayment { ID = c.ID, NAME = c.NAME }).ToList();
                return AccountTypeList;
            }
        }
    }
}
