﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using TareasMVC.Models;

namespace TareasMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> localizer;

        //Agregamos al controlador el IStringLocalizer<HomeController> localizer) vero todos los pasos en 
        //program
        public HomeController(ILogger<HomeController> logger, 
            IStringLocalizer<HomeController> localizer)
        {
            _logger = logger;
            this.localizer = localizer;
        }
        //Agregamos a la vista el ViewBag para usar otros idiomas.
        public IActionResult Index()
        {
            ViewBag.Saludo = localizer["Buenos dias"];
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}