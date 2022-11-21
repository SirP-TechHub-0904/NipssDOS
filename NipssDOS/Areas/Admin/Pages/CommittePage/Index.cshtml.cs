using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Admin.Pages.CommittePage
{
    public class IndexModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public IndexModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<CommitteeCategory> CommitteeCategory { get;set; }

        public async Task OnGetAsync()
        {
            CommitteeCategory = await _context.CommitteeCategories
                .Include(c => c.Alumni)
                .Include(c => c.Committees)
                .ToListAsync();
        }
    }
}
