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
    public class DetailsModel : PageModel
    {
        private readonly PetAplication.Data.PetAplicationContext _context;

        public DetailsModel(PetAplication.Data.PetAplicationContext context)
        {
            _context = context;
        }

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
    }
}
