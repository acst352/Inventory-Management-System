using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Migrations;

namespace RPInventarios.Pages.Usuarios
{
    public class IndexModel : PageModel
    {
        private readonly InventariosContext _context;
        private readonly IConfiguration _configuration;

        public IndexModel(InventariosContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public List<Usuario> Usuarios { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Usuarios = await _context.Usuario.ToListAsync();
        }
    }
}
