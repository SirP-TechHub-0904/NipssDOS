using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Participant.Pages.Dashboard
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class TestDetailsModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public TestDetailsModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public UserAnswer UserAnswer { get; set; }
        public IList<AnswerList> AnswerList { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserAnswer = await _context.UserAnswers
                .Include(u => u.Profile).FirstOrDefaultAsync(m => m.Id == id);
            AnswerList = await _context.AnswerLists.Include(x => x.RapidQuestion).Include(x=>x.RapidQuestion.RapidOptions).Where(x => x.UserAnswer.Id == id).ToListAsync();
            if (UserAnswer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
