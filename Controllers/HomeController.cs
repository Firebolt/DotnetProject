using FinalProject.Models;
using FinalProject.Services.Implementations;
using FinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ISeatService _seatService;

        public HomeController(
            ILogger<HomeController> logger,
            IFlightService flightService,
            IQueryService queryService,
            UserManager<User> userManager,
            ISeatService seatService)
        {
            _logger = logger;
            _flightService = flightService;
            _queryService = queryService;
            _userManager = userManager;
            _seatService = seatService;
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

        public async Task<ActionResult> FilterFlights(string takeoffLocation, string destination, decimal maxTicketCost, int? flightId)
        {
            var filteredFlights = await _flightService.GetAllFlightsAsync();
            if (filteredFlights != null)
            {
            filteredFlights = filteredFlights
                .Where(f =>
                    (string.IsNullOrEmpty(takeoffLocation) || f.TakeOffLocation == takeoffLocation) &&
                    (string.IsNullOrEmpty(destination) || f.Destination == destination) &&
                    f.TicketCost <= maxTicketCost &&
                    (flightId == null || f.FID == flightId))
                .ToList();
            }
            return PartialView("_FlightList", filteredFlights);
        }

        public async Task<IActionResult> QueryList()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var queries = await _queryService.GetQueryByUserIdAsync(user.Id);
            return View(queries);
        }

        [HttpGet]
        public async Task<IActionResult> Query()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Query(QueryRequest body)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            await _queryService.CreateQueryAsync(body, user.Id);
            return RedirectToAction("Index");
        }

        [HttpGet, Authorize(Roles = "TicketAgent")]
        public async Task<IActionResult> FlightDetails(int id)
        {
            var result = await _flightService.GetFlightByIdAsync(id);
            return (result == null)? RedirectToAction("Index"): View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Book(int id)
        {
            var result = await _seatService.GetSeatsAsync(id);
            result = result
                .OrderBy(s => int.Parse(string.Join("", s.Name.Where(char.IsDigit))))
                .ThenBy(s => s.Name)
                .ToList();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Book(IEnumerable<Seat> seats)
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