using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.NIPSS.Pages.Admin
{
    public class NIPSSExecutiveModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public NIPSSExecutiveModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Profile> Executive { get;set; }

        public async Task OnGetAsync()
        {
            Executive = await _context.Profiles.Include(x => x.User).Where(x => x.OfficialRoleStatus == OfficialRoleStatus.ManagingStaff).OrderBy(x=>x.SortOrder).ToListAsync();
        }
    }
}
