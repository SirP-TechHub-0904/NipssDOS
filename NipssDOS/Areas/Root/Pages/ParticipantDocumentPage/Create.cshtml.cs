﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.ParticipantDocumentPage
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
        ViewData["ParticipantDocumentCategoryId"] = new SelectList(_context.ParticipantDocumentCategories, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public ParticipantDocument ParticipantDocument { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ParticipantDocuments.Add(ParticipantDocument);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
