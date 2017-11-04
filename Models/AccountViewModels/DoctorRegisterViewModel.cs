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
    }
}
