using Microsoft.EntityFrameworkCore;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Models.DAO;

namespace TechSupportHelpSystem.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Client { get; set; }
        public DbSet<Room> Schdlr_Resource { get; set; }
        public DbSet<Modality> Modality { get; set; }
        public DbSet<ProcedureRef> ProcedureRef { get; set; }
        public DbSet<RoomToProcedure> Schdlr_ResourceProcedureref { get; set; }


        public ApplicationContext(DbContextOptions options)
             : base(options)
        {
            Database.EnsureCreated();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    IConfigurationRoot configuration = new ConfigurationBuilder()
        //        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        //        .AddJsonFile("appsettings.json")
        //        .Build();
        //    optionsBuilder.UseMySql(configuration.GetConnectionString("MasterDatabase"), new MySqlServerVersion(new Version(5, 7)));
        //}
    }
}
