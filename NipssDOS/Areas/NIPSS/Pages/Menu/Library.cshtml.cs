﻿using System;
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
    public class LibraryModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public LibraryModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<DocumentCategory> DocumentCategory { get; set; }

        public async Task OnGetAsync()
        {
            DocumentCategory = await _context.DocumentCategories.Include(x => x.Documents).ToListAsync();


        }
    }
}
