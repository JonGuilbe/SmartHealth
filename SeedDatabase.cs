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

namespace SmartHealth.SeedDatabase
{
    public class SeedDatabase
    {
        public static void fill()
        {
        var JaneDoe = new DoctorUser { FirstName = "Jane", LastName = "Doe", UserName = "janedoe@123.com", Email = "janedoe@123.com",
                 Address = "123 North Street", PhoneNumber = "911" };

        var JohnSmith = new PatientUser { FirstName = "John", LastName = "Smith", UserName = "johnsmith@123.com", Email = "johnsmith@123.com", DateOfBirth = "1/1/1992", 
                 ZipCode = "34769", Ethnicity = "Cracker", Sex = "Attack Helicopter", Insurance = "None", PhoneNumber = "4071417934"};
        }
    }
}