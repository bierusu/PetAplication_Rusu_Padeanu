using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAplication.Data;
using PetAplication.Models;

namespace PetAplication.Pages.Pets
{
    public class DeleteModel : PageModel
    {
        private readonly PetAplication.Data.PetAplicationContext _context;

        public DeleteModel(PetAplication.Data.PetAplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Pet Pet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Pet == null)
            {
                return NotFound();
            }

            var pet = await _context.Pet.FirstOrDefaultAsync(m => m.ID == id);

            if (pet == null)
            {
                return NotFound();
            }
            else 
            {
                Pet = pet;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Pet == null)
            {
                return NotFound();
            }
            var pet = await _context.Pet.FindAsync(id);

            if (pet != null)
            {
                Pet = pet;
                _context.Pet.Remove(Pet);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
