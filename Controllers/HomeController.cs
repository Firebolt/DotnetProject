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
        private readonly ITicketService _ticketService;

        public HomeController(
            ILogger<HomeController> logger,
            IFlightService flightService,
            IQueryService queryService,
            UserManager<User> userManager,
            ISeatService seatService,
            ITicketService ticketService)
        {
            _logger = logger;
            _flightService = flightService;
            _queryService = queryService;
            _userManager = userManager;
            _seatService = seatService;
            _ticketService = ticketService;
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

        [HttpGet, Authorize]
        public async Task<IActionResult> QueryList()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var queries = await _queryService.GetQueryByUserIdAsync(user.Id);
            return View(queries);
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Query()
        {
            return View();
        }

        [HttpPost, Authorize]
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

        [HttpGet, Authorize]
        public async Task<IActionResult> Book(int id)
        {
            var result = await _seatService.GetSeatsAsync(id);
            result = result
                .OrderBy(s => int.Parse(string.Join("", s.Name.Where(char.IsDigit))))
                .ThenBy(s => s.Name)
                .ToList();
            if (result != null && result.Any())
                return View(result);
            else
                return RedirectToAction("Index");
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Book(IEnumerable<Seat> seats)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            foreach (var seat in seats)
            {
                if (seat.Booked)
                {
                    await _seatService.UpdateSeatAsync(seat.FID, seat.Name, true);
                    await _ticketService.CreateTicketAsync(user.Id, seat.FID, DateTime.UtcNow, seat.Name);
                }
            }
            return RedirectToAction("Tickets");
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Tickets()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var tickets = await _ticketService.GetTicketsAsync(user.Id);
            return View(tickets);
        }

        [HttpPost]
        public async Task<IActionResult> CancelTicket(string uid, int fid)
        {
            var currentTicket = await _ticketService.GetTicketAsync(uid, fid);
            await _seatService.UpdateSeatAsync(fid, currentTicket.SeatNumber, false);
            await _ticketService.DeleteTicketAsync(uid, fid);
            return RedirectToAction("Tickets");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}