using System.Collections.Generic;
using Microsoft.Office.Interop.Outlook;

namespace ExceptionHandler.Mail
{
    public class MailMessage : IMailMessage
    {
        public List<string> AttachmentPaths { get; private set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public List<string> To { get; private set; } 

        public MailMessage()
        {
            AttachmentPaths = new List<string>();
            To = new List<string>();
        }
      
    }
}