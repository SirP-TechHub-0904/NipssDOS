using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Participant.Pages.Dashboard
{
    public class DocPreviewModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public DocPreviewModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public Document Document { get; set; }
        public IList<DocumentCategory> DocumentCategoryList { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Document = await _context.Documents
                .Include(d => d.DocumentCategory)
                .Include(d => d.Event)
                .Include(d => d.Profile).FirstOrDefaultAsync(m => m.Id == id);

            if (Document == null)
            {
                return NotFound();
            }
            DocumentCategoryList = await _context.DocumentCategories.Include(x => x.Documents).Where(x=>x.Show == true).ToListAsync();

            return Page();
        }
    }
}
