using System.Collections.Generic;

namespace SmartHealth.Models
{
    public class PdfViewModel
    {
        public IEnumerable<Appointment> Appointments { get; set; }
        public string PatientName { get; set; }
    }
}