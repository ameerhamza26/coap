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
    
    public partial class ACCOUNT_DELTE_LOG
    {
        public int ID { get; set; }
        public string TEMP_AC_NO { get; set; }
        public string BRANCH_CODE { get; set; }
        public string CNIC { get; set; }
        public string ACCOUNT_TITLE { get; set; }
        public string INITIAL_DEPOSIT { get; set; }
        public Nullable<int> ACCOUNT_MODE { get; set; }
        public Nullable<int> ACCOUNT_TYPE { get; set; }
        public Nullable<System.DateTime> ACCOUNT_ENTRY_DATE { get; set; }
        public Nullable<int> CREATED_USER { get; set; }
        public Nullable<int> DELETED_USER { get; set; }
        public Nullable<System.DateTime> DELETE_DATETIME { get; set; }
    }
}
