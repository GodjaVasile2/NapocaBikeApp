using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NapocaBike.Data;
using NapocaBike.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication;
using NapocaBike.Models;
using Microsoft.AspNetCore.Identity;

namespace NapocaBike.Pages.BikeParkings
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


        public IList<BikeParking> BikeParking { get; set; }
        [BindProperty(SupportsGet = true)]
        public int CapacityFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public int SecurityFilter { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                CurrentMember = await _context.Member.FirstOrDefaultAsync(m => m.Email == user.Email);
            }

            BikeParking = await _context.BikeParking.ToListAsync();

            
            if (CapacityFilter > 0 && SecurityFilter > 0)
            {
                BikeParking = BikeParking.Where(bp => bp.Capacity >= CapacityFilter && bp.SecurityLevel >= SecurityFilter).ToList();
            }
            else if (CapacityFilter > 0)
            {
                BikeParking = BikeParking.Where(bp => bp.Capacity >= CapacityFilter).ToList();
            }
            else if (SecurityFilter > 0)
            {
                BikeParking = BikeParking.Where(bp => bp.SecurityLevel >= SecurityFilter).ToList();
            }

            await LoadUserDataAsync();
        }





    }
}