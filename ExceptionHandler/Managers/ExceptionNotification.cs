using System;
using System.IO;
using System.Windows.Forms;
using ExceptionHandler.Mail;
using ExceptionHandler.ScreenShot;
using ExceptionHandler.View;

namespace ExceptionHandler.Managers
{
    public class ExceptionNotification : IExceptionNotification
    {
        private readonly string _screenshotPath = Path.Combine(Application.UserAppDataPath, "UnhandledException");
        private readonly string _logFilePath = Path.Combine(Application.UserAppDataPath, "UnhandledExceptionLog.txt");

        public bool IsScreenshotOk { get; private set; }
        public bool IsSendEmailOk { get; private set; }
        public bool IsWriteToEventLogOk { get; private set; }
        public bool IsWriteToLogOk { get; private set; }

        private string _screenshotStatusMessage; 
        private string _logToFileStatusMessage;
        internal string ScreenshotFullPath;
        private readonly IExceptionMessageBuilder _exceptionMessageBuilder;
        private string _exceptionMessage;
        private string _exceptionType;
        private readonly IMailClient _mailClient;

        public ExceptionNotification(IExceptionMessageBuilder exceptionMessageBuilder, IMailClient mailClient)
        {
            _exceptionMessageBuilder = exceptionMessageBuilder;
            _mailClient = mailClient;
        }

        public void TakeScreenshot()
        {
            try
            {
                ScreenshotFullPath = ScreenSnapper.TakeScreenshot(_screenshotPath);
                _screenshotStatusMessage = "A screenshot was captured and sent to the development team.";
                IsScreenshotOk = true;
            }
            catch (Exception)
            {
                _screenshotStatusMessage = "A screenshot could NOT be taken.";
                IsScreenshotOk = false;
            }
        }

        public void WriteToEventLog()
        {
            try
            {
                System.Diagnostics.EventLog.WriteEntry(System.AppDomain.CurrentDomain.FriendlyName,
                    Environment.NewLine + _exceptionMessage, System.Diagnostics.EventLogEntryType.Error);
                IsWriteToEventLogOk = true;
            }
            catch (Exception)
            {
                IsWriteToEventLogOk = false;
                _logToFileStatusMessage = "Details could NOT be written to the text log at: " + _logFilePath;
            }
        }

        public void WriteToLog()
        {
            try
            {
                var objStreamWriter = new StreamWriter(_logFilePath, true);
                objStreamWriter.Write(_exceptionMessage);
                objStreamWriter.WriteLine();
                objStreamWriter.Close();
                _logToFileStatusMessage = "Details were written to a text log at: " + _logFilePath;
                IsWriteToLogOk = true;
            }
            catch (Exception)
            {
                IsWriteToLogOk = false;
                _logToFileStatusMessage = "Details could NOT be written to the text log at: " + _logFilePath;
            }
        }

        public void SendEmail(MailMessage mailMessage)
        {
            mailMessage.Subject = String.Format("UI Exception for {0} - {1}", Environment.UserName, _exceptionType);
            mailMessage.Body = _exceptionMessage;
            mailMessage.AttachmentPaths.Add(ScreenshotFullPath);
            mailMessage.AttachmentPaths.Add(_logFilePath);
            try
            {
                _mailClient.PrepareAndSend(mailMessage);
                IsSendEmailOk = true;
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
            {
                IsSendEmailOk = false;
            }
            // ReSharper restore EmptyGeneralCatchClause
        }

        public void CreateAndOpenExceptionUI()
        {
            const string strWhatHappened =
                "There was an unexpected error in (app). This may be due to a programming bug.";
            const string strWhatUserCanDo =
                "Restart (app) and try repeating your last action. Try alternative methods of performing the same action.";
            const string strHowUserAffected = "When you click OK, (app) will close.";
            var userFacingException = _exceptionMessageBuilder.FormatExceptionForUser(_screenshotStatusMessage,
                                                                        _logToFileStatusMessage, _exceptionMessage);
            var handledExceptionManager = new HandledExceptionManager(new MailClient(new SmtpPostalWorker()), _exceptionMessageBuilder, new ExceptionDialog());
            handledExceptionManager.ShowDialog(strWhatHappened, strHowUserAffected, strWhatUserCanDo,
                                        userFacingException);
        }

        public string BuildExceptionMessage(Exception exception, string exceptionType)
        {
            _exceptionType = exceptionType;
            _exceptionMessage = _exceptionMessageBuilder.ExceptionInfo(exception);
            return _exceptionMessage;
        }
    }
}