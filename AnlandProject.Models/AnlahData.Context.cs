﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnlandProject.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class anlandEntities : DbContext
    {
        public anlandEntities()
            : base("name=anlandEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<admin> admin { get; set; }
        public virtual DbSet<board_setup> board_setup { get; set; }
        public virtual DbSet<board1> board1 { get; set; }
        public virtual DbSet<board1class> board1class { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<director> director { get; set; }
        public virtual DbSet<email> email { get; set; }
        public virtual DbSet<form> form { get; set; }
        public virtual DbSet<lawview> lawview { get; set; }
        public virtual DbSet<post_group> post_group { get; set; }
        public virtual DbSet<post_team> post_team { get; set; }
        public virtual DbSet<qa> qa { get; set; }
        public virtual DbSet<qaclass> qaclass { get; set; }
        public virtual DbSet<qaview> qaview { get; set; }
        public virtual DbSet<setup_smtp> setup_smtp { get; set; }
        public virtual DbSet<suggest> suggest { get; set; }
        public virtual DbSet<suggest2> suggest2 { get; set; }
        public virtual DbSet<suggest3> suggest3 { get; set; }
        public virtual DbSet<type> type { get; set; }
        public virtual DbSet<main_menu> main_menu { get; set; }
        public virtual DbSet<cake> cake { get; set; }
        public virtual DbSet<Cate> Cate { get; set; }
        public virtual DbSet<service> service { get; set; }
        public virtual DbSet<theme> theme { get; set; }
        public virtual DbSet<newsclass> newsclass { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<lawclass> lawclass { get; set; }
        public virtual DbSet<an_news> an_news { get; set; }
        public virtual DbSet<landlaw> landlaw { get; set; }
        public virtual DbSet<intro10> intro10 { get; set; }
    }
}
