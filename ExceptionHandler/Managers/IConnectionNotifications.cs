namespace ExceptionHandler.Managers
{
    public interface IConnectionNotifications
    {
        void NotifyConnectionFailed();
        void NotifyConnectionSuspended();
    }
}