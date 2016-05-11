using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class TransportationType
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public List<TransportationType> GetTransportationTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var TransportationTypeList = db.Transportation_Types.Select(c => new TransportationType { ID = c.ID, Name = c.Name }).ToList();
                return TransportationTypeList;
            }
        }
    }
}
