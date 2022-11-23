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
    public class DashboardPageModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public DashboardPageModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public Alumni Alumni { get; set; }
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


         
        public async Task<IActionResult> OnGetAsync(long id)
        {
          

            Alumni = await _context.Alumnis
               
                .Where(m => m.Id == id).FirstOrDefaultAsync();

            if (Alumni == null)
            {
                return NotFound();
            }
            ParlyReportCategories = await _context.ParlyReportCategories.Include(x=>x.ParlyReportSubCategories).Include(x => x.Alumni).Include(x=>x.ParlyReportDocuments).Where(x => x.Show == true && x.FolderType == FolderType.Main && x.AlumniId == Alumni.Id).ToListAsync();
            ParlyFolder = await _context.ParlyReportCategories.Include(x => x.ParlyReportSubCategories).Include(x => x.Alumni).Include(x => x.ParlyReportDocuments).Where(x => x.Show == true && x.FolderType == FolderType.Parly && x.AlumniId == Alumni.Id).ToListAsync();
            TotalParticipant = await _context.Participants.Where(x => x.IsTrue == true && x.AlumniId == Alumni.Id).CountAsync();
            Male = await _context.Participants.Where(x => x.IsTrue == true && x.Profile.Gender.ToUpper() == "MALE" && x.AlumniId == Alumni.Id).CountAsync();
            Female = await _context.Participants.Where(x => x.IsTrue == true && x.Profile.Gender.ToUpper() == "FEMALE" && x.AlumniId == Alumni.Id).CountAsync();
            return Page();
        }

    }
}
