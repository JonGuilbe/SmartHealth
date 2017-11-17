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
            ViewData["id"] = null;
            ApplicationUser user;
            if (string.IsNullOrEmpty(id)) //If empty, get the current user
            {
                user = await _userManager.GetUserAsync(HttpContext.User);
            }
            else //If not empty, get the doctor we're looking for
                user = await _userManager.FindByIdAsync(id);
                ViewData["id"] = user.Id;
            return View(user);
        }
    }
}