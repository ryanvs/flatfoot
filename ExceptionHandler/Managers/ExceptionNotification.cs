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
            }
            catch (Exception)
            {
                _screenshotStatusMessage = "A screenshot could NOT be taken.";
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
            }
            catch (Exception)
            {
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
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception) { }
            // ReSharper restore EmptyGeneralCatchClause
        }

        public void CreateAndOpenExceptionUI()
        {
            const string strWhatHappened =
                "There was an unexpected error in GRT UI. This may be due to a programming bug.";
            const string strWhatUserCanDo =
                "Restart GRT UI and try repeating your last action. Try alternative methods of performing the same action.";
            const string strHowUserAffected = "When you click OK, GRT UI will close.";
            var userFacingException = _exceptionMessageBuilder.FormatExceptionForUser(_screenshotStatusMessage,
                                                                        _logToFileStatusMessage, _exceptionMessage);
            var handledExceptionManager = new HandledExceptionManager(new MailClient(new PostalWorker()), _exceptionMessageBuilder, new ExceptionDialog());
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