using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHealth.Models;
using SmartHealth;

namespace SmartHealth.Controllers
{
    public class TestControllerOld : Controller 
    {
        private postgresContext testContext;

        public TestControllerOld(postgresContext context)
        {
            testContext = context;
        }

        // GET /Test/
        public IEnumerable<Test> Find()
        {
            return testContext.Test.ToList();
        }

        // GET by ID /Test/Find
        //[HttpGet("{id}")]
        public String Find(int id)
        {
            return testContext.Test.FirstOrDefault(x => x.Id == id).Name;
        }

        // POST /Test/
        //[HttpPost]
        public IActionResult Insert([FromBody]Test value)
        {
            testContext.Test.Add(value);
            testContext.SaveChanges();
            return View();
        }

        public IActionResult Index()
        {
            int id = 1;
            Console.Out.WriteLine();
            Console.Out.WriteLine(testContext.Test.FirstOrDefault(x => x.Id == id).Id);
            Console.Out.WriteLine();
            ViewData["Message"] = testContext.Test.FirstOrDefault(x => x.Id == id).Name;
            return View();
        }
    }
}