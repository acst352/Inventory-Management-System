using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Productos;

public class DeleteModel : PageModel
{
    private readonly InventariosContext _context;
    private readonly INotyfService _servicioNotificacion;

    public DeleteModel(InventariosContext context, INotyfService servicioNotificacion)
    {
        _context = context;
        _servicioNotificacion = servicioNotificacion;
    }

    [BindProperty]
    public Producto Producto { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            _servicioNotificacion.Warning("El ID del producto debe tener un valor");
            return NotFound();
        }

        var producto = await _context.Productos
            .Include(p => p.Marca)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (producto is not null)
        {
            Producto = producto;

            return Page();
        }
        _servicioNotificacion.Warning($"No se encontró el producto con ID {id}");
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
        _servicioNotificacion.Success($"Producto {Producto.Nombre} eliminado correctamente");
        return RedirectToPage("./Index");
    }
}
