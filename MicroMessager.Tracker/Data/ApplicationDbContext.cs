using MicroMessager.Entites;
using Microsoft.EntityFrameworkCore;

namespace MicroMessager.Tracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Server> Servers { get; set; }
        public DbSet<ServerClient> ServerClients { get; set; }
    }
}