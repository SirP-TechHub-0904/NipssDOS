
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using NipssDOS.Data.Model;
using NipssDOS.Data;
using NipssDOS.Data.Dtos;

namespace NipssDOS.Pages.Shared.ViewComponents
{
    public class NipssStaffViewComponent : ViewComponent
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly NIPSSDbContext _context;


        public NipssStaffViewComponent(
            UserManager<IdentityUser> userManager,
            NIPSSDbContext context
           
            /*IEmailSender emailSender*/)
        {
            _userManager = userManager;
            _context = context;
        }

        public string UserInfo{ get; set; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var acc = await _context.ManagingStaffs.Include(x => x.Profile).Include(x => x.Alumni).Where(x => x.Alumni.Active == true).OrderBy(x => x.SortOrder).Take(3).ToListAsync();
            //var nipssstaff = _context.Profiles.Include(x => x.User).Where(x => x.OfficialRoleStatus == OfficialRoleStatus.ManagingStaff).OrderBy(x => x.SortOrder).Take(3).ToList();
            var output = acc.Select(x => new NipssStaffListDto
            {
                Id = x.Id,
                Position = x.Position,
                Fullname = x.Profile.Title + " " + x.Profile.FullName,
                Photo = x.Profile.AboutProfile,
            });

           ViewBag.NipssStaffList = output.ToList();
            return View();
        }
    }
}
