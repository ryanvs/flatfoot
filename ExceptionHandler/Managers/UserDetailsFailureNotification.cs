using System;
using ExceptionHandler.Mail;
using ExceptionHandler.View;

namespace ExceptionHandler.Managers
{
    public class UserDetailsFailureNotification : HandledExceptionManager, IUserDetailsFailureNotification
    {
        public UserDetailsFailureNotification(IMailClient mailClient) : 
            base(mailClient, new ExceptionMessageBuilder(new AssemblyInfo()), new ExceptionDialog())
        {
            WhatHappened      = "The application failed to properly set the 'AppData' directory";
            HowUserIsAffected = "This impacts the persistence of the user's settings during an upgrade. For now, s/he should" +
                                "be able to save their settings between sessions. However, on the next upgrade these settings" +
                                "will be lost.";
            WhatUserCanDo     = "Nothing.";
        }

        public void NotifyTeam(Exception exception)
        {
            ExceptionType = exception.GetType().FullName;
            SendNotificationEmail(WhatHappened, HowUserIsAffected, WhatUserCanDo);
        }
    }
}