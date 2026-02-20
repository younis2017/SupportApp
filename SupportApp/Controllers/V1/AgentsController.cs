using Microsoft.AspNetCore.Mvc;
using SupportApp.Application.Abstractions.Services;
using SupportApp.Dtos;


namespace SupportHub.Controllers
    {
    [ApiController]
    [Route("api/v1/agents")]
    public class AgentsController: ControllerBase
        {
        private readonly IAgentService _agentService;
        private readonly ILogger<AgentsController> _logger;

        public AgentsController (IAgentService agentService, ILogger<AgentsController> logger)
            {
            _agentService = agentService;
            _logger = logger;
            }

        [HttpPost]
        public async Task<IActionResult> Create (AgentCreateDto dto)
            {
            if (string.IsNullOrWhiteSpace(dto.DisplayName) || string.IsNullOrWhiteSpace(dto.Email))
                return BadRequest("DisplayName and Email are required");

            var agent = await _agentService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = agent.Id }, agent);
            }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById (int id)
            {
            var agent = await _agentService.GetByIdAsync(id);
            if (agent == null) return NotFound();

            return Ok(agent);
            }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
            {
            await _agentService.DeleteAsync(id);
            return NoContent();
            }
        }
    }