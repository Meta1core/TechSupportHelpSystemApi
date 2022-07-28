using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using TechSupportHelpSystem.Models;

namespace TechSupportHelpSystem.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Client { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseMySql(configuration.GetConnectionString("MasterDatabase"), new MySqlServerVersion(new Version(5, 7)));
        }
    }
}
