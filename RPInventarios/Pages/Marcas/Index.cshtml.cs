using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Marcas;

public class IndexModel : PageModel
{
    private readonly InventariosContext _context;
    private readonly IConfiguration _configuration;

    // Readonly indica que la variable _context solo puede ser asignada en el constructor de la clase.
    // Después de eso no puede ser modificada en ningún otro lugar de la clase, 
    // lo que previene cambios accidentales o intencionales. Garantiza la inmutabilidad de la referencia.

    public IndexModel(InventariosContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public List<Marca> Marcas { get; set; } = default!;
    [BindProperty(SupportsGet = true)]
    public int? Pagina { get; set; }
    public int TotalRegistros { get; set; }
    public int TotalPaginas { get; set; }
    public async Task OnGetAsync()
    {
        // Paginación manual usando Skip y Take con EF 
        var registrosPorPagina = _configuration.GetValue("RegistrosPorPagina", 10);
        var consulta = _context.Marcas.AsNoTracking();

        TotalRegistros = await consulta.CountAsync();
        var numeroPagina = Pagina ?? 1;
        TotalPaginas = (int)Math.Ceiling((double)TotalRegistros / registrosPorPagina);

        Marcas = await consulta
            .OrderBy(m => m.Id)
            .Skip((numeroPagina - 1) * registrosPorPagina)
            .Take(registrosPorPagina)
            .ToListAsync();
    }
}