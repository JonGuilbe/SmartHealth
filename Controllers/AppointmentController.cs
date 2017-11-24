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
            //Console.WriteLine("THE ISSUE IS PROBABLY RIGHT HERE. THE SERVICE NAME IS " + model.Service);
            var service = _context.Services.FirstOrDefault(i => i.Id == model.Service);
            var StartTime = DateTime.ParseExact(model.Time, "h:mm tt", System.Globalization.CultureInfo.CurrentCulture);
            var AddDuration = new TimeSpan(0,0,service.Duration, 0);
            var EndTime = StartTime.Add(AddDuration);
            var endtime = EndTime.ToString("h:mm tt");

            var currentAppointments = (from appointment in _context.Appointments 
                                       where appointment.Date == model.Date select appointment);

            foreach (var app in currentAppointments)
            {
                var ExistingStart = DateTime.ParseExact(app.starttime, "h:mm tt", System.Globalization.CultureInfo.CurrentCulture);
                var ExistingEnd = DateTime.ParseExact(app.endtime, "h:mm tt", System.Globalization.CultureInfo.CurrentCulture);
                if(StartTime < ExistingEnd && ExistingStart < EndTime){
                    model.conflicts = currentAppointments;
                    List<Service> serviceList = new List<Service>();
                    serviceList = (from services in _context.Services
                                where services.DoctorID == id select services).ToList();

                    serviceList.Insert(0, new Service { Id = 0, Name = "Select"});

                    ViewBag.ServiceList = serviceList;                    
                    return View(model);
                }

            }

            if(ModelState.IsValid){
                var appointment = new Appointment { Date = model.Date, DoctorID = id, PatientID = user.Id, Cost = service.Cost, Service = service.Name, Notes = model.Notes, starttime = model.Time, endtime = endtime }; //Fix duration thing at some point
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
                return Redirect(returnUrl);
            }
            return View(model);
        }

        public async Task<IActionResult> ViewApp(int id)
        {
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
            data.isHistory = ( DateTime.Parse(appointment.Date) < DateTime.Now);
            data.isPatientUser = (user.AccountType == "Patient");
            data.id = id;
            return View(data);
        }

         public async Task<IActionResult> Cancel(int id)
        {
            var appointment = _context.Appointments.SingleOrDefault(u => u.Id == id);
            if(appointment != null){
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if(user.AccountType == "Doctor")
                return Redirect("/Message/Conversation/" + appointment.PatientID);
            else
                return Redirect("/Patient/Home");
        }

        public async Task<IActionResult> ChangePrivacy(int id)
        {
            var appointment = _context.Appointments.SingleOrDefault(u => u.Id == id);
            if(appointment != null){
                appointment.IsPrivate = !appointment.IsPrivate;
                await _context.SaveChangesAsync();
            }
            return Redirect("/Appointment/ViewApp/"+ id.ToString());
        }
    }
}