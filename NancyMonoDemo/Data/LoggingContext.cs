using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using NancyMonoDemo.Entities;

namespace NancyMonoDemo.Data
{
    public class LoggingContext : DbContext
    {
        public LoggingContext() : base("LoggingContext")
        {
        }

        public DbSet<PageRequest> PageRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // PostgreSQL uses the public schema by default - not dbo.
            // TODO: would need to add an environment variable to control dbo vs public for various databases?
            modelBuilder.HasDefaultSchema("demo");

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
