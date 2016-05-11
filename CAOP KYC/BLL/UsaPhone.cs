using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class UsaPhone
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<UsaPhone> GetUsaPhone()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var UsaPhoneList = db.USA_PHONE_NOS.Select(c => new UsaPhone { ID = c.ID, Name = c.Name }).ToList();
                return UsaPhoneList;
            }
        }
    }
}
