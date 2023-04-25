using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NapocaBike.Data;
using NapocaBike.Models;
using System.Net;
using NapocaBike.Models;

namespace NapocaBike.Pages.Locations
{
    public class IndexModel : PageModel
    {
        private readonly NapocaBike.Data.NapocaBikeContext _context;

        public IndexModel(NapocaBike.Data.NapocaBikeContext context)
        {
            _context = context;
        }

        public IList<Location> Location { get; set; }
        public LocationData LocationD { get; set; }
        public int LocationID { get; set; }
        public int CategoryID { get; set; }


        public async Task OnGetAsync(int? id, int? categoryID)
        {
            //if (_context.Location != null)
            //{
            LocationD = new LocationData();

            LocationD.Locations = await _context.Location


                    .Include(b => b.LocationCategories)
                    .ThenInclude(b => b.Category)
                    .AsNoTracking()
                    .OrderBy(b => b.Name)
                    .ToListAsync();


            

            if (id != null)
            {
                LocationID = id.Value;
                Location location = LocationD.Locations
                .Where(i => i.ID == id.Value).Single();
                LocationD.Categories = location.LocationCategories.Select(s => s.Category);
            }

            //}

        }
    }
}



