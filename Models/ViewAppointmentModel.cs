using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHealth.Models
{
    public class ViewAppointmentModel
    {
        public DoctorUser Doctor { get; set; }

        public PatientUser Patient { get; set; }

        public Appointment App { get; set; }

        public Boolean isHistory { get; set; }

        public Boolean isPatientUser { get; set; }

        public int id { get; set; }

    }
}