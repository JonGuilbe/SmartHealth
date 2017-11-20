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

            return View(query);
        }

        public async Task<IActionResult> Profile(string id)
        {
            ApplicationUser user;
            if (string.IsNullOrEmpty(id))
                user = await _userManager.GetUserAsync(HttpContext.User);
            else
                user = await _userManager.FindByIdAsync(id);
            return View(user);
        }

        public async Task<FileContentResult> UserPhotos()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var img = user.UserPhoto;

            return new FileContentResult(img, "image/jpeg");
        }

    }
}