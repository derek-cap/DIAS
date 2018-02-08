using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DIAS.Data;
using DIAS.Infrasturcture;

namespace DIAS.RazorPages.Pages.Studies
{
    public class CreateModel : PageModel
    {
        private readonly DIAS.Infrasturcture.StudyContext _context;

        public CreateModel(DIAS.Infrasturcture.StudyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public StudyRecord StudyRecord { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Study.Add(StudyRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}