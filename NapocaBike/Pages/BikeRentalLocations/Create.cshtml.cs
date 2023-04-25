using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NapocaBike.Data;

namespace NapocaBike.Pages.BikeRentalLocations
{
    public class CreateModel : PageModel
    {
        private readonly NapocaBike.Data.NapocaBikeContext _context;

        public CreateModel(NapocaBike.Data.NapocaBikeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BikeRentalLocation BikeRentalLocation { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.BikeRentalLocation == null || BikeRentalLocation == null)
            {
                return Page();
            }

            _context.BikeRentalLocation.Add(BikeRentalLocation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
