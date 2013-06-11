using ExceptionHandler.Mail;
using ExceptionHandler.View;

namespace ExceptionHandler.Managers
{
    public class ConnectionNotifications : HandledExceptionManager, IConnectionNotifications
    {
        public ConnectionNotifications(IMailClient mailClient, IExceptionDialog exceptionDialog) : 
            base(mailClient,
            new ExceptionMessageBuilder(new AssemblyInfo()), exceptionDialog) { }

        public void NotifyConnectionFailed()
        {
            WhatHappened = "This application has lost its connection to our servers.";
            HowUserIsAffected = "This means the application has stopped refreshing and will " +
                                "not receive updated data";
            WhatUserCanDo = "The team has been notified and is working on this issue. " +
                                "First, please try to restart the application. If the problem persists, contact " +
                                "the team directly at somebody@nobody.com";

            SendNotificationEmail(WhatHappened, HowUserIsAffected, WhatUserCanDo);
            ShowDialog(WhatHappened, HowUserIsAffected, WhatUserCanDo);
        }

        public void NotifyConnectionSuspended()
        {
            WhatHappened =      "This application has been offline for greater than one minute. This " +
                                " may be because our servers are going down for the night or because " +
                                "there is a more serious issue.";
            HowUserIsAffected = "You will be automatically reconnected when the application comes " +
                                "back online. In the mean time the application will not receive updated data.";
            WhatUserCanDo =     "The team has been notified and is working on this issue. " +
                                "You can also contact the team directly at " +
                                "somebody@nobody.com";

            SendNotificationEmail(WhatHappened, HowUserIsAffected, WhatUserCanDo);
            ShowDialog(WhatHappened, HowUserIsAffected, WhatUserCanDo);
        }
    }
}