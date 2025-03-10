﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Participant.Pages.Dashboard
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class StartPreviewModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public StartPreviewModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public UserAnswer UserAnswer { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserAnswer = await _context.UserAnswers
                .Include(u => u.Profile).FirstOrDefaultAsync(m => m.Id == id);

            if (UserAnswer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
