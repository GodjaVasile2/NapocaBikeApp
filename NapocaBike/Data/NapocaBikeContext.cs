using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NapocaBike.Models;

namespace NapocaBike.Data
{
    public class NapocaBikeContext : DbContext
    {
        public NapocaBikeContext (DbContextOptions<NapocaBikeContext> options)
            : base(options)
        {
        }
       

        public DbSet<NapocaBike.Models.Member> Member { get; set; } = default!;
       
        public DbSet<NapocaBike.Models.Location>? Location { get; set; }
      

        public DbSet<NapocaBike.Models.Category>? Category { get; set; }
    

        //public DbSet<NapocaBike.Models.BikeRental>? BikeRental { get; set; }
       

        public DbSet<NapocaBike.Models.BikeParking>? BikeParking { get; set; }
       

        public DbSet<BikeRentalLocation>? BikeRentalLocation { get; set; }
       

      
    }
}
