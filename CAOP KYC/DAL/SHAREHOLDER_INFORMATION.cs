//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SHAREHOLDER_INFORMATION
    {
        public int ID { get; set; }
        public Nullable<int> BID { get; set; }
        public string NAME { get; set; }
        public string ADDRESS { get; set; }
        public string IDENTITY_TYPE { get; set; }
        public Nullable<int> IDENTITY_TYPE_VALUE { get; set; }
        public string IDENTITY_NO { get; set; }
        public string IDENTITY_EXPIRY_DATE { get; set; }
        public string RESIDENCE_PHONE { get; set; }
        public string OFFICE_PHONE { get; set; }
        public string MOBILE_NO { get; set; }
        public string FAX_NO { get; set; }
        public string EMAIL { get; set; }
        public string NO_SHARES { get; set; }
        public string AMOUNT_SHARES { get; set; }
        public string SHAREHOLDER_PERCENTAGE { get; set; }
        public string NET_WORTH { get; set; }
        public string DIRECTOR_STATUS { get; set; }
        public Nullable<int> DIRECTOR_STATUS_VALUE { get; set; }
    }
}