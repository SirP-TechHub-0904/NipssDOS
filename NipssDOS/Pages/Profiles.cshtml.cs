using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Pages
{
    [Authorize]
    public class ProfilesModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public ProfilesModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<SecParticipant> Profile { get;set; }

        public async Task OnGetAsync()
        {

            Profile = await _context.Participants
               .Include(s => s.Alumni)
               .Include(s => s.Profile)
               .ThenInclude(s => s.User)
               .Include(s => s.StudyGroup).Where(x => x.Alumni.Active == true && x.IsTrue == true).ToListAsync();

          
        }
    }
}
