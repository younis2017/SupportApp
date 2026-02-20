using Microsoft.AspNetCore.Mvc;
using SupportApp.Application.Abstractions.Services;
using SupportApp.Application.Services;
using SupportApp.Dtos;

[ApiController]
[Route("api/v1/customers")]
public class CustomersController: ControllerBase
    {
    private readonly ICustomerService _customerService; // ✅ Should be the interface, not CustomerService
    private readonly ITicketService _ticketService;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController (
        ICustomerService repo,        // ✅ interface
        ITicketService ticketRepo,
        ILogger<CustomersController> logger)
        {
        _customerService = repo;
        _ticketService = ticketRepo;
        _logger = logger;
        }

    [HttpPost]
    public async Task<IActionResult> Create (CustomerCreateDto dto)
        {
        var customer = await _customerService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById (int id)
        {
        var customer = await _customerService.GetByIdAsync(id);
        if (customer == null) return NotFound();

        return Ok(customer);
        }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update (int id, CustomerUpdateDto dto)
        {
        await _customerService.UpdateAsync(id, dto);
        return NoContent();
        }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete (int id)
        {
        await _customerService.DeleteAsync(id);
        return NoContent();
        }

    [HttpGet("{id}/tickets")]
    public async Task<IActionResult> GetTickets (int id)
        {
        var tickets = await _ticketService.GetByCustomerIdAsync(id);
        return Ok(tickets);
        }
    }