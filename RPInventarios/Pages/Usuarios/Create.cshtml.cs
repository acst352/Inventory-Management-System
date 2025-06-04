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

    public CreateModel(InventariosContext context)
    {
        _context = context;
    }

    private void CargarListas()
    {
        // Perfil de usuario
        ViewData["PerfilId"] = _context.Perfiles // Corrected "Perfil" to "Perfiles"
            .AsNoTracking()
            .Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Nombre
            }).ToList();
    }

    public IActionResult OnGet()
    {
        ViewData["PerfilId"] = new SelectList(_context.Perfiles, "Id", "Nombre"); // Corrected "Perfil" to "Perfiles"
        CargarListas();
        return Page();
    }

    [BindProperty]
    public Usuario Usuario { get; set; } = default!;

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Usuarios.Add(Usuario);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
