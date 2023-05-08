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
using Microsoft.AspNetCore.Identity;

namespace NapocaBike.Pages.Locations
{
    public class IndexModel : BasePageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, UserManager<IdentityUser> userManager, NapocaBikeContext context, RoleManager<IdentityRole> roleManager)
            : base(userManager, context, roleManager)
        {
            _logger = logger;
        }
        public Member CurrentMember { get; set; }
        public IList<Location> Location { get; set; }
        public LocationData LocationD { get; set; }
        public int LocationID { get; set; }
        public int CategoryID { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool ShowAllLocations { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CategoryFilter { get; set; }


        public async Task OnGetAsync(int? id, int? categoryID)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                CurrentMember = await _context.Member.FirstOrDefaultAsync(m => m.Email == user.Email);
            }
            LocationD = new LocationData();

            LocationD.Categories = await _context.Category.ToListAsync();

            IQueryable<Location> locationsQuery = _context.Location
                .Include(b => b.LocationCategories)
                .ThenInclude(b => b.Category);

            locationsQuery = locationsQuery.Where(l => l.IsApproved);


            if (CategoryFilter > 0)
            {
                locationsQuery = locationsQuery.Where(l => l.LocationCategories.Any(c => c.Category.ID == CategoryFilter));
            }

            LocationD.Locations = await locationsQuery.OrderBy(b => b.Name).ToListAsync();

            if (id != null)
            {
                LocationID = id.Value;
                Location location = LocationD.Locations
                .Where(i => i.ID == id.Value).Single();
                LocationD.Categories = location.LocationCategories.Select(s => s.Category);
            }
            await LoadUserDataAsync();
        }
    }
}
