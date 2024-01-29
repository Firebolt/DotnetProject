using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFlightService _flightService;
        private readonly IQueryService _queryService;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger, IFlightService flightService, IQueryService queryService, UserManager<User> userManager)
        {
            _logger = logger;
            _flightService = flightService;
            _queryService = queryService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var flights = await _flightService.GetAllFlightsAsync();
            return View(flights);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<ActionResult> FilterFlights(string takeoffLocation, string destination, decimal maxTicketCost)
        {
            var filteredFlights = await _flightService.GetAllFlightsAsync();
            filteredFlights = filteredFlights
                .Where(f =>
                    (string.IsNullOrEmpty(takeoffLocation) || f.TakeOffLocation == takeoffLocation) &&
                    (string.IsNullOrEmpty(destination) || f.Destination == destination) &&
                    f.TicketCost <= maxTicketCost)
                .ToList();

            return PartialView("_FlightList", filteredFlights);
        }

        [HttpGet("/YourQueries")]
        public async Task<IActionResult> QueryList()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Query()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Query(QueryRequest body)
        {
            await _queryService.CreateQueryAsync(body, _userManager.GetUserAsync(HttpContext.User).Id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}