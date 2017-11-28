using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHealth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using SmartHealth.Data;
using DinkToPdf;

namespace SmartHealth.Controllers
{
    [Authorize]
    public class PdfController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        private readonly TemplateService _templateService;

        public PdfController(TemplateService templateService, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _templateService = templateService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {
            var converter = new SynchronizedConverter(new PdfTools());
            PatientUser user;
            if(id == null)    
                user =  (PatientUser)await _userManager.GetUserAsync(HttpContext.User);
            else
                user = (PatientUser)await _userManager.FindByIdAsync(id);
            var query = from appointment in _context.Appointments.AsEnumerable() where
                        appointment.PatientID == user.Id && ( DateTime.Parse(appointment.Date) < DateTime.Now) == true
                         select appointment;
            PdfViewModel records = new PdfViewModel();
            records.PatientName = user.FirstName + " " + user.LastName;
            records.Appointments = query;
            var documentContent = await _templateService.RenderTemplateAsync("Pdf/Records", records);
            var output = converter.Convert(new HtmlToPdfDocument()
            {
                Objects =
                {
                    new ObjectSettings()
                    {
                        HtmlContent = documentContent
                    }
                }
            });
            return File(output, "application/pdf");
        }

    }
}