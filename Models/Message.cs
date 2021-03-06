using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SmartHealth.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string DoctorID  { get; set; }
        public string PatientID  { get; set; }
        public string MessageString { get; set;}
        public Boolean FromPatient { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public string messagetime { get; set; }
    }
}