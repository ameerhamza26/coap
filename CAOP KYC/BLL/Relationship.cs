using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;


namespace BLL
{
    public class Relationship
    {
        public int? ID { get; set; }
        public string NAME { get; set; }


        public List<Relationship> GetRelationship()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountTypeList = db.RELATIONSHIP.Select(c => new Relationship { ID = c.ID, NAME = c.NAME }).ToList();
                return AccountTypeList;
            }
        }
    }
}
