using Grt.ExceptionHandler.Mail;
using Machine.Specifications;

// ReSharper disable InconsistentNaming 
// ReSharper disable CheckNamespace
namespace Grt.ExceptionHandler.Tests.Mail.MailClientTest
{
    class MailClientSpec
    {
        protected static MailClient MailClient;
        Establish context = () => MailClient = new MailClient();
    }

    [Subject(typeof (MailClient), "SendMailViaOutlook")]
    class when_sending_mail : MailClientSpec
    {
        It will_send = () => MailClient.sendEMailThroughOUTLOOK();
    }
}