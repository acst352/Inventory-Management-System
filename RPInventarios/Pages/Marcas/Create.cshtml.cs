using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Marcas;

public class CreateModel : PageModel
{
    private readonly InventariosContext _context; // Declaración de clase privada de solo lectura

    public CreateModel(InventariosContext context)
    {
        _context = context; // Inicialización de la variable context 
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
            return Page();
        }

        _context.Marcas.Add(Marca);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
