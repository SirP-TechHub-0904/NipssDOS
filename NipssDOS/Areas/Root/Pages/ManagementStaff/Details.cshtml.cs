using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.ManagementStaff
{
    public class DetailsModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public DetailsModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public ManagingStaff ManagingStaff { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ManagingStaff = await _context.ManagingStaffs
                .Include(m => m.Alumni)
                .Include(m => m.Profile).FirstOrDefaultAsync(m => m.Id == id);

            if (ManagingStaff == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
