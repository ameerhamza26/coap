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
    
    public partial class BUSINESS_DETAIL
    {
        public int ID { get; set; }
        public Nullable<int> INFO_TYPE { get; set; }
        public string INFO_DESC { get; set; }
        public Nullable<int> BI_ID { get; set; }
        public Nullable<System.DateTime> UPDATED_DATE { get; set; }
        public string MAIN_BUSINESS_ACTIVITY { get; set; }
        public string MOJOR_SUPPLIERS { get; set; }
        public string MAIN_CUSTOMERS { get; set; }
    }
}