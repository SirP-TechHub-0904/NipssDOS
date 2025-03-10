﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.ExecutivePage
{
    public class DeleteModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public DeleteModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Executive Executive { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Executive = await _context.Executive
                .Include(e => e.Alumni)
                .Include(e => e.Participant)
                .Include(e => e.Profile).FirstOrDefaultAsync(m => m.Id == id);

            if (Executive == null)
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

            Executive = await _context.Executive.FindAsync(id);

            if (Executive != null)
            {
                _context.Executive.Remove(Executive);
                await _context.SaveChangesAsync();
                TempData["aasuccess"] = "Updated successfully";

            }

            return RedirectToPage("/Main/ExecutiveManage", new { id = Executive.AlumniId });
        }
    }
}
