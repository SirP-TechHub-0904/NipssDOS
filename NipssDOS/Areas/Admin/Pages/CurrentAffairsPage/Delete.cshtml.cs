using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Admin.Pages.CurrentAffairsPage
{
    [Authorize(Roles = "mSuperAdmin,Admin,Content")]

    public class DeleteModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public DeleteModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CurrentAffair CurrentAffair { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CurrentAffair = await _context.CurrentAffairs.FirstOrDefaultAsync(m => m.Id == id);

            if (CurrentAffair == null)
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

            CurrentAffair = await _context.CurrentAffairs.FindAsync(id);

            if (CurrentAffair != null)
            {
                _context.CurrentAffairs.Remove(CurrentAffair);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
