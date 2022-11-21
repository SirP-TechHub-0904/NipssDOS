using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Participant.Pages.Forms
{
    public class ResponseModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public ResponseModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public Questionner Questionner { get; set; }

        public async Task<IActionResult> OnGetAsync(string x = null)
        {
           

            Questionner = await _context.Questionners
                .Include(q => q.Profile).FirstOrDefaultAsync(m => m.LongLink == x);

            if (Questionner == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
