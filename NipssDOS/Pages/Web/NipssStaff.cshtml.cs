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
    public class NipssStaffModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public NipssStaffModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<ManagingStaff> ManagingStaff { get;set; }

        public async Task OnGetAsync()
        {
           
            ManagingStaff = await _context.ManagingStaffs.Include(x => x.Alumni).Include(x => x.Profile).Where(m => m.Alumni.Active == true).OrderBy(x=>x.SortOrder).ToListAsync();

        }
    }
}
