using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NapocaBike.Data;
using NapocaBike.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using NapocaBike.Models;

namespace NapocaBike.Pages.Locations
{
    public class CreateModel : LocationCategoriesPageModel
    {
        private readonly NapocaBike.Data.NapocaBikeContext _context;

        public CreateModel(NapocaBike.Data.NapocaBikeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {

            var location = new Location();
            location.LocationCategories = new List<LocationCategory>();
            PopulateAssignedCategoryData(_context, location);
            return Page();
        }

        [BindProperty]
        public Location Location { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {

            var newLocation = Location;
            if (selectedCategories != null)
            {
                newLocation.LocationCategories = new List<LocationCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new LocationCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newLocation.LocationCategories.Add(catToAdd);
                }
            }

            _context.Location.Add(newLocation);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");

            PopulateAssignedCategoryData(_context, newLocation);
            return Page();


        }
    }
}
