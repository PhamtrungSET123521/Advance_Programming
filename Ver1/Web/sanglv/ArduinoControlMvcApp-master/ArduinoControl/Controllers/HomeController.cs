using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ArduinoControl.Models;
using ArduinoControl.library;

namespace ArduinoControl.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private SerialPortConnector _serialPortConnector;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _serialPortConnector = new SerialPortConnector();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Status(string command, string roomNumber)
        {
            return Ok(Commons.FileUtils.read("minhhanh.dat", ""));
        }

        [HttpPost]
        public IActionResult Send(string command,string roomNumber)
        {
            try
            {
                return _serialPortConnector.Send(command, roomNumber) 
                    ? (IActionResult) Ok()
                    : (IActionResult) BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
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
