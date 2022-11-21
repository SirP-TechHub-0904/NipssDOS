using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.Main
{
    public class HomeImageModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly ILogger<EditModel> _logger;
        public HomeImageModel(NipssDOS.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv, ILogger<EditModel> logger)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _logger = logger;
        }

        [BindProperty]
        public Alumni Alumni { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Alumni = await _context.Alumnis.FirstOrDefaultAsync(m => m.Id == id);

            if (Alumni == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            int imgCount = 0;
            string WebsiteFirstImage = "";
            string WebsiteSecondImage = "";

            if (HttpContext.Request.Form.Files != null && HttpContext.Request.Form.Files.Count > 0)
            {

                var newFileName = string.Empty;
                var newFileNameThumbnail = string.Empty;
                var filePath = string.Empty;
                var filePathThumbnail = string.Empty;
                string pathdb = string.Empty;
                var files = HttpContext.Request.Form.Files;
                foreach (var file in files)
                {

                    if (file.Length > 0)
                    {
                        filePath = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        filePathThumbnail = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        imgCount++;
                        var now = DateTime.Now;
                        string nameproduct = "project";
                        var uniqueFileName = $"{now.Millisecond}{now.Minute}{now.Second}{now.Day}-".Trim() + nameproduct;

                        var fileExtension = Path.GetExtension(filePath);

                        newFileName = uniqueFileName + fileExtension;

                        // if you wish to save file path to db use this filepath variable + newFileName
                        var fileDbPathName = $"/DOCUMENTS/".Trim();

                        filePath = $"{_hostingEnv.WebRootPath}{fileDbPathName}".Trim();

                        if (!(Directory.Exists(filePath)))
                            Directory.CreateDirectory(filePath);


                        var fileName = "";
                        fileName = filePath + $"{newFileName}".Trim();
                        //

                        var fullPath = "";
                        if (file.Name == "WebsiteFirstImage")
                        {
                            fullPath = filePath + Alumni.WebsiteFirstImage;
                        }
                        else if (file.Name == "FrontView")
                        {
                            fullPath = filePath + Alumni.WebsiteSecondImage;
                        }
                      

                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }


                        using (FileStream fsa = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fsa);
                            fsa.Flush();
                        }





                        if (file.Name == "WebsiteFirstImage")
                        {
                            WebsiteFirstImage = $"{fileDbPathName}{newFileName}";
                        }
                        else if (file.Name == "WebsiteSecondImage")
                        {
                            WebsiteSecondImage = $"{fileDbPathName}{newFileName}";
                        }
                        

                        #region Save Image Propertie to Db

                        #endregion

                        if (imgCount >= 5)
                            break;
                    }
                }
            }

            if (!String.IsNullOrEmpty(WebsiteFirstImage))
            {
                Alumni.WebsiteFirstImage = WebsiteFirstImage;
            }
            if (!String.IsNullOrEmpty(WebsiteSecondImage))
            {
                Alumni.WebsiteSecondImage = WebsiteSecondImage;

            }

            var updatealumni = await _context.Alumnis.FirstOrDefaultAsync(x => x.Id == Alumni.Id);
            updatealumni.WebsiteFirstImage = Alumni.WebsiteFirstImage;
            updatealumni.WebsiteSecondImage = Alumni.WebsiteSecondImage;

            _context.Attach(updatealumni).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            TempData["aasuccess"] = "Updated successfully";

            return RedirectToPage("./HomeImage", new { id = Alumni.Id });
        }


    }
}
