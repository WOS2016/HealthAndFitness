
namespace ReposUtilities.Log
{
    public static class LogHelper
    {

        private static LogBase _logger = null;

        public static void Log(LogTarget target, string message)
        {
            switch (target)
            {
                case LogTarget.File:
                    _logger = new FileLogger();
                    _logger.Log(message);
                    break;

                case LogTarget.Database:
                    _logger = new DataBaseLogger();
                    _logger.Log(message);
                    break;

                case LogTarget.EventLog:
                    _logger = new EventLogger();
                    _logger.Log(message);
                    break;

                default:
                    return;
            }
        }

    }
}
