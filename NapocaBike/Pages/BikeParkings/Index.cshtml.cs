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
    public class IndexModel : PageModel
    {
        private readonly NapocaBikeContext _context;
        private readonly ILogger<BikeParkingsListModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ILogger<BikeParkingsListModel> logger, UserManager<IdentityUser> userManager, NapocaBikeContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
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



            IQueryable<BikeParking> bikeParkingsQuery = _context.BikeParking;

            if (CapacityFilter > 0 && SecurityFilter > 0)
            {
                bikeParkingsQuery = bikeParkingsQuery.Where(bp => bp.Capacity >= CapacityFilter && bp.SecurityLevel >= SecurityFilter);
            }
            else if (CapacityFilter > 0)
            {
                bikeParkingsQuery = bikeParkingsQuery.Where(bp => bp.Capacity >= CapacityFilter);
            }
            else if (SecurityFilter > 0)
            {
                bikeParkingsQuery = bikeParkingsQuery.Where(bp => bp.SecurityLevel >= SecurityFilter);
            }

            BikeParking = await bikeParkingsQuery.ToListAsync();
        }



    }
}
