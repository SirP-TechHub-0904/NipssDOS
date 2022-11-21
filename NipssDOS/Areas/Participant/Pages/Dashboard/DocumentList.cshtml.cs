using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Participant.Pages.Dashboard
{
    public class DocumentListModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public DocumentListModel(NipssDOS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Document> Document { get; set; }
        public IList<DocumentCategory> DocumentCategoryList { get; set; }
        public DocumentCategory DocumentCategory { get; set; }
        public long? CID { get; set; }
        public async Task OnGetAsync(long id)
        {
            Document = await _context.Documents.Include(x => x.DocumentCategory).Include(x => x.Event).Include(x => x.Profile).Where(x => x.DocumentCategoryId == id).Where(x => x.DontShow == false).OrderByDescending(x => x.Event.Date).ToListAsync();

            DocumentCategory = await _context.DocumentCategories.FirstOrDefaultAsync(x => x.Id == id);

            DocumentCategoryList = await _context.DocumentCategories.Include(x => x.Documents).Where(x => x.Show == true).ToListAsync();
            CID = id;
        }
    }
}
