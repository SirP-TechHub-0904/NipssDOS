using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data.Model;
using ServiceStack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NipssDOS.Areas.Root.Pages.Main
{
    [Authorize]
    public class InnerFolderFiveModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public InnerFolderFiveModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }
        public IList<ParlyReportDocument> ParlyReportDocuments { get; set; }
        public ParlySubThreeCategory ParlySubThreeCategory { get; set; }


        public async Task OnGetAsync(long id)
        {
           
            ParlySubThreeCategory = await _context.ParlySubThreeCategories.Include(x=>x.ParlySubTwoCategory).ThenInclude(x => x.ParlyReportSubCategory).ThenInclude(x => x.ParlyReportCategory).FirstOrDefaultAsync(x => x.Id == id);

            ParlyReportDocuments = await _context.ParlyReportDocuments.Where(x=>x.ParlyReportCategoryId==null && x.ParlyReportSubCategoryId == null && x.ParlySubTwoCategoryId == null && x.ParlySubThreeCategoryId == id).OrderBy(x => x.SortOrder).ToListAsync();
        }
    }
}
