using Microsoft.EntityFrameworkCore;
using TrabajoFinalProgram3.Models;

namespace TrabajoFinalProgram3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Transaccion> Transacciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaccion>()
                .Property(t => t.CantidadCripto)
                .HasPrecision(18, 8);

            modelBuilder.Entity<Transaccion>()
                .Property(t => t.Dinero)
                .HasPrecision(18, 2);
        }
    }
}
