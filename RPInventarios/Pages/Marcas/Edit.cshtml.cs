using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Marcas;

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
    public Marca Marca { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            _servicioNotificacion.Warning("El ID de la marca debe tener un valor no nulo");
            return NotFound();
        }

        var marca = await _context.Marcas.FirstOrDefaultAsync(m => m.Id == id);
        if (marca == null)
        {
            _servicioNotificacion.Warning($"No se encontró la marca con ID {id}");
            return NotFound();
        }
        Marca = marca;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            _servicioNotificacion.Error($"Error al editar la marca {Marca.Nombre}");
            return Page();
        }

        /* Validar si ya existe una marca con el mismo nombre al intentar editar una marca */
        var existeMarcaBd = _context.Marcas.Any(u => u.Nombre.ToLower().Trim() == Marca.Nombre.ToLower().Trim() && u.Id != Marca.Id);
        if (existeMarcaBd)
        {
            _servicioNotificacion.Warning($"Ya existe una marca con el nombre {Marca.Nombre}");
            return Page();
        }

        _context.Attach(Marca).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        // DbUpdateConcurrencyException se utiliza para manejar conflictos de concurrencia.
        // Por ejemplo, si dos usuarios intentan modificar o eliminar el mismo registro al mismo tiempo:
        // Usuario 1 elimina el registro, Usuario 2 intenta guardar cambios sobre ese mismo registro.
        // Entity Framework detecta el conflicto y lanza esta excepción.
        // Si el registro ya no existe, se retorna NotFound(); si existe pero fue modificado, se relanza la excepción.

        {
            if (!MarcaExists(Marca.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
                // Después de capturar una excepción en un bloque catch, se vuelve a lanzar la misma excepción
                // para que sea manejada por otro bloque de manejo de excepciones más arriba en la pila de llamadas. 
                // Si la excepción no puede ser resuelta localmente, se relanza para que otro manejador de excepciones 
                // la procese o para que el sistema la registre como un error.
                // Permite no 'ocultar' errores graves y mantener el flujo de control adecuado. 
            }
        }

        _servicioNotificacion.Success($"Marca {Marca.Nombre} editada correctamente");
        return RedirectToPage("./Index");
    }

    private bool MarcaExists(int id)
    {
        return _context.Marcas.Any(e => e.Id == id);
    }
}

