using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Marcas;

public class DeleteModel : PageModel
{
    private readonly InventariosContext _context;

    public DeleteModel(InventariosContext context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var marca = await _context.Marcas.FindAsync(id);
        if (marca != null)
        {
            Marca = marca;
            _context.Marcas.Remove(Marca);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}