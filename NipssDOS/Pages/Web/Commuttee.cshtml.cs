using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Pages.Web
{
    public class CommutteeModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public CommutteeModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<CommitteeCategory> CommitteeCategory { get;set; }

        public async Task OnGetAsync()
        {
            CommitteeCategory = await _context.CommitteeCategories
                .Include(c => c.Alumni).ToListAsync();
        }
    }
}
