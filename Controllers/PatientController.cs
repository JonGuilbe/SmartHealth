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

namespace SmartHealth.Controllers 

{
    [Authorize]
    public class PatientController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}