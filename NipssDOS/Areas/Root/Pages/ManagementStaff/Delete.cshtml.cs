﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.ManagementStaff
{
    public class DeleteModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public DeleteModel(NipssDOS.Data.NIPSSDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ManagingStaff = await _context.ManagingStaffs.FindAsync(id);

            if (ManagingStaff != null)
            {
                _context.ManagingStaffs.Remove(ManagingStaff);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Main/ManagingStaffPage", new { id = ManagingStaff.AlumniId });
        }
    }
}
