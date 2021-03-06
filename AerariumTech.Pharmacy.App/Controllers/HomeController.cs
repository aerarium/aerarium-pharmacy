﻿using System.Diagnostics;
using AerariumTech.Pharmacy.App.Models.SharedViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AerariumTech.Pharmacy.App.Controllers
{
    [Route("[action]")]
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
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
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}