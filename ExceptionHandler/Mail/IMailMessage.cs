using System.Collections.Generic;

namespace ExceptionHandler.Mail
{
    public interface IMailMessage
    {
        List<string> AttachmentPaths { get; }
        string Body { get; set; }
        string Subject { get; set; }
        List<string> To { get; }
    }
}