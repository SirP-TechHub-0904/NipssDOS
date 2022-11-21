using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.DSPage
{
    public class DeleteModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public DeleteModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DirectingStaff DirectingStaff { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DirectingStaff = await _context.DirectingStaffs
                .Include(d => d.Alumni)
                .Include(d => d.Profile)
                .Include(d => d.StudyGroup).FirstOrDefaultAsync(m => m.Id == id);

            if (DirectingStaff == null)
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

            DirectingStaff = await _context.DirectingStaffs.FindAsync(id);

            if (DirectingStaff != null)
            {
                _context.DirectingStaffs.Remove(DirectingStaff);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Main/ManageDS", new { id = DirectingStaff.AlumniId });
        }
    }
}
