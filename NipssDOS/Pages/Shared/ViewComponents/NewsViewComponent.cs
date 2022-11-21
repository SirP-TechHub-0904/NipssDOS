
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
    public class NewsViewComponent : ViewComponent
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly NIPSSDbContext _context;


        public NewsViewComponent(
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
           var News = _context.News.Include(x => x.Comments).Include(x => x.Alumni).Where(x=>x.Alumni.Active == true).OrderBy(x => x.Date).Take(3).ToList();

            ViewBag.News = News.ToList();
            return View();
        }
    }
}
