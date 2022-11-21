using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Admin.Pages
{
    public class GroupParticipantsModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public GroupParticipantsModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<StudyGroupMemeber> StudyGroupMemeber { get;set; }
        public StudyGroup Group { get; set; }
        public async Task OnGetAsync(long id, string xgroup)
        {
            StudyGroupMemeber = await _context.StudyGroupMemebers
                .Include(s => s.StudyGroup)
                .Include(s => s.Profile).OrderBy(x=>x.SortNumber).Where(x=>x.StudyGroupId == id).ToListAsync();

            Group = await _context.StudyGroups.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
