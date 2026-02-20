using Microsoft.EntityFrameworkCore;
using SupoortApp.Domain.Entity;

namespace SupportApp.Infrastructure
    {
    public class SupportHubDbContext: DbContext
        {
        // Constructor
        public SupportHubDbContext (DbContextOptions<SupportHubDbContext> options)
            : base(options)
            {
            }

        // DbSets
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketComment> TicketComments { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
            {

            modelBuilder.Entity<Customer>(entity =>

            {

                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name).IsRequired().HasMaxLength(200);

                entity.Property(c => c.Email).IsRequired().HasMaxLength(50);

                entity.Property(c => c.CreatedAtUtc).IsRequired().HasDefaultValue(DateTime.UtcNow);

            });

            modelBuilder.Entity<Ticket>(entity =>

            {

                entity.HasKey(c => c.Id);

                entity.Property(c => c.CreatedAtUtc).IsRequired().HasDefaultValue(DateTime.UtcNow);

                entity.Property(c => c.Title).IsRequired().HasMaxLength(300);

                entity.HasOne(c => c.Customer)

                      .WithMany(c => c.Tickets)

                      .HasForeignKey(c => c.CustomerId)

                      .OnDelete(DeleteBehavior.Cascade);

            });


            }
        }


    }
