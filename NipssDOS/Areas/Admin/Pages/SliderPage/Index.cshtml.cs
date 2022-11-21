using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Admin.Pages.SliderPage
{
    public class IndexModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;

        public IndexModel(NipssDOS.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _hostingEnv = hostingEnv;
        }

        public IList<Slider> Slider { get;set; }

        public async Task OnGetAsync()
        {
            Slider = await _context.Sliders.Include(x=>x.SliderCategory).ThenInclude(x=>x.Alumni).ToListAsync();
        }
        [BindProperty]
        public long SliderId { get; set; }
        public async Task<IActionResult> OnPostDelete()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == SliderId);
            var fileDbPathName = $"/SliderImages/".Trim();

            string filePath = $"{_hostingEnv.WebRootPath}".Trim();

            if (!(Directory.Exists(filePath)))
                Directory.CreateDirectory(filePath);



            var fullPath = filePath + slider.ImagePath;

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }




            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();

            return RedirectToPage("/SliderPage/Category/Details", new { id = slider.SliderCategoryId });
        }
        public async Task<IActionResult> OnPostShow()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == SliderId);
            if(slider.Show == false)
            {
                slider.Show = true;
            }
            else
            {
                slider.Show = false;
            }
            _context.Attach(slider).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return RedirectToPage("/SliderPage/Category/Details", new { id = slider.SliderCategoryId });
        }

    }
}
