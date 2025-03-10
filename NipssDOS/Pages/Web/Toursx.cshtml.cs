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
    public class ToursxModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public ToursxModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }
        public Alumni Alumni { get; set; }
        public IList<TourCategory> TourCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TourCategory = await _context.TourCategories.Include(x => x.TourSubCategories).Where(x => x.AlumniId == id).ToListAsync();

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
