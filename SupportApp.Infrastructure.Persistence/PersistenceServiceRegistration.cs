using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SupportApp.Application.Abstractions.Persistence;
using SupportApp.Infrastructure.Persistence.Options;
using SupportApp.Infrastructure.Persistence.Repositories;

namespace SupportApp.Infrastructure.Persistence;

public static class PersistenceServiceRegistration
    {
    public static IServiceCollection AddPersistenceServices (this IServiceCollection services, IConfiguration configuration)
        {
        // Bind options from configuration
        services
     .AddOptions<DatabaseOptions>()
     .Bind(configuration.GetSection(DatabaseOptions.Key))
     .Validate(config => !string.IsNullOrWhiteSpace(config.ConnectionString), "Database connection string must not be empty.")
     .ValidateOnStart();

        // Register DbContext
        services.AddDbContext<SupportAppDbContext>((sp, options) =>
        {
            var dbOptions = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            options.UseSqlServer(dbOptions.ConnectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(dbOptions.MaxRetryCount);
            });
        });

        // ✅ Register repositories with implementations
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IAgentRepository, AgentRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();

        return services;
        }
    }