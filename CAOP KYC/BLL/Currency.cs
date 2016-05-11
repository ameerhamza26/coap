using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Currency
    {
        public int? ID { get; set; }
        public string NAME { get; set; }

        public List<Currency> GetGlCodeTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var currency = db.CURRENCY.Select(c => new Currency { ID = c.ID, NAME = c.NAME }).ToList();
                return currency;
            }
        }

    }
}
