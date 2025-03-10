using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Participant.Pages.ProjectPage
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(NipssDOS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public LegacyProject LegacyProject { get; set; }
        [BindProperty]
        public string Email { get; set; }
        

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var check = await _context.LegacyProjectAnswers.Where(x=>x.VotingType == VotingType.Project).Select(x=>x.Email).ToListAsync();
            if (check.Contains(user.Email))
            {
                TempData["alert"] = "You have Voted Already";
                return RedirectToPage("./Result");
            }
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);
            Email = user.Email;
           
            LegacyProject = await _context.LegacyProjects.FirstOrDefaultAsync();

            return Page();
        }
        [BindProperty]
        public LegacyProjectAnswer LegacyProjectAnswer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            LegacyProjectAnswer.VotingType = VotingType.Project;
            LegacyProjectAnswer.Email = Email;
            _context.LegacyProjectAnswers.Add(LegacyProjectAnswer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Result");
        }
    }

}
