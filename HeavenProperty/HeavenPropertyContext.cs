using System;
using HeavenProperty.Models;
using Microsoft.EntityFrameworkCore;

namespace HeavenProperty
{
    public class HeavenPropertyContext : DbContext
    {
        string _connectionString;
        public HeavenPropertyContext()
        {

        }
        public HeavenPropertyContext(string connectionString)
        {
            this._connectionString = connectionString;

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Seller>(entity => {
                entity.HasIndex(e => new { e.Email, e.Phone }).IsUnique();
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (this._connectionString == null)
            {
                optionsBuilder.UseSqlServer("Server = localhost, 1433; Database = HeavenProperty; User = sa; Password = yourStrong(!)Password");
            }
            else
            {
                optionsBuilder.UseSqlServer(this._connectionString);

            }
        }


        public DbSet<Property> Properties { get; set; }
        public DbSet<Seller> Sellers { get; set; }


    }
}
