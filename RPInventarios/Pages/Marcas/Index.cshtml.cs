using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Marcas;

public class IndexModel : PageModel
{
    private readonly InventariosContext _context;
    // Readonly indica que la variable _context solo puede ser asignada en el constructor de la clase.
    // Después de eso no puede ser modificada en ningún otro lugar de la clase, 
    // lo que previene cambios accidentales o intencionales. Garantiza la inmutabilidad de la referencia.

    public IndexModel(InventariosContext context)
    {
        _context = context;
    }

    public IReadOnlyList<Marca> Marcas { get; set; } = default!;
    // IReadOnlyList es una interfaz que expone una lista de solo lectura. Fuera de la lase no se pueden
    // agregar, quitar ni modificar elementos de la colección.
    // La propiedad tiene un set público por lo que puede ser reasignada desde detro de la clase, pero 
    // la colección es de solo lectura para quien la consume (como la vista Razor).

    public async Task OnGetAsync()
    {
        Marcas = await _context.Marcas.ToListAsync();
    }
    // Este método se ejecuta cuando se hace una petición GET a la página. 
    // Recupera la lista de marcas desde la BD de forma asíncrona y le asigna a la propiedad Marcas.
    // Aunque la propiedad es de solo lectura para la vista, dentro del modelo de página sí puede ser asignada.
}

// La colección Marcas solo muestra la lista de marcas, sin permitir su modificación desde la interfaz de usuario.