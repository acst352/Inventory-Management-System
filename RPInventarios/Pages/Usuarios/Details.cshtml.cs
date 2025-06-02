using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RPInventarios.Pages.Usuarios
{
    public class DetailsModel : PageModel
    {
        private readonly RPInventarios.Data.InventariosContext _context;

        public DetailsModel(RPInventarios.Data.InventariosContext context)
        {
            _context = context;
        }

        public RPInventarios.Models.Usuarios Usuario { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Id == id);

            if (usuario is not null)
            {
                Usuario = usuario;

                return Page();
            }

            return NotFound();
        }
    }
}
