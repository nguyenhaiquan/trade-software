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
    
    public partial class investor
    {
        public investor()
        {
            this.portfolios = new HashSet<portfolio>();
        }
    
        public string code { get; set; }
        public byte type { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string displayName { get; set; }
        public byte sex { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string account { get; set; }
        public string password { get; set; }
        public string catCode { get; set; }
        public System.DateTime expireDate { get; set; }
        public byte status { get; set; }
        public string note { get; set; }
    
        public virtual ICollection<portfolio> portfolios { get; set; }
    }
}