﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.ParticipantManager
{
    public class IndexModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public IndexModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<SecParticipant> SecParticipant { get; set; }
        public IList<ParticipantDocumentCategory> ParticipantDocumentCategory { get; set; }
        public Alumni Alumni { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SecParticipant = await _context.Participants
                .Include(s => s.ParticipantDocuments)
                .Include(s => s.Alumni)
                .Include(s => s.Profile)
                .ThenInclude(s => s.User)
                .Include(s => s.StudyGroup).Where(x => x.AlumniId == id).ToListAsync();

            ParticipantDocumentCategory = await _context.ParticipantDocumentCategories.ToListAsync();


            Alumni = await _context.Alumnis
        .FirstOrDefaultAsync(m => m.Id == id);

            if (Alumni == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
