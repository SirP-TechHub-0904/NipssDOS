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
    public class DocListModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public DocListModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Document> Document { get; set; }
        public IList<DocumentCategory> DocumentCategoryList { get; set; }
        public DocumentCategory DocumentCategory { get; set; }

        public async Task OnGetAsync(long id)
        {
            Document = await _context.Documents.Include(x => x.DocumentCategory).Include(x => x.Event).Include(x=>x.Profile).Where(x=>x.DocumentCategoryId==id).Where(x=>x.DontShow == false).OrderByDescending(x=>x.Event.Date).ToListAsync();

            DocumentCategory = await _context.DocumentCategories.FirstOrDefaultAsync(x => x.Id == id);

            DocumentCategoryList = await _context.DocumentCategories.Include(x => x.Documents).Where(x => x.Show == true).ToListAsync();

        }
    }
}
