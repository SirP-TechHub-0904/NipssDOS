using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Pages.TourPage
{
    public class NipssTourModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public NipssTourModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<TourCategory> TourCategory { get;set; }
        public IList<StudyGroup> StudyGroup { get;set; }

        public async Task OnGetAsync()
        {
            TourCategory = await _context.TourCategories.Include(x=>x.TourSubCategories).ToListAsync();
            StudyGroup = await _context.StudyGroups.ToListAsync();


        }
    }
}
