//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace databaseEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class priceDataSum
    {
        public string type { get; set; }
        public string stockCode { get; set; }
        public System.DateTime onDate { get; set; }
        public decimal openPrice { get; set; }
        public decimal closePrice { get; set; }
        public decimal lowPrice { get; set; }
        public decimal highPrice { get; set; }
        public decimal volume { get; set; }
    
        public virtual stockCode stockCode1 { get; set; }
    }
}
