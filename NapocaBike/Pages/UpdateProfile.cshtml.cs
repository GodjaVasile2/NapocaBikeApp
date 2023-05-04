using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NapocaBike.Data;
using NapocaBike.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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
        
        [BindProperty]
        [Display(Name = "Profile Picture")]
        public IFormFile ProfilePicture { get; set; }

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

            var existingMember = await _context.Member.FirstOrDefaultAsync(m => m.Email == user.Email);

            if (existingMember == null)
            {
                // Handle the case when the Member is not found in the database
            }

            // Update the fields
            existingMember.FirstName = Member.FirstName;
            existingMember.LastName = Member.LastName;
            existingMember.Adress = Member.Adress;
            existingMember.Phone = Member.Phone;

            // Save the new profile picture if one is provided
            if (ProfilePicture != null && ProfilePicture.Length > 0)
            {
                // Generate a unique filename
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePicture.FileName);

                // Save the profile picture file to wwwroot/images folder
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfilePicture.CopyToAsync(fileStream);
                }

                // Update existingMember.ProfilePicturePath with the new file path
                existingMember.ProfilePicturePath = "/images/" + fileName;
            }

            // Save the changes
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
