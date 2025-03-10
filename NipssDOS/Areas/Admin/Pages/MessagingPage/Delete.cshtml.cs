﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Admin.Pages.MessagingPage
{
    public class DeleteModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public DeleteModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Message Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);

            if (Message == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Message = await _context.Messages.FindAsync(id);

            if (Message != null)
            {
                _context.Messages.Remove(Message);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        [BindProperty]
        public int Number { get; set; }
        public async Task<IActionResult> OnPostNewdelete()
        {
            

            var xMessage = await _context.Messages.OrderBy(x => x.Id).Take(Number).ToListAsync();
            foreach (var x in xMessage)
            {
               var yMessage = await _context.Messages.FindAsync(x.Id);
                if (yMessage != null)
                {
                    _context.Messages.Remove(yMessage);
                   
                }
            } await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostResettozero()
        {


            var xMessage = await _context.Messages.Where(x=>x.NotificationStatus == NotificationStatus.NotSent).OrderBy(x => x.Id).ToListAsync();
            foreach (var x in xMessage)
            {
                x.Retries = 0;
                _context.Attach(x).State = EntityState.Modified;

            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
