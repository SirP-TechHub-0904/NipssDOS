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
    public class LectureNoteModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public LectureNoteModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Event> Event { get; set; }
        public IList<DocumentCategory> DocumentCategoryList { get; set; }

        public async Task OnGetAsync()
        {
            Event = await _context.Events.Include(x => x.Document).Where(x => x.IsLecture == true && x.Date <= DateTime.UtcNow).OrderByDescending(x => x.Date).ToListAsync();

            DocumentCategoryList = await _context.DocumentCategories.Include(x => x.Documents).Where(x => x.Show == true).ToListAsync();

        }
    }
}
