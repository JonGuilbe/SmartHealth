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
    public class MessageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public MessageController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Conversation(string id, string returnUrl = null)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string DocId;
            string PatId;
            if(user.AccountType == "Doctor")
            {
                DocId = user.Id;
                PatId = id;
            }
            else
            {
                DocId = id;
                PatId = user.Id;
            }
            ViewData["ReturnUrl"] = "/Message/Conversation/"+ id;
            var messageList = (from message in _context.Messages
                           where message.DoctorID == DocId && message.PatientID == PatId select message);
            var data = new ConversationModel();
            data.IsDoctor = (user.AccountType == "Doctor");
            data.Messages = messageList;
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Conversation(string id, ConversationModel model, string returnUrl = null)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string DocId;
            string PatId;
            if(user.AccountType == "Doctor")
            {
                DocId = user.Id;
                PatId = id;
            }
            else
            {
                DocId = id;
                PatId = user.Id;
            }
            ViewData["ReturnUrl"] = "/Message/Conversation/"+ id;
            /*var messageList = (from message in _context.Messages
                           where message.DoctorID == DocId && message.PatientID == PatId select message);
            var data = new ConversationModel(); 
            data.IsDoctor = (user.AccountType == "Doctor");
            data.Messages = messageList; */
            if(ModelState.IsValid){
                var message = new Message { DoctorID = DocId, PatientID = PatId, MessageString = model.text, FromPatient = (user.AccountType == "Patient")};
                _context.Messages.Add(message);
                _context.SaveChanges();
                return Redirect(returnUrl);
            }
            return View(model);
        }
    }
}