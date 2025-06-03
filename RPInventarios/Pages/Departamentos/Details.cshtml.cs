using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Models;

namespace RPInventarios.Pages.Departamentos
{
    public class DetailsModel : PageModel
    {
        private readonly RPInventarios.Data.InventariosContext _context;

        public DetailsModel(RPInventarios.Data.InventariosContext context)
        {
            _context = context;
        }

        public Departamento Departamento { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (departamento is not null)
            {
                Departamento = departamento;

                return Page();
            }

            return NotFound();
        }
    }
}
