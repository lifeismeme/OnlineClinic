using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineClinic.Areas.Identity.Data;
using OnlineClinic.Models;

namespace OnlineClinic.Models
{
    public class OnlineClinicContext : IdentityDbContext<User>
    {
        public OnlineClinicContext()
            : base()
        {
        }
        public OnlineClinicContext(DbContextOptions<OnlineClinicContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<OnlineClinic.Models.Appointment> Appointment { get; set; }
        public DbSet<OnlineClinic.Models.Staff> Staff { get; set; }
        public DbSet<OnlineClinic.Models.Patient> Patient { get; set; }
        public DbSet<OnlineClinic.Models.Slot> Slot { get; set; }
    }
}
