using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NapocaBike.Data;
using NapocaBike.Models;

namespace NapocaBike.Pages.BikeParkings.ReviewProposals
{
    //[Authorize(Roles = "Admin")]
    public class ReviewProposalsModel : PageModel
    {
        private readonly NapocaBike.Data.NapocaBikeContext _context;

        public ReviewProposalsModel(NapocaBike.Data.NapocaBikeContext context)
        {
            _context = context;
        }

        public IList<ProposedBikeParking> ProposedBikeParkings { get; set; }
        public IList<Location> Location { get; set; }

        public async Task OnGetAsync()
        {
            ProposedBikeParkings = await _context.ProposedBikeParkings
                .Where(p => !p.IsApproved)
                .ToListAsync();

            Location = await _context.Location
                .Where(l => !l.IsApproved)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostApproveAsync(int id)
        {
            var proposedBikeParking = await _context.ProposedBikeParkings.FindAsync(id);

            if (proposedBikeParking != null)
            {
                proposedBikeParking.IsApproved = true;
                _context.BikeParking.Add(new BikeParking
                {
                    Name = proposedBikeParking.Name,
                    Latitude = proposedBikeParking.Latitude,
                    Longitude = proposedBikeParking.Longitude,
                    Capacity = proposedBikeParking.Capacity,
                    SecurityLevel = proposedBikeParking.SecurityLevel
                });
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRejectAsync(int id)
        {
            var proposedBikeParking = await _context.ProposedBikeParkings.FindAsync(id);

            if (proposedBikeParking != null)
            {
                _context.ProposedBikeParkings.Remove(proposedBikeParking);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }



        public async Task<IActionResult> OnPostApproveLocationAsync(int id)
        {
            var proposedLocation = await _context.Location.FindAsync(id);

            if (proposedLocation != null)
            {
                proposedLocation.IsApproved = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRejectLocationAsync(int id)
        {
            Location location = await _context.Location.FindAsync(id);

            if (location != null)
            {
                _context.Location.Remove(location);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

    }
}
