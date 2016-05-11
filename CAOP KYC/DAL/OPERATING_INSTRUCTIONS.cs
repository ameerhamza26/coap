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
    
    public partial class OPERATING_INSTRUCTIONS
    {
        public int ID { get; set; }
        public Nullable<int> BI_ID { get; set; }
        public Nullable<int> AUTHORITY_TO_OPERATE { get; set; }
        public string DESCRIPTION_IF_OTHER { get; set; }
        public Nullable<bool> ZAKAT_DEDUCTION { get; set; }
        public Nullable<int> ZAKAT_EXEMPTION_TYPE { get; set; }
        public string EXEMPTION_REASON_DETAIL { get; set; }
        public Nullable<int> ACCOUNT_STATEMENT_FREQUENCY { get; set; }
        public string DESCRIPTION_IF_HOLD_MAIL { get; set; }
        public Nullable<bool> ATM_CARD_REQUIRED { get; set; }
        public string CUSTOMER_NAME_ON_ATMCARD { get; set; }
        public Nullable<int> E_STATEMENT_REQUIRED { get; set; }
        public Nullable<bool> MOBILE_BANKING_REQUIRED { get; set; }
        public string MOBILE_NO { get; set; }
        public Nullable<bool> IBT_ALLOWED { get; set; }
        public Nullable<bool> IS_PROFIT_APPLICABLE { get; set; }
        public Nullable<bool> IS_FED_EXEMPTED { get; set; }
        public string EXPIRY_DATE_EXEMPTED { get; set; }
        public string APPLICABLE_PROFIT_RATE { get; set; }
        public Nullable<int> PROFIT_PAYMENT { get; set; }
        public Nullable<bool> WHT_DEDUCTED_ON_PROFIT { get; set; }
        public string EXPIRY_DATE_PROFIT { get; set; }
        public Nullable<bool> WHT_DEDUCTED_ON_TRANSACTION { get; set; }
        public string EXPIRY_DATE_TRANSACTION { get; set; }
        public string SPECIAL_PROFIT_VALUE { get; set; }
        public Nullable<System.DateTime> UPDATED_DATE { get; set; }
    }
}