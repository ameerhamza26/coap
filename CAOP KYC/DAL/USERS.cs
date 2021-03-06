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
    
    public partial class USERS
    {
        public USERS()
        {
            this.USERS_PERMISSIONS = new HashSet<USERS_PERMISSIONS>();
            this.USERS_ROLES = new HashSet<USERS_ROLES>();
        }
    
        public int USER_ID { get; set; }
        public int PARENT_ID { get; set; }
        public string USER_NAME { get; set; }
        public string DISPLAY_NAME { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
        public string DESIGNATION { get; set; }
        public string USER_TYPE { get; set; }
        public Nullable<decimal> SAPID { get; set; }
        public Nullable<int> CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATETIME { get; set; }
        public Nullable<int> UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATED_DATETIME { get; set; }
        public Nullable<bool> FIRST_LOGIN { get; set; }
        public Nullable<bool> ACTIVE { get; set; }
    
        public virtual ICollection<USERS_PERMISSIONS> USERS_PERMISSIONS { get; set; }
        public virtual ICollection<USERS_ROLES> USERS_ROLES { get; set; }
    }
}
