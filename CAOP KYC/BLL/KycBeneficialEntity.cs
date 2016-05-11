using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class KycBeneficialEntity
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string IDENTITY_DOCUMENT { get; set; }
        public string IDENTITY_NUMBER { get; set; }
        public string EXPIRY_DATE { get; set; }
        public string POB { get; set; }
        public string US { get; set; }
        public string OWNERSHIP { get; set; }
    }
}
