using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Sms_Alert_Required
    {
        public int ID { get; set; }
        public string NAME { get; set; }

        public List<Sms_Alert_Required> GetAccountTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var List = db.SMS_ALERT_REQUIRED.Select(c => new Sms_Alert_Required { ID = c.ID, NAME = c.NAME }).ToList();
                return List;
            }
        }
    }
}
