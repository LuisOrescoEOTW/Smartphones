using Microsoft.EntityFrameworkCore;
using Smartphones.Models;

namespace Smartphones.Models
{
    public class SmartphoneContext : DbContext
    {
        public SmartphoneContext(DbContextOptions<SmartphoneContext> options) : base(options)
        {
        }
        public DbSet<App> App { get; set; }
        public DbSet<Instalacion> Instalacion { get; set; }
        public DbSet<Operario> Operario { get; set; }
        public DbSet<Sensor> Sensor { get; set; }
        public DbSet<Telefono> Telefono { get; set; }


        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }*/



     }
}
