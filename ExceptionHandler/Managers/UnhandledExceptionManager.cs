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

        #region Properties
        public bool DisplayDialog { get; set; }
        public bool EmailScreenshot { get; set; }
        public bool IgnoreDebugErrors { get; set; }
        public bool IsConsoleApp { get; set; }
        public bool LogToEventLog { get; set; }
        public bool LogToFile { get; set; }
        public bool KillAppOnException { get; set; }
        public bool SendEmail { get; set; }
        public bool TakeScreenshot { get; set; }
        public System.Drawing.Imaging.ImageFormat ScreenshotImageFormat { get; set; }
        #endregion

        public UnhandledExceptionManager(IExceptionNotification exceptionNotification, IAppKiller appKiller)
        {
            _exceptionNotification = exceptionNotification;
            _appKiller = appKiller;
            UserNotified = false;
        }

        public void AddHandler()
        {
            LoadConfigSettings();

            if (IgnoreDebugErrors)
            {
                if (AppSettings.DebugMode) return;
            }

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.Automatic);
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
            Application.ThreadException += ThreadExceptionHandler;
        }

        internal void LoadConfigSettings()
        {
            SendEmail = GetConfigBoolean("SendEmail", true);
            TakeScreenshot = GetConfigBoolean("TakeScreenshot", true);
            EmailScreenshot = GetConfigBoolean("EmailScreenshot", true);
            LogToEventLog = GetConfigBoolean("LogToEventLog", false);
            LogToFile = GetConfigBoolean("LogToFile", true);
            DisplayDialog = GetConfigBoolean("DisplayDialog", true);
            IgnoreDebugErrors = GetConfigBoolean("IgnoreDebug", true);
            KillAppOnException = GetConfigBoolean("KillAppOnException", true);
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

            if (!IsConsoleApp) Cursor.Current = Cursors.WaitCursor;

            try
            {
                if (TakeScreenshot) _exceptionNotification.TakeScreenshot();
                if (LogToEventLog) _exceptionNotification.WriteToEventLog();
                if (LogToFile) _exceptionNotification.WriteToLog();
                if (SendEmail) _exceptionNotification.SendEmail(new MailMessage());
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception e)
            {
                Logger.Error("Exception thrown while trying to send mail: " + e);
            }
            // ReSharper restore EmptyGeneralCatchClause

            if (!IsConsoleApp) Cursor.Current = Cursors.Default;

            // Display message to the user
            _exceptionNotification.CreateAndOpenExceptionUI();

            if (KillAppOnException)
            {
                _appKiller.Kill();
            }
        }

        #region Configuration details - TODO: refactor...
        private const string _strClassName = "UnhandledExceptionManager";
        private const string _strKeyNotPresent = "The key <{0}> is not present in the <appSettings> section of .config file";
        private const string _strKeyError = "Error {0} retrieving key <{1}> from <appSettings> section of .config file";

        //--
        //-- Returns the specified String value from the application .config file,
        //-- with many fail-safe checks (exceptions, key not present, etc)
        //--
        //-- this is important in an *unhandled exception handler*, because any unhandled exceptions will simply exit!
        //-- 
        internal static string GetConfigString(string strKey, string strDefault = null)
        {
            try
            {
                string strTemp = System.Configuration.ConfigurationManager.AppSettings.Get(_strClassName + "/" + strKey);
                if (strTemp == null)
                {
                    if (strDefault == null)
                        return string.Format(_strKeyNotPresent, _strClassName + "/" + strKey);
                    else
                        return strDefault;
                }
                else
                    return strTemp;
            }
            catch (Exception ex)
            {
                if (strDefault == null)
                    return string.Format(_strKeyError, ex.Message, _strClassName + "/" + strKey);
                else
                    return strDefault;
            }
        }

        //--
        //-- Returns the specified boolean value from the application .config file,
        //-- with many fail-safe checks (exceptions, key not present, etc)
        //--
        //-- this is important in an *unhandled exception handler*, because any unhandled exceptions will simply exit!
        //-- 
        internal static bool GetConfigBoolean(string strKey, bool? blnDefault = null)
        {
            string strTemp;
            try
            {
                strTemp = System.Configuration.ConfigurationManager.AppSettings.Get(_strClassName + "/" + strKey);
            }
            catch
            {
                return blnDefault.GetValueOrDefault(false);
            }

            if (strTemp == null)
            {
                return blnDefault.GetValueOrDefault(false);
            }
            else
            {
                switch (strTemp.ToLower())
                {
                    case "1":
                    case "true":
                        return true;
                    default:
                        return false;
                }
            }
        }
        #endregion
    }
}