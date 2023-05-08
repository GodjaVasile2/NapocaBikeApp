using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NapocaBike.Data;
using NapocaBike.Models;

namespace NapocaBike.Pages
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

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                CurrentMember = await _context.Member.FirstOrDefaultAsync(m => m.Email == user.Email);
            }
            await LoadUserDataAsync();
            return Page();
        }
    }
}
