#region ReSharper Disable

// ReSharper disable InconsistentNaming 
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global

#endregion

using System;
using ExceptionHandler.Mail;
using ExceptionHandler.Managers;
using Machine.Fakes;
using Machine.Specifications;
using Rhino.Mocks;

namespace ExceptionHandler.Tests.Managers
{
    class UserDetailsFailureNotificationSpec : WithFakes
    {
        protected static IMailClient IMailClient;
        protected static UserDetailsFailureNotification UserDetailsFailureNotification;

        private Establish context = () =>
            {
                IMailClient = An<IMailClient>();
                UserDetailsFailureNotification = new UserDetailsFailureNotification(IMailClient);
            };
    }

    [Subject(typeof(UserDetailsFailureNotification), "NotifyTeam")]
    internal class when_emailing_team : UserDetailsFailureNotificationSpec
    {

        private Because of = () => UserDetailsFailureNotification.NotifyTeam(new NullReferenceException());

        private It will_set_the_exception_type =
            () => UserDetailsFailureNotification.ExceptionType.ShouldEqual("System.NullReferenceException");

        private It will_set_the_email_body = () => UserDetailsFailureNotification.EmailBody.ShouldContain(UserDetailsFailureNotification.WhatHappened);
        private It will_send_an_email_to_team = () => IMailClient.WasToldTo(x => x.PrepareAndSend(Arg<MailMessage>.Is.Anything));
    }
}