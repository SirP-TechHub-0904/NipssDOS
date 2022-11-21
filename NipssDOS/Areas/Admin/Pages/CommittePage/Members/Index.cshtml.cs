using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Admin.Pages.CommittePage.Members
{
    public class IndexModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public IndexModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Committee> Committee { get;set; }

        public async Task OnGetAsync()
        {
            Committee = await _context.Committees
                .Include(c => c.CommitteeCategory)
                .Include(c => c.Profile).ToListAsync();

            
        }
    }
}
