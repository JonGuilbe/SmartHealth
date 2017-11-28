using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SmartHealth.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string DoctorID  { get; set; }
        public string PatientID  { get; set; }
        public decimal Cost { get; set; }
        public string Service { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
        public string Notes { get;  set; }
        public Boolean IsPrivate { get; set; }
    }
}