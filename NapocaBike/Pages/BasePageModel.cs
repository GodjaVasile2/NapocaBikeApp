using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NapocaBike.Data;
using NapocaBike.Models;
using System.Threading.Tasks;

public class BasePageModel : PageModel
{
    protected readonly UserManager<IdentityUser> _userManager;
    protected readonly NapocaBikeContext _context;
    protected readonly RoleManager<IdentityRole> _roleManager;

    public BasePageModel(UserManager<IdentityUser> userManager, NapocaBikeContext context, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _context = context;
        _roleManager = roleManager;
    }

    public Member CurrentMember { get; set; }
    public bool IsAdmin { get; set; }

    public async Task LoadUserDataAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            CurrentMember = await _context.Member.FirstOrDefaultAsync(m => m.Email == user.Email);
            var roles = await _userManager.GetRolesAsync(user);
            IsAdmin = roles.Contains("Admin");
        }
    }
}
