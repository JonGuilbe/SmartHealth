using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHealth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SmartHealth.Controllers
{
        public class HomeController : Controller
    {
        public static void fill()
        {
        var JaneDoe = new DoctorUser { FirstName = "Jane", LastName = "Doe", UserName = "janedoe@123.com", Email = "janedoe@123.com",
                 Address = "123 North Street", PhoneNumber = "911" };

        var JohnSmith = new PatientUser { FirstName = "John", LastName = "Smith", UserName = "johnsmith@123.com", Email = "johnsmith@123.com", DateOfBirth = "1/1/1992", 
                 ZipCode = "34769", Ethnicity = "Cracker", Sex = "Attack Helicopter", Insurance = "None", PhoneNumber = "4071417934"};
        }
        public IActionResult Index()
        {
            fill();
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
