using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.ParlyCategory
{
    public class IndexModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public IndexModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<ParlyReportCategory> ParlyReportCategory { get;set; }

        public async Task OnGetAsync()
        {
            ParlyReportCategory = await _context.ParlyReportCategories.Include(x=>x.ParlyReportSubCategories).OrderBy(x=>x.SortOrder).ToListAsync();
        }
    }
}
