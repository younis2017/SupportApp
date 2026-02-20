using SupportApp.Domain.Entities;

namespace SupportApp.Application.Abstractions.Persistence;

public interface ICommentRepository
    {
    Task<IReadOnlyList<Comment>> GetAllAsync (CancellationToken cancellationToken = default);
    Task<Comment?> GetByIdAsync (int id, CancellationToken cancellationToken = default);

    Task AddAsync (Comment comment, CancellationToken cancellationToken = default);
    void Update (Comment comment);
    Task DeleteByIdAsync (int id, CancellationToken cancellationToken = default);

    Task<bool> CanAddCommentAsync (int ticketId, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync (CancellationToken cancellationToken = default);
    }