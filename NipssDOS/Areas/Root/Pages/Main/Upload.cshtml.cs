﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Root.Pages.Main
{
    [Authorize]

    public class UploadModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHostingEnvironment _hostingEnv;

        public UploadModel(NipssDOS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnv = hostingEnv;
        }
        [BindProperty]
        public ParlyReportCategory ParlyReportCategory { get; set; }
        [BindProperty]
        public ParlyReportSubCategory ParlyReportSubCategory { get; set; }
        [BindProperty]
        public ParlySubTwoCategory ParlySubTwoCategory { get; set; }
        [BindProperty]
        public ParlySubThreeCategory ParlySubThreeCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(long? f1, long? f2, long? f3, long? f4, string redir, string source)
        {

            if (f1 != null)
            {
                ParlyReportCategory = await _context.ParlyReportCategories.FirstOrDefaultAsync(x => x.Id == f1);

            }

            if (f2 != null)
            {
                ParlyReportSubCategory = await _context.ParlyReportSubCategories.Include(x=>x.ParlyReportCategory).FirstOrDefaultAsync(x => x.Id == f2);

            }
            if (f3 != null)
            {
                ParlySubTwoCategory = await _context.ParlySubTwoCategories.Include(x=>x.ParlyReportSubCategory).ThenInclude(x=>x.ParlyReportCategory).FirstOrDefaultAsync(x => x.Id == f3);
            }
            if (f4 != null)
            {
                ParlySubThreeCategory = await _context.ParlySubThreeCategories.Include(x=>x.ParlySubTwoCategory).ThenInclude(x => x.ParlyReportSubCategory).ThenInclude(x => x.ParlyReportCategory).FirstOrDefaultAsync(x => x.Id == f4);
            }


            //SUB FOLDER
            return Page();
        }
        [BindProperty]
        public string Redirt { get; set; }

        [BindProperty]
        public bool MainSource { get; set; }


        [BindProperty]
        public ParlyReportDocument ParlyReportDocument { get; set; }

        //[BindProperty]
        //public ParlyReportCategory ParlyReportCategories { get; set; }

        //[BindProperty]
        //public ParlyReportSubCategory ParlyReportSubCategories { get; set; }

        [BindProperty]
        public Profile Profile { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["usernull"] = "null";
            }
            else
            {
                Profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);

            }
            int imgCount = 0;
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

                        var uniqueFileName = $"{now.Millisecond}{now.Minute}{now.Second}{now.Day}-".Trim() + "Parly-Contribution";

                        var fileExtension = Path.GetExtension(filePath);

                        newFileName = uniqueFileName + fileExtension;

                        // if you wish to save file path to db use this filepath variable + newFileName
                        var fileDbPathName = $"/ParlyDocument2022/".Trim();

                        filePath = $"{_hostingEnv.WebRootPath}{fileDbPathName}".Trim();

                        if (!(Directory.Exists(filePath)))
                            Directory.CreateDirectory(filePath);

                        var fileName = "";
                        fileName = filePath + $"{newFileName}".Trim();

                        // copy the file to the desired location from the tempMemoryLocation of IFile and flush temp memory
                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }

                        ParlyReportDocument.Document = $"{fileDbPathName}{newFileName}";


                        #region Save Image Propertie to Db

                        #endregion

                        if (imgCount >= 5)
                            break;
                    }
                }
            }



            ParlyReportDocument.ProfileId = Profile.Id;
            ParlyReportDocument.Date = DateTime.UtcNow.AddHours(1);
            _context.ParlyReportDocuments.Add(ParlyReportDocument);
            await _context.SaveChangesAsync();

            TempData["result"] = "Uploaded Successfully";

            if (ParlyReportDocument.ParlyReportCategoryId != null)
            {
                return RedirectToPage("./InnerFolderTwo", new { id = ParlyReportDocument.ParlyReportCategoryId });
            }
            if (ParlyReportDocument.ParlyReportSubCategoryId != null)
            {
                return RedirectToPage("./InnerFolderThree", new { id = ParlyReportDocument.ParlyReportSubCategoryId });
            }
            if (ParlyReportDocument.ParlySubTwoCategoryId != null)
            {
                return RedirectToPage("./InnerFolderFour", new { id = ParlyReportDocument.ParlySubTwoCategoryId });
            }
            if (ParlyReportDocument.ParlySubThreeCategoryId != null)
            {
                return RedirectToPage("./InnerFolderFive", new { id = ParlyReportDocument.ParlySubThreeCategoryId });
            }

            return RedirectToPage("./Upload");
        }
    }
}
