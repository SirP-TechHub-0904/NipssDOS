using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.ExecutivePage
{
    public class EditModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public EditModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Executive Executive { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Executive = await _context.Executive
                .Include(e => e.Alumni)
                .Include(e => e.Participant)
                .Include(e => e.Profile).FirstOrDefaultAsync(m => m.Id == id);

            if (Executive == null)
            {
                return NotFound();
            }
           ViewData["AlumniId"] = new SelectList(_context.Alumnis, "Id", "Id");
           ViewData["ParticipantId"] = new SelectList(_context.Participants, "Id", "Id");
           ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Executive).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExecutiveExists(Executive.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ExecutiveExists(long id)
        {
            return _context.Executive.Any(e => e.Id == id);
        }
    }
}
