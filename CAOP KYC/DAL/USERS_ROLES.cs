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
    
    public partial class USERS_ROLES
    {
        public int USER_ROLE_ID { get; set; }
        public int USER_ID { get; set; }
        public int ROLE_ID { get; set; }
    
        public virtual USERS USERS { get; set; }
    }
}