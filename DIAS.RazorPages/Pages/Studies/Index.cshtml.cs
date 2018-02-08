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
    public class IndexModel : PageModel
    {
        private readonly DIAS.Infrasturcture.StudyContext _context;

        public IndexModel(DIAS.Infrasturcture.StudyContext context)
        {
            _context = context;
        }

        public IList<StudyRecord> StudyRecord { get;set; }

        public async Task OnGetAsync()
        {
            StudyRecord = await _context.Study.ToListAsync();
        }
    }
}
