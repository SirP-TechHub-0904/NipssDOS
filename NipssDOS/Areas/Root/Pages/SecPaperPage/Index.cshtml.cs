using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.SecPaperPage
{
    public class IndexModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public IndexModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<SecPaper> SecPaper { get;set; }

        public async Task OnGetAsync()
        {
            SecPaper = await _context.SecPapers
                .Include(s => s.Alumni)
                .Include(s => s.DocumentCategory).ToListAsync();
        }
    }
}
