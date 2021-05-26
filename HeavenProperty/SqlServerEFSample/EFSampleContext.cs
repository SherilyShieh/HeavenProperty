using System;
using Microsoft.EntityFrameworkCore;
using SqlServerEFSample;

namespace HeavenProperty.SqlServerEFSample
{
    public class EFSampleContext : DbContext
    {
        string _connectionString;
        public EFSampleContext()
        {

        }
        public EFSampleContext(string connectionString)
        {
            this._connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (this._connectionString == null)
            {
                optionsBuilder.UseSqlServer("Server = localhost, 1433; Database = EFSampleDB; User = sa; Password = yourStrong(!)Password");
            } else
            {
                optionsBuilder.UseSqlServer(this._connectionString);

            }
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Seller> Sellers { get; set; }
    }

}
