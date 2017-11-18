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
    public class DoctorController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Home()
        {
            return View();
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
                           service.DoctorID == user.Id select service;

            var data = new DoctorViewModel();
            data.Doctor = (DoctorUser)user;
            data.Services = services;
            return View(data);
        }
        //TODO Fix this
        public IActionResult AddService()
        {
            return View();
        }

        public async Task Add(ServiceAddViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var service = new Service { Name = model.Name, Cost = model.Cost, DoctorID = user.Id};
            _context.Services.Add(service);
            _context.SaveChanges();
        }
    }
}