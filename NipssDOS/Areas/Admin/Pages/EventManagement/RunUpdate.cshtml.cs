using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Admin.Pages.EventManagement
{   
    //[Authorize(Roles = "mSuperAdmin,Admin,Content")]

    public class RunUpdateModel : PageModel
    {

        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public RunUpdateModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Event> Event { get;set; }

        public async Task OnGetAsync()
        {
            Event = await _context.Events.AsNoTracking().OrderByDescending(x=>x.Date).ToListAsync();
            //foreach(var x in Event)
            //{
            //    x.AlumniId = 1;
            //    _context.Attach(x).State = EntityState.Modified;

                    
            //    }
            //await _context.SaveChangesAsync();
        }
    }
}
