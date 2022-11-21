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
    public class DirectingStaffModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public DirectingStaffModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Profile> DS { get;set; }

        public async Task OnGetAsync()
        {
            DS = await _context.Profiles.Include(x=>x.StudyGroupMemeber).ThenInclude(x=>x.StudyGroup).Where(x => x.OfficialRoleStatus == OfficialRoleStatus.DirectingStaff).ToListAsync();
        }
    }
}
