using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NapocaBike.Data;

namespace NapocaBike.Pages.BikeRentalLocations
{
    public class DeleteModel : PageModel
    {
        private readonly NapocaBike.Data.NapocaBikeContext _context;

        public DeleteModel(NapocaBike.Data.NapocaBikeContext context)
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

            var bikerentallocation = await _context.BikeRentalLocation.FirstOrDefaultAsync(m => m.Id == id);

            if (bikerentallocation == null)
            {
                return NotFound();
            }
            else 
            {
                BikeRentalLocation = bikerentallocation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.BikeRentalLocation == null)
            {
                return NotFound();
            }
            var bikerentallocation = await _context.BikeRentalLocation.FindAsync(id);

            if (bikerentallocation != null)
            {
                BikeRentalLocation = bikerentallocation;
                _context.BikeRentalLocation.Remove(BikeRentalLocation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
