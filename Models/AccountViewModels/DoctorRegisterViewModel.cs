using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHealth.Models.AccountViewModels
{
    public class DoctorRegisterViewModel : RegisterViewModel
    {
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Start of Working Hours")]
        public string starttime { get; set; }

        [Required]
        [Display(Name = "End of Working Hours")]
        public string endtime { get; set; }
    }
}
