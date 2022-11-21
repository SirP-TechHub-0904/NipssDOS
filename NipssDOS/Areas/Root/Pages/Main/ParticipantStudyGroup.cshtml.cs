using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.Main
{
       public class ParticipantStudyGroupModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public ParticipantStudyGroupModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<SecParticipant> SecParticipant { get; set; }
        public Alumni Alumni { get; set; }
        public StudyGroup StudyGroup { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SecParticipant = await _context.Participants
                .Include(s => s.Alumni)
                .Include(s => s.Profile)
                .ThenInclude(s => s.User)
                .Include(s => s.StudyGroup).Where(x => x.StudyGroupId == id).ToListAsync();

           
          
            StudyGroup = await _context.StudyGroups
      .FirstOrDefaultAsync(m => m.Id == id);
            Alumni = await _context.Alumnis
      .FirstOrDefaultAsync(m => m.Id == StudyGroup.AlumniId);

            if (Alumni == null)
            {
                return NotFound();
            }

            return Page();
        }
    }

}
