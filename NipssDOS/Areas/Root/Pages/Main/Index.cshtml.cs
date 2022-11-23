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
    public class IndexModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public IndexModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public Alumni Alumni { get; set; }
        public Alumni AlumniNext { get; set; }
        public IList<Event> EventOnly { get; set; }
        public IList<ParlyReportCategory> ParlyReportCategories { get; set; }
        public IList<ParlyReportCategory> ParlyFolder { get; set; }
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


            ParlyReportCategories = await _context.ParlyReportCategories.Include(x=>x.ParlyReportSubCategories).Include(x=>x.ParlyReportDocuments).Where(x => x.Show == true && x.FolderType == FolderType.Main && x.Alumni.Active == true).ToListAsync();
            ParlyFolder = await _context.ParlyReportCategories.Include(x => x.ParlyReportSubCategories).Include(x => x.ParlyReportDocuments).Where(x => x.Show == true && x.FolderType == FolderType.Parly && x.Alumni.Active == true).ToListAsync();
            TotalParticipant = await _context.Participants.Where(x => x.IsTrue == true).CountAsync();
            Male = await _context.Participants.Where(x => x.IsTrue == true && x.Profile.Gender.ToUpper() == "MALE").CountAsync();
            Female = await _context.Participants.Where(x => x.IsTrue == true && x.Profile.Gender.ToUpper() == "FEMALE").CountAsync();


            AlumniNext = await _context.Alumnis

                .Where(m => m.Loading == true).FirstOrDefaultAsync();
            return Page();
        }

    }
}
