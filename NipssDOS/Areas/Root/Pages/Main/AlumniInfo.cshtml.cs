using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.Main
{
    [Authorize]
    public class AlumniInfoModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public AlumniInfoModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public Alumni Alumni { get; set; }
        public IList<Event> EventOnly { get; set; }
        public IQueryable<Event> Event { get; set; }
        public IQueryable<string> Events { get; set; }
        public string Desc { get; set; }
        public string PreviousWeek { get; set; }
        public string PreviousWeekTitle { get; set; }
        public string NextWeek { get; set; }
        public string NextWeekTitle { get; set; }
        public string Title { get; set; }

        public int TotalParticipant { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
        public int ProjectAssigned { get; set; }
        public int ProjectAssigned_Submitted { get; set; }
        public int LecturesDocuments { get; set; }
        public int RESOURCEMATERIALS { get; set; }
        public int GroupResearch { get; set; }
        public int INDIVIDUALESSAYS_Submitted { get; set; }
        public int INDIVIDUALESSAYS { get; set; }
        public int OTHERRESOURCEMATERIALS { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
          

            Alumni = await _context.Alumnis
                
                .Where(m => m.Active == true).FirstOrDefaultAsync();

            if (Alumni == null)
            {
                return NotFound();
            }

           
            return Page();
        }

    }
}
