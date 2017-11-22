using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SmartHealth.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class DoctorUser : ApplicationUser
    {
        public string Address { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
    }
}
