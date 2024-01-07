using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAplication.Data;
using PetAplication.Models;

namespace PetAplication.Pages.Facilities
{
    public class DetailsModel : PageModel
    {
        private readonly PetAplication.Data.PetAplicationContext _context;

        public DetailsModel(PetAplication.Data.PetAplicationContext context)
        {
            _context = context;
        }

      public Facility Facility { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Facility == null)
            {
                return NotFound();
            }

            var facility = await _context.Facility
                .Include(b => b.Location)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (facility == null)
            {
                return NotFound();
            }
            else 
            {
                Facility = facility;
            }
            return Page();
        }
    }
}
