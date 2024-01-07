using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAplication.Data;
using PetAplication.Models;

namespace PetAplication.Pages.Requests
{
    public class CreateModel : PageModel
    {
        private readonly PetAplication.Data.PetAplicationContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(PetAplication.Data.PetAplicationContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        public async Task<IActionResult> OnGetAsync()
        {
            var currentClientId = await GetCurrentClientIdAsync();

            var facilityList = _context.Facility
                .Include(b => b.Location)
                .Select(x => new { x.ID, facilityFullName = x.Room + " - " + x.Location.LocationName });
            ViewData["FacilityID"] = new SelectList(facilityList, "ID", "facilityFullName");
            ViewData["ClientID"] = new SelectList(_context.Client, "ID", "FullName");

            var petList = _context.Pet
                .Where(p => p.ClientID == currentClientId)
                .Select(p => new { p.ID, petName = p.Name });

            ViewData["PetID"] = new SelectList(petList, "ID", "petName");


            return Page();
        }
        private async Task<int> GetCurrentClientIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userEmail = user.Email;

                var client = await _context.Client
                    .Where(c => c.Email == userEmail)
                    .FirstOrDefaultAsync();

                if (client != null)
                {
                    return client.ID;
                }
            }

            // If the client is not found or user is not authenticated, return a default value or handle accordingly
            return 0; // You might want to handle this case based on your application logic
        }



        [BindProperty]
        public Request Request { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var currentClientId = await GetCurrentClientIdAsync();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var facility = await _context.Facility.FindAsync(Request.FacilityID);
            if (facility != null && Request.CheckIn < facility.AvailableDate)
            {
                ModelState.AddModelError("", "Check-in date must be after the facility's available date.");
                return Page();
            }

            if (Request.CheckOut <= Request.CheckIn)
            {
                ModelState.AddModelError("", "Check-out date must be after the check-in date.");
                return Page();
            }

            try
            {
               
                var rentedDays = (int)(Request.CheckOut - Request.CheckIn).TotalDays;
                Request.TotalPrice = rentedDays * facility.Price;

                Request.ClientID = currentClientId;

                _context.Request.Add(Request);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
               
                ModelState.AddModelError("", "An error occurred while processing the request. Please try again later.");
                return Page();
            }
        }
    }
}
