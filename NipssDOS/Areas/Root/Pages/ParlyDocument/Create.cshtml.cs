﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.ParlyDocument
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
        ViewData["ParlyReportCategoryId"] = new SelectList(_context.ParlyReportCategories, "Id", "Id");
        ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public ParlyReportDocument ParlyReportDocument { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ParlyReportDocuments.Add(ParlyReportDocument);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
