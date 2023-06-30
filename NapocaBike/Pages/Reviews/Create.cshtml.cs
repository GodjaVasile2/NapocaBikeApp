using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NapocaBike.Data;
using NapocaBike.Models;

namespace NapocaBike.Pages.Reviews
{
    public class CreateModel : PageModel
    {
        private readonly NapocaBike.Data.NapocaBikeContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public string LocationName { get; set; }


        public CreateModel(NapocaBike.Data.NapocaBikeContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            string locationName = System.Net.WebUtility.UrlDecode(Request.Query["locationName"]);

            // Fetch the location based on its name
            var location = await _context.Location.FirstOrDefaultAsync(l => l.Name == locationName);

            if (location == null)
            {
                // If there is no location with the specified name, return an error.
                return NotFound();
            }

            // Get current logged in user
            var user = await _userManager.GetUserAsync(User);

            // Fetch the member associated with the user
            var member = await _context.Member.FirstOrDefaultAsync(m => m.FirstName == user.UserName);

            // Check if a member is associated with the user
            if (member == null)
            {
                // Handle situation when no member is associated with the user
                return NotFound();
            }

            Review = new Review
            {
                // Set the location ID, member ID, and member name
                LocationID = location.ID,
                MemberID = member.ID,
                MemberName = member.FirstName
            };

            LocationName = location.Name;  // Add this line
            return Page();
        }


        [BindProperty]
        public Review Review { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Review == null)
            {
                return RedirectToPage("/Locations/Index");
            }
            
           
            var user = await _userManager.GetUserAsync(User);
            var member = await _context.Member.FirstOrDefaultAsync(m => m.FirstName == user.UserName);
            if (member != null)
            {
                
                Review.MemberID = member.ID;
                Review.MemberName = member.FirstName;
            }
            else
            {
                
                return NotFound();
            }

            string locationName = System.Net.WebUtility.UrlDecode(Request.Query["locationName"]);
            var location = await _context.Location.FirstOrDefaultAsync(l => l.Name == locationName);

            
            if (location != null)
            {
              
                Review.LocationID = location.ID;
            }
            else
            {
              
                return NotFound();
            }

            _context.Review.Add(Review);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }



    }
}
