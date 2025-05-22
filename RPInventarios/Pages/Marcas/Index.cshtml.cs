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

    public IndexModel(InventariosContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public List<Marca> Marcas { get; set; } = default!;
    [BindProperty(SupportsGet = true)]
    public int? Pagina { get; set; }
    [BindProperty(SupportsGet = true)] // Permite enlazar la propiedad TerminoBusqueda desde la URL
    public string TerminoBusqueda { get; set; }
    public int TotalRegistros { get; set; }
    public int TotalPaginas { get; set; }
    public async Task OnGetAsync()
    {
        // Paginación manual usando Skip y Take con EF 
        var registrosPorPagina = _configuration.GetValue("RegistrosPorPagina", 10);
        var consulta = _context.Marcas.AsNoTracking();

        // Filtrar por nombre de marca
        if (!string.IsNullOrEmpty(TerminoBusqueda))
        {
            consulta = consulta.Where(m => m.Nombre.Contains(TerminoBusqueda));
        }

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