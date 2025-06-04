using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Usuarios;

public class DeleteModel : PageModel
{
    private readonly InventariosContext _context;

    public DeleteModel(InventariosContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Usuario Usuario { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == id);

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

        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            Usuario = usuario;
            _context.Usuarios.Remove(Usuario);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
