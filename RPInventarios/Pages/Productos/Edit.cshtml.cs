using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Productos;

public class EditModel : PageModel
{
    private readonly InventariosContext _context;
    private readonly INotyfService _servicioNotificacion;

    public EditModel(InventariosContext context, INotyfService servicioNotificacion)
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
            _servicioNotificacion.Warning("El ID del producto debe tener un valor no nulo");
            return NotFound();
        }

        var producto = await _context.Productos.FirstOrDefaultAsync(m => m.Id == id);
        if (producto == null)
        {
            _servicioNotificacion.Warning($"No se encontró el producto con ID {id}");
            return NotFound();
        }
        Producto = producto;
        ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre");

        // Obten los estados del producto
        ViewData["EstatusList"] = Enum.GetValues(typeof(EstatusProducto))
                    .Cast<EstatusProducto>()
                    .Select(e => new SelectListItem
                    {
                        Value = ((int)e).ToString(),
                        Text = e.GetType()
                                .GetMember(e.ToString())[0]
                                .GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false)
                                .Cast<System.ComponentModel.DataAnnotations.DisplayAttribute>()
                                .FirstOrDefault()?.Name ?? e.ToString()
                    }).ToList();

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            _servicioNotificacion.Error($"Error al editar el producto {Producto.Nombre}");
            return Page();
        }
        /* Validar si ya existe un producto con el mismo nombre al intentar editar un producto */
        var existeProductoBd = _context.Marcas.Any(u => u.Nombre.ToLower().Trim() == Producto.Nombre.ToLower().Trim() && u.Id != Producto.Id);
        if (existeProductoBd)
        {
            _servicioNotificacion.Warning($"Ya existe un producto con el nombre {Producto.Nombre}");
            return Page();
        }

        _context.Attach(Producto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductoExists(Producto.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        _servicioNotificacion.Success($"Producto {Producto.Nombre} editado correctamente");
        return RedirectToPage("./Index");
    }

    private bool ProductoExists(int id)
    {
        return _context.Productos.Any(e => e.Id == id);
    }
}
