//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace databaseEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class importPrice
    {
        public System.DateTime onDate { get; set; }
        public string stockCode { get; set; }
        public decimal closePrice { get; set; }
        public decimal volume { get; set; }
        public bool isTotalVolume { get; set; }
        public Nullable<decimal> totalVolume { get; set; }
    }
}
