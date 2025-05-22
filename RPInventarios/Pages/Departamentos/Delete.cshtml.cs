using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Departamentos
{
    public class DeleteModel : PageModel
    {
        private readonly RPInventarios.Data.InventariosContext _context;

        public DeleteModel(RPInventarios.Data.InventariosContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Departamento Departamento { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos.FirstOrDefaultAsync(m => m.Id == id);

            if (departamento is not null)
            {
                Departamento = departamento;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos.FindAsync(id);
            if (departamento != null)
            {
                Departamento = departamento;
                _context.Departamentos.Remove(Departamento);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
