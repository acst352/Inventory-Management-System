using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

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

    private void CargarListas()
    {
        // Perfil de usuario
        ViewData["PerfilId"] = _context.Perfiles
            .AsNoTracking()
            .Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Nombre
            }).ToList();
    }

    public IActionResult OnGet()
    {
        ViewData["PerfilId"] = new SelectList(_context.Perfiles, "Id", "Nombre");
        CargarListas();
        return Page();
    }

    [BindProperty]
    public Usuario Usuario { get; set; } = default!;
    public SelectList Perfiles { get; set; }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            CargarListas();
            _servicioNotificacion.Error($"Es necesario corregir los problemas para poder crear el usuario {Usuario.Username}");
            return Page();
        }

        var existeUsuarioBd = _context.Usuarios.Any(u => u.Username.ToLower().Trim() == Usuario.Username.ToLower().Trim());
        if (existeUsuarioBd)
        {
            CargarListas();
            _servicioNotificacion.Warning($"Ya existe un usuario con la cuenta {Usuario.Username}");
            return Page();
        }

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
