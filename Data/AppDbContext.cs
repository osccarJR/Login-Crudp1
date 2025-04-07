using Microsoft.EntityFrameworkCore;
using MiCrudMVC.Models;

namespace MiCrudMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        // Método OnModelCreating para sembrar datos iniciales
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Agregar un usuario por defecto
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1, // Asegúrate de que el Id sea único y no choque con otros datos.
                    NombreUsuario = "admin", // Nombre de usuario por defecto.
                    Contraseña = "1234" // Contraseña por defecto.
                }
            );
        }
    }
}
