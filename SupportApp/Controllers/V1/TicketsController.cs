using Microsoft.AspNetCore.Mvc;
using SupportApp.Application.Abstractions.Services;
using SupportApp.Dtos;


namespace SupportApp.Controllers
    {
    [ApiController]
    [Route("api/v1/tickets")]
    public class TicketsController: ControllerBase
        {
        private readonly ITicketService _ticketService;
        private readonly ILogger<TicketsController> _logger;

        public TicketsController (ITicketService ticketService, ILogger<TicketsController> logger)
            {
            _ticketService = ticketService;
            _logger = logger;
            }

        [HttpPost]
        public async Task<IActionResult> Create (TicketCreateDto dto)
            {
            var ticket = await _ticketService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
            }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById (int id)
            {
            var ticket = await _ticketService.GetByIdAsync(id);
            if (ticket == null) return NotFound();

            return Ok(ticket);
            }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update (int id, TicketUpdateDto dto)
            {
            await _ticketService.UpdateAsync(id, dto);
            return NoContent();
            }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
            {
            await _ticketService.DeleteAsync(id);
            return NoContent();
            }

        [HttpPost("{ticketId}/assign/{agentId}")]
        public async Task<IActionResult> AssignAgent (int ticketId, int agentId)
            {
            await _ticketService.AssignAgentAsync(ticketId, agentId);
            return NoContent();
            }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus (int id, TicketStatusUpdateDto dto)
            {
            await _ticketService.UpdateStatusAsync(id, dto.Status);
            return NoContent();
            }

        [HttpPatch("{id}/category")]
        public async Task<IActionResult> UpdateCategory (int id, TicketCategoryUpdateDto dto)
            {
            await _ticketService.UpdateCategoryAsync(id, dto.Category);
            return NoContent();
            }

        [HttpPatch("{id}/priority")]
        public async Task<IActionResult> UpdatePriority (int id, TicketPriorityUpdateDto dto)
            {
            await _ticketService.UpdatePriorityAsync(id, dto.Priority);
            return NoContent();
            }
        }
    }