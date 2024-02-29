namespace API.Extentions
{
    public static class LoggerExtention
    {
        public static string AddErrorDetails(Exception ex, string username, string message = "")
        {
            return $"{message} - {username} : {DateTime.UtcNow} - {ex.InnerException.Message}, {ex.InnerException.InnerException.Message}, {ex.Message}";
        }
    }
}