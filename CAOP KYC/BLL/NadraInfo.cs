using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity.Core.Objects;

namespace BLL
{
    public class NadraInfo
    {
        public long ID { get; set; }
        public string CNIC { get; set; }
        public System.DateTime DATETIME { get; set; }
        public string NAME { get; set; }
        public string FH_NAME { get; set; }
        public string PRESENT_ADDRESS { get; set; }
        public string PERMANENT_ADDRESS { get; set; }
        public string DOB { get; set; }
        public string BIRTH_PLACE { get; set; }
        public string EXPIRYDATE { get; set; }
        public string STATUSCODE { get; set; }

        public NadraInfo GetNadraInfo(string CNIC)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.NADRA_INFO.Where(c => c.CNIC == CNIC).Any())
                {
                    NadraInfo BData = db.NADRA_INFO.
                                        Where(c => c.CNIC == CNIC)
                                        .OrderByDescending(c => c.DATETIME)
                                        .Select(c => new
                                            NadraInfo()
                                            {
                                                ID = c.ID,
                                                CNIC = c.CNIC.Substring(0, 5) + "-" + c.CNIC.Substring(5, 7) + "-" + c.CNIC.Substring(12, 1),
                                                DATETIME = (DateTime) EntityFunctions.TruncateTime(c.DATETIME),
                                                NAME = c.NAME,
                                                FH_NAME = c.FH_NAME,
                                                PRESENT_ADDRESS = c.PRESENT_ADDRESS,
                                                PERMANENT_ADDRESS = c.PERMANENT_ADDRESS,
                                                DOB = c.DOB,
                                                BIRTH_PLACE = c.BIRTH_PLACE,
                                                EXPIRYDATE = c.EXPIRYDATE,
                                                STATUSCODE = c.STATUSCODE
                                            }
                                            )
                                        .FirstOrDefault();

                    //BData.DOB = DateTime.ParseExact(BData.DOB, "dd-mm-yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MM-yyyy").Replace("/", "-");
                   // BData.EXPIRYDATE = DateTime.ParseExact(BData.EXPIRYDATE, "dd-mm-yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MM-yyyy").Replace("/", "-");
                    return BData;

                }
                else
                    return null;
            }
        }
    }
}
