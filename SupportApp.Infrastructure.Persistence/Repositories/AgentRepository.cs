using Microsoft.EntityFrameworkCore;
using SupportApp.Application.Abstractions.Persistence;
using SupportApp.Domain.Entities;
using SupportApp.Infrastructure.Persistence;

namespace SupportApp.Infrastructure.Persistence.Repositories;

public class AgentRepository: IAgentRepository
    {
    private readonly SupportAppDbContext _context;

    public AgentRepository (SupportAppDbContext context) => _context = context;

    public async Task<IReadOnlyList<Agent>> GetAllAsync (CancellationToken cancellationToken = default) =>
        await _context.Agents.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<Agent?> GetByIdAsync (int id, CancellationToken cancellationToken = default) =>
        await _context.Agents.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    public async Task AddAsync (Agent agent, CancellationToken cancellationToken = default) =>
        await _context.Agents.AddAsync(agent, cancellationToken);

    public void Update (Agent agent) => _context.Agents.Update(agent);

    public async Task DeleteByIdAsync (int id, CancellationToken cancellationToken = default)
        {
        var agent = await _context.Agents.FindAsync(new object[] { id }, cancellationToken);
        if (agent != null) _context.Agents.Remove(agent);
        }

    public async Task<int> SaveChangesAsync (CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken);



    public async Task<bool> EmailExistsAsync (string email, int? excludeId = null, CancellationToken cancellationToken = default)
        {
        return await _context.Agents
            .AnyAsync(a => a.Email == email && (!excludeId.HasValue || a.Id != excludeId.Value), cancellationToken);
        }
 
    }