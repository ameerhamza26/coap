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
    
    public partial class BRANCHES
    {
        public int BRANCH_ID { get; set; }
        public int REGION_ID { get; set; }
        public string NAME { get; set; }
        public string BRANCH_CODE { get; set; }
        public Nullable<int> CATEGORY_ID { get; set; }
        public string AREA { get; set; }
        public string ServiceCenterCode { get; set; }
    
        public virtual REGIONS REGIONS { get; set; }
    }
}