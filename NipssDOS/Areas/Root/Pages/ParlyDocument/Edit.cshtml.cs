﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.ParlyDocument
{
    public class EditModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public EditModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ParlyReportDocument ParlyReportDocument { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParlyReportDocument = await _context.ParlyReportDocuments
                .Include(p => p.ParlyReportCategory)
                .Include(p => p.Profile).FirstOrDefaultAsync(m => m.Id == id);

            if (ParlyReportDocument == null)
            {
                return NotFound();
            }
           ViewData["ParlyReportSubCategoryId"] = new SelectList(_context.ParlyReportSubCategories, "Id", "Title");
           ViewData["ParlyReportCategoryId"] = new SelectList(_context.ParlyReportCategories, "Id", "Title");
            ViewData["ParlySubTwoCategoryId"] = new SelectList(_context.ParlySubTwoCategories, "Id", "Title");
            ViewData["ParlySubThreeCategoryId"] = new SelectList(_context.ParlySubThreeCategories, "Id", "Title");
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title");
            ViewData["AlumniId"] = new SelectList(_context.Alumnis, "Id", "Title");
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    ViewData["ParlyReportSubCategoryId"] = new SelectList(_context.ParlyReportSubCategories, "Id", "Title");
            //    ViewData["ParlyReportCategoryId"] = new SelectList(_context.ParlyReportCategories, "Id", "Title");
            //    ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "FulName");
            //    ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title");
            //    ViewData["AlumniId"] = new SelectList(_context.Alumnis, "Id", "Title");

            //    return Page();
            //}

            _context.Attach(ParlyReportDocument).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParlyReportDocumentExists(ParlyReportDocument.Id))
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

        private bool ParlyReportDocumentExists(long id)
        {
            return _context.ParlyReportDocuments.Any(e => e.Id == id);
        }
    }
}
