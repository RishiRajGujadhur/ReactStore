namespace API.Extentions
{
    public static class LoggerExtention
    {
        public static string AddErrorDetails(Exception ex, string username, string message = "")
        {
            return message + " " + username + " : " + DateTime.UtcNow.ToString() + " " + ex.Message + " " + ex.InnerException.Message + " " + ex.InnerException.InnerException.Message;
        }
    }
}