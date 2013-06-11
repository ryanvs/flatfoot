using System;
using ExceptionHandler.Mail;

namespace ExceptionHandler.Managers
{
    public interface IExceptionNotification
    {
        void TakeScreenshot();
        void WriteToLog();
        void SendEmail(MailMessage message);
        void CreateAndOpenExceptionUI();
        string BuildExceptionMessage(Exception exceptionMessage, string exceptionType);
    }
}