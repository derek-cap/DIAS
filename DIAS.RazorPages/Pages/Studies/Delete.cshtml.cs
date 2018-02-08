using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DIAS.Data;
using DIAS.Infrasturcture;

namespace DIAS.RazorPages.Pages.Studies
{
    public class DeleteModel : PageModel
    {
        private readonly DIAS.Infrasturcture.StudyContext _context;

        public DeleteModel(DIAS.Infrasturcture.StudyContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StudyRecord = await _context.Study.FindAsync(id);

            if (StudyRecord != null)
            {
                _context.Study.Remove(StudyRecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
