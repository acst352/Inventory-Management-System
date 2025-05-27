using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Departamentos;

public class EditModel : PageModel
{
    private readonly InventariosContext _context;
    private readonly INotyfService _servicioNotificacion;

    public EditModel(InventariosContext context, INotyfService servicioNotificacion)
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
            _servicioNotificacion.Warning("El ID del departamento debe tener un valor no nulo");
            return NotFound();
        }

        var departamento = await _context.Departamentos.FirstOrDefaultAsync(m => m.Id == id);
        if (departamento == null)
        {
            _servicioNotificacion.Warning($"No se encontró el departamento con ID {id}");
            return NotFound();
        }
        Departamento = departamento;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            _servicioNotificacion.Error($"Error al editar el departamento {Departamento.Nombre}");
            return Page();
        }
        /* Validar si ya existe un departamento con el mismo nombre al intentar editar un departamento */
        var existeDepartamentoBd = _context.Marcas.Any(u => u.Nombre.ToLower().Trim() == Departamento.Nombre.ToLower().Trim() && u.Id != Departamento.Id);
        if (existeDepartamentoBd)
        {
            _servicioNotificacion.Warning($"Ya existe un departamento con el nombre {Departamento.Nombre}");
            return Page();
        }

        _context.Attach(Departamento).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DepartamentoExists(Departamento.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        _servicioNotificacion.Success($"Departamento {Departamento.Nombre} editado correctamente");
        return RedirectToPage("./Index");
    }

    private bool DepartamentoExists(int id)
    {
        return _context.Departamentos.Any(e => e.Id == id);
    }
}
