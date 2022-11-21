using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.ParticipantDocumentPage
{
    public class DetailsModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public DetailsModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public ParticipantDocument ParticipantDocument { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParticipantDocument = await _context.ParticipantDocuments
                .Include(p => p.ParticipantDocumentCategory).FirstOrDefaultAsync(m => m.Id == id);

            if (ParticipantDocument == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
