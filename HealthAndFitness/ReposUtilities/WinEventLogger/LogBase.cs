
namespace ReposUtilities.Log
{
    public enum LogTarget
    {
        File,
        Database,
        EventLog
    }
    public abstract class LogBase
    {
        protected readonly object LockObj = new object();
        public abstract void Log(string message);

    }
}
