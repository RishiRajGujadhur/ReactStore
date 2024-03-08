namespace Store.API.Extentions
{
    public static class LoggerExtention
    {
        public static string AddErrorDetails(Exception ex, string username, string message = "", string methodName = "")
        {
            return $"{message} - {username} : {DateTime.UtcNow} - {ex.InnerException.Message}, {ex.InnerException.InnerException.Message}, {ex.Message}, method: {methodName}";
        }
    }
}