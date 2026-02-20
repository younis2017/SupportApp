using Microsoft.EntityFrameworkCore;
using SupportApp.Application.Abstractions.Persistence;
using SupportApp.Domain.Entities;
using SupportApp.Infrastructure.Persistence;

namespace SupportApp.Infrastructure.Persistence.Repositories;

public class CustomerRepository: ICustomerRepository
    {
    private readonly SupportAppDbContext _context;

    public CustomerRepository (SupportAppDbContext context) => _context = context;

    // Read
    public async Task<IReadOnlyList<Customer>> GetAllAsync (CancellationToken cancellationToken = default) =>
        await _context.Customers.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<Customer?> GetByIdAsync (int id, CancellationToken cancellationToken = default) =>
        await _context.Customers.FindAsync(new object[] { id }, cancellationToken);

    // Write
    public async Task AddAsync (Customer customer, CancellationToken cancellationToken = default) =>
        await _context.Customers.AddAsync(customer, cancellationToken);

    public void Update (Customer customer) => _context.Customers.Update(customer);

    public async Task DeleteByIdAsync (int id, CancellationToken cancellationToken = default)
        {
        var customer = await _context.Customers.FindAsync(new object[] { id }, cancellationToken);
        if (customer != null)
            _context.Customers.Remove(customer);
        }

    public Task DeleteByEntityAsync (Customer customer, CancellationToken cancellationToken = default)
        {
        _context.Customers.Remove(customer);
        return Task.CompletedTask;
        }

    // Validation
    public async Task<bool> EmailExistsAsync (string email, int? excludeId = null, CancellationToken cancellationToken = default) =>
        await _context.Customers
            .AnyAsync(c => c.Email == email && (!excludeId.HasValue || c.Id != excludeId.Value), cancellationToken);

    // Save
    public async Task<int> SaveChangesAsync (CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken);
    }