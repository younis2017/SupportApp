namespace SupportApp.Infrastructure.Persistence.Options;

public class DatabaseOptions
    {
    public const string Key = "DatabaseOptions"; // Must match JSON
    public string ConnectionString { get; set; } = string.Empty;
    public int MaxRetryCount { get; set; } = 5;
    }