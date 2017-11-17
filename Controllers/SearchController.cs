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
    public class SearchController : Controller
    {

        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Result([FromQuery] string Query)
        {
            ViewData["Query"] = Query;
            var query = from user in _context.Doctors where
                        user.LastName == Query || user.FirstName == Query select user;
            
            foreach(var result in query)
            {
                Console.WriteLine("Doctor Info: First name - {0} Last name - {1}", result.FirstName, result.LastName );
            }

            ViewData["Result"] = query;
            return View(query);
        }
       
    }
}