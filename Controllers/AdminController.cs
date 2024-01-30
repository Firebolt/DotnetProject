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
        public AdminController(IQueryService queryService, IFlightService flightService)
        {
            _queryService = queryService;
            _flightService = flightService;
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
            await _flightService.CreateFlightAsync(flight);
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
                    ArrDateandTimeOffset = result.ArrDateandTimeOffset,
                    DeparDateandTimeOffset = result.DeparDateandTimeOffset,
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
    }
}