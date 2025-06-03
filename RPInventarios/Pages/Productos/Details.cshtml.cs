using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Productos;

public class DetailsModel : PageModel
{
    private readonly InventariosContext _context;

    public DetailsModel(InventariosContext context)
    {
        _context = context;
    }

    public Producto Producto { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var producto = await _context.Productos
            .Include(p => p.Marca)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (producto is not null)
        {
            Producto = producto;

            return Page();
        }

        return NotFound();
    }
}