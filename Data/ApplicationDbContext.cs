using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHealth.Models;

namespace SmartHealth.Data
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string DoctorID  { get; set; }
        public string PatientID  { get; set; }
        public string Cost { get; set; }
        public string Service { get; set; }
        public TimeSpan[] Time { get; set; }
        public string Notes { get;  set; }
        public Boolean IsPrivate { get; set; }
    }
    
    public class Message
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string DoctorID  { get; set; }
        public string PatientID  { get; set; }
        public string Message { get; set;}
        public Boolean FromPatient { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
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

        public DbSet<PatientUser> Patients { get; set; }
        public DbSet<DoctorUser> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set;}
        public DbSet<Appointment> Messages { get; set;}
    }
}
