//namespace Quantum.Framework
//{
//    using System;
//    using System.Data.Entity;
//    using System.ComponentModel.DataAnnotations.Schema;
//    using System.Linq;

//    public partial class QuantumDb : DbContext
//    {
//        public QuantumDb()
//            : base("name=QuantumDb")
//        {
//        }

//        public virtual DbSet<Account> Accounts { get; set; }

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Account>()
//                .Property(e => e.username)
//                .IsFixedLength();

//            modelBuilder.Entity<Account>()
//                .Property(e => e.password)
//                .IsFixedLength();
//        }
//    }
//}
