#region ReSharper Disable

// ReSharper disable InconsistentNaming 
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global

#endregion

using System.Runtime.InteropServices;
using ExceptionHandler.Mail;
using Machine.Fakes;
using Machine.Specifications;

namespace ExceptionHandler.Tests.Mail.MailMessageTests
{
    internal class MailMessageSpec : WithFakes
    {
        private static MailMessage MailMessage;
        private const string MessageBody = "Message body";
        private const string MessageSubject = "Subject";

        private Establish context = () =>
            {
                Castle.DynamicProxy.Generators.AttributesToAvoidReplicating.Add(typeof (TypeIdentifierAttribute));
                MailMessage = new MailMessage
                    {
                        Body = MessageBody,
                        Subject = MessageSubject
                    };
            };

        private It will_set_message_body = () => MailMessage.Body.ShouldEqual(MessageBody);
        private It will_set_message_subject = () => MailMessage.Subject.ShouldEqual(MessageSubject);
    }
}