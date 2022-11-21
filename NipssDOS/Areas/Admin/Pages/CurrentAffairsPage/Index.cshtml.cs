using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Admin.Pages.CurrentAffairsPage
{
    [Authorize(Roles = "mSuperAdmin,Admin,Content")]

    public class IndexModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public IndexModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<CurrentAffair> CurrentAffair { get;set; }

        public async Task OnGetAsync()
        {
            CurrentAffair = await _context.CurrentAffairs.ToListAsync();
        }
    }
}
