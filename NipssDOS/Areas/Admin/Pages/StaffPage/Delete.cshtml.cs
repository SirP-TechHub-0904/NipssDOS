using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Admin.Pages.StaffPage
{
    public class DeleteModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public DeleteModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public NipssStaff NipssStaff { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NipssStaff = await _context.NipssStaff
                .Include(n => n.Profile).FirstOrDefaultAsync(m => m.Id == id);

            if (NipssStaff == null)
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

            NipssStaff = await _context.NipssStaff.FindAsync(id);

            if (NipssStaff != null)
            {
                _context.NipssStaff.Remove(NipssStaff);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
