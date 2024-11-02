using Microsoft.EntityFrameworkCore;
using ServerApp.Entities;
using System;

namespace ServerApp.Data
{
    public class RiraContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public RiraContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<Personel> Personels { get; set; }
    }
}
