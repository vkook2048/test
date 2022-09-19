using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SQLiteForMovement
{
    public class ApplicationContext : DbContext
    {
        // TODO Shop, Product, Area, Vendor
        // TODO change shopID to Shop
        public DbSet<Movement> Movements { get; set; } = null;
        public DbSet<Shop> Shops { get; set; } = null;
        public DbSet<Product> Products { get; set; } = null;
        public DbSet<Vendor> Vendors { get; set; } = null;
        public DbSet<Area> Areas { get; set; } = null;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=c:\\Users\\Lexa\\Desktop\\SmartGit\\test\\12.db");
        }
    }
}

