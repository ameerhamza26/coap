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
    
    public partial class STATUS_LOG
    {
        public int ID { get; set; }
        public Nullable<int> USERID { get; set; }
        public Nullable<int> BID { get; set; }
        public string LOG_TYPE { get; set; }
        public string OLD_STATUS { get; set; }
        public string NEW_STATUS { get; set; }
        public Nullable<System.DateTime> LOG_DATETIME { get; set; }
    }
}
