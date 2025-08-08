using Booking.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure
{
    public class BookingDbContext: DbContext
    {

        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        {
          
        }
       
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<ApartmentPhoto> ApartmentPhotos { get; set; }
        public DbSet<Booking.Domain.Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Owner> Owners { get; set; }



    }
}
