using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Gl_code
    {
        public int? ID { get; set; }
        public string NAME { get; set; }

        public List<Gl_code> GetGlCodeTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var glCodes = db.GL_CODE.Select(c => new Gl_code { ID = c.ID, NAME = c.NAME }).ToList();
                return glCodes;
            }
        }
    }
}
