using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHealth.Models.AccountViewModels
{
    public class PatientRegisterViewModel : RegisterViewModel
    {
        [Required]
        [Display(Name = "Date of Birth")]
        public string DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "Ethnicity")]
        public string Ethnicity { get; set; }

        [Required]
        [Display(Name = "Sex")]
        public string Sex { get; set; }

        [Required]
        [Display(Name = "Insurance")]
        public string Insurance { get; set; }
    }
}
