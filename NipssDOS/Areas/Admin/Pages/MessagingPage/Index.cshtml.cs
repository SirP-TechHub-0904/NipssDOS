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
    public class IndexModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public IndexModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Message> Message { get;set; }
        public int xMessage { get; set; }
        public int notxMessage { get; set; }
        public int notxMessage5 { get; set; }

        public async Task OnGetAsync(int number =0,int number2 = 0)
        {
            Message = await _context.Messages.OrderByDescending(x=>x.Date).Take(50).ToListAsync();
            

            if(number > 0)
            {
                Message = await _context.Messages.Skip(number).Take(number2).OrderByDescending(x => x.Date).ToListAsync();
            }
            xMessage = await _context.Messages.CountAsync();
            notxMessage = await _context.Messages.Where(x=>x.NotificationStatus == NotificationStatus.NotSent).CountAsync();
            notxMessage5 = await _context.Messages.Where(x=>x.NotificationStatus == NotificationStatus.NotSent && x.Retries > 5).CountAsync();
        }
    }
}
