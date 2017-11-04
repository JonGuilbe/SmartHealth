using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartHealth.Models;
using SmartHealth.Models.AccountViewModels;
using SmartHealth.Services;

namespace SmartHealth.Controllers {
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}