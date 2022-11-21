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

        public IList<Profile> Profile { get;set; }

        public async Task OnGetAsync()
        {
            Profile = await _context.Profiles
                .Include(p => p.User).Where(x=>x.DontShow==false).Where(x=>x.User.Email != "jinmcever@gmail.com").ToListAsync();
        }
    }
}
