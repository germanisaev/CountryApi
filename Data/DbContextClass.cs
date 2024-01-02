using CRUDWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDWebAPI.Data {
    public class DbContextClass: DbContext
    {
        protected readonly IConfiguration Configuration;

        protected readonly string[] _allowedOrigins;

        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;

            _allowedOrigins = configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}


