using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using SupportApp.Dtos;

namespace SupportApp.Controllers
    {
    public class sss: Controller
        {
        private readonly HttpClient _httpClient;
        private readonly ILogger<sss> _logger; // ✅ Note the generic type

        public sss (IHttpClientFactory httpClientFactory, ILogger<sss> logger)
            {
            _httpClient = httpClientFactory.CreateClient("SupportApp");
            _logger = logger;
            }

        public async Task<IActionResult> Index ()
            {
            List<TicketReadDto> tickets = new List<TicketReadDto>();

            try
                {
                var response = await _httpClient.GetAsync("api/v1/tickets");

                if (response.IsSuccessStatusCode)
                    {
                    tickets = await response.Content.ReadFromJsonAsync<List<TicketReadDto>>();
                    }
                else
                    {
                    // Log error or handle different status codes
                    // For example: 404, 500, etc.
                    _logger.LogError("API call failed with status code {StatusCode}", response.StatusCode);
                    }
                }
            catch (Exception ex)
                {
                _logger.LogError(ex, "Error calling API");
                }

            return View(tickets);
            }

        public IActionResult Privacy ()
            {
            return View();
            }

        [HttpPost]
        public async Task<IActionResult> CreateTicket (TicketCreateDto dto)
            {
            var response = await _httpClient.PostAsJsonAsync("api/v1/tickets", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View("Error"); // simple error handling
            }
        }
    }