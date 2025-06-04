using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Usuarios;

public class IndexModel : PageModel
{
    private readonly InventariosContext _context;

    public IndexModel(InventariosContext context)
    {
        _context = context;
    }

    public IList<Usuario> Usuario { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Usuario = await _context.Usuarios
            .Include(u => u.Perfil).ToListAsync();
    }
}
