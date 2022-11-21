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

namespace NipssDOS.Areas.Root.Pages.ParlyCategory.SubCategory
{
    public class CreateModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public CreateModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }
        public long Id { get; set; }
        public ParlyReportCategory ParlyReportCategory { get; set; }

        public async Task<IActionResult> OnGet(long id)
        {
            ParlyReportCategory = await _context.ParlyReportCategories.FirstOrDefaultAsync(x => x.Id == id);
            if(ParlyReportCategory == null)
            {
                return RedirectToPage("/ParlyCategory/Index");
            }

            return Page();
        }

        [BindProperty]
        public ParlyReportSubCategory ParlyReportSubCategory { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ParlyReportSubCategories.Add(ParlyReportSubCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Folders", new { id = ParlyReportSubCategory.ParlyReportCategoryId });
        }
    }
}
