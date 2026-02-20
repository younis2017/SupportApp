using SupportApp.Domain.Entities;

namespace SupportApp.Application.Abstractions.Persistence;

public interface ITicketRepository
    {
    Task<IReadOnlyList<Ticket>> GetAllAsync (CancellationToken cancellationToken = default);
    Task<Ticket?> GetByIdAsync (int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Ticket>> GetByCustomerIdAsync (int customerId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Ticket>> GetByAgentIdAsync (int agentId, CancellationToken cancellationToken = default);

    Task AddAsync (Ticket ticket, CancellationToken cancellationToken = default);
    void Update (Ticket ticket);
    Task DeleteByIdAsync (int id, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync (int id, CancellationToken cancellationToken = default);
    Task<bool> CanAssignAgentAsync (int ticketId, CancellationToken cancellationToken = default);
    Task<bool> CanChangeCategoryOrPriorityAsync (int ticketId, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync (CancellationToken cancellationToken = default);
    }