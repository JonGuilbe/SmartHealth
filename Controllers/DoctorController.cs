using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHealth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SmartHealth.Data;
using SmartHealth.Models.AccountViewModels;
using System.IO;

namespace SmartHealth.Controllers 

{
    [Authorize]
    public class DoctorController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Home()
        {
            var user =  await _userManager.GetUserAsync(HttpContext.User);
            var userId = user.Id;
            //Console.WriteLine("HELLO YES THIS IS THE DATE!!!!!: " + DateTime.Now.ToString("MM/dd/yyyy"));
            var query = from appointment in _context.Appointments.AsEnumerable() where
                        appointment.DoctorID == userId && ( DateTime.Parse(appointment.Date) >= DateTime.Now) == true
                         select appointment;

            var messageList = (from message in _context.Messages
                           where message.DoctorID == user.Id && message.FromPatient select message);

            var data = new DoctorViewModel();
            data.Appointments = query;
            data.Messages = messageList;
            return View(data);
        }

        public async Task<IActionResult> Profile(string id)
        {
            ApplicationUser user;
            //Console.WriteLine("Hello yes the ID is: " + id);
            if (id == null) //If empty, get the current user
            {
                user = await _userManager.GetUserAsync(HttpContext.User);
                Console.WriteLine("This is the id not existing branch");
            }
            else //If not empty, get the doctor we're looking for
            {
            //Console.WriteLine("This is the id existing branch");
                user = await _userManager.FindByIdAsync(id);
                ViewData["id"] = user.Id;
            }

            var services = from service in _context.Services where
                           service.DoctorID == user.Id 
                           select service;

            var data = new DoctorViewModel();
            data.Doctor = (DoctorUser)user;
            data.Services = services;
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string returnUrl = null){
            var model = new DoctorRegisterViewModel();
            var user = (DoctorUser)await _userManager.GetUserAsync(HttpContext.User);
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Address = user.Address;
            model.starttime = user.starttime;
            model.endtime = user.endtime;
            model.PhoneNumber = user.PhoneNumber;
            model.UserPhoto = model.UserPhoto;
            ViewData["ReturnUrl"] = "/Doctor/Profile";
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DoctorRegisterViewModel model, string returnUrl = null){
            ViewData["ReturnUrl"] = "/Doctor/Profile";
            var currentUser =  await _userManager.GetUserAsync(HttpContext.User);
            var DBuser = _context.Doctors.SingleOrDefault(u => u.Id == currentUser.Id);
            if(DBuser != null) 
            {
                if(DBuser.FirstName != model.FirstName || DBuser.LastName != model.LastName){
                    var messages = (from message in _context.Messages
                           where message.DoctorID == currentUser.Id select message).ToList();
                
                    foreach(Message m in messages){
                        m.DoctorName = model.FirstName + " " + model.LastName;
                    }
                }        
                        
                DBuser.FirstName = model.FirstName;
                DBuser.LastName = model.LastName;
                DBuser.Address = model.Address;
                DBuser.starttime = model.starttime;
                DBuser.endtime = model.endtime;
                DBuser.PhoneNumber = model.PhoneNumber;
                using (var memoryStream = new MemoryStream())
                {
                  if(model.UserPhoto != null){
                        await model.UserPhoto.CopyToAsync(memoryStream);
                        DBuser.UserPhoto = memoryStream.ToArray();
                  }
                }
                
                _context.SaveChanges();
                return Redirect("/Doctor/Profile");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AddService(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = "/Doctor/Profile";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddService(ServiceAddViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = "/Doctor/Profile";
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if(ModelState.IsValid){
                var service = new Service { Name = model.Name, Cost = model.Cost, DoctorID = user.Id, Duration = model.Duration};
                _context.Services.Add(service);
                _context.SaveChanges();
                return Redirect(returnUrl);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult UpdateService(int id, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = "/Doctor/Profile";
            var service = _context.Services.SingleOrDefault(u => u.Id == id);
            var model = new ServiceAddViewModel();
            model.Name = service.Name;
            model.Cost = service.Cost;
            model.Duration = service.Duration;
            model.Id = id;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateService(int id, ServiceAddViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = "/Doctor/Profile";
            var oldService = _context.Services.SingleOrDefault(u => u.Id == id);
            if(ModelState.IsValid){
                oldService.Name = model.Name;
                oldService.Cost = model.Cost;
                oldService.Duration = model.Duration;
                await _context.SaveChangesAsync();
                return Redirect("/Doctor/Profile");
            }
            return View(model);
        }

        public async Task<IActionResult> RemoveService(int id)
        {
            var service = _context.Services.SingleOrDefault(u => u.Id == id);
            if(service != null){
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
            return Redirect("/Doctor/Profile");
        }
    }
}