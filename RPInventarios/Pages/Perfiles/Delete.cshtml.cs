using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;

namespace RPInventarios.Pages.Perfil;

public class DeleteModel : PageModel
{
    private readonly InventariosContext _context;

    public DeleteModel(InventariosContext context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var perfil = await _context.Perfiles.FindAsync(id);
        if (perfil != null)
        {
            Perfil = perfil;
            _context.Perfiles.Remove(Perfil);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
