using Microsoft.EntityFrameworkCore;
using ams_finstek_dotnet.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ams_finstek_dotnet.Data
{
    public class AmsContext : DbContext
    {
        private readonly IConfiguration _config;
        public AmsContext(IConfiguration config)
        {
            _config = config;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }

        public DbSet<Access> Accesses { get; set; }
        
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:AmsContextDb"]);  
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*
            modelBuilder.Entity<User>()
                .HasData(new User()
                {
                    Id = 1,

                });*/
        }
    }
}
