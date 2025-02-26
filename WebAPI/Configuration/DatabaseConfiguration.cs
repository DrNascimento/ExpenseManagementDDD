namespace WebAPI.Configuration;

public static class DatabaseConfiguration
{
    public static string GetPathSQLite(this ConfigurationManager configuration)
    {
        string directoryName = configuration?.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Database path is not defined.");                 

        string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", directoryName));

        return $"Data source={path}";
    }
}
