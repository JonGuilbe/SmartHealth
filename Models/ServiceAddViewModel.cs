using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHealth.Models
{
    public class ServiceAddViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set ;}

        [Display(Name = "Cost")]
        public decimal Cost { get; set ;}
    }
}