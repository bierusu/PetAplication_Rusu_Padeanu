﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAplication.Data;
using PetAplication.Models;

namespace PetAplication.Pages.Locations
{
    public class DetailsModel : PageModel
    {
        private readonly PetAplication.Data.PetAplicationContext _context;

        public DetailsModel(PetAplication.Data.PetAplicationContext context)
        {
            _context = context;
        }

      public Location Location { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Location == null)
            {
                return NotFound();
            }

            var location = await _context.Location.FirstOrDefaultAsync(m => m.ID == id);
            if (location == null)
            {
                return NotFound();
            }
            else 
            {
                Location = location;
            }
            return Page();
        }
    }
}
