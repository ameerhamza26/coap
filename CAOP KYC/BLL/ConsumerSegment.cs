using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class ConsumerSegment
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<ConsumerSegment> GetConsumerSegmentTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var ConsumerSegmentList = db.CONSUMER_SEGMENTS.Select(c => new ConsumerSegment { ID = c.ID, Name = c.Name }).ToList();
                return ConsumerSegmentList;
            }
        }
    }
}
