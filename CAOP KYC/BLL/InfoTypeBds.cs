using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class InfoTypeBds
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<InfoTypeBds> GetInfoTypeBds()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var InfoTypeBdsList = db.INFO_TYPES_BDS.Select(c => new InfoTypeBds { ID = c.ID, Name = c.NAME }).ToList();
                return InfoTypeBdsList;
            }
        }
    }
}
