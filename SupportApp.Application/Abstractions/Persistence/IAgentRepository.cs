using SupportApp.Domain.Entities;

namespace SupportApp.Application.Abstractions.Persistence;

public interface IAgentRepository
    {
    Task<IReadOnlyList<Agent>> GetAllAsync (CancellationToken cancellationToken = default);
    Task<Agent?> GetByIdAsync (int id, CancellationToken cancellationToken = default);

    Task AddAsync (Agent agent, CancellationToken cancellationToken = default);
    void Update (Agent agent);
    Task DeleteByIdAsync (int id, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync (CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync (string email, int? excludeId = null, CancellationToken cancellationToken = default);

    }