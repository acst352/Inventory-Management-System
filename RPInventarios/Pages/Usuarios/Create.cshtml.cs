using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RPInventarios.Data;
using RPInventarios.Migrations;

namespace RPInventarios.Pages.Usuarios;

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
        return Page();
    }

    [BindProperty]
    public Usuarios Usuario { get; set; } = default!; // Cambiar el tipo a 'Usuarios'  

    // For more information, see https://aka.ms/RazorPagesCRUD.  
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Usuario.Add(Usuario);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
