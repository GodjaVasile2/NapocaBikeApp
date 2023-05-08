using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NapocaBike.Data;
using NapocaBike.Models;

namespace NapocaBike.Pages.BikeParkings.Propose
{
    [Authorize]
    public class ProposeModel : PageModel
    {
        private readonly NapocaBike.Data.NapocaBikeContext _context;

        public ProposeModel(NapocaBike.Data.NapocaBikeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ProposedBikeParking ProposedBikeParking { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ProposedBikeParking.IsApproved = false;
            _context.ProposedBikeParkings.Add(ProposedBikeParking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
