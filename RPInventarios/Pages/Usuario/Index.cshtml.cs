using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Usuario;

public class IndexModel : PageModel
{
    private readonly RPInventarios.Data.InventariosContext _context;

    public IndexModel(RPInventarios.Data.InventariosContext context)
    {
        _context = context;
    }

    public IList<RPInventarios.Models.Usuario> Usuario { get;set; } = default!;

    public async Task OnGetAsync()
    {
        Usuario = await _context.Usuario.ToListAsync();
    }
}
