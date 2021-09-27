using LoggingWithSerilogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingWithSerilogApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                decimal result = Divide(5, 0);
            }
            catch (DivideByZeroException ex)
            {
                _logger.LogWarning(ex, "An exception occurred while dividing two numbers");
            }

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

        public decimal Divide(decimal a, decimal b)
        {
            _logger.LogInformation("Parameter 1: " + a);
            _logger.LogInformation("Parameter 2: " + b);

            decimal result = 0;

            try
            {
                result = a / b;
            }
            catch (DivideByZeroException ex)
            {
                _logger.LogWarning(ex, "You cannot divide by zero.");
                throw ex;
            }

            return result;
        }
    }
}
