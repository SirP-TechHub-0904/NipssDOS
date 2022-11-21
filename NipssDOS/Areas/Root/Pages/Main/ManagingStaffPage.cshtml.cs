using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.Main
{
    public class ManagingStaffPageModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly ILogger<EditModel> _logger;
        public ManagingStaffPageModel(NipssDOS.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv, ILogger<EditModel> logger)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _logger = logger;
        }

        [BindProperty]
        public IList<ManagingStaff> ManagingStaff { get; set; }
        public Alumni Alumni { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ManagingStaff = await _context.ManagingStaffs.Include(x => x.Alumni).Include(x => x.Profile).Where(m => m.AlumniId == id).ToListAsync();
            Alumni = await _context.Alumnis.FirstOrDefaultAsync(m => m.Id == id);
            return Page();
        }



    }
}
