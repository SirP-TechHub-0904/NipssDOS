﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Admin.Pages.CommittePage
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
        public CommitteeCategory CommitteeCategory { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CommitteeCategories.Add(CommitteeCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
