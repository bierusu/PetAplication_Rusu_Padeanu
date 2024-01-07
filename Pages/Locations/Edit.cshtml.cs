using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAplication.Data;
using PetAplication.Models;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace PetAplication.Pages.Locations
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly PetAplication.Data.PetAplicationContext _context;

        public EditModel(PetAplication.Data.PetAplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Location Location { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Location = await _context.Location.FindAsync(id);

            if (Location == null)
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

            _context.Attach(Location).State = EntityState.Modified;

            try
            {
                if (!string.IsNullOrEmpty(Location.Picture))
                {
                    string imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images");
                    string imagePath = Path.Combine(imagesFolder, Location.Picture);
                    if (!System.IO.File.Exists(imagePath))
                    {
                        ModelState.AddModelError("Location.Picture", "The specified image does not exist.");
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(Location.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LocationExists(int id)
        {
            return _context.Location.Any(e => e.ID == id);
        }
    }
}
