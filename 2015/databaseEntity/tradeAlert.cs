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
    
    public partial class tradeAlert
    {
        public int id { get; set; }
        public System.DateTime onTime { get; set; }
        public string type { get; set; }
        public byte tradeAction { get; set; }
        public string portfolio { get; set; }
        public string stockCode { get; set; }
        public string timeScale { get; set; }
        public string strategy { get; set; }
        public string subject { get; set; }
        public string msg { get; set; }
        public byte status { get; set; }
    
        public virtual portfolio portfolio1 { get; set; }
        public virtual stockCode stockCode1 { get; set; }
    }
}
