using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAplication.Data;
using PetAplication.Models;
using Microsoft.AspNetCore.Identity;

namespace PetAplication.Pages.Requests
{
    public class IndexModel : PageModel
    {
        private readonly PetAplicationContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(PetAplicationContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Request> Request { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userEmail = user.Email;

                Request = await _context.Request
                    .Include(r => r.Client)
                    .Include(r => r.Facility)
                    
                        .ThenInclude(f => f.Location)
                    .Include(r => r.Pet)
                    .Where(r => r.Client.Email == userEmail)
                    .ToListAsync();
            }
            else
            {
                Request = new List<Request>();
            }
        }
    }
}
