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
    public class DetailsModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public DetailsModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

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
    }
}
