using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RPInventarios.Pages.Usuarios
{
    public class DeleteModel : PageModel
    {
        private readonly RPInventarios.Data.InventariosContext _context;

        public DeleteModel(RPInventarios.Data.InventariosContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario != null)
            {
                Usuario = usuario;
                _context.Usuario.Remove(Usuario);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
