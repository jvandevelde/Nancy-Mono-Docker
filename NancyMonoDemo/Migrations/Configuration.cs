using System.Collections.Generic;
using NancyMonoDemo.Data;
using System;
using System.Data.Entity.Migrations;
using NancyMonoDemo.Entities;

namespace NancyMonoDemo.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<LoggingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LoggingContext context)
        {
            var students = new List<PageRequest>
            {
                new PageRequest {EnvironmentReturned = "SeedData1", EsConnectionReturned = "SeedDate1", RequestTime = DateTime.Now},
                new PageRequest {EnvironmentReturned = "SeedData2", EsConnectionReturned = "SeedData2", RequestTime = DateTime.Now},
                new PageRequest {EnvironmentReturned = "SeedData3", EsConnectionReturned = "SeedDate3", RequestTime = DateTime.Now},
                new PageRequest {EnvironmentReturned = "SeedData4", EsConnectionReturned = "SeedDate4", RequestTime = DateTime.Now},
                new PageRequest {EnvironmentReturned = "SeedData5", EsConnectionReturned = "SeedDate5", RequestTime = DateTime.Now}
            };

            students.ForEach(s => context.PageRequests.Add(s));
            context.SaveChanges();
        }
    }
}
