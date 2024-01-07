using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAplication.Data;
using PetAplication.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace PetAplication.Pages.Facilities
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel

    {
        private readonly PetAplication.Data.PetAplicationContext _context;

        public CreateModel(PetAplication.Data.PetAplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["LocationID"] = new SelectList(_context.Set<Models.Location>(), "ID", "LocationName");

            var facility = new Facility();
           

            return Page();
        }

        [BindProperty]
        public Facility Facility { get; set; }


        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newFacility = new Facility();
            if (selectedCategories != null)
            {

                if (!string.IsNullOrEmpty(Facility.Picture))
                {
                    string imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images");
                    string imagePath = Path.Combine(imagesFolder, Facility.Picture);
                    if (!System.IO.File.Exists(imagePath))
                    {
                        ModelState.AddModelError("Facility.Picture", "The specified image does not exist.");
                    }
                }

                
            }
            
            _context.Facility.Add(Facility);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
       

    }
}
