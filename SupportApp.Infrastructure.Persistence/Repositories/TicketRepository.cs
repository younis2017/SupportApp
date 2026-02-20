using Microsoft.EntityFrameworkCore;
using SupportApp.Application.Abstractions.Persistence;
using SupportApp.Domain.Entities;
using SupportApp.Infrastructure.Persistence;

namespace SupportApp.Infrastructure.Persistence.Repositories;

public class TicketRepository: ITicketRepository
    {
    private readonly SupportAppDbContext _context;

    public TicketRepository (SupportAppDbContext context) => _context = context;

    // Read
    public async Task<IReadOnlyList<Ticket>> GetAllAsync (CancellationToken cancellationToken = default) =>
        await _context.Tickets
            .Include(t => t.Customer)
            .Include(t => t.AssignedAgent)
            .Include(t => t.Comments)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<Ticket?> GetByIdAsync (int id, CancellationToken cancellationToken = default) =>
        await _context.Tickets
            .Include(t => t.Customer)
            .Include(t => t.AssignedAgent)
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Ticket>> GetByCustomerIdAsync (int customerId, CancellationToken cancellationToken = default) =>
        await _context.Tickets
            .Where(t => t.CustomerId == customerId)
            .Include(t => t.AssignedAgent)
            .Include(t => t.Comments)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Ticket>> GetByAgentIdAsync (int agentId, CancellationToken cancellationToken = default) =>
        await _context.Tickets
            .Where(t => t.AssignedAgentId == agentId)
            .Include(t => t.Customer)
            .Include(t => t.Comments)
            .ToListAsync(cancellationToken);

    // Write
    public async Task AddAsync (Ticket ticket, CancellationToken cancellationToken = default) =>
        await _context.Tickets.AddAsync(ticket, cancellationToken);

    public void Update (Ticket ticket) => _context.Tickets.Update(ticket);

    public async Task DeleteByIdAsync (int id, CancellationToken cancellationToken = default)
        {
        var ticket = await _context.Tickets.FindAsync(new object[] { id }, cancellationToken);
        if (ticket != null)
            _context.Tickets.Remove(ticket);
        }

    // Validation
    public async Task<bool> ExistsAsync (int id, CancellationToken cancellationToken = default) =>
        await _context.Tickets.AnyAsync(t => t.Id == id, cancellationToken);

    public async Task<bool> CanAssignAgentAsync (int ticketId, CancellationToken cancellationToken = default)
        {
        var ticket = await _context.Tickets.FindAsync(new object[] { ticketId }, cancellationToken);
        return ticket != null && ticket.Status != TicketStatus.Closed;
        }

    public async Task<bool> CanChangeCategoryOrPriorityAsync (int ticketId, CancellationToken cancellationToken = default)
        {
        var ticket = await _context.Tickets.FindAsync(new object[] { ticketId }, cancellationToken);
        return ticket != null && ticket.Status != TicketStatus.Closed && ticket.Status != TicketStatus.Resolved;
        }

    // Save
    public async Task<int> SaveChangesAsync (CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken);
    }