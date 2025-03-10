﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.ParticipantDocumentCategoryPage
{
    public class DeleteModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public DeleteModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ParticipantDocumentCategory ParticipantDocumentCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParticipantDocumentCategory = await _context.ParticipantDocumentCategories.FirstOrDefaultAsync(m => m.Id == id);

            if (ParticipantDocumentCategory == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParticipantDocumentCategory = await _context.ParticipantDocumentCategories.FindAsync(id);

            if (ParticipantDocumentCategory != null)
            {
                _context.ParticipantDocumentCategories.Remove(ParticipantDocumentCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
