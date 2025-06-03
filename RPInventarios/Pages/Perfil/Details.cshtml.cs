using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;

namespace RPInventarios.Pages.Perfil;

public class DetailsModel : PageModel
{
    private readonly InventariosContext _context;

    public DetailsModel(InventariosContext context)
    {
        _context = context;
    }

    public RPInventarios.Models.Perfil Perfil { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var perfil = await _context.Perfiles.FirstOrDefaultAsync(m => m.Id == id);

        if (perfil is not null)
        {
            Perfil = perfil;

            return Page();
        }

        return NotFound();
    }
}
