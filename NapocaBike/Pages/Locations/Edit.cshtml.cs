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
using System.Security.Policy;
using NapocaBike.Models;

namespace NapocaBike.Pages.Locations
{
    public class EditModel : LocationCategoriesPageModel
    {
        private readonly NapocaBike.Data.NapocaBikeContext _context;

        public EditModel(NapocaBike.Data.NapocaBikeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Location Location { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Location == null)
            {
                return NotFound();
            }
            Location = await _context.Location

                .Include(b => b.LocationCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Location == null)
            {
                return NotFound();
            }

            PopulateAssignedCategoryData(_context, Location);


            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }
            var locationToUpdate = await _context.Location
                .Include(i => i.LocationCategories)
                .ThenInclude(i => i.Category)
                .FirstOrDefaultAsync(s => s.ID == id);
            if (locationToUpdate == null)
            {
                return NotFound();
            }

            // Preia noile coordonate din formular și le salvează în model
            locationToUpdate.Latitude = Convert.ToDouble(Request.Form["Latitude"]);
            locationToUpdate.Longitude = Convert.ToDouble(Request.Form["Longitude"]);

            // Actualizează restul câmpurilor modelului
            if (await TryUpdateModelAsync<Location>(
                locationToUpdate,
                "Location",
                i => i.Name, i => i.Adress, i => i.Latitude, i => i.Longitude,
                i => i.Program))
            {
                UpdateLocationCategories(_context, selectedCategories, locationToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Actualizează categoriile și afișează pagina cu erorile
            UpdateLocationCategories(_context, selectedCategories, locationToUpdate);
            PopulateAssignedCategoryData(_context, locationToUpdate);
            return Page();
        }


        private bool LocationExists(int id)
        {
            return _context.Location.Any(e => e.ID == id);
        }
    }
}