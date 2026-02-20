using Microsoft.EntityFrameworkCore;
using SupportApp.Application.Abstractions.Persistence;
using SupportApp.Domain.Entities;
using SupportApp.Infrastructure.Persistence;

namespace SupportApp.Infrastructure.Persistence.Repositories;

public sealed class CommentRepository: ICommentRepository
    {
    private readonly SupportAppDbContext _dbContext;

    public CommentRepository (SupportAppDbContext dbContext)
        {
        _dbContext = dbContext;
        }

    public async Task<IReadOnlyList<Comment>> GetAllAsync (CancellationToken cancellationToken = default) =>
        await _dbContext.Comments
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<Comment?> GetByIdAsync (int id, CancellationToken cancellationToken = default) =>
        await _dbContext.Comments
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task AddAsync (Comment comment, CancellationToken cancellationToken = default) =>
        await _dbContext.Comments.AddAsync(comment, cancellationToken);

    public void Update (Comment comment) => _dbContext.Comments.Update(comment);

    public async Task DeleteByIdAsync (int id, CancellationToken cancellationToken = default)
        {
        var comment = await _dbContext.Comments
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (comment != null)
            _dbContext.Comments.Remove(comment);
        }

    public async Task<bool> CanAddCommentAsync (int ticketId, CancellationToken cancellationToken = default) =>
        await _dbContext.Tickets
            .AnyAsync(t => t.Id == ticketId, cancellationToken);

    public async Task<int> SaveChangesAsync (CancellationToken cancellationToken = default) =>
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
