using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class PrimaryDocumentType
    {

        public int? ID { get; set; }
        public string Name { get; set; }


        public List<PrimaryDocumentType> GetPrimaryDocumentTypes()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var PrimaryDocumentTypeList = db.DOCUMENT_TYPES_PRIMARY.Select(c => new PrimaryDocumentType { ID = c.ID, Name = c.Name }).ToList();
                return PrimaryDocumentTypeList;
            }
        }

        public int GetDocValue(string DocumentName)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.DOCUMENT_TYPES_PRIMARY.FirstOrDefault(d => d.Name == DocumentName).ID;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
