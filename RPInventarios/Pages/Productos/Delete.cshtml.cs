using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Productos;

public class DeleteModel : PageModel
{
    private readonly InventariosContext _context;

    public DeleteModel(InventariosContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Producto Producto { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var producto = await _context.Productos.FirstOrDefaultAsync(m => m.Id == id);

        if (producto is not null)
        {
            Producto = producto;

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



        var producto = await _context.Productos.FindAsync(id);
        if (producto != null)
        {
            Producto = producto;
            _context.Productos.Remove(Producto);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
