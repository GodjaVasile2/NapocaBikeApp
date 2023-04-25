using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NapocaBike.Data;
using NapocaBike.Models;
using NapocaBike.Models;

namespace NapocaBike.Pages.BikeParkings
{
    public class EditModel : PageModel
    {
        private readonly NapocaBike.Data.NapocaBikeContext _context;

        public EditModel(NapocaBike.Data.NapocaBikeContext context)
        {
            _context = context;
        }

        [BindProperty]
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
            BikeParking = bikeparking;
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

            // Setează coordonatele noi din formular
            BikeParking.Latitude = Convert.ToDouble(Request.Form["BikeParking.Latitude"]);
            BikeParking.Longitude = Convert.ToDouble(Request.Form["BikeParking.Longitude"]);

            _context.Attach(BikeParking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BikeParkingExists(BikeParking.ID))
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


        private bool BikeParkingExists(int id)
        {
            return _context.BikeParking.Any(e => e.ID == id);
        }
    }
}
