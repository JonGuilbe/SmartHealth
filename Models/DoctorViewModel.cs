using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHealth.Models
{
    public class DoctorViewModel
    {
        public DoctorUser Doctor { get; set; }
        public IQueryable<Service> Services { get; set; }
        public IQueryable<Message> Messages { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
    }
}