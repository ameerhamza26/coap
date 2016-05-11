using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public  class Title
    {

        public int ID { get; set; }
        public string Name { get; set; }

        public List<Title> GetTitles()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var TitleList = db.TITLES.Select(c => new Title { ID = c.ID, Name = c.Name.Trim() }).ToList();
                return TitleList;
            }
        }


       // public int GetVal(string )

        public override string ToString()
        {
            return Name;
        }
    }
}
