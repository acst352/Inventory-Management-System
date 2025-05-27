using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Productos;

public class CreateModel : PageModel
{
    private readonly InventariosContext _context;
    private readonly INotyfService _servicioNotificacion;

    public CreateModel(InventariosContext context, INotyfService servicioNotificacion)
    {
        _context = context;
        _servicioNotificacion = servicioNotificacion;
    }

    public IActionResult OnGet()
    {
        // Obtén todas las marcas de la base de datos y crea la lista de opciones
        ViewData["MarcaId"] = _context.Marcas
            .Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Nombre
            }).ToList();

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

    [BindProperty]
    public Producto Producto { get; set; } = default!;

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            _servicioNotificacion.Error($"Error al crear el producto {Producto.Nombre}");
            return Page();
        }

        _context.Productos.Add(Producto);
        await _context.SaveChangesAsync();
        _servicioNotificacion.Success($"Producto {Producto.Nombre} creado correctamente");
        return RedirectToPage("./Index");
    }
}