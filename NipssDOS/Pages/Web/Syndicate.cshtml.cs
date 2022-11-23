using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data.Model;

namespace NipssDOS.Pages.Web
{
    public class SyndicateModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public SyndicateModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }
        public Alumni Alumni { get; set; }
        public IList<StudyGroup> StudyGroup { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //StudyGroup = await _context.StudyGroups.Include(x => x.StudyGroupMemebers).Where(x=>x.AlumniId == id).OrderBy(x => x.SortNumber).ToListAsync();
            StudyGroup = await _context.StudyGroups.Include(x => x.Alumni).Include(x => x.SecParticipants).Where(m => m.AlumniId == id).OrderBy(x => x.SortNumber).ToListAsync();

            Alumni = await _context.Alumnis
                .Include(x => x.SecProject)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Alumni == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
