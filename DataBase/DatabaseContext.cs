
using Microsoft.EntityFrameworkCore;
using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;

namespace DataBase
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser,ApplicationRole,Int64>

    {
        public DatabaseContext() : base()
        {


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(DatabaseSettings.getConnectionString());
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Prints> Prints { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Types> Types { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
        }

    }
}
