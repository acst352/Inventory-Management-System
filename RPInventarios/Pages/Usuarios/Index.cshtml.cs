using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;
using System.Reflection;

namespace RPInventarios.Pages.Usuarios;

public class IndexModel : PageModel
{
    private readonly InventariosContext _context;
    private readonly IConfiguration _configuration;

    public IndexModel(InventariosContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public IList<Usuario> Usuarios { get; set; } = default!;
    // Propiedades de Búsqueda
    [BindProperty(SupportsGet = true)]
    public string? TerminoBusqueda { get; set; }
    public int TotalRegistros { get; set; }
    //Propiedades de Paginación
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
        // Búsqueda o Filtrado
        var consulta = _context.Usuarios
            .Include(u => u.Perfil)
            .AsNoTracking();

        if (!string.IsNullOrEmpty(TerminoBusqueda))
        {
            // Obtén todas las propiedades string del modelo Usuario
            var stringProps = typeof(Usuario)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(string))
                .ToList();

            // Construye la expresión dinámica
            consulta = consulta.Where(u =>
                stringProps.Any(prop =>
                    EF.Functions.Like(EF.Property<string>(u, prop.Name), $"%{TerminoBusqueda}%")
                )
                // Incluye búsqueda en Id
                || u.Id.ToString().Contains(TerminoBusqueda)
                // Incluye búsqueda en Perfil.Nombre
                || u.Perfil.Nombre.Contains(TerminoBusqueda)
            );
        }

        TotalRegistros = await consulta.CountAsync();

        // Ordenamiento dinámico por ID y Nombre
        string orden = Orden ?? "Id";
        string direccion = Direccion ?? "desc";
        consulta = (orden, direccion) switch
        {
            ("Nombre", "asc") => consulta.OrderBy(p => p.Nombre),
            ("Nombre", "desc") => consulta.OrderByDescending(p => p.Nombre),
            ("Id", "desc") => consulta.OrderByDescending(p => p.Id),
            _ => consulta.OrderBy(p => p.Id)
        };

        // Paginación
        var numeroPagina = Pagina ?? 1;
        var registrosPorPagina = _configuration.GetValue("RegistrosPorPagina", 10);
        TotalPaginas = (int)Math.Ceiling((double)TotalRegistros / registrosPorPagina);

        Usuarios = await consulta
            .Skip((numeroPagina - 1) * registrosPorPagina)
            .Take(registrosPorPagina)
            .ToListAsync();
    }
}
