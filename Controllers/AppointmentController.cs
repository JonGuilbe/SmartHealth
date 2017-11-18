using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHealth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHealth.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartHealth;
using Microsoft.AspNetCore.Authorization;

namespace SmartHealth.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppointmentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //TODO Fix this
        public IActionResult Create(string id)
        {
            return View();
        }
        [HttpPost]
        public async Task Add(AppointmentRegisterViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if(ModelState.IsValid){
                var appointment = new Appointment { Date = model.Date, DoctorID = "123", PatientID = user.Id, Cost = "123", Service = "Placeholder", Notes = model.Notes };
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
            }
        }
    }
}