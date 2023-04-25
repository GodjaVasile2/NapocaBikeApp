using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NapocaBike.Data;
using NapocaBike.Models;

namespace NapocaBike.Pages.Locations
{
    public class DetailsModel : PageModel
    {
        private readonly NapocaBike.Data.NapocaBikeContext _context;

        public DetailsModel(NapocaBike.Data.NapocaBikeContext context)
        {
            _context = context;
        }

      public Location Location { get; set; } = default!; 

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
