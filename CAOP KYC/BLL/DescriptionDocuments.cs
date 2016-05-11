using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class DescriptionDocuments
    {

        public int ID { get; set; }
        public string SERIAL_NO { get; set; }
        public string Description { get; set; }

        public List<DescriptionDocuments> GetAccountTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountTypeList = db.Description_Documents.Select(c => new DescriptionDocuments { ID = c.ID, SERIAL_NO = c.SERIAL_NO, Description =c.Descr }).ToList();
                return AccountTypeList;
            }
        }

        public List<DescriptionDocuments> GetDescpDocBuss()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var AccountTypeList = db.Description_Documents_Business.Select(c => new DescriptionDocuments { ID = c.ID, SERIAL_NO = c.SERIAL_NO, Description = c.Descr }).ToList();
                return AccountTypeList;
            }
        }
    }
}
