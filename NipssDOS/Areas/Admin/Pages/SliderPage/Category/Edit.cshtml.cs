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

namespace NipssDOS.Areas.Admin.Pages.SliderPage.Category
{
    public class EditModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public EditModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SliderCategory SliderCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SliderCategory = await _context.SliderCategories.FirstOrDefaultAsync(m => m.Id == id);

            if (SliderCategory == null)
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
            if (SliderCategory.Active == true)
            {
                var cslide = await _context.SliderCategories.FirstOrDefaultAsync(x => x.Active == true);
                cslide.Active = false;
                _context.Attach(SliderCategory).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            _context.Attach(SliderCategory).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                TempData["aasuccess"] = "Updated successfully";

            }
            catch (DbUpdateConcurrencyException)
            {
                TempData["aaerror"] = "unable to update";

            }
            return RedirectToPage("./Index");
        }

        private bool SliderCategoryExists(long id)
        {
            return _context.SliderCategories.Any(e => e.Id == id);
        }
    }
}
