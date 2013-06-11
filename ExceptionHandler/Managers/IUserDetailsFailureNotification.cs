using System;

namespace ExceptionHandler.Managers
{
    public interface IUserDetailsFailureNotification
    {
        void NotifyTeam(Exception exception);
    }
}