using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Marcas;

public class CreateModel : PageModel
{
    private readonly InventariosContext _context; // Declaración de clase privada de solo lectura
    private readonly INotyfService _servicioNotificación;

    public CreateModel(InventariosContext context, INotyfService servicioNotificación)
    {
        _context = context; // Inicialización de la variable context 
        _servicioNotificación = servicioNotificación; // Inicialización de la variable servicioNotificación
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
            _servicioNotificación.Error($"Error al crear la marca {Marca.Nombre}");
            return Page();
        }

        _context.Marcas.Add(Marca);
        await _context.SaveChangesAsync();
        _servicioNotificación.Success($"Marca {Marca.Nombre} creada correctamente");
        return RedirectToPage("./Index");
    }
}
