//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AmbucareDAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class T74060
    {
        public int T_PR_RCV_ID { get; set; }
        public string T_PR_RCV_NO { get; set; }
        public Nullable<int> T_PR_ID { get; set; }
        public string T_RCV_UNIT_NO { get; set; }
        public string T_CHALLAN_NO { get; set; }
        public Nullable<System.DateTime> T_RECEIVE_DATE { get; set; }
        public Nullable<int> T_SUPPLIER_ID { get; set; }
        public string T_BILL_NO { get; set; }
        public Nullable<System.DateTime> T_BILL_DATE { get; set; }
        public string T_DISCOUNT { get; set; }
        public string T_DISCOUNT_PERCENT { get; set; }
        public Nullable<decimal> T_DISCOUNT_AMOUNT { get; set; }
        public Nullable<decimal> T_TOTAL_VALUE { get; set; }
        public Nullable<decimal> T_TOTAL_VLU_AFTER_DISC { get; set; }
        public Nullable<decimal> T_GRAND_TOTAL_VALUE { get; set; }
        public string T_REMARKS { get; set; }
        public Nullable<decimal> T_VAT { get; set; }
        public string T_RECEIVE_BY { get; set; }
        public Nullable<System.DateTime> T_ACTUAL_DELIVERY_DATE { get; set; }
        public Nullable<int> T_CURRENCY_ID { get; set; }
        public Nullable<decimal> T_CURRENCY_CONVERSION_TK { get; set; }
        public string T_ENTRY_USER { get; set; }
        public Nullable<System.DateTime> T_ENTRY_DATE { get; set; }
        public string T_UPD_USER { get; set; }
        public Nullable<System.DateTime> T_UPD_DATE { get; set; }
        public string T_LC_NO { get; set; }
        public Nullable<System.DateTime> T_LC_DATE { get; set; }
    }
}