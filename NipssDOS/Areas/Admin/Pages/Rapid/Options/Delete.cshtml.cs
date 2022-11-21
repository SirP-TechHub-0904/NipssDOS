using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Admin.Pages.Rapid.Options
{
    public class DeleteModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public DeleteModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RapidOption RapidOption { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RapidOption = await _context.RapidOption
                .Include(r => r.RapidQuestion).FirstOrDefaultAsync(m => m.Id == id);

            if (RapidOption == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RapidOption = await _context.RapidOption.FindAsync(id);

            if (RapidOption != null)
            {
                _context.RapidOption.Remove(RapidOption);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
