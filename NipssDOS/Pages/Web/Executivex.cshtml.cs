using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data.Model;

namespace NipssDOS.Pages.Web
{
        public class ExecutivexModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public ExecutivexModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }
        public Alumni Alumni { get; set; }
        public IList<Executive> Executive { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            Executive = await _context.Executive.Include(x => x.Profile).Where(x => x.AlumniId == id).OrderBy(x=>x.SortOrder).ToListAsync();
            
            Alumni = await _context.Alumnis
                .Include(x => x.SecProject)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Alumni == null)
            {
                return NotFound();
            }
            return Page();
        }

    }

}
