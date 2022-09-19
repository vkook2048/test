using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AnotherSQLite
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=c:\\Users\\Lexa\\Desktop\\SmartGit\\test\\third.s3db");
        }
    }
}
