using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NapocaBike.Data;
using NapocaBike.Models;

namespace NapocaBike.Pages
{
    public class UpdateProfileModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly NapocaBikeContext _context;

        public UpdateProfileModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, NapocaBikeContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [BindProperty]
        public Member Member { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Index");
            }

            Member = await _context.Member.FirstOrDefaultAsync(m => m.Email == user.Email);

            if (Member == null)
            {
                // Handle the case when the Member is not found in the database
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Index");
            }

            // Update the Member information in the database
            _context.Attach(Member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle exceptions while updating the database
            }

            return RedirectToPage("/Index");
        }
    }
}
