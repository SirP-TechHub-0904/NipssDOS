using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.NIPSS.Pages.Menu
{
    public class QuestionnerModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public QuestionnerModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Alumni> Alumni { get;set; }

        public async Task OnGetAsync()
        {
            Alumni = await _context.Alumnis.ToListAsync();
        }
    }
}
