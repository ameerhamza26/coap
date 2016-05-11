using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class ApplicantInformationCifs
    {
        public int ID { get; set; }
        public int BI_ID { get; set; }
        public string CUSTOMER_CIF_NO { get; set; }
        public int IS_PRIMARY_ACCOUNT_HOLDER { get; set; }
        public int POWER_OF_ATTORNY { get; set; }
        public int SIGNATURE_AUTHORITY { get; set; }
        public string INVESTMENT_SHARE { get; set; }
        public string APPLICANT_STATUS { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string CUSTOMER_IDENTITY { get; set; }

        public List<ApplicantInformationCifs> APPLICANT_INFORMANTS { get; set; }
    }
}
