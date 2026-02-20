using SupoortApp.Domain.Entity;
using SupportApp.Domain.Entities;

namespace SupportHub.Infrastructure.Persistence.DataMock;

public static class CustomerDataMock
{
    public static Task<List<Customer>> GetCustomerSeedData()
    {
        var seedUtc = new DateTime(2026, 1, 29, 0, 0, 0, DateTimeKind.Utc);

        return Task.FromResult(new List<Customer>()
        {
            new() { Name = "Acme Corp", Email = "support@acme.com",CreatedAtUtc = DateTime.UtcNow},
            new() { Name = "Globex Ltd", Email = "it@globex.com" ,CreatedAtUtc = DateTime.UtcNow},
            new() { Name = "Initech", Email = "helpdesk@initech.com",CreatedAtUtc = DateTime.UtcNow },
            new() { Name = "Umbrella Group", Email = "tickets@umbrella.com" ,CreatedAtUtc = DateTime.UtcNow},
            new() { Name = "Wayne Enterprises", Email = "support@wayneenterprises.com" , CreatedAtUtc = DateTime.UtcNow},
            new() { Name = "Stark Industries", Email = "it@starkindustries.com" , CreatedAtUtc = DateTime.UtcNow},
            new() { Name = "Oscorp", Email = "help@oscorp.com" , CreatedAtUtc = DateTime.UtcNow},
            new() { Name = "Wonka Factory", Email = "support@wonka.com" , CreatedAtUtc = DateTime.UtcNow},
            new() { Name = "Hooli", Email = "it@hooli.com" , CreatedAtUtc = DateTime.UtcNow},
            new() { Name = "Cyberdyne Systems", Email = "support@cyberdyne.com" , CreatedAtUtc = DateTime.UtcNow},
            new() { Name = "Tyrell Corporation", Email = "it@tyrell.com" , CreatedAtUtc = DateTime.UtcNow},
            new() { Name = "Aperture Science", Email = "helpdesk@aperturescience.com" , CreatedAtUtc = DateTime.UtcNow},
            new() { Name = "Soylent Corp", Email = "support@soylent.com" , CreatedAtUtc = DateTime.UtcNow},
            new() { Name = "Blue Sun", Email = "it@bluesun.com" , CreatedAtUtc = DateTime.UtcNow},
            new() { Name = "Pied Piper", Email = "support@piedpiper.com" , CreatedAtUtc = DateTime.UtcNow}
        });
    }
}