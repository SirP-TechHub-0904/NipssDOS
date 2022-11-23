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
    public class LegacyProjectPageModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly ILogger<EditModel> _logger;
        public LegacyProjectPageModel(NipssDOS.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv, ILogger<EditModel> logger)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _logger = logger;
        }

        [BindProperty]
        public SecProject SecProject { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SecProject = await _context.secProjects.Include(x=>x.Alumni).FirstOrDefaultAsync(m => m.AlumniId == id);

            if (SecProject == null)
            {
                var alum = await _context.Alumnis.FirstOrDefaultAsync(x => x.Id == id);
                if (alum == null)
                {
                    return NotFound();
                }
                SecProject spp = new SecProject();
                spp.AlumniId = alum.Id;
                spp.Title = "Project Name";
                spp.SubTitle = "";
                _context.secProjects.Add(spp);
                await _context.SaveChangesAsync();
                TempData["aasuccess"] = "Updated successfully";
                SecProject = await _context.secProjects.Include(x => x.Alumni).FirstOrDefaultAsync(m => m.AlumniId == id);

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
            string SideView = "";
            string FrontView = "";
            string TopView = "";

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
                        if (file.Name == "SideView")
                        {
                            fullPath = filePath + SecProject.SideViewImage;
                        }
                        else if (file.Name == "FrontView")
                        {
                            fullPath = filePath + SecProject.FrontViewImage;
                        }
                        else if (file.Name == "TopView")
                        {
                            fullPath = filePath + SecProject.TopViewImage;
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





                        if (file.Name == "SideView")
                        {
                            SideView = $"{fileDbPathName}{newFileName}";
                        }
                        else if (file.Name == "FrontView")
                        {
                            FrontView = $"{fileDbPathName}{newFileName}";
                        }
                        else if (file.Name == "TopView")
                        {
                            TopView = $"{fileDbPathName}{newFileName}";
                        }

                        #region Save Image Propertie to Db

                        #endregion

                        if (imgCount >= 5)
                            break;
                    }
                }
            }

            if (!String.IsNullOrEmpty(SideView))
            {
                SecProject.SideViewImage = SideView;
            }
            if (!String.IsNullOrEmpty(FrontView))
            {
                SecProject.FrontViewImage = FrontView;

            }
            if (!String.IsNullOrEmpty(TopView))
            {
                SecProject.TopViewImage = TopView;

            }

            _context.Attach(SecProject).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            TempData["aasuccess"] = "Updated successfully";

            return RedirectToPage("./LegacyProjectPage", new { id = SecProject.AlumniId });
        }


    }
}
