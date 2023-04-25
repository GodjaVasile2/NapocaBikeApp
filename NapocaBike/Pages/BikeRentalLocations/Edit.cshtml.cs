using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NapocaBike.Data;

namespace NapocaBike.Pages.BikeRentalLocations
{
    public class EditModel : PageModel
    {
        private readonly NapocaBike.Data.NapocaBikeContext _context;

        public EditModel(NapocaBike.Data.NapocaBikeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BikeRentalLocation BikeRentalLocation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BikeRentalLocation == null)
            {
                return NotFound();
            }

            var bikerentallocation =  await _context.BikeRentalLocation.FirstOrDefaultAsync(m => m.Id == id);
            if (bikerentallocation == null)
            {
                return NotFound();
            }
            BikeRentalLocation = bikerentallocation;
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

            _context.Attach(BikeRentalLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BikeRentalLocationExists(BikeRentalLocation.Id))
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

        private bool BikeRentalLocationExists(int id)
        {
          return (_context.BikeRentalLocation?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
