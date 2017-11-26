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
    public class PatientController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Home()
        {
            var user =  await _userManager.GetUserAsync(HttpContext.User);
            var userId = user.Id;
            var query = from appointment in _context.Appointments.AsEnumerable() where
                        appointment.PatientID == userId && ( DateTime.Parse(appointment.Date) >= DateTime.Now) == true
                         select appointment;

            var messageList = (from message in _context.Messages
                           where message.PatientID == user.Id  && !message.FromPatient select message);             

            var data = new PatientViewModel();
            data.Appointments = query;
            data.Messages = messageList;

            return View(data);
        }

        public async Task<IActionResult> Profile(string id)
        {
            ApplicationUser user;
            if (string.IsNullOrEmpty(id))
                user = await _userManager.GetUserAsync(HttpContext.User);
            else
                user = await _userManager.FindByIdAsync(id);
            
            

            var medicalHistory = from appointment in _context.Appointments.AsEnumerable() where
                                 appointment.PatientID == user.Id && ( DateTime.Parse(appointment.Date) < DateTime.Now) == true
                                 select appointment;
            
            var data = new PatientViewModel();
            data.Patient = (PatientUser)user;
            data.History = medicalHistory;
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string returnUrl = null){
            var model = new PatientRegisterViewModel();
            var user = (PatientUser)await _userManager.GetUserAsync(HttpContext.User);
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.ZipCode = user.ZipCode;
            model.Sex = user.Sex;
            model.Insurance = user.Insurance;
            model.PhoneNumber = user.PhoneNumber;
            model.UserPhoto = model.UserPhoto;
            ViewData["ReturnUrl"] = "/Patient/Profile";
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PatientRegisterViewModel model, string returnUrl = null){
            ViewData["ReturnUrl"] = "/Patient/Profile";
            var currentUser =  await _userManager.GetUserAsync(HttpContext.User);
            var DBuser = _context.Patients.SingleOrDefault(u => u.Id == currentUser.Id);
            if(DBuser != null) 
            {
                if(DBuser.FirstName != model.FirstName || DBuser.LastName != model.LastName){
                    var messages = (from message in _context.Messages
                           where message.PatientID == currentUser.Id select message).ToList();
                
                    foreach(Message m in messages){
                        m.PatientName = model.FirstName + " " + model.LastName;
                    }
                }

                DBuser.FirstName = model.FirstName;
                DBuser.LastName = model.LastName;
                DBuser.ZipCode = model.ZipCode;
                DBuser.Sex = model.Sex;
                DBuser.Insurance = model.Insurance;
                DBuser.PhoneNumber = model.PhoneNumber;
                using (var memoryStream = new MemoryStream())
                {
                  if(model.UserPhoto != null){
                        await model.UserPhoto.CopyToAsync(memoryStream);
                        DBuser.UserPhoto = memoryStream.ToArray();
                  }
                }
                
                _context.SaveChanges();
                return Redirect("/Patient/Profile");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult UpdateMedicalHistory(int id, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = "/Patient/Profile";
            var history = _context.Appointments.SingleOrDefault(u => u.Id == id);
            var model = new Appointment();
            model.Service= history.Service;
            model.Date = history.Date;
            return View(model);
        }

        public async Task<FileContentResult> UserPhotos(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var img = user.UserPhoto;

            return new FileContentResult(img, "image/jpeg");
        }

    }
}