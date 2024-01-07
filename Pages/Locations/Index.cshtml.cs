using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAplication.Data;
using PetAplication.Models;
using PetAplication.Models.ViewModels;

namespace PetAplication.Pages.Locations
{
    public class IndexModel : PageModel
    {
        private readonly PetAplication.Data.PetAplicationContext _context;

        public IndexModel(PetAplication.Data.PetAplicationContext context)
        {
            _context = context;
        }

        public IList<Location> Location { get; set; } = default!;

        public LocationIndexData LocationData { get; set; }
        public int LocationID { get; set; }
        public int FacilityID { get; set; }

        public async Task OnGetAsync(int? id, int? bookID)
        {
            LocationData = new LocationIndexData();
            LocationData.Locations = await _context.Location
            .Include(i => i.Facilities)

            .OrderBy(i => i.LocationName)
            .ToListAsync();
            if (id != null)
            {
                LocationID = id.Value;
                Location location = LocationData.Locations
                .Where(i => i.ID == id.Value).Single();
                LocationData.Facilities = location.Facilities;
            }
            //if (_context.Location != null)
            //{
            //  Location = await _context.Location.ToListAsync();
            //}
        }
    }
}