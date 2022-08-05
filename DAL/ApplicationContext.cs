using Microsoft.EntityFrameworkCore;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Models.DAO;
using TechSupportHelpSystem.Models.POCO;

namespace TechSupportHelpSystem.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Client { get; set; }
        public DbSet<Room> Schdlr_Resource { get; set; }
        public DbSet<Modality> Modality { get; set; }
        public DbSet<ProcedureRef> ProcedureRef { get; set; }
        public DbSet<ProceduresToRoom> Schdlr_ResourceProcedureref { get; set; }
        public DbSet<Clinic> Clinic { get; set; }
        public DbSet<ProceduresToClinic> ProcedureRef_Clinic { get; set; }
        public DbSet<CashSchedule> Cash_Fee_Schedule { get; set; }
        public DbSet<TeachingCollection> TeachingCollection { get; set; }
        public DbSet<Configuration> Configuration { get; set; }
        public DbSet<OHIPClinicGroupNumber> Ohip_ClinicNumber { get; set; }

        public ApplicationContext(DbContextOptions options)
             : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProceduresToRoom>()
                .HasKey(c => new { c.ID_Resource, c.ID_ProcedureRef });
            modelBuilder.Entity<ProceduresToClinic>()
                .HasKey(c => new { c.ID_ProcedureRef, c.ID_Clinic });
            modelBuilder.Entity<Configuration>()
                .HasKey(c => new { c.ParameterName });
            modelBuilder.Entity<OHIPClinicGroupNumber>()
                .HasKey(c => new { c.ID_Clinic, c.GroupNumber });
        }
    }
}
