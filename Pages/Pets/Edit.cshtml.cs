﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAplication.Data;
using PetAplication.Models;

namespace PetAplication.Pages.Pets
{
    public class EditModel : PageModel
    {
        private readonly PetAplication.Data.PetAplicationContext _context;

        public EditModel(PetAplication.Data.PetAplicationContext context)
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

            var pet =  await _context.Pet.FirstOrDefaultAsync(m => m.ID == id);
            if (pet == null)
            {
                return NotFound();
            }
            Pet = pet;
           ViewData["ClientID"] = new SelectList(_context.Client, "ID", "ID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Pet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetExists(Pet.ID))
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

        private bool PetExists(int id)
        {
          return (_context.Pet?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
