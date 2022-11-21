using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Dtos;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.DSPage
{
    public class EditModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public EditModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DirectingStaff DirectingStaff { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var al = await _context.Alumnis.FindAsync(id);
            if (al == null)
            {
                return NotFound();
            }

            DirectingStaff = await _context.DirectingStaffs
                .Include(d => d.Alumni)
                .Include(d => d.Profile)
                .Include(d => d.StudyGroup).FirstOrDefaultAsync(m => m.Id == id);

            if (DirectingStaff == null)
            {
                return NotFound();
            }
           ViewData["AlumniId"] = new SelectList(_context.Alumnis, "Id", "Title");
           


            var prof = await _context.Profiles.ToListAsync();
            var part = await _context.Participants.Where(x => x.AlumniId == al.Id).Select(x => x.ProfileId).ToListAsync();
            var output = prof.Where(x => !part.Contains(x.Id)).Select(x => new ProfileWithTitle
            {
                Id = x.Id,
                Fullname = x.Title + " " + x.FullName
            });

            ViewData["ProfileId"] = new SelectList(output, "Id", "Fullname");
            ViewData["StudyGroupId"] = new SelectList(_context.StudyGroups.Where(x => x.AlumniId == al.Id), "Id", "Title");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DirectingStaff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectingStaffExists(DirectingStaff.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Main/ManageDS", new { id = DirectingStaff.AlumniId });
        }

        private bool DirectingStaffExists(long id)
        {
            return _context.DirectingStaffs.Any(e => e.Id == id);
        }
    }
}
