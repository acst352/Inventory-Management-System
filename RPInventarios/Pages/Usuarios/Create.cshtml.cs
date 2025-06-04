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
        ViewData["PerfilList"] = Enum.GetValues(typeof(EstatusProducto))
            .Cast<EstatusProducto>().Select(e => new SelectListItem
            {
                Value = ((int)e).ToString(),
                Text = e.GetType()
                        .GetMember(e.ToString())[0]
                        .GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false)
                        .Cast<System.ComponentModel.DataAnnotations.DisplayAttribute>()
                        .FirstOrDefault()?.Name ?? e.ToString()
            }).ToList();
    }

    public IActionResult OnGet()
    {
        ViewData["PerfilId"] = new SelectList(_context.Perfiles, "Id", "Nombre");
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
