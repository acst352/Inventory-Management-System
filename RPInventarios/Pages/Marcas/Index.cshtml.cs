using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Marcas
{
    public class IndexModel : PageModel
    {
        private readonly RPInventarios.Data.InventariosContext _context;

        public IndexModel(RPInventarios.Data.InventariosContext context)
        {
            _context = context;
        }

        public IList<Marca> Marca { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Marca = await _context.Marca.ToListAsync();
        }
    }
}
