﻿using System;
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

        public IList<NipssStaff> NipssStaff { get;set; }

        public async Task OnGetAsync()
        {
            NipssStaff = await _context.NipssStaff
                .Include(n => n.Profile).ToListAsync();
        }
    }
}
