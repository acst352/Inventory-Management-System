using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Marca>().ToTable("Marca");
            modelBuilder.Entity<Departamento>().ToTable("Departamento");
            modelBuilder.Entity<Producto>().ToTable("Producto");

            base.OnModelCreating(modelBuilder);
        }
    }
}
