using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace FinalProject.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IQueryService _queryService;
        private readonly IFlightService _flightService;
        private readonly ISeatService _seatService;
        public AdminController(IQueryService queryService, IFlightService flightService, ISeatService seatService)
        {
            _queryService = queryService;
            _flightService = flightService;
            _seatService = seatService;
        }

        [HttpGet]
        public async Task<IActionResult> QueryList()
        {
            var queries = await _queryService.GetAllQueriesAsync();
            ViewBag.Unanswered = queries.Where(q => string.IsNullOrEmpty(q.Answer)).ToList();
            ViewBag.Answered = queries.Where(q => !string.IsNullOrEmpty(q.Answer)).ToList();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AnswerQuery(int qid, string Id)
        {
            var query = await _queryService.GetQueryAsync(qid, Id);
            return View(query);
        }

        [HttpPost]
        public async Task<IActionResult> AnswerQuery(Query query)
        {
            var body = new QueryRequest
            {
                Question = query.Question,
                Answer = query.Answer
            };
            await _queryService.UpdateQueryAsync(query.QID, query.Id, body);
            return RedirectToAction("QueryList");
        }

        [HttpGet]
        public IActionResult AddFlight()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFlight(FlightRequest flight)
        {
            flight.FlightDuration = flight.ArrDateandTimeOffset.Subtract(flight.DeparDateandTimeOffset);
            double total = flight.FlightDuration.TotalHours;
            var smth = flight.FlightDuration.Duration();
            int fid = await _flightService.CreateFlightAsync(flight);
            for (int i = 0; i < 6; i++)
                for (int j = 1; j <= flight.NumofRows; j++)
                    _seatService.CreateSeatAsync(fid, GetSeatName(i, j));
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DeleteFlight(int id)
        {
            await _flightService.DeleteFlightAsync(id);
            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        public async Task<IActionResult> EditFlight(int id)
        {
            var result = await _flightService.GetFlightByIdAsync(id);
            if (result != null)
            {
                var flight = new FlightRequest
                {
                    NumofRows = result.NumofRows,
                    AirportLoc = result.AirportLoc,
                    ArrDateandTimeOffset = result.ArrDateandTimeOffset.ToUniversalTime(),
                    DeparDateandTimeOffset = result.DeparDateandTimeOffset.ToUniversalTime(),
                    Destination = result.Destination,
                    TakeOffLocation = result.TakeOffLocation,
                    TicketCost = result.TicketCost
                };
                return View(flight);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> EditFlight(FlightRequest flight, int id)
        {
            flight.FlightDuration = flight.ArrDateandTimeOffset.Subtract(flight.DeparDateandTimeOffset);
            await _flightService.UpdateFlightAsync(id, flight);
            return RedirectToAction("Index", "Home");
        }
        private string GetSeatName(int i, int j)
        {
            switch (i)
            {
                case 0:
                    return j + "A";
                case 1:
                    return j + "B";
                case 2:
                    return j + "C";
                case 3:
                    return j + "D";
                case 4:
                    return j + "E";
                case 5:
                    return j + "F";
                default:
                    return "Error";
            }
        }
    }
}