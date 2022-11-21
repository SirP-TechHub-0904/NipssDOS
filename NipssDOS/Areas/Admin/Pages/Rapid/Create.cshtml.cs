﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NipssDOS.Data;
using NipssDOS.Data.Model;

namespace NipssDOS.Areas.Admin.Pages.Rapid
{
    public class CreateModel : PageModel
    {
        private readonly NipssDOS.Data.NIPSSDbContext _context;

        public CreateModel(NipssDOS.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RapidQuestion RapidQuestion { get; set; }

        [BindProperty]
        public RapidOption RapidOption { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RapidQuestions.Add(RapidQuestion);
            await _context.SaveChangesAsync();
           
            RapidOption.RapidQuestionId = RapidQuestion.Id;
            _context.RapidOption.Add(RapidOption);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = RapidQuestion.Id });
        }
    }
}
