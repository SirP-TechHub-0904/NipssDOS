using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.ParlyDocument
{
    public class IndexModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public IndexModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<ParlyReportDocument> ParlyReportDocument { get;set; }

        public async Task OnGetAsync()
        {
            ParlyReportDocument = await _context.ParlyReportDocuments
                .Include(p => p.ParlyReportCategory)
                .Include(p => p.ParlyReportSubCategory)
                .Include(p => p.ParlySubTwoCategory)
                .Include(p => p.ParlySubThreeCategory)
                .Include(p => p.Profile).ToListAsync();
        }
    }
}
