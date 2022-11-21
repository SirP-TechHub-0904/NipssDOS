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

namespace NipssDOS.Areas.Admin.Pages.Rapid.Answers
{
    public class EditModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public EditModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserAnswer UserAnswer { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserAnswer = await _context.UserAnswers
                .Include(u => u.Profile).FirstOrDefaultAsync(m => m.Id == id);

            if (UserAnswer == null)
            {
                return NotFound();
            }
           ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UserAnswer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAnswerExists(UserAnswer.Id))
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

        private bool UserAnswerExists(long id)
        {
            return _context.UserAnswers.Any(e => e.Id == id);
        }
    }
}
