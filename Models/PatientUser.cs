using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SmartHealth.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class PatientUser : ApplicationUser
    {
        public string DateOfBirth { get; set; }
        public string ZipCode { get; set; }
        public string Ethnicity { get; set; }
        public string Sex { get; set; }
        public string Insurance { get; set; }
    }
}
