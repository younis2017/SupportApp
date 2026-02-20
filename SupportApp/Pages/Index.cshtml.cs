using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupportApp.Dtos;
using System.Net.Http;
using System.Net.Http.Json;

namespace SupportApp.Pages
    {
    public class IndexModel: PageModel
        {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel (IHttpClientFactory httpClientFactory)
            {
            _httpClientFactory = httpClientFactory;
            }

        // List of tickets to display
        public List<TicketReadDto> Tickets { get; set; } = new();

        // Bind properties for form input
        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public int CustomerId { get; set; }

        [BindProperty]
        public int AgentId { get; set; }

        // GET: load tickets
        public async Task OnGetAsync ()
            {
            var httpClient = _httpClientFactory.CreateClient("SupportApp");
            try
                {
                Tickets = await httpClient.GetFromJsonAsync<List<TicketReadDto>>("api/v1/tickets") ?? new();
                }
            catch
                {
                Tickets = new();
                }
            }

        // POST: create new ticket
        public async Task<IActionResult> OnPostCreateAsync ()
            {
            var httpClient = _httpClientFactory.CreateClient("SupportApp");

            var newTicket = new TicketCreateDto
                {
                Title = this.Title,
                Description = this.Description,
                CustomerId = this.CustomerId,
                AgentId = this.AgentId
                };

            try
                {
                var response = await httpClient.PostAsJsonAsync("api/v1/tickets", newTicket);
                if (!response.IsSuccessStatusCode)
                    {
                    ModelState.AddModelError(string.Empty, "Failed to create ticket.");
                    return Page();
                    }
                }
            catch
                {
                ModelState.AddModelError(string.Empty, "Error calling API.");
                return Page();
                }

            // Refresh ticket list after create
            await OnGetAsync();
            return Page();
            }
        }
    }