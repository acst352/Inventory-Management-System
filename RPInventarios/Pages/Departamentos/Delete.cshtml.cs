using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Departamentos;

public class DeleteModel : PageModel
{
    private readonly InventariosContext _context;
    private readonly INotyfService _servicioNotificacion;

    public DeleteModel(InventariosContext context, INotyfService servicioNotificacion)
    {
        _context = context;
        _servicioNotificacion = servicioNotificacion;
    }

    [BindProperty]
    public Departamento Departamento { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            _servicioNotificacion.Warning("El ID del departamento debe tener un valor");
            return NotFound();
        }

        var departamento = await _context.Departamentos.FirstOrDefaultAsync(m => m.Id == id);

        if (departamento is not null)
        {
            Departamento = departamento;

            return Page();
        }

        _servicioNotificacion.Warning($"No se encontró el departamento con ID {id}");
        return NotFound();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            _servicioNotificacion.Warning("El ID del departamento debe tener un valor");
            return NotFound();
        }

        var departamento = await _context.Departamentos.FindAsync(id);
        if (departamento != null)
        {
            Departamento = departamento;
            _context.Departamentos.Remove(Departamento);
            await _context.SaveChangesAsync();
            _servicioNotificacion.Success($"Departamento {Departamento.Nombre} eliminado correctamente");
        }
        else
        {
            _servicioNotificacion.Warning($"No se encontró el departamento con ID {id}");
            //return NotFound();
        }

        return RedirectToPage("./Index");
    }
}
