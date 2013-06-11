#region

using System;
using System.Threading;
using System.Windows.Forms;
using ExceptionHandler.Mail;
using ExceptionHandler.Settings;
using log4net;

#endregion

//'--
//'-- Generic UNHANDLED error handling class
//'--
//'-- Intended as a last resort for errors which crash our application, so we can get feedback on what
//'-- caused the error.
//'--
//'-- To use: UnhandledExceptionManager.AddHandler() in the STARTUP of your application
//'--
//'-- more background information on Exceptions at:
//'--   http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnbda/html/exceptdotnet.asp

namespace ExceptionHandler.Managers
{
    public class UnhandledExceptionManager
    {
        private readonly IExceptionNotification _exceptionNotification;
        private readonly IAppKiller _appKiller;
        private static readonly ILog Logger = LogManager.GetLogger(typeof (UnhandledExceptionManager));

        public UnhandledExceptionManager(IExceptionNotification exceptionNotification, IAppKiller appKiller)
        {
            _exceptionNotification = exceptionNotification;
            _appKiller = appKiller;
            UserNotified = false;
        }

        public void AddHandler()
        {
            if (AppSettings.DebugMode) return;

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.Automatic);
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
            Application.ThreadException += ThreadExceptionHandler;
        }

        internal void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Error("Unhandled Exception Caught by UnhandledExceptionHandler");     
            PerformUnhandledExceptionActions(e.ExceptionObject as Exception);
        }

        internal static bool UserNotified { get; set; }

        internal void ThreadExceptionHandler(Object sender, ThreadExceptionEventArgs e)
        {
            Logger.Error("Unhandled Exception Caught by ThreadExceptionHandler");     
            PerformUnhandledExceptionActions(e.Exception);
        }

        private void PerformUnhandledExceptionActions(Exception exception)
        {
            try
            {
                var exceptionType = exception.GetType().FullName;
                _exceptionNotification.BuildExceptionMessage(exception, exceptionType);
            }
            catch (Exception e)
            {
                Logger.Error("Failed to build ExceptionMessage: " + e);
                _exceptionNotification.BuildExceptionMessage(exception, "");
            }

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                _exceptionNotification.TakeScreenshot();
                _exceptionNotification.WriteToLog();
                _exceptionNotification.SendEmail(new MailMessage());
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch (Exception e)
            {
                Logger.Error("Exception thrown while trying to send mail: " + e);
            }
            // ReSharper restore EmptyGeneralCatchClause

            Cursor.Current = Cursors.Default;
            _exceptionNotification.CreateAndOpenExceptionUI();
            _appKiller.Kill();
        }
    }
}