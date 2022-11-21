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
    public class NipssGalleryPageModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public NipssGalleryPageModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Gallery> Gallery { get;set; }

        public async Task OnGetAsync()
        {
            Gallery = await _context.Galleries.Where(x=>x.DontShow == false).OrderByDescending(x=>x.Date).ToListAsync();
        }
    }
}
