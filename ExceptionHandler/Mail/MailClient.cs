#region

using System;
using System.Threading;

#endregion

namespace ExceptionHandler.Mail
{
    public class MailClient : IMailClient
    {
        private const int IntMaxRetries = 5;
        private readonly IPostalWorker _postalWorker;
        private int _intRetries = 1;

        public MailClient(IPostalWorker postalWorker)
        {
            _postalWorker = postalWorker;
        }

        public void PrepareAndSend(MailMessage mailMessage)
        {
            const int intRetryInterval = 333;
            try
            {
                _postalWorker.SendMail(mailMessage);
            }
// ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
// ReSharper restore EmptyGeneralCatchClause
            {
                _intRetries += 1;
                if (_intRetries > IntMaxRetries)
                {
                    throw;
                }
                Thread.Sleep(intRetryInterval);
                PrepareAndSend(mailMessage);
            }
        }
    }
}