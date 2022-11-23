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

namespace NipssDOS.Areas.Root.Pages.ParlyCategory
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
            return Page();
        }

        [BindProperty]
        public ParlyReportCategory ParlyReportCategory { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            var alum = await _context.Alumnis.FirstOrDefaultAsync(x => x.Active == true);
            if(alum == null)
            {
                TempData["aaerror"] = "unable to update";
                return Page();
            }
            ParlyReportCategory.AlumniId = alum.Id;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ParlyReportCategory.Show = true;
            _context.ParlyReportCategories.Add(ParlyReportCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
