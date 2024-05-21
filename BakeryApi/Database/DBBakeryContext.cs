using Microsoft.EntityFrameworkCore;
using BakeryApi.Entities;

namespace BakeryApi.Database
{
    public class DBBakeryContext: DbContext
    {
     
        //public DbSet<Baker> Bakers { get; set; }
        //public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Customer> Customers { get; set; }

        //public DbSet<Order> Orders { get; set; }

        //public DbSet<RecipesToOrders> RecipesToOrders { get; set; }
  
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=TWISTA;Database=Bakery;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}


//using SoftwareCompany.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration.Conventions;
//using System.Linq;


//namespace SoftwareCompany.DAL
//{
//    public class SoftwareCompanyDbContext : DbContext
//    {
//        public SoftwareCompanyDbContext() : base("SoftwareCompanyDbContext")
//        {
//        }

//        // To be deleted
//        public DbSet<Project> Projects { get; set; }
//        public DbSet<Recipe> Recipes { get; set; }

//        public DbSet<Holiday> Holidays { get; set; }
//        public DbSet<Baker> Bakers { get; set; }

//        public DbSet<BakerHolidays> BakerHolidays { get; set; }

//        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        //{
//        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
//        //    base.OnModelCreating(modelBuilder);
//        //}

//    }
//}