using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Productos
{
    public class CreateModel : PageModel
    {
        private readonly RPInventarios.Data.InventariosContext _context;

        public CreateModel(RPInventarios.Data.InventariosContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre");
            return Page();
        }

        [BindProperty]
        public Producto Producto { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Productos.Add(Producto);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
