using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFlightService _flightService;

        public HomeController(ILogger<HomeController> logger, IFlightService flightService)
        {
            _logger = logger;
            _flightService = flightService;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<IActionResult> Index()
        {
            var flights = await _flightService.GetAllFlightsAsync();
            return View(flights);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<Microsoft.AspNetCore.Mvc.ActionResult> FilterFlights(string takeoffLocation, string destination)
        {
            var filteredFlights = await _flightService.GetAllFlightsAsync();
            filteredFlights = filteredFlights
                .Where(f =>
                    (string.IsNullOrEmpty(takeoffLocation) || f.TakeOffLocation == takeoffLocation) &&
                    (string.IsNullOrEmpty(destination) || f.Destination == destination))
                .ToList();

            return PartialView("_FlightList", filteredFlights);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}