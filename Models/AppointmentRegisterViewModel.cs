using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHealth.Models
{
    public class AppointmentRegisterViewModel
    {
        [Required]
        [Display(Name = "Date")]
        public string Date { get; set ;}

        [Required]
        [Display(Name = "Service")]
        public int Service { get; set ;}

        [Display(Name = "Time")]
        public string Time { get; set ;}

        [Display(Name = "Notes")]
        public string Notes { get; set ;}

    }
}