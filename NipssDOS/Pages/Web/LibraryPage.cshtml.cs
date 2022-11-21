using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data.Model;

namespace NipssDOS.Pages.Web
{
    public class LibraryPageModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public LibraryPageModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<ParlyReportCategory> ParlyReportCategory { get; set; }

        public async Task OnGetAsync()
        {
            ParlyReportCategory = await _context.ParlyReportCategories.Where(x => x.Show == true && x.FolderType == FolderType.Main).ToListAsync();


        }
    }
}
