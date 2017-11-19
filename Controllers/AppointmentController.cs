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
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using SmartHealth.Services;

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
        [HttpGet]
        public IActionResult Create(string id, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = "/Patient/Home";
            List<Service> serviceList = new List<Service>();
            serviceList = (from service in _context.Services
                          where service.DoctorID == id select service).ToList();

            serviceList.Insert(0, new Service { Id = 0, Name = "Select"});

            ViewBag.ServiceList = serviceList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string id, AppointmentRegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = "/Patient/Home";
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if(ModelState.IsValid){
                var appointment = new Appointment { Date = model.Date, DoctorID = id, PatientID = user.Id, Cost = 123, Service = "Placeholder", Notes = model.Notes };
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
                return Redirect(returnUrl);
            }
            return View(model);
        }
        public async Task<IActionResult> ViewApp(int id){
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var appointment = _context.Appointments.FirstOrDefault(i => i.Id == id);
            var doctorID = appointment.DoctorID;
            var patientID = appointment.PatientID;
            var doctor = await _userManager.FindByIdAsync(doctorID);
            var patient = await _userManager.FindByIdAsync(patientID);

            var data = new ViewAppointmentModel();
            data.Doctor = (DoctorUser)doctor;
            data.Patient = (PatientUser)patient;
            data.App = appointment;
            return View(data);
        }
    }
}