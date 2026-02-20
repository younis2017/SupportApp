using SupportApp.Domain.Entities;

namespace SupportApp.Application.Abstractions.Persistence;

public interface ICustomerRepository
    {
    Task<IReadOnlyList<Customer>> GetAllAsync (CancellationToken cancellationToken = default);
    Task<Customer?> GetByIdAsync (int id, CancellationToken cancellationToken = default);

    Task AddAsync (Customer customer, CancellationToken cancellationToken = default);
    void Update (Customer customer);
    Task DeleteByIdAsync (int id, CancellationToken cancellationToken = default);
    Task DeleteByEntityAsync (Customer customer, CancellationToken cancellationToken = default);

    Task<bool> EmailExistsAsync (string email, int? excludeId = null, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync (CancellationToken cancellationToken = default);
    }