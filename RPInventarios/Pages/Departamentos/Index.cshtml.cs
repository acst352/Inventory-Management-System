using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Departamentos;

public class IndexModel : PageModel
{
    private readonly InventariosContext _context;

    public IndexModel(InventariosContext context)
    {
        _context = context;
    }

    public IList<Departamento> Departamento { get; set; } = default!;
    // Propiedad de Búsqueda
    [BindProperty(SupportsGet = true)]
    public string TerminoBusqueda { get; set; }

    public int TotalRegistros { get; set; }
    public async Task OnGetAsync()
    {
        // Filtrar por nombre de Departamento
        var consulta = _context.Marcas.AsNoTracking();

        if (!string.IsNullOrEmpty(TerminoBusqueda))
        {
            consulta = consulta.Where(m => m.Nombre.Contains(TerminoBusqueda));
        }

        TotalRegistros = await consulta.CountAsync();

        Departamento = await _context.Departamentos.ToListAsync();
    }
}

