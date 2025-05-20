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
    public class DetailsModel : PageModel
    {
        private readonly RPInventarios.Data.InventariosContext _context;

        public DetailsModel(RPInventarios.Data.InventariosContext context)
        {
            _context = context;
        }

        public Marca Marca { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas.FirstOrDefaultAsync(m => m.Id == id);

            if (marca is not null)
            {
                Marca = marca;

                return Page();
            }

            return NotFound();
        }
    }
}
