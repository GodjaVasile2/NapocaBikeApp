using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NapocaBike.Data;
using NapocaBike.Models;
using NapocaBike.Models;

namespace NapocaBike.Pages.BikeParkings
{
    public class CreateModel : PageModel
    {
        private readonly NapocaBike.Data.NapocaBikeContext _context;

        public CreateModel(NapocaBike.Data.NapocaBikeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BikeParking BikeParking { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.BikeParking.Add(BikeParking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
