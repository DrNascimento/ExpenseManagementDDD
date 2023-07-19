namespace WebAPI.Configuration
{
    public static class DatabaseConfiguration
    {
        public static string GetPathSQLite(this ConfigurationManager configuration)
        {
            string directoryName = configuration is null ? null : configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(directoryName))
                throw new Exception("Database path is not defined.");

            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", directoryName));

            return $"Data source={path}";
        }
    }
}
