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

namespace NipssDOS.Areas.Admin.Pages.ProfileAccount
{
    public class IsParticipantModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public IsParticipantModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Profile Profile { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Profile = await _context.Profiles
                .Include(p => p.Alumni)
                .Include(p => p.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Profile == null)
            {
                return NotFound();
            }
            if (Profile.IsParticipant == true)
            {
                Profile.IsParticipant = false;
            }
            else
            {
                Profile.IsParticipant = true;
            }
            _context.Attach(Profile).State = EntityState.Modified;


            await _context.SaveChangesAsync();

            return Page();
        }


    }
}
