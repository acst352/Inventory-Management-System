using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Marcas;

public class IndexModel : PageModel
{
    private readonly InventariosContext _context;

    public IndexModel(InventariosContext context)
    {
        _context = context;
    }

    public IReadOnlyList<Marca> Marcas { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Marcas = await _context.Marcas.ToListAsync();
    }
}