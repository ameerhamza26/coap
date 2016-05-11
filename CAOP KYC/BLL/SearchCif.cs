using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SearchCif
    {
        public int ID { get; set; }
        public CifType CifType { get; set; }
        public string NAME { get; set; }
        public string CNIC { get; set; }

        public string ADDRESS_PERMANENT { get; set; }
        public District DISTRICT_PERMANENT { get; set; }
        public string POBOX_PERMANENT { get; set; }
        public City CITY_PERMANENT { get; set; }
        public string POSTAL_CODE_PERMANENT { get; set; }
        public Country COUNTRY_PERMANENT { get; set; }     
        public string OFFICE_CONTACT { get; set; }
        public string RESIDENCE_CONTACT { get; set; }
        public string MOBILE_NO { get; set; }
        public string FAX_NO { get; set; }
        public string EMAIL { get; set; }

    }
}
