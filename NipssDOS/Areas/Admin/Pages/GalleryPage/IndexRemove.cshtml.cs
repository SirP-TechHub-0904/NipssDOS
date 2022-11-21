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

namespace NipssDOS.Areas.Admin.Pages.GalleryPage
{
    public class IndexRemoveModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;


        public IndexRemoveModel(NipssDOS.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _hostingEnv = hostingEnv;
        }

        public IActionResult OnGet()
        {
            var LagefileDbPathName = $"/GalleryLargeImage/".Trim();

            var LargefilePath = $"{_hostingEnv.WebRootPath}{LagefileDbPathName}".Trim();

            DirectoryInfo dir = new DirectoryInfo(LargefilePath);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();

            }

            return Page();
        }
    }
}
