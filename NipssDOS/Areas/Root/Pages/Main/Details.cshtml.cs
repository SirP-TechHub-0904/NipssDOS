using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.Main
{
    public class DetailsModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public DetailsModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public Alumni Alumni { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Alumni = await _context.Alumnis
                .Include(x => x.StudyGroup)
                .Include(x=>x.Participants)
                
                .ThenInclude(x=>x.Profile)

                .ThenInclude(x=>x.User)


                .Include(x => x.Executives)
                .ThenInclude(x => x.Profile)

                .Include(x => x.ManagingStaffs)
                .ThenInclude(x => x.Profile)


                 .Include(x => x.DirectingStaffs)
                .ThenInclude(x => x.Profile)
                .Include(x => x.StudyGroup)
                .Include(x => x.SecProject)
                .Include(x => x.SecPapers)
                .ThenInclude(x=>x.DocumentCategory)

                //.Include(x => x.CommitteeCategory)
                //.ThenInclude(x => x.Committees)

                .FirstOrDefaultAsync(m => m.Id == id);

            if (Alumni == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
