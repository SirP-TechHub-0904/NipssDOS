
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

namespace NipssDOS.Pages.Shared.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly NIPSSDbContext _context;


        public SliderViewComponent(
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

            var sliderc = await _context.SliderCategories.Include(x=>x.Alumni).FirstOrDefaultAsync(x => x.Active == true && x.Alumni.Active == true);
            //if(sliderc == null)
            //{
            //    sliderc = await _context.SliderCategories.FirstOrDefaultAsync();

            //}
            if (sliderc != null)
            {
                var xslider = await _context.Sliders.Where(x => x.SliderCategoryId == sliderc.Id && x.Show == true).ToListAsync();
                ViewBag.slide = xslider;
            }
            return View();
        }
    }
}
