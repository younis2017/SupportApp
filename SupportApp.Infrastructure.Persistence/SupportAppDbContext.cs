

using Microsoft.EntityFrameworkCore;
using SupoortApp.Domain.Entity;
using SupportApp.Domain.Entities;
using SupportApp.Infrastructure.Persistence.Options;

namespace SupportApp.Infrastructure.Persistence;

public class SupportAppDbContext:DbContext
{
    private readonly DatabaseOptions _databaseOptions;
    public SupportAppDbContext (DbContextOptions<SupportAppDbContext> options) : base(options)
        {
        }


    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Agent> Agents => Set<Agent>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
        base.OnModelCreating(modelBuilder);

        // Customer -> Tickets
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Customer)
            .WithMany(c => c.Tickets)
            .HasForeignKey(t => t.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Agent -> Tickets
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.AssignedAgent)
            .WithMany(a => a.AssignedTickets)
            .HasForeignKey(t => t.AssignedAgentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Ticket -> Comments
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Ticket)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        // Optional Author relationships
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Agent)
            .WithMany(a => a.Comments)
            .HasForeignKey(c => c.AgentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Customer)
            .WithMany(cu => cu.Comments)
            .HasForeignKey(c => c.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }