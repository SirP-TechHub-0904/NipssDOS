﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.NIPSS.Pages.MaintainancePage
{
    [Authorize(Roles = "Admin")]

    public class MaterialItemsModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;

        public MaterialItemsModel(NipssDOS.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _hostingEnv = hostingEnv;
        }

        public IList<TicketRequirement> TicketRequirements { get; set; }
        [BindProperty]
        public TicketRequirement TicketRequirement { get; set; }


        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TicketRequirements = await _context.TicketRequirements.Where(x => x.TicketId == id).ToListAsync();
            TicketId = id ?? 0;
            return Page();
        }

        [BindProperty]
        public long TicketId { get; set; }

        public async Task<IActionResult> OnPostMaterialx()
        {

            int imgCount = 0;
            var fullPath = string.Empty;
            if (HttpContext.Request.Form.Files != null && HttpContext.Request.Form.Files.Count > 0)
            {
                var newFileName = string.Empty;
                var newFileNameThumbnail = string.Empty;
                var filePath = string.Empty;
                var LargefilePath = string.Empty;
                var filePathThumbnail = string.Empty;
                string pathdb = string.Empty;

                var files = HttpContext.Request.Form.Files;
                foreach (var file in files)
                {

                    if (file.Length > 0)
                    {
                        filePath = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        LargefilePath = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        filePathThumbnail = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        imgCount++;
                        var now = DateTime.Now;
                        string nameproduct = "maintenace";
                        var uniqueFileName = $"{now.Millisecond}{now.Minute}{now.Second}{now.Day}-".Trim() + nameproduct;

                        var fileExtension = Path.GetExtension(filePath);

                        newFileName = uniqueFileName + fileExtension;

                        // if you wish to save file path to db use this filepath variable + newFileName
                        var fileDbPathName = $"/Maintenace/".Trim();

                        filePath = $"{_hostingEnv.WebRootPath}{fileDbPathName}".Trim();

                        var LagefileDbPathName = $"/GalleryLargeImage/".Trim();

                        LargefilePath = $"{_hostingEnv.WebRootPath}{LagefileDbPathName}".Trim();

                        if (!(Directory.Exists(filePath)))
                            Directory.CreateDirectory(filePath);

                        if (!(Directory.Exists(LargefilePath)))
                            Directory.CreateDirectory(LargefilePath);

                        var fileName = "";
                        var LargefileName = "";
                        fileName = filePath + $"{newFileName}".Trim();
                        LargefileName = LargefilePath + $"{newFileName}".Trim();


                        string newFileNamex = uniqueFileName + fileExtension;

                        using (FileStream fsa = System.IO.File.Create(LargefileName))
                        {
                            file.CopyTo(fsa);
                            fsa.Flush();
                            fsa.Close();
                            fsa.Dispose();
                        }

                        Image myImage = Image.FromFile(LargefileName, true);
                        myImage = ScaleByPercent(myImage, 60);
                        myImage.Save(filePath + newFileNamex, ImageFormat.Jpeg);

                        myImage.Dispose();

                        TicketRequirement.Image = $"{fileDbPathName}{newFileNamex}";


                        fullPath = fileName;



                        if (imgCount >= 5)
                            break;
                    }
                }
            }
            TicketRequirement.TicketId = TicketId;
            TicketRequirement.TicketItemStatus = TicketItemStatus.Active;
            TicketRequirement.Date = DateTime.UtcNow.AddHours(1);
            _context.TicketRequirements.Add(TicketRequirement);
            await _context.SaveChangesAsync();
            TicketStage tstage = new TicketStage();
            tstage.Title = "Material " + TicketRequirement.Title + " added";
            tstage.Date = DateTime.UtcNow.AddHours(1);
            tstage.TicketId = TicketId;
            _context.TicketStages.Add(tstage);
            await _context.SaveChangesAsync();
            TempData["status"] = "Added Successfully";
            return RedirectToPage("./MaterialItems", new { id = TicketId });
        }

        [BindProperty]
        public long? TicketRequirementId { get; set; }

        public async Task<IActionResult> OnPostRemoved()
        {
            var mx = await _context.TicketRequirements.Include(x=>x.Ticket).FirstOrDefaultAsync(x => x.Id == TicketRequirementId);
            mx.TicketItemStatus = TicketItemStatus.Remove;
            _context.Attach(mx).State = EntityState.Modified;

            TicketStage tstage = new TicketStage();
            tstage.Title = "Material " + mx.Title + " removed";
            tstage.Date = DateTime.UtcNow.AddHours(1);
            tstage.TicketId = mx.TicketId;
            _context.TicketStages.Add(tstage);
            await _context.SaveChangesAsync();

            TempData["status"] = "Added Successfully";
            return RedirectToPage("./MaterialItems", new { id = TicketId });
        }

        public async Task<IActionResult> OnPostActive()
        {
            var mx = await _context.TicketRequirements.Include(x => x.Ticket).FirstOrDefaultAsync(x => x.Id == TicketRequirementId);
            mx.TicketItemStatus = TicketItemStatus.Active;
            _context.Attach(mx).State = EntityState.Modified;

            TicketStage tstage = new TicketStage();
            tstage.Title = "Material " + mx.Title + " is active";
            tstage.Date = DateTime.UtcNow.AddHours(1);
            tstage.TicketId = mx.TicketId;
            _context.TicketStages.Add(tstage);
            await _context.SaveChangesAsync();

            TempData["status"] = "Added Successfully";
            return RedirectToPage("./MaterialItems", new { id = TicketId });
        }

        public async Task<IActionResult> OnPostChange()
        {
            var mx = await _context.TicketRequirements.Include(x => x.Ticket).FirstOrDefaultAsync(x => x.Id == TicketRequirementId);
            mx.TicketItemStatus = TicketItemStatus.Changed;
            _context.Attach(mx).State = EntityState.Modified;
            TicketStage tstage = new TicketStage();
            tstage.Title = "Material " + mx.Title + " changed";
            tstage.Date = DateTime.UtcNow.AddHours(1);
            tstage.TicketId = mx.TicketId;
            _context.TicketStages.Add(tstage);
            await _context.SaveChangesAsync();

            TempData["status"] = "Added Successfully";
            return RedirectToPage("./MaterialItems", new { id = TicketId });
        }

        static Image ScaleByPercent(Image imgPhoto, int Percent)
        {
            float nPercent = ((float)Percent / 100);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;

            int destX = 0;
            int destY = 0;
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight,
                                     PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                                    imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }
        /// <summary> 
        /// Saves an image as a jpeg image, with the given quality 
        /// </summary> 
        /// <param name="path"> Path to which the image would be saved. </param> 
        /// <param name="quality"> An integer from 0 to 100, with 100 being the highest quality. </param> 
        public static void SaveJpeg(string path, Image img, int quality)
        {
            if (quality < 0 || quality > 100)
                throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");

            // Encoder parameter for image quality 
            EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);
            // JPEG image codec 
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, jpegCodec, encoderParams);
        }

        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];

            return null;
        }

    }
}
