using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Participant.Pages.Dashboard
{
      [Authorize]

    public partial class ChangeImageModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;


        public ChangeImageModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, Data.NIPSSDbContext context, IHostingEnvironment hostingEnv)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _hostingEnv = hostingEnv;
        }


        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public Profile Profile { get; set; }

        [BindProperty]
        public string GroupPosition { get; set; }

        public StudyGroupMemeber StudyGroup { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            Profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {


                var pro = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == Profile.Id);
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
                            string nameproduct = "ProfileImage";
                            var uniqueFileName = $"{now.Millisecond}{now.Minute}{now.Second}{now.Day}-".Trim() + nameproduct;

                            var fileExtension = Path.GetExtension(filePath);

                            newFileName = uniqueFileName + fileExtension;

                            // if you wish to save file path to db use this filepath variable + newFileName
                            var fileDbPathName = $"/ProfileImage/".Trim();

                            filePath = $"{_hostingEnv.WebRootPath}{fileDbPathName}".Trim();

                            if (!(Directory.Exists(filePath)))
                                Directory.CreateDirectory(filePath);

                            var fileName = "";
                            fileName = filePath + $"{newFileName}".Trim();

                            var oldfilePath = $"{_hostingEnv.WebRootPath}".Trim();
                            string fullPath = filePath + pro.AboutProfile;
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }

                            using (FileStream fsa = System.IO.File.Create(fileName))
                            {
                                file.CopyTo(fsa);
                                fsa.Flush();
                            }



                            pro.AboutProfile = $"{fileDbPathName}{newFileName}";


                            #region Save Image Propertie to Db

                            #endregion

                            if (imgCount >= 5)
                                break;
                        }
                    }
                }



                pro.ProfileUpdateLevel = ProfileUpdateLevel.THREE;
                pro.ProfileUpdatePictureFirstTime = true;

                _context.Attach(pro).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                TempData["alert"] = "Profile Updated Successfull";
                return RedirectToPage("./Profile");
            }
            catch (Exception c)
            {
                TempData["alert"] = "Unable to Updated Successfull";
            }
            return RedirectToPage("./ChangeImage");
        }

    }

}
