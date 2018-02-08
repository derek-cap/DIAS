using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DIAS.Data;
using DIAS.Infrasturcture;

namespace DIAS.RazorPages.Pages.Studies
{
    public class EditModel : PageModel
    {
        private readonly DIAS.Infrasturcture.StudyContext _context;

        public EditModel(DIAS.Infrasturcture.StudyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StudyRecord StudyRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StudyRecord = await _context.Study.SingleOrDefaultAsync(m => m.InstanceId == id);

            if (StudyRecord == null)
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

            _context.Attach(StudyRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return RedirectToPage("./Index");
        }
    }
}
