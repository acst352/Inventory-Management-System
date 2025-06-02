using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Migrations;

namespace RPInventarios.Pages.Usuarios;

public class DetailsModel : PageModel
{
    private readonly InventariosContext _context;

    public DetailsModel(InventariosContext context)
    {
        _context = context;
    }

    public Usuario Usuario { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuario
            .FirstOrDefaultAsync(m => m.Id == id);

        if (usuario is not null)
        {
            Usuario = usuario;

            return Page();
        }

        return NotFound();
    }
}
