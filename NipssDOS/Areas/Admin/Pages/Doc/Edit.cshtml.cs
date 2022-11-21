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

namespace NipssDOS.Areas.Admin.Pages.Doc
{
    public class EditModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public EditModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DocumentCategory DocumentCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DocumentCategory = await _context.DocumentCategories
                .Include(d => d.Alumni).FirstOrDefaultAsync(m => m.Id == id);

            if (DocumentCategory == null)
            {
                return NotFound();
            }
           ViewData["AlumniId"] = new SelectList(_context.Alumnis, "Id", "Title");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DocumentCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentCategoryExists(DocumentCategory.Id))
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

        private bool DocumentCategoryExists(long id)
        {
            return _context.DocumentCategories.Any(e => e.Id == id);
        }
    }
}
