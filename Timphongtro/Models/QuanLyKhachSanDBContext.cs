using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;

namespace QuanLyKhachSan.Models
{
    public class QuanLyKhachSanDBContext : DbContext
    {
        public QuanLyKhachSanDBContext() : base("DBConnectionString")
        {
            Database.SetInitializer<QuanLyKhachSanDBContext>(null);
        }

        public DbSet<User> users { get; set; }

        public DbSet<Role> roles { get; set; }

        public DbSet<Room> rooms { get; set; }

        public DbSet<Booking> bookings { get; set; }

        public DbSet<BookingService> BookingServices { get; set; }

        public DbSet<Type> types { get; set; }

        public DbSet<Service> services { get; set; }

        public DbSet<RoomComment> roomComments { get; set; }

        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + ": " + x.ErrorMessage));
                throw new DbEntityValidationException(errorMessages);
            }
        }

    }
}