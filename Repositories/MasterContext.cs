using Microsoft.EntityFrameworkCore;
using TechSupportHelpSystem.Models;

namespace TechSupportHelpSystem.Repositories
{
    public class MasterContext : DbContext
    {
        public DbSet<Client> Client { get; set; }

        public MasterContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
