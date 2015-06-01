using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace ExceptionHandler.Mail
{
    public class SmtpPostalWorker : IPostalWorker
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string AuthUser { get; set; }
        public string AuthPassword { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }

        #region IPostalWorker Members

        public void SendMail(IMailMessage mailMessage)
        {
            var client = new SmtpClient();
            client.Host = Host;
            client.Port = Port;
            client.EnableSsl = EnableSsl;
            client.UseDefaultCredentials = UseDefaultCredentials;
            if (!UseDefaultCredentials && !string.IsNullOrEmpty(AuthUser))
            {
                client.Credentials = new NetworkCredential(AuthUser, AuthPassword);
            }

            var mail = new System.Net.Mail.MailMessage();
            mail.Body = mailMessage.Body;
            mail.Subject = mailMessage.Subject;
            mailMessage.To.ForEach(x => mail.To.Add(x));
            mailMessage.AttachmentPaths.ForEach(x => mail.Attachments.Add(new Attachment(x, MediaTypeNames.Application.Octet)));
            client.Send(mail);
        }

        #endregion
    }
}
