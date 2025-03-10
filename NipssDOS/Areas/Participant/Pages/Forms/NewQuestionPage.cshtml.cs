﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Participant.Pages.Forms
{
    public class NewQuestionPageModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public NewQuestionPageModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var questionner = await _context.QuestionnerPages.Where(x => x.QuestionnerId == id).ToListAsync();
            QuestionnerPage p = new QuestionnerPage();
            p.QuestionnerId = id;
            p.Description = "Description";
            p.Title = "Title";
            p.Instruction = "Instruction";
            p.Number = questionner.Count() + 1;

            _context.QuestionnerPages.Add(p);
            await _context.SaveChangesAsync();
            var qx = await _context.Questionners.Include(x => x.QuestionnerPages).FirstOrDefaultAsync(x => x.Id == id);
            qx.TotalPage = qx.QuestionnerPages.Count();
            return RedirectToPage("./FormMaker", new { o = qx.ShortLink, q = qx.LongLink });
        }

       
    }
}
