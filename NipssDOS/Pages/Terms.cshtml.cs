using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Pages
{
    public class TermModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public TermModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }
       
        public IActionResult OnGet()
        {

            return Page();
        }
 }
}
