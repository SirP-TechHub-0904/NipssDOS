using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.Main
{
    public class EditThemeModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public EditThemeModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Alumni Alumni { get; set; }
        [BindProperty]
        public SubGeneralTopic SubGeneralTopic { get; set; }
        public IList<SubGeneralTopic> SubGeneralTopics { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Alumni = await _context.Alumnis.Include(x=>x.SubGeneralTopics).FirstOrDefaultAsync(m => m.Id == id);

            if (Alumni == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateTheme()
        {

           var xAlumni = await _context.Alumnis.Include(x => x.SubGeneralTopics).FirstOrDefaultAsync(m => m.Id == Alumni.Id);
            xAlumni.GeneralTopic = Alumni.GeneralTopic;
            xAlumni.GeneralTopicContinuation = Alumni.GeneralTopicContinuation;
            _context.Attach(xAlumni).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                TempData["aasuccess"] = "Updated successfully";

            }
            catch (DbUpdateConcurrencyException)
            {
                TempData["aaerror"] = "unable to update";

            }

            return RedirectToPage("./EditTheme", new { id = xAlumni.Id });
        }

        private bool AlumniExists(long id)
        {
            return _context.Alumnis.Any(e => e.Id == id);
        }


        public async Task<IActionResult> OnPostSubTheme()
        {

            _context.SubGeneralTopics.Add(SubGeneralTopic);
            await _context.SaveChangesAsync();
            TempData["aasuccess"] = "Updated successfully";

            return RedirectToPage("./EditTheme", new { id = SubGeneralTopic.AlumniId });

        }
        [BindProperty]
        public long SubthemeId { get; set; }
        [BindProperty]
        public long AID { get; set; }
        public async Task<IActionResult> OnPostDelete()
        {
           
          var them = await _context.SubGeneralTopics.FindAsync(SubthemeId);

            if (them != null)
            {
                _context.SubGeneralTopics.Remove(them);
                await _context.SaveChangesAsync();
                TempData["aaerror"] = "Deleted successful";

            }

            return RedirectToPage("./EditTheme", new { id = AID });
        }

    }
}
