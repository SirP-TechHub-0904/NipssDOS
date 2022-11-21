using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Admin.Pages.SliderPage.Category
{
    public class IndexModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public IndexModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<SliderCategory> SliderCategory { get;set; }

        public async Task OnGetAsync()
        {
            SliderCategory = await _context.SliderCategories.Include(x=>x.Alumni).Include(x => x.Sliders).ToListAsync();
        }
    }
}
