using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGumtree.Model.Migrations;

namespace VGumtree.Model
{
    public class VGumtreeDb : DbContext
    {
        public VGumtreeDb()
            : base("name=DefaultConnection")
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategorys { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<AdAttribute> AdAttributes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<AdminAreaLevel1> AdminAreaLevel1s { get; set; }
        public DbSet<AdminAreaLevel2> AdminAreaLevel2s { get; set; }
                

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Use singular table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Turn off ON DELETE CASCADE globally
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // Add ASP.NET WebPages Simple Membership User table
            modelBuilder.Configurations.Add(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}
