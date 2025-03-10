﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Dtos;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.ParticipantPage
{
    public class CreateModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public CreateModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            var prof = await _context.Profiles.ToListAsync();

            var output = prof.Select(x => new ProfileWithTitle
            {
                Id = x.Id,
                Fullname  = x.Title +" "+x.FullName
            }) ;

            ViewData["AlumniId"] = new SelectList(_context.Alumnis, "Id", "Title");
        ViewData["ProfileId"] = new SelectList(output, "Id", "Fullname");
            return Page();
        }

        [BindProperty]
        public SecParticipant Participant { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Participants.Add(Participant);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
