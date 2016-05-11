using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class Know_Your_Customer_Relationship
    {
        public int? ID { get; set; }
        public string NAME { get; set; }

        public List<Know_Your_Customer_Relationship> GetRelationships()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var List = db.KNOW_YOUR_CUSTOMER_RELATIONSHIP.Select(c => new Know_Your_Customer_Relationship { ID = c.ID, NAME = c.NAME }).ToList();
                return List;
            }
        }
    }
}
