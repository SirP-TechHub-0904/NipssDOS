using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Participant.Pages.TicketPage
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

        public IList<Ticket> Ticket { get;set; }

        public async Task OnGetAsync()
        {

            var user = await _userManager.GetUserAsync(User);
            

            Ticket = await _context.Tickets
                .Include(t => t.ApprovedBy)
                .Include(t => t.ForwardedTo)
                .Include(t => t.JobCompletionCertifiedBy)
                .Include(t => t.ReceivedAndPassTo).Where(x=>x.UserId == user.Id).OrderByDescending(x=>x.CreatedTime).ToListAsync();
        }
    }
}
