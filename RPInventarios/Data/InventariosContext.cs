using Microsoft.EntityFrameworkCore;
using RPInventarios.Models;

namespace RPInventarios.Data
{
    public class InventariosContext : DbContext
    {
        public InventariosContext(DbContextOptions<InventariosContext> options)
            : base(options)
        {
        }

        public DbSet<Marca> Marcas { get; set; } = default!;
        public DbSet<Departamento> Departamentos { get; set; } = default!;
        public DbSet<Producto> Productos { get; set; } = default!;

        // Mapeo del nombre de clases con el nombre en la tabla de la BD
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Marca>().ToTable("Marca");
            modelBuilder.Entity<Departamento>().ToTable("Departamento");
            modelBuilder.Entity<Producto>().ToTable("Producto");

            // Especificar precisión y escala para Costo
            modelBuilder.Entity<Producto>()
                .Property(p => p.Costo)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<RPInventarios.Models.Usuario> Usuario { get; set; }
    }
}
