using System;

namespace ExceptionHandler.Managers
{
    public interface IExceptionMessageBuilder
    {
        string BuildEmailBody(string whatHappened, string howUserAffected, string whatUserCanDo, string moreDetails);
        string SystemInfo();
        string ExceptionInfo(Exception exception);
        string FormatExceptionForUser(string screenshotStatusMessage, string logToFileStatusMessage, string strException);
        string ReplaceStringVals(string input);
    }
}