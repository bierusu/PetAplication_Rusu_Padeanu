﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAplication.Data;
using PetAplication.Models;

namespace PetAplication.Pages.Facilities
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly PetAplication.Data.PetAplicationContext _context;

        public DeleteModel(PetAplication.Data.PetAplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Facility Facility { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Facility == null)
            {
                return NotFound();
            }

            var facility = await _context.Facility.FirstOrDefaultAsync(m => m.ID == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Facility == null)
            {
                return NotFound();
            }
            var facility = await _context.Facility.FindAsync(id);

            if (facility != null)
            {
                Facility = facility;
                _context.Facility.Remove(Facility);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
