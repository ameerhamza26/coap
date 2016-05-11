using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class E_Statement_Required
    {


        public int ID { get; set; }
        public string NAME { get; set; }

        public List<E_Statement_Required> GetAccountTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountTypeList = db.E_STATEMENT_REQUIRED.Select(c => new E_Statement_Required { ID = c.ID, NAME = c.NAME }).ToList();
                return AccountTypeList;
            }
        }
    }
}
