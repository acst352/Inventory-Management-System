using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RPInventarios.Data;

namespace RPInventarios.Pages.Perfil;

public class CreateModel : PageModel
{
    private readonly InventariosContext _context;

    public CreateModel(InventariosContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public RPInventarios.Models.Perfil Perfil { get; set; } = default!;

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Perfiles.Add(Perfil);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
