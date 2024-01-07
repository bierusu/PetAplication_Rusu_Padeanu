using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAplication.Data;
using PetAplication.Models;
using Microsoft.AspNetCore.Identity;


namespace PetAplication.Pages.Pets
{
    public class IndexModel : PageModel
    {
        private readonly PetAplication.Data.PetAplicationContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(PetAplication.Data.PetAplicationContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Pet> Pet { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userEmail = user.Email;

                Pet = await _context.Pet
                    .Include(p => p.Client)
                    .Where(p => p.Client.Email == userEmail)
                    .ToListAsync();
            }
            else
            {
                Pet = new List<Pet>();
            }
        }
    }

}
