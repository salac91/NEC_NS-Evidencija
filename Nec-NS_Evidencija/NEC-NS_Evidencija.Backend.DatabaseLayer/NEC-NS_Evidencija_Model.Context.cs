﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NEC_NS_Evidencija.Backend.DatabaseLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MyEntities : DbContext
    {
        public MyEntities()
            : base("name=MyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<COACH> COACHes { get; set; }
        public DbSet<MONTLYPAYMENT> MONTLYPAYMENTS { get; set; }
        public DbSet<PERSON> People { get; set; }
        public DbSet<STUDENT> STUDENTs { get; set; }
        public DbSet<STUDENT_TRAINING> STUDENT_TRAINING { get; set; }
        public DbSet<TRAINING> TRAININGs { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}