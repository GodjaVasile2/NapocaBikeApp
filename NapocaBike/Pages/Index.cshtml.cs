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
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly NapocaBikeContext _context;

        public IndexModel(ILogger<IndexModel> logger, UserManager<IdentityUser> userManager, NapocaBikeContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public Member CurrentMember { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                CurrentMember = await _context.Member.FirstOrDefaultAsync(m => m.Email == user.Email);
            }

            return Page();
        }
    }
}
