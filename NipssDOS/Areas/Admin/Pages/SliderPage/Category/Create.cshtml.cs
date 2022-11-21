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
    public class CreateModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public CreateModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["AlumniId"] = new SelectList(_context.Alumnis, "Id", "Title");

            return Page();
        }

        [BindProperty]
        public SliderCategory SliderCategory { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (SliderCategory.Active == true)
            {
                var cslide = await _context.SliderCategories.AsNoTracking().FirstOrDefaultAsync(x => x.Active == true);
                if (cslide != null)
                {
                    cslide.Active = false;
                    _context.Attach(cslide).State = EntityState.Modified;
                }
                //await _context.SaveChangesAsync();
            }
            _context.SliderCategories.Add(SliderCategory);
            await _context.SaveChangesAsync();
            TempData["aasuccess"] = "Updated successfully";

            return RedirectToPage("./Index");
        }
    }
}
