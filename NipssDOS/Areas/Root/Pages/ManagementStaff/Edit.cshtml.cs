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

namespace NipssDOS.Areas.Root.Pages.ManagementStaff
{
    public class EditModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public EditModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ManagingStaff ManagingStaff { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ManagingStaff = await _context.ManagingStaffs
                .Include(m => m.Alumni)
                .Include(m => m.Profile).FirstOrDefaultAsync(m => m.Id == id);

            if (ManagingStaff == null)
            {
                return NotFound();
            }
           ViewData["AlumniId"] = new SelectList(_context.Alumnis, "Id", "Id");
           ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ManagingStaff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManagingStaffExists(ManagingStaff.Id))
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

        private bool ManagingStaffExists(long id)
        {
            return _context.ManagingStaffs.Any(e => e.Id == id);
        }
    }
}
