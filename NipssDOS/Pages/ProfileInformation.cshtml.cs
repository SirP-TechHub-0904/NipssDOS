using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Pages
{
    public class ProfileInformationModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public ProfileInformationModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public Profile Profile { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Profile = await _context.Profiles
                .Include(p => p.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Profile == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
