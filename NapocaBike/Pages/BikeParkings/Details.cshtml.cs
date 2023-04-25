using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NapocaBike.Data;
using NapocaBike.Models;

namespace NapocaBike.Pages.BikeParkings
{
    public class DetailsModel : PageModel
    {
        private readonly NapocaBike.Data.NapocaBikeContext _context;

        public DetailsModel(NapocaBike.Data.NapocaBikeContext context)
        {
            _context = context;
        }

      public BikeParking BikeParking { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BikeParking == null)
            {
                return NotFound();
            }

            var bikeparking = await _context.BikeParking.FirstOrDefaultAsync(m => m.ID == id);
            if (bikeparking == null)
            {
                return NotFound();
            }
            else 
            {
                BikeParking = bikeparking;
            }
            return Page();
        }
    }
}
