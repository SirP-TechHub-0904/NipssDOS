using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.ParlyCategory.SubThree
{
    public class FoldersModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public FoldersModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<ParlySubThreeCategory> ParlySubThreeCategory { get;set; }
        public ParlySubTwoCategory ParlySubTwoCategory { get; set; }
        public ParlyReportSubCategory ParlyReportSubCategory { get; set; }

        public async Task OnGetAsync(long id)
        {
            ParlySubThreeCategory = await _context.ParlySubThreeCategories
                 .Include(p => p.ParlySubTwoCategory).Where(x => x.ParlySubTwoCategoryId == id).OrderBy(x => x.SortOrder).ToListAsync();

            ParlySubTwoCategory = await _context.ParlySubTwoCategories.Include(x => x.ParlyReportSubCategory).FirstOrDefaultAsync(x => x.Id == id);
            //ParlyReportSubCategory = await _context.ParlyReportSubCategories.Include(x => x.ParlyReportCategory).FirstOrDefaultAsync(x => x.Id == ParlySubTwoCategory.Id);

        }
    }
}
