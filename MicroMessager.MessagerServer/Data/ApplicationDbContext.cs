using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroMessager.Entites;
using Microsoft.EntityFrameworkCore;

namespace MicroMessager.MessagerServer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        
        public DbSet<Message> Messages { get; set; }
    }
}
