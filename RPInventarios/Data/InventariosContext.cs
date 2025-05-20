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
        public InventariosContext (DbContextOptions<InventariosContext> options)
            : base(options)
        {
        }

        public DbSet<RPInventarios.Models.Marca> Marca { get; set; } = default!;
    }
}
