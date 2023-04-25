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

namespace NapocaBike.Pages.BikeParkings
{
    public class IndexModel : PageModel
    {
        private readonly NapocaBikeContext _context;

        public IndexModel(NapocaBikeContext context)
        {
            _context = context;
        }

        public IList<BikeParking> BikeParking { get; set; }
        [BindProperty(SupportsGet = true)]
        public int CapacityFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public int SecurityFilter { get; set; }

        public async Task OnGetAsync()
        {
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
