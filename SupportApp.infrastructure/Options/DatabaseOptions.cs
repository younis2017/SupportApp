namespace SupportHub.Infrastructure.Options
    {
    public class DatabaseOptions
        {
        // Connection string for your database
        public string ConnectionString { get; set; } = string.Empty;

        // Optional: database provider type (e.g., SqlServer, PostgreSQL)
        public string Provider { get; set; } = "SqlServer";

        // Optional: other settings
        public int CommandTimeout { get; set; } = 30;
        }
    }