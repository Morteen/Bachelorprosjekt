using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace APMKommune.Models
{
    public class APMContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Segment> Segments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Integration> Integrations { get; set; }
        public DbSet<Dataset> Datasets { get; set; }
        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}