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

namespace NipssDOS.Areas.Root.Pages.ParticipantManager
{
    public class EditModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public EditModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SecParticipant SecParticipant { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SecParticipant = await _context.Participants
                .Include(s => s.Alumni)
                .Include(s => s.Profile)
                .Include(s => s.StudyGroup).FirstOrDefaultAsync(m => m.Id == id);

            if (SecParticipant == null)
            {
                return NotFound();
            }
           ViewData["AlumniId"] = new SelectList(_context.Alumnis, "Id", "Id");
           ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id");
           ViewData["StudyGroupId"] = new SelectList(_context.StudyGroups, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SecParticipant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SecParticipantExists(SecParticipant.Id))
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

        private bool SecParticipantExists(long id)
        {
            return _context.Participants.Any(e => e.Id == id);
        }
    }
}
