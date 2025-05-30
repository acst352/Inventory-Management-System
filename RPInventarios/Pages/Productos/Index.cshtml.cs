#nullable enable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Productos;

public class IndexModel : PageModel
{
    private readonly InventariosContext _context;
    private readonly IConfiguration _configuration;

    public IndexModel(InventariosContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public List<Producto> Productos { get; set; } = default!;
    // Propiedades de B�squeda
    [BindProperty(SupportsGet = true)]
    public string? TerminoBusqueda { get; set; }
    public int TotalRegistros { get; set; }
    //Propiedades de Paginaci�n
    [BindProperty(SupportsGet = true)]
    public int? Pagina { get; set; }
    public int TotalPaginas { get; set; }
    // Propiedades de Ordenamiento por ID y Nombre
    [BindProperty(SupportsGet = true)]
    public string? Orden { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? Direccion { get; set; }

    public async Task OnGetAsync()
    {
        // B�squeda o Filtrado
        var consulta = _context.Productos.AsNoTracking();

        if (!string.IsNullOrEmpty(TerminoBusqueda))
        {
            consulta = consulta.Where(m => m.Nombre.Contains(TerminoBusqueda));
        }

        TotalRegistros = await consulta.CountAsync();

        // Ordenamiento din�mico por ID y Nombre
        string orden = Orden ?? "Id";
        string direccion = Direccion ?? "desc";
        consulta = (orden, direccion) switch
        {
            ("Nombre", "asc") => consulta.OrderBy(p => p.Nombre),
            ("Nombre", "desc") => consulta.OrderByDescending(p => p.Nombre),
            ("Id", "desc") => consulta.OrderByDescending(p => p.Id),
            _ => consulta.OrderBy(p => p.Id)
        };

        // Paginaci�n
        var numeroPagina = Pagina ?? 1;
        var registrosPorPagina = _configuration.GetValue("RegistrosPorPagina", 10);
        TotalPaginas = (int)Math.Ceiling((double)TotalRegistros / registrosPorPagina);

        Productos = await consulta
            .Skip((numeroPagina - 1) * registrosPorPagina)
            .Take(registrosPorPagina)
            .ToListAsync();
    }
}
