using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Marcas;

public class CreateModel : PageModel
{
    private readonly InventariosContext _context; // Declaración de clase privada de solo lectura
    private readonly INotyfService _servicioNotificacion;

    public CreateModel(InventariosContext context, INotyfService servicioNotificacion)
    {
        _context = context; // Inicialización de la variable context 
        _servicioNotificacion = servicioNotificacion; // Inicialización de la variable servicioNotificacion
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Marca Marca { get; set; } = default!;

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            _servicioNotificacion.Error($"Error al crear la marca {Marca.Nombre}");
            return Page();
        }

        /* Validar si ya existe una marca con el mismo nombre al intentar crear una marca */

        var existeMarcaBd = _context.Marcas.Any(u => u.Nombre.ToLower().Trim() == Marca.Nombre.ToLower().Trim());
        if (existeMarcaBd)
        {
            _servicioNotificacion.Warning($"Ya existe una marca con el nombre {Marca.Nombre}");
            return Page();
        }

        _context.Marcas.Add(Marca);
        await _context.SaveChangesAsync();
        _servicioNotificacion.Success($"Marca {Marca.Nombre} creada correctamente");
        return RedirectToPage("./Index");
    }
}
