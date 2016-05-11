using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class SourceOfFunds
    {

        public int ID { get; set; }
        public string NAME { get; set; }

        public List<SourceOfFunds> GetAccountTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountTypeList = db.SOURCE_OF_FUNDS.Select(c => new SourceOfFunds { ID = c.ID, NAME = c.NAME.Trim() }).ToList();
                return AccountTypeList;
            }
        }

        public List<SourceOfFunds> GetSourceOfFundsBusiness()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountTypeList = db.SOURCE_OF_FUNDS_BUSINESS.Select(c => new SourceOfFunds { ID = c.ID, NAME = c.NAME.Trim() }).ToList();
                return AccountTypeList;
            }
        }
    }
}
