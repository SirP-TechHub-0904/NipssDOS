using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Pages
{
    public class SyndicateModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public SyndicateModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<StudyGroup> StudyGroup { get;set; }

        public async Task OnGetAsync()
        {
            StudyGroup = await _context.StudyGroups.Include(x=>x.StudyGroupMemebers).OrderBy(x=>x.SortNumber).ToListAsync();
        }
    }
}
